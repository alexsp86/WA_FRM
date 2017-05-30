using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WA_FRM.Models
{
    interface IMObject
    {
        int size { get; set; }
        string this[int i, int j]
        {
            get;
            set;
        }
        void Rotate();
        void Resize(int n);
        MemoryStream ToStream();
        void FromStream(Stream ms);
    }
}
