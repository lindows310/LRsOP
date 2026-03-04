using System.Collections.Generic;
using System;

namespace LAB01
{
    public class VectorsCompare : IComparer<IVectorable>
    {
        public int Compare(IVectorable vec1, IVectorable vec2)
        {
            if (vec1.GetNorm() == vec2.GetNorm())      return 0;
            else if (vec1.GetNorm() > vec2.GetNorm())  return -1;
            else                                       return 1;
        }
    }
}
