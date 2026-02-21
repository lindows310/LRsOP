using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    public interface IVectorable<T>
    {
        T this[int index] { get; set; }
        int Length { get; }
        double GetNorm();
    }

    internal class ArrayVector : IVectorable<int>
    {
        private int[] cords;
        public int[] Cords
        {
            get { return cords; }
            set { cords = value; }
        }
        public int Length
        {
            get { return cords.Length; }
        }
        public int this[int index]
        {
            get
            {
                try
                {
                    return Cords[index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception($"Ошибка. Не существует элемента с индексом {index}");
                }
            }
            set
            {
                try
                {
                    Cords[index] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception($"Ошибка. Не существует элемента с индексом {index}");
                }
            }
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
                    this[i] = int.Parse(temp[i]);
                }
                catch (FormatException)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Неправильный формат ввода. В координату записано значение 0.", new object[] { 0, 3, ConsoleColor.Red }, new object[] { 4, 8, ConsoleColor.DarkGray });
                    this[i] = 0;
                }
                catch (IndexOutOfRangeException)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Компоненте не было происвоено значение. {i + 1}-ая координата равна 0.", new object[] { 0, 5, ConsoleColor.Red }, new object[] { 6, 9, ConsoleColor.DarkGray });
                    this[i] = 0;
                }
                catch (OverflowException)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Значение, присваиваемое компоненте, не принадлежит области определения типа int. Координате присвоено значение 1", new object[] { 0, 9, ConsoleColor.Red }, new object[] { 10, 13, ConsoleColor.DarkGray });
                    this[i] = 1;
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

            for (int i = 1; i < Length + 1; i++)
                if (i % 2 == 0 && this[i - 1] > 0)
                {
                    elFound = true;
                    sum += this[i - 1];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return sum;
        }
        public int SumLessFromNechetIndex()
        {
            int sum = 0; double average = 0; bool elFound = false;

            for (int i = 0; i < Length; i++)
                average += Math.Abs(Cords[i]);
            average /= Length;

            for (int i = 1; i < Length + 1; i++)
                if (i % 2 == 1 && this[i - 1] < average)
                {
                    elFound = true;
                    sum += this[i - 1];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return sum;
        }
        public int MultChet()
        {
            int mult = 1; bool elFound = false;

            for (int i = 0; i < Length; i++)
                if (this[i] > 0 && this[i] % 2 == 0)
                {
                    elFound = true;
                    mult *= this[i];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return mult;
        }
        public int MultNechet()
        {
            int mult = 1; bool elFound = false;
            for (int i = 0; i < Length; i++)
                if (this[i] % 3 != 0 && Math.Abs(this[i]) % 2 == 1)
                {
                    elFound = true;
                    mult *= this[i];
                }

            if (elFound == false)
                throw new Exception("Обнаружена ошибка: подходящие элементы не найдены");

            return mult;
        }
        public void SortUp()
        {
            for (int s = Length / 2; s > 0; s /= 2)
                for (int i = s; i < Length; i++)
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
            int[] temp = new int[Length];
            for (int i = Length - 1, j = 0; i >= 0; i--, j++)
                temp[j] = this[i];
            Cords = temp;
        }
        public override string ToString()
        {
            string vector = " ";
            for (int i = 0; i < Length; i++)
                vector += this[i] + " ";
            return $"Кол-во коорд.: [{Length}]; " + "(" + vector + ")";
        }
    }
}
