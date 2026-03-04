using System;
using System.Collections.Generic;

namespace LAB01
{
    internal static class Vectors
    {
        public static IVectorable SumSt(IVectorable vec1, IVectorable vec2)
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
        public static IVectorable MultNumberSt(IVectorable vec, int num)
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
        public static IVectorable[] CreateArray(int length)
        {
            Random rnd = new Random();
            Type[] typesArr = { typeof(ArrayVector), typeof(LinkedListVector) };

            List<IVectorable> vectors = new List<IVectorable>(length);
            for (int i = 0; i < length; i++)
            {
                Type tempType = typesArr[rnd.Next(typesArr.Length)];
                IVectorable vec = (IVectorable) Activator.CreateInstance(tempType);

                vec = vec.Create(rnd.Next(2, 13));
                for (int j = 0; j < vec.Length; j++)
                    vec[j] = rnd.Next(20);

                vectors.Add(vec);
            }
            return vectors.ToArray();
        }
        public static (IVectorable minLen, IVectorable maxLen) FindByMinMaxLength(IVectorable[] vectorsArr)
        {
            if (vectorsArr == null || vectorsArr.Length == 0)
                throw new Exception("Массив векторов не существует или длина массива равна нулю");

            IVectorable maxCords = vectorsArr[0]; IVectorable minCords = vectorsArr[0];
            for (int i = 0; i < vectorsArr.Length; i++)
                if (vectorsArr[i].CompareTo(maxCords) == 1) 
                    minCords = vectorsArr[i];

            for (int i = 0; i < vectorsArr.Length; i++)
                if (vectorsArr[i].CompareTo(maxCords) == -1)
                    maxCords = vectorsArr[i];

            return (maxCords, minCords);
        }
        public static IVectorable[] VectorsSort(IVectorable[] vecs)
        {
            Array.Sort(vecs, new VectorsCompare());
            return vecs;
        }
    }
}
