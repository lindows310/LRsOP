using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    public interface IVectorable
    {
        int this[int index] { get; set; }
        int Length { get; }
        IVectorable Create(int length);
        double GetNorm();
        int CompareTo(IVectorable vec);
    }
}
