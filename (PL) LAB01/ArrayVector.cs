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
        private int[] cords;
        public int[] Cords
        {
            get { return cords; }
            set { cords = value; }
        }
        public int this[int index]
        {
            get { return Cords[index]; }
            set { Cords[index] = value; }
        }
        public ArrayVector()
        {
            Cords = new int[5];
        }
        public ArrayVector(int length)
        {
            Cords = new int[length];
        }
        public void FillVal()
        {
            string[] temp = Console.ReadLine().Split(' ');
            for (int i = 0; i < Cords.Length; i++)
            {
                try
                {
                    Cords[i] = int.Parse(temp[i]);
                }
                catch (FormatException e)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Неправильный формат ввода. В координату записано значение 0.", new object[] { 0, 3, ConsoleColor.Red }, new object[] { 4, 8, ConsoleColor.DarkGray });
                    Cords[i] = 0;
                }
                catch (IndexOutOfRangeException e)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Компоненте не было происвоено значение. {i + 1}-ая координата равна 0.", new object[] { 0, 5, ConsoleColor.Red }, new object[] { 6, 9, ConsoleColor.DarkGray });
                    Cords[i] = 0;
                }
                catch (OverflowException e)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Значение, присваиваемое компоненте, не принадлежит области определения типа int. Координате присвоено значение 1", new object[] { 0, 9, ConsoleColor.Red }, new object[] { 10, 13, ConsoleColor.DarkGray });
                    Cords[i] = 1;
                }
            }
        }
        public double GetNorm()
        {
            double norm = 0;
            foreach (int i in Cords)
                norm += Math.Pow(i, 2);
            return Math.Sqrt(norm);
        }
        public int SumPositivesFromChetIndex()
        {
            int sum = 0; bool elFound = false;

            for (int i = 1; i < Cords.Length + 1; i++)
                if (i % 2 == 0 && Cords[i - 1] > 0)
                {
                    elFound = true;
                    sum += Cords[i - 1];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return sum;
        }
        public int SumLessFromNechetIndex()
        {
            int sum = 0; double average = 0; bool elFound = false;

            for (int i = 0; i < Cords.Length; i++)
                average += Math.Abs(Cords[i]);
            average = average / Cords.Length;

            for (int i = 1; i < Cords.Length + 1; i++)
                if (i % 2 == 1 && Cords[i - 1] < average)
                {
                    elFound = true;
                    sum += Cords[i - 1];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return sum;
        }
        public int MultChet()
        {
            int mult = 1; bool elFound = false;

            for (int i = 0; i < Cords.Length; i++)
                if (Cords[i] > 0 && Cords[i] % 2 == 0)
                {
                    elFound = true;
                    mult *= Cords[i];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return mult;
        }
        public int MultNechet()
        {
            int mult = 1; bool elFound = false;
            for (int i = 0; i < Cords.Length; i++)
                if (Cords[i] % 3 != 0 && Math.Abs(Cords[i]) % 2 == 1)
                {
                    elFound = true;
                    mult *= Cords[i];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return mult;
        }
        public void SortUp()
        {
            for (int s = Cords.Length / 2; s > 0; s /= 2)
                for (int i = s; i < Cords.Length; i++)
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
            int[] temp = new int[Cords.Length];
            for (int i = Cords.Length - 1, j = 0; i >= 0; i--, j++)
                temp[j] = this[i];
            Cords = temp;
        }
        public override string ToString()
        {
            string vector = " ";
            for (int i = 0; i < Cords.Length; i++)
                vector += this[i] + " ";
            return vector = "(" + vector + ")";
        }
    }
}
