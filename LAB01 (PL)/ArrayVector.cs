using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class ArrayVector
    {
        public int[] cords;
        public int this[int index]
        {
            get { return cords[index]; }
            set { cords[index] = value; }
        }
        public ArrayVector()
        {
            cords = new int[5];
        }
        public ArrayVector(int length)
        {
            cords = new int[length];
        }
        public void FillVal()
        {
            string[] temp = Console.ReadLine().Split(' ');
            for (int i = 0; i < cords.Length; i++)
            {
                try
                {
                    cords[i] = int.Parse(temp[i]);
                }
                catch (FormatException e)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Неправильный формат ввода. В координату записано значение 0.", new object[] { 0, 5, ConsoleColor.Red }, new object[] { 6, 8, ConsoleColor.DarkGray });
                    cords[i] = 0;
                }
                catch (IndexOutOfRangeException e)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Компоненте не было происвоено значение. {i + 1}-ая координата равна 0.", new object[] { 0, 5, ConsoleColor.Red }, new object[] { 6, 8, ConsoleColor.DarkGray });
                    cords[i] = 0;
                }
                catch (OverflowException e)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Значение, присваиваемое компоненте, не принадлежит области определения типа int. Координате присвоено значение 1", new object[] { 0, 9, ConsoleColor.Red }, new object[] { 13, 13, ConsoleColor.Yellow });
                    cords[i] = 1;
                }
            }
        }
        public double GetNorm()
        {
            double norm = 0;
            foreach (int i in cords)
                norm += Math.Pow(i, 2);
            return Math.Sqrt(norm);
        }
        public int SumPositivesFromChetIndex()
        {
            int sum = 0;
            for (int i = 1; i < cords.Length + 1; i++)
                if ( i % 2 == 0 && cords[i - 1] > 0)
                    sum += cords[i - 1];
            return sum;
        }
        public int SumLessFromNechetIndex()
        {
            int sum = 0;
            double average = 0;
            for (int i = 0; i < cords.Length; i++)
                average += Math.Abs(cords[i]);
            average = average / cords.Length;
            for (int i = 1; i < cords.Length + 1; i++)
                if (i % 2 == 1 && cords[i - 1] < average)
                    sum += cords[i - 1];
            return sum;
        }
        public int MultChet()
        {
            int mult = 1;
            for (int i = 0; i < cords.Length; i++)
                if (cords[i] > 0 && cords[i] % 2 == 0)
                    mult *= cords[i];
            return mult;
        }
        public int MultNechet()
        {
            int mult = 1;
            for (int i = 0; i < cords.Length; i++)
                if (cords[i] % 3 != 0 && cords[i] % 2 == 1)
                    mult *= cords[i];
            return mult;
        }
        public void SortUp()
        {
            for (int s = cords.Length / 2; s > 0; s /= 2)
                for (int i = s; i < cords.Length; i++)
                    for (int j = i - s; j >= 0 && this[j] > this[j + s]; j -= s)
                    {
                        int temp = this[j];
                        this[j] = this[j + s];
                        this[j + s] = temp;
                    }
        }
        public void SortDown()
        {
            SortUp();
            int[] temp = new int[cords.Length];
            for (int i = cords.Length - 1, j = 0; i >= 0; i--, j++)
                temp[j] = this[i];
            cords = temp;
        }
        public override string ToString()
        {
            string vector = " ";
            for (int i = 0; i < cords.Length; i++)
                vector += this[i] + " ";
            return vector = "(" + vector + ")";
        }
    }
}
