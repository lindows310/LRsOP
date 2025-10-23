using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
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
