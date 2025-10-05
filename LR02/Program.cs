using System;
using System.Collections.Generic;
using System.Text;

namespace LAB02_01
{
    class Program
    {
        static void Main(string[] args)
        {
            bool key;
            string option;

            option = "";
            key = true;

            Console.WriteLine("Лабораторная работа 2, выполнил студент Абросимов А.О. (6101-020302D)");

            while (key)
            {
                Console.WriteLine(" \nВыберите опцию: \n(1) Таблица значений функции \n(2) Серия выстрелов по мишени \n(3) Сумма рядов \n(4) Выход\n ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        {
                            double x, xMax, dX, y;
                            Console.Write("Введите минимальное значение интервала: ");
                            x = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Введите максимальное значение интервала: ");
                            xMax = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Введите величину шага (dx): ");
                            dX = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("{0,10}{1,16}", "x", "y");

                            while (x <= xMax)
                            {
                                if (-7 <= x && 3 >= x)
                                {
                                    if (-6 >= x)
                                    {
                                        y = 2;
                                    }
                                    else if (-2 >= x)
                                    {
                                        y = 0.25 * x + 0.5;
                                    }
                                    else if (0 >= x)
                                    {
                                        y = -(Math.Sqrt(4 - Math.Pow(x + 2, 2))) + 2;
                                    }
                                    else if (2 >= x)
                                    {
                                        y = Math.Sqrt(4 - Math.Pow(x, 2));
                                    }
                                    else
                                    {
                                        y = -x + 2;
                                    }
                                    Console.WriteLine("{0,10:0.00}{1,16:0.00}", x, y);
                                }
                                else
                                {
                                    Console.WriteLine("{0,10:0.00}{1,16:0.00}", x, "Не определен");
                                }
                                x += dX;
                            }
                        }
                        break;

                    case "2":
                        {
                            double shAmount;

                            Console.Write("Введите желаемое количество выстрелов: ");
                            shAmount = Convert.ToInt32(Console.ReadLine());

                            for (int i = 0; i < shAmount; i++)
                            {
                                Console.Write("Введите x: ");
                                double x = Convert.ToDouble(Console.ReadLine());

                                Console.Write("Введите y: ");
                                double y = Convert.ToDouble(Console.ReadLine());

                                double funcVal = Math.Pow((x - 2), 2) - 3;

                                if ((y >= funcVal && y <= x && y >= 0) || (y >= funcVal && Math.Abs(y) >= x && y <= 0))
                                {
                                    Console.WriteLine("Точка ({0}, {1}) принадлежит области.", x, y);
                                }
                                else
                                {
                                    Console.WriteLine("Точка ({0}, {1}) не принадлежит области.", x, y);
                                }
                            }
                        }
                        break;

                    case "3":
                        {
                            double x, row1, row2, iElement;
                            int n = 0;
                            row1 = row2 = (Math.PI / 2);

                            Console.Write("Введите значение для x: ");
                            x = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Введите степень приближения: ");
                            double epsilon = Convert.ToDouble(Console.ReadLine());

                            do
                            {
                                iElement = (Math.Pow(-1, n + 1)) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                                row2 = row1 += iElement;
                                row2 -= iElement;
                                n++;
                            } while (Math.Abs(row1 - row2) > epsilon);
                            Console.WriteLine("Сумма ряда при x = {0}, e = {1} равна: {2} \nколичество членов в ряду {3}", x, epsilon, row1, n + 1);
                        }
                        break;

                    case "4":
                        {
                            key = false;
                            Console.WriteLine("\nВы вышли из меню.");
                            Console.ReadLine();
                        }
                        break;

                    default:
                        {
                            Console.WriteLine(" \nВведен неправильный символ. Повторите ввод.");
                        }
                        break;
                }
            }
        }
    }
}