using System;
using System.Collections.Generic;

namespace LAB01
{
    internal static class Vectors
    {
        public static ArrayVector SumSt(ArrayVector vec1, ArrayVector vec2)
        {
            if (vec1.Cords.Length != vec2.Cords.Length)
                throw new Exception("Длины векторов не совпадают.");

            int[] temp = new int[vec1.Cords.Length];
            for (int i = 0; i < vec1.Cords.Length; i++)
                temp[i] = vec1[i] + vec2[i];
            return new ArrayVector(temp.Length) { Cords = temp };
        }
        public static ArrayVector SumSt(ArrayVector vec1, LinkedListVector vec2)
        {
            if (vec1.Length != vec2.Length)
                throw new Exception("Длины векторов не совпадают.");

            int[] temp = new int[vec1.Cords.Length];
            for (int i = 0; i < vec1.Cords.Length; i++)
                temp[i] = vec1[i] + vec2[i];
            return new ArrayVector(temp.Length) { Cords = temp };
        }
        public static int ScalarSt(ArrayVector vec1, ArrayVector vec2)
        {
            if (vec1.Cords.Length != vec2.Cords.Length)
                throw new Exception("Длины векторов не совпадают.");

            int res = 0;
            for (int i = 0; i < vec1.Cords.Length; i++)
                res += vec1[i] * vec2[i];
            return res;
        }
        public static ArrayVector MultNumberSt(ArrayVector vec, int num)
        {
            int[] temp = new int[vec.Cords.Length];
            for (int i = 0; i < vec.Cords.Length; i++)
                temp[i] = vec[i] * num;
            return new ArrayVector(temp.Length) { Cords = temp };
        }
        public static double GetNormSt(ArrayVector vec)
        {
            return vec.GetNorm();
        }
        public static IVectorable[] CreateArray(int length)
        {
            Random rnd = new Random();
            Type[] typesArr = { typeof(ArrayVector), typeof(LinkedListVector) };

            List<IVectorable> vectors = new List<IVectorable>(length);
            for (int i = 0; i < rnd.Next(5, 20); i++)
            {
                Type tempType = typesArr[rnd.Next(typesArr.Length)];
                IVectorable vec = (IVectorable) Activator.CreateInstance(tempType);

                for (int j = 0; j < vec.Length; j++)
                    vec[j] = rnd.Next(20);

                vectors.Add(vec);
            }
            return vectors.ToArray();
        }
        public static IVectorable FindByMaxLength(IVectorable[] vectorsArr)
        {
            IVectorable maxCords = null;
            for (int i = 0; i < vectorsArr.Length; i++)
                if (vectorsArr[i].CompareTo(maxCords) == 1)
                    maxCords = vectorsArr[i];
            return maxCords;
        }
        public static IVectorable FindByMinLength(IVectorable[] arr)
        {
            IVectorable maxCords = null; IVectorable minCords = null;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i].CompareTo(maxCords) == -1)
                    maxCords = arr[i];
            return minCords;
        }
    }
}
