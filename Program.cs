using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAB02_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string option;
            option = "";

            Console.WriteLine("Лабораторная работа 2, выполнил студент Абросимов А.О. (6101-020302D)");

            while (option != "4")
            {
                Console.WriteLine(" \n Выберите опцию: \n (1) Таблица значений функции \n (2) Серия выстрелов по мишени \n (3) Сумма рядов \n (4) Выход ");
                option = Console.ReadLine();

                if (option == "1")
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
                            Console.WriteLine("x не принадлежит области определения.");
                            break;
                        }
                        x += dX;
                    }
                }

                else if (option == "2")
                {
                    double shAmount;

                    Console.WriteLine("Введите желаемое количество выстрелов: ");
                    shAmount = Convert.ToInt32(Console.ReadLine());

                    for (int i = 0; i < shAmount; i++)
                    {
                        Console.Write("Введите x: ");
                        double x = Convert.ToDouble(Console.ReadLine());

                        Console.Write("Введите y: ");
                        double y = Convert.ToDouble(Console.ReadLine());

                        double funcVal = Math.Pow((x - 2), 2) - 3;

                        if ( (y >= funcVal && y <= x && y >= 0) || y >= funcVal && Math.Abs(y) >= x && y <= 0)
                        {
                            Console.WriteLine("Точка принадлежит области.");
                        }
                        else
                        {
                            Console.WriteLine("Точка не принадлежит области.");
                        }
                    }
                }

                else if (option == "3")
                {
                    double result, x, row1, row2, iElement;
                    int amount = 0;
                    row1 = row2 = (Math.PI / 2);

                    Console.Write("Введите значение для x: ");
                    x = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите степень приближения: ");
                    double epsilon = Convert.ToDouble(Console.ReadLine());
                    
                    for (int n = 0;;n++)
                    {
                        Console.WriteLine(row1 + " {0} член", n);
                        iElement = (Math.Pow(-1, n + 1)) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                        row2 = row1 += iElement;
                        row2 -= iElement;

                        amount = n;
                        result = row1;
                        if (Math.Abs(row1 - row2) < epsilon)
                            break;
                    }
                    Console.WriteLine("Сумма ряда при x = {0}, e = {1} равна: {2} \n количество членов в ряду {3}", x, epsilon, result, amount + 1);
                }

                else if (option == "4")
                {
                    Console.WriteLine("Вы вышли из меню.");
                    Console.ReadLine();
                    break;
                }

                else
                {
                    Console.WriteLine(" \n Введен неправильный символ. Повторите ввод.");
                    continue;
                }
            }
        }
    }
}
