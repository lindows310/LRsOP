using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal static class Vectors
    {
        public static ArrayVector SumSt(ArrayVector vec1, ArrayVector vec2)
        {
            int[] temp = new int[vec1.cords.Length];
            for (int i = 0; i < vec1.cords.Length; i++)
                temp[i] = vec1[i] + vec2[i];
            return new ArrayVector(temp.Length) { cords = temp };
        }
        public static int ScalarSt(ArrayVector vec1, ArrayVector vec2)
        {
            int res = 0;
            try
            {
                if (vec1.cords.Length != vec2.cords.Length)
                    throw new Exception("Длины векторов не совпадают.");

                for (int i = 0; i < vec1.cords.Length; i++)
                    res += vec1[i] * vec2[i];
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка. {e.Message}");
                return res;
            }
        }
        public static ArrayVector MultNumberSt(ArrayVector vec, int num)
        {
            int[] temp = new int[vec.cords.Length];
            for (int i = 0; i < vec.cords.Length; i++)
                temp[i] = vec[i] * num;
            return new ArrayVector(temp.Length) { cords = temp };
        }
        public static double GetNormSt(ArrayVector vec)
        {
            return vec.GetNorm();
        }
    }
}
