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
        static void Main()
        {
            int len1, len2, len3; bool run = true;
            Program.PrintTitle();

            Utils.ColoredWrite("Введите размерности векторов и односвязного списка (через пробел): ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 6, 7, ConsoleColor.Red });
            string input = Console.ReadLine();
            try
            {
                len1 = int.Parse(input.Split(' ')[0]);
                len2 = int.Parse(input.Split(' ')[1]);
                len3 = int.Parse(input.Split(' ')[2]);
            }
            catch (FormatException)
            {
                len1 = len2 = len3 = 2;
                Utils.ColoredWriteLine("Ошибка. Неправильный формат ввода. Созданы векторы размерности 2.", new object[] { 0, 3, ConsoleColor.Red }, new object[] { 4, 7, ConsoleColor.DarkGray });
            }
            catch (IndexOutOfRangeException)
            {
                len1 = len2 = len3 = 2;
                Utils.ColoredWriteLine("Ошибка. Вектору (векторам) не была присвоена размерность. Созданы векторы размерности 2.", new object[] { 0, 6, ConsoleColor.Red }, new object[] { 7, 9, ConsoleColor.DarkGray });
            }
            catch (OverflowException)
            {
                len1 = len2 = len3 = 2;
                Utils.ColoredWriteLine("Ошибка. Размерностью вектора (векторов) было задано отрицательное число. Созданы векторы размерностью 2.", new object[] { 0, 6, ConsoleColor.Red }, new object[] { 7, 10, ConsoleColor.DarkGray });
            }
            Console.WriteLine("======================================================================================");

            ArrayVector vec1 = new ArrayVector(len1); ArrayVector vec2 = new ArrayVector(len2);
            LinkedListVector linkedListVec = new LinkedListVector(len3);

            Console.WriteLine($"Созданы вектора размерностями {len1} и {len2} соответственно, односвязный список, содержащий {len3} узлов.");

            Console.Write("Введите значения компонент вектора 1 (через пробел): ");
            vec1.FillVal();
            Console.Write("Введите значения компонент вектора 2 (через пробел): ");
            vec2.FillVal();
            Console.Write("Введите значения узлов односвязного списка (через пробел): ");
            linkedListVec.FillVal();

            ArrayVector[] vectors = new ArrayVector[] { vec1, vec2 };

            Utils.ColoredWrite("Нажмите любую клавишу, чтобы продолжить...", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
            Console.ReadKey();

            Console.Clear();

            while (run)
            {
                Console.Clear();
                Program.PrintTitle();
                Utils.ColoredWriteLine("Выберите раздел: \n[1] Действия над векторами \n[2] Операции над векторами \n[3] Односвязный список\n[любой другой символ] выход из программы", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
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
                                                 "\n[7] Подсчет произведения всех нечетных элементов \n[8] Сортировка массива по возрастанию \n[9] Сортировка массива по убыванию \n[Любой другой символ] Выход из программы");

                            Utils.ColoredWrite("\nВведите опцию: ", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
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
                                Utils.ColoredWriteLine($"Произошла ошибка. {e.Message}", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 1, ConsoleColor.Red });
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
                                            "\n[3] Умножение вектора на число \n[4] Посчитать норму вектора " +
                                            "\n[5] Сложение векторов разных типов [любой другой символ] Выйти из раздела");

                            Utils.ColoredWrite("\nВведите опцию: ", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
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
                                        int choice4 = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Норма вектора равна: {Vectors.GetNormSt(vectors[choice4 - 1])}");
                                        break;

                                    case ('5'):
                                        Console.Write("Введите номер вектора, который вы хотите сложить с вектором значений односвязного списка: ");
                                        int choice5 = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Сумма векторов равна: {Vectors.SumSt(vectors[choice5 - 1], linkedListVec)}");
                                        break;

                                    default:
                                        flag2 = false;
                                        break;
                                }
                            }
                            catch (FormatException)
                            {
                                Utils.ColoredWriteLine($"Ошибка. Неверный формат ввода.", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 3, ConsoleColor.DarkGray });
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Utils.ColoredWriteLine($"Ошибка. Вектора с заданным номером не существует", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 6, ConsoleColor.DarkGray });
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"Ошибка. {e.Message}", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 4, ConsoleColor.DarkGray });
                            }
                            Utils.ColoredWriteLine("\nЧтобы продолжить, нажмите любую клавишу...", new object[] { 0, 2, ConsoleColor.Yellow }, new object[] { 3, 4, ConsoleColor.Yellow });
                            Console.ReadKey();
                        }
                        break;

                    case ('3'):
                        bool flag3 = true;
                        while (flag3)
                        {
                            Console.Clear();
                            Program.PrintTitle();

                            Console.WriteLine("[1] Отобразить информацию об односвязном списке\n[2] Создать односвязный список\n[3] Добавить элемент в начало списка" +
                                            "\n[4] Добавить элемент в конец списка\n[5] Добавить элемент вовнутрь списка\n[6] Вычислить норму вектора\n[Любой другой символ] Выйти из раздела");

                            Utils.ColoredWrite("\nВведите опцию: ", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                            char option3 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            Program.PrintTitle();

                            try
                            {
                                switch (option3)
                                {
                                    case ('1'):
                                        Console.WriteLine($"Вектор равен: {linkedListVec.ToString()}");
                                        break;
                                    case ('2'):
                                        Utils.ColoredWrite("Введите количество координат вектора: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        linkedListVec = new LinkedListVector(int.Parse(Console.ReadLine()));
                                        Utils.ColoredWrite($"Инициализирован вектор: {linkedListVec.ToString()}", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                                        break;
                                    case ('3'):
                                        Utils.ColoredWrite("Введите значение узла, который вы хотите добавить в начало списка: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        linkedListVec.AddToStart(int.Parse(Console.ReadLine()));
                                        Utils.ColoredWrite($"Измененный вектор: {linkedListVec.ToString()}", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                                        break;
                                    case ('4'):
                                        Utils.ColoredWrite("Введите значение узла, который вы хотите добавить в конец списка: ", new object[] { 1, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        linkedListVec.AddToEnd(int.Parse(Console.ReadLine()));
                                        Utils.ColoredWrite($"Измененный вектор: {linkedListVec.ToString()}", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                                        break;
                                    case ('5'):
                                        Utils.ColoredWrite("Введите значение узла и индекс, по которому вы хотите добавить элемент: ", new object[] { 1, 2, ConsoleColor.Yellow }, new object[] { 4, 4, ConsoleColor.Yellow });
                                        string[] input5 = Console.ReadLine().Split(' ');
                                        linkedListVec.AddInBetween(Convert.ToInt32(input5[0]), Convert.ToInt32(input5[1]));
                                        Utils.ColoredWrite($"Измененный вектор: {linkedListVec.ToString()}", new object[] { 0, 0, ConsoleColor.Yellow }, new object[] { 1, 1, ConsoleColor.Yellow });
                                        break;
                                    case ('6'):
                                        Utils.ColoredWrite($"Норма вектора равна: {linkedListVec.GetNorm()}", new object[] { 0, 1, ConsoleColor.Yellow }, new object[] { 2, 2, ConsoleColor.Yellow });
                                        break;
                                    default:
                                        flag3 = false;
                                        break;
                                }
                            }
                            catch (FormatException)
                            {
                                Utils.ColoredWriteLine($"Неправильный формат ввода. Повторите ввод.", new object[] { 0, 2, ConsoleColor.Red }, new object[] { 3, 5, ConsoleColor.DarkGray });
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Utils.ColoredWriteLine($"Ошибка. Вектора с заданным номером не существует", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 6, ConsoleColor.DarkGray });
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"Ошибка. {e.Message}", new object[] { 0, 0, ConsoleColor.Red }, new object[] { 1, 4, ConsoleColor.DarkGray });
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
