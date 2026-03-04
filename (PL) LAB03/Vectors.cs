using LAB02;
using LR02;
using System;
using System.Collections.Generic;

namespace LAB01
{
    internal static class Vectors
    {
        public static ArrayVector SumSt(IVectorable vec1, IVectorable vec2)
        {
            if (vec1.Length != vec2.Length)
                throw new Exception("Длины векторов не совпадают.");

            int[] temp = new int[vec1.Length];
            for (int i = 0; i < vec1.Length; i++)
                temp[i] = vec1[i] + vec2[i];
            return new ArrayVector(temp.Length) { Cords = temp };
        }
        public static int ScalarSt(IVectorable vec1, IVectorable vec2)
        {
            if (vec1.Length != vec2.Length)
                throw new Exception("Длины векторов не совпадают.");

            int res = 0;
            for (int i = 0; i < vec1.Length; i++)
                res += vec1[i] * vec2[i];
            return res;
        }
        public static ArrayVector MultNumberSt(IVectorable vec, int num)
        {
            int[] temp = new int[vec.Length];
            for (int i = 0; i < vec.Length; i++)
                temp[i] = vec[i] * num;
            return new ArrayVector(temp.Length) { Cords = temp };
        }
        public static double GetNormSt(IVectorable vec)
        {
            return vec.GetNorm();
        }
    }
}
