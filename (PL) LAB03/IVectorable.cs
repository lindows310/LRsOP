using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR02
{
    public interface IVectorable
    {
        int this[int index] { get; set; }
        int Length { get; }
        double GetNorm();
    }
}
