using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Counter
    {
        private int minVal { get; set; }
        private int maxVal { get; set; }
        private int curVal { get; set; }

        public Counter(int maxValue, int minValue, int currentValue)
        {
            this.curVal = currentValue;
            this.minVal = minValue;
            this.maxVal = maxValue;
        }

        /// <summary>
        /// Добавляет единицу к десятичному счетчику.
        /// </summary>
        public void Addition()
        {
            if (IsUpLimited())
                curVal = minVal;
            else 
                curVal += 1;
        }

        /// <summary>
        /// Вычитает единицу из десятичного счетчика.
        /// </summary>
        public void Difference()
        {
            if (IsDownLimited())
                curVal = maxVal;
            else
                curVal -= 1;
        }

        public void ShowCounter()
        {
            Console.WriteLine("Текущее значение счетчика: {0}. Нажмите Enter, чтобы продолжить", curVal);
            Console.ReadLine();
        }

        /// <summary>
        /// Проверяет достижение счетчиком его предельных значений.
        /// </summary>
        private bool IsUpLimited()
        {
            bool status;
            if (curVal == maxVal)
                status = true;
            else
                status = false;
            return status;
        }

        private bool IsDownLimited()
        {
            bool status;
            if (curVal == minVal)
                status = true;
            else
                status = false;
            return status;
        }
    }

    public class Polynom
    {
        private double varA { get; set; }
        private double varB { get; set; }
        private double varC { get; set; }
        public Polynom (double varA, double varB, double varC)
        {
            this.varA = varA;
            this.varB = varB;
            this.varC = varC;
        }
        /// <summary>
        /// Метод нахожденния решений квадратного трехчлена с заданными вещественными коэффициентами a, b и c.
        /// </summary>
        public void FindSolution()
        {
            double x1, x2;
            if ((Math.Pow(varB, 2) - 4 * (varA * varC) ) > 0)
            {
                x1 = (-(varB) - Math.Sqrt(Math.Pow(varB, 2) - 4 *  (varA * varC))) / (varA * 2);
                x2 = (-(varB) + Math.Sqrt(Math.Pow(varB, 2) - 4 * (varA * varC))) / (varA * 2);
                Console.WriteLine("Уравнение имеет два решения: \nx(1) = {0}\nx(2) = {1}", x1, x2);
            }
            else if ((Math.Pow(varB, 2) - 4 * (varA * varC)) == 0)
            {
                x1 = (-(varB) / (varA * 2));
                Console.WriteLine("Уравнение имеет единственное решение: \nx(1) = {0}", x1);
            }
            else
            {
                Console.WriteLine("Уравнение не имеет решений на множестве вещественных чмсел.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            bool toContinue;
            string input;
            Console.WriteLine("Лабораторная работа No 4. Выполнил студент группы 6101-020302D Абросимов А.");

            toContinue = true;
            while (toContinue)
            {
                Console.WriteLine("Выберите опцию: \n(1) Десятичный счетчик\n(2) Решение квадратного трехчлена.\n(3) Выход из программы");
                input = Console.ReadLine();
                switch (input)
                {
                    case ("1"):
                        {
                            bool run;
                            string inp;
                            Console.Clear();
                            Console.Write("Введите максимальное, минимальное и текущее значения счетчика через пробел: ");
                            string[] values1 = Console.ReadLine().Split(' ');
                            Counter counter = new Counter(int.Parse(values1[0]), int.Parse(values1[1]), int.Parse(values1[2]));

                            run = true;
                            while (run)
                            {
                                Console.Clear();
                                Console.WriteLine("Выберите одну из опций: \n(1) Увеличть счетчик на единицу \n(2) Уменьшить счетчик на единицу \n(3) Показать значение счетчика \n(любой символ) Выйти из раздела");
                                inp = Console.ReadLine();

                                switch (inp)
                                {
                                    case ("1"):
                                        counter.Addition();
                                        break;

                                    case ("2"):
                                        counter.Difference();
                                        break;

                                    case ("3"):
                                        counter.ShowCounter();
                                        break;

                                    default:
                                        run = false;
                                        break;
                                }
                            }
                        }
                        break;

                    case ("2"):
                        {
                            bool run;
                            string inp;
                            run = true;
                            while (run)
                            {
                                Console.Clear();
                                Console.Write("Введите вещественные коэффициенты a, b и c через пробел: ");
                                string[] values = Console.ReadLine().Split(' ');

                                Polynom polynom = new Polynom(double.Parse(values[0]), double.Parse(values[1]), double.Parse(values[2]));
                                polynom.FindSolution();

                                Console.WriteLine("Желаете ввести еще один многочлен? \n(1) Да \n(2) Нет");
                                inp = Console.ReadLine();
                                switch (inp)
                                {
                                    case ("1"):
                                        run = true;
                                        break;
                                    case ("2"):
                                        run = false;
                                        break;
                                }
                            }
                        }
                        break;

                    case ("3"):
                        {
                            Console.WriteLine("Вы вышли из программы.");
                            toContinue = false;
                        }
                        break;
                }
                Console.Clear();
            }
        }
    }
}
