using Microsoft.VisualStudio.TestTools.UnitTesting;
using WA_FRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_FRM.Models.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void InitTest()
        {
            int n = 3, c = 0;
            Matrix m = new Matrix(n);

            Assert.IsNotNull(m);
            Assert.AreEqual(m.size, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c++;
                    Assert.AreEqual(m[i, j], c.ToString());
                }
            }            
        }
        
        public void RotateMatrix(int n)
        {
            int c = 0;
            string[,] ar = new string[n, n];
            Matrix m = new Matrix(n);

            m.Rotate();

            for (int i = (n - 1); i >= 0; i--)
            {
                for (int j = 0; j < n; j++)
                {
                    c++;
                    Assert.AreEqual(m[j, i], c.ToString());
                }
            }
        }

        [TestMethod()]
        public void RotateTest()
        {
            for (int i = 0; i < 100; i++)
                RotateMatrix(i);
        }
    }
}