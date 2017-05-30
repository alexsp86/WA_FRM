using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;


namespace WA_FRM.Models
{
    [Serializable]
    public class Matrix:IMObject
    {
        public string[][] elements;

        public int size { get; set;}

        public Matrix()
        { }

        public Matrix(int n=3)
        {
            size = n;
            Resize(n);
        }

        public void Resize(int n)
        {
            int c = 0;
            size = n;
            elements = new string[size][];

            for (int i = 0; i < size; i++)
            {
                elements[i] = new string[size];

                for (int j = 0; j < size; j++)
                {
                    c++;
                    elements[i][j] = c.ToString();
                }
            }
        }

        public string this[int i,int j]
        {            
            get
            {                
                return elements[i][j];                
            }            
            set
            {
                elements[i][j] = value;                
            }
        }


        private int j_idx(int i, int n)
        {
            if (i < n)
                return i;
            else
            if (i < (n * 2) - 1)
                return n - 1;
            else
            if (i < (n * 3) - 2)
                return ((n * 3) - 3) - i;
            else
                return 0;
        }

        private int i_idx(int i, int n)
        {
            if (i < n)
                return 0;
            else
            if (i < (n * 2) - 1)
                return i - (n - 1);
            else
            if (i < (n * 3) - 2)
                return n - 1;
            else
                return ((n * 4) - 4) - i;
        }

        private void RotateLevel(ref string[][] m, int n, int cycle, int level = 0)
        {
            int c = (n * 4) - 3,
                i_m, j_m;
            string prev = "",
                   curr = "";

            for (int i = 0; i < c; i++)
            {
                i_m = i_idx(i, n) + level;
                j_m = j_idx(i, n) + level;

                prev = m[i_m][j_m];
                if (i > 0)
                {
                    m[i_m][j_m] = curr;
                }                
                curr = prev;
            }           

            if (cycle < (n - 2))
                RotateLevel(ref m, n, ++cycle, level);
        }

        private void RotateElements(ref string[][] m, int n, int level = 0)
        {
            RotateLevel(ref m, n, 0, level);
            if (n - 2 != 1 && n - 2 > 0)
                RotateElements(ref m, n - 2, ++level);
        }

        public void Rotate()
        {
            RotateElements(ref elements, size);
        }

        public MemoryStream ToStream()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Matrix));
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, this);
            ms.Position = 0;
            return ms;
        }

        public void FromStream(Stream ms)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Matrix));
            Matrix m = (formatter.Deserialize(ms) as Matrix);

            this.Resize(m.size);

            for (int i = 0; i<size; i++)
            {
                for (int j = 0; j<size; j++)
                {
                    this[i,j]=m[i,j];
                }
            }
        }
    }
}
