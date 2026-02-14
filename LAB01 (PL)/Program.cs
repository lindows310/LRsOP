using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class Program
    {
        public static void PrintTitle()
        {
            Utils.ColoredWrite("Лабораторная работа No1. Язык программирования C#: " +
                              "Повторение. Исключительные ситуации. \nВыполнил студент группы 6101-020302 " +
                              "Абросимов Артём.\n", new object[] { 0, 8, ConsoleColor.Yellow }, new object[] { 13, 14, ConsoleColor.Yellow });
            Console.WriteLine("======================================================================================");
        }
        static void Main(string[] args)
        {
            int len1, len2;
            bool run = true;
            Program.PrintTitle();

            Utils.ColoredWrite("Введите размерности векторов (через пробел): ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Red });
            string input = Console.ReadLine();
            try
            {
                len1 = int.Parse(input.Split(' ')[0]);
                len2 = int.Parse(input.Split(' ')[1]);
                Utils.ColoredWriteLine($"Созданы вектора размерностями {len1} и {len2} соответственно.");
            }
            catch (FormatException e)
            {
                len1 = len2 = 2;
                Utils.ColoredWriteLine("Ошибка. Неправильный формат ввода. Созданы векторы размерности 2.", new object[] { 0, 3, ConsoleColor.Red }, new object[] { 7, 7, ConsoleColor.Yellow });
            }
            catch (IndexOutOfRangeException e)
            {
                len1 = len2 = 2;
                Utils.ColoredWriteLine("Ошибка. Одному из векторов не была присвоена размерность. Созданы векторы размерности 2.", new object[] { 0, 3, ConsoleColor.Red }, new object[] { 4, 7, ConsoleColor.Yellow });
            }
            Console.WriteLine("======================================================================================");
            
            Console.Write("Введите значения компонент вектора 1 (через пробел): ");
            ArrayVector vec1 = new ArrayVector(len1);
            vec1.FillVal();

            Console.Write("Введите значения компонент вектора 2 (через пробел): ");
            ArrayVector vec2 = new ArrayVector(len2);
            vec2.FillVal();

            ArrayVector[] vectors = new ArrayVector[] { vec1, vec2 };

            Utils.ColoredWrite("Нажмите любую клавишу, чтобы продолжить...", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
            Console.ReadKey();

            Console.Clear();

            while (run)
            {
                Console.Clear();
                Program.PrintTitle();
                Utils.ColoredWriteLine("Выберите раздел: \n[1] Действия над векторами \n[2] Операции над векторами \n[любой другой символ] выход из программы", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                char optionCat = Console.ReadKey().KeyChar;
                switch (optionCat)
                {
                    case ('1'):
                        bool flag1 = true;
                        while (flag1)
                        {
                            Console.Clear();
                            Program.PrintTitle();

                            Console.WriteLine("[1] Отображение информации о векторе \n[2] Изменение компоненты вектора по индексу" +
                                                 "\n[3] Вычисление нормы вектора \n[4] Сумма положительных компонент вектора с четными номерами" +
                                                 "\n[5] Сумма компонент вектора с нечетными номерами \n[6] Подсчет произведения четных положительных компонент" +
                                                 "\n[7] Подсчет произведения всех нечетных элементов \n[8] Сортировка массива по возрастанию \n[9] Сортировка массива по убыванию \n[Любой другой символ] Выход из программы\n");

                            Utils.ColoredWrite("Введите опцию: ", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                            char option1 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            Program.PrintTitle();

                            int selectNum;
                            try
                            {
                                switch (option1)
                                {
                                    case ('1'):
                                        Utils.ColoredWrite("Введите номер вектора, информацию о котором вы хотите получить: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Вектор равен: {vectors[selectNum - 1].ToString()}");
                                        break;
                                    case ('2'):
                                        Utils.ColoredWrite("Введите номер вектора, который вы хотите изменить, желаемую координату и новое значение (в указанном порядке): ", new object[] { 1, 2, ConsoleColor.Yellow }, new object[] { 7, 8, ConsoleColor.Yellow }, new object[] { 10, 11, ConsoleColor.Yellow });
                                        string[] values = Console.ReadLine().Split(' ');
                                        vectors[Convert.ToInt32(values[0]) - 1][Convert.ToInt32(values[1]) - 1] = Convert.ToInt32(values[2]);
                                        Console.WriteLine($"{values[1]}-ая координата {values[0]}-го вектора теперь равна {values[2]}");
                                        break;
                                    case ('3'):
                                        Utils.ColoredWrite("Введите номер вектора, норму которого вы хотите посчитать: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Норма вектора {selectNum} равна {vectors[selectNum - 1].GetNorm()}");
                                        break;
                                    case ('4'):
                                        Utils.ColoredWrite("Введите номер вектора, к которому вы хотите применить операцию: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Сумма положительных компонент вектора {selectNum} с четными номерами равна: {vectors[selectNum - 1].SumPositivesFromChetIndex()}");
                                        break;
                                    case ('5'):
                                        Utils.ColoredWrite("Введите номер вектора, к которому вы хотите применить операцию: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Сумма компонент вектора {selectNum} с нечетными номерами равна: {vectors[selectNum - 1].SumLessFromNechetIndex()}");
                                        break;
                                    case ('6'):
                                        Utils.ColoredWrite("Введите номер вектора, к которому вы хотите применить операцию: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Произведение четных положительных компонент вектора {selectNum} равна: {vectors[selectNum - 1].MultChet()}");
                                        break;
                                    case ('7'):
                                        Utils.ColoredWrite("Введите номер вектора, к которому вы хотите применить операцию: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Произведение нечетных компонент вектора {selectNum} равна: {vectors[selectNum - 1].MultNechet()}");
                                        break;
                                    case ('8'):
                                        Utils.ColoredWrite("Введите номер вектора, который вы хотите отсортировать (по возрастанию): ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        vectors[selectNum - 1].SortUp();
                                        Utils.ColoredWriteLine($"Сортировка вектора {selectNum} по возрастанию проведена успешно.", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
                                        break;
                                    case ('9'):
                                        Utils.ColoredWrite("Введите номер вектора, который вы хотите отсортировать (по убыванию): ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        vectors[selectNum - 1].SortDown();
                                        Utils.ColoredWriteLine($"Сортировка вектора {selectNum} по убыванию проведена успешно.", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
                                        break;
                                    default:
                                        flag1 = false;
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"Неправильный формат ввода. Повторите ввод.", new object[] { 0, 2, ConsoleColor.Red }, new object[] { 3, 4, ConsoleColor.DarkGray });
                            }
                            Utils.ColoredWriteLine("\nЧтобы продолжить, нажмите любую клавишу...", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
                            Console.ReadKey();
                        }
                        break;
                    case ('2'):
                        bool flag2 = true;
                        while (flag2)
                        {
                            Console.Clear();
                            Program.PrintTitle();

                            Console.WriteLine("[1] Сложение векторов \n[2] Скалярное произведение векторов" +
                                            "\n[3] Умножение вектора на число \n[4] Посчитать норму вектора \n[любой другой символ] Выйти из раздела");

                            Utils.ColoredWrite("Введите опцию: ", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                            char option2 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            Program.PrintTitle();

                            try
                            {
                                switch (option2)
                                {
                                    case ('1'):
                                        try
                                        {
                                            if (vec1.cords.Length != vec2.cords.Length)
                                                throw new Exception("Длины векторов не совпадают");

                                            ArrayVector result1 = Vectors.SumSt(vec1, vec2);
                                            Utils.ColoredWriteLine($"Сложение векторов произведено успешно. Результат сложения: {result1.ToString()}", new object[] { 0, 1, ConsoleColor.Yellow }, new object[] { 3, 3, ConsoleColor.Yellow });
                                        }
                                        catch (Exception e)
                                        {
                                            Utils.ColoredWriteLine($"Ошибка. {e.Message}", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 4, ConsoleColor.DarkGray });
                                        }
                                        break;
                                    case ('2'):
                                        try
                                        {
                                            if (vec1.cords.Length != vec2.cords.Length)
                                                throw new Exception("Длины векторов не совпадают");

                                            int result2 = Vectors.ScalarSt(vec1, vec2);
                                            Utils.ColoredWriteLine($"Скалярное произведение векторов произведено успешно. Результат операции: {result2}", new object[] { 0, 1, ConsoleColor.Yellow }, new object[] { 4, 4, ConsoleColor.Yellow });
                                        }
                                        catch (Exception e)
                                        {
                                            Utils.ColoredWriteLine($"Ошибка. {e.Message}", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 4, ConsoleColor.DarkGray });
                                        }
                                        break;
                                    case ('3'):
                                        Console.Write("Введите номер вектора и число, на которое вы хотите умножить вектор: ");
                                        string[] options = Console.ReadLine().Split(' ');
                                        ArrayVector result3 = Vectors.MultNumberSt(vectors[Convert.ToInt32(options[0]) - 1], Convert.ToInt32(options[1]));
                                        Console.WriteLine($"Результат умножения вектора на число равняется: {result3.ToString()}");
                                        break;
                                    case ('4'):
                                        Console.Write("Введите номер вектора, норму которого хотите посчитать: ");
                                        int choice = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Норма вектора равна: {Vectors.GetNormSt(vectors[choice - 1])}");
                                        break;
                                    default:
                                        flag2 = false;
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"Неправильный формат ввода. Повторите ввод.", new object[] { 0, 2, ConsoleColor.Red }, new object[] { 3, 4, ConsoleColor.DarkGray });
                            }
                            Utils.ColoredWriteLine("\nЧтобы продолжить, нажмите любую клавишу...", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Utils.ColoredWriteLine($"\nВыход из программы...", new object[] { 0, 1, ConsoleColor.Red }, new object[] { 2, 2, ConsoleColor.Red });
                        run = false;
                        break;
                }
            }
            
        }
    }
}
