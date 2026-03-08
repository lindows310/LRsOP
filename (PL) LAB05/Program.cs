using System;

namespace LAB01
{
    internal class Program
    {
        public static void PrintTitle()
        {
            Utils.ColoredWrite("|YELLOW| Лабораторная работа No1. Язык программирования C#: " +
                              "Повторение. Исключительные ситуации. |GRAY| \nВыполнил студент группы 6101-020302 " +
                              "|YELLOW| Абросимов Артём.\n");
            Console.WriteLine("======================================================================================");
        }
        static void Main()
        {
            int len1, len2, len3; bool run = true;
            Program.PrintTitle();

            Utils.ColoredWrite("Введите |YELLOW| размерности векторов |GRAY| и |YELLOW| односвязного списка |RED| (через пробел): ");
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
                Utils.ColoredWriteLine("|RED| Ошибка. Неправильный формат ввода. |DARKGRAY| Созданы векторы размерности 2.");
            }
            catch (IndexOutOfRangeException)
            {
                len1 = len2 = len3 = 2;
                Utils.ColoredWriteLine("|RED| Ошибка. Вектору (векторам) не была присвоена размерность. |DARKGRAY| Созданы векторы размерности 2.");
            }
            catch (OverflowException)
            {
                len1 = len2 = len3 = 2;
                Utils.ColoredWriteLine("|RED| Ошибка. Размерностью вектора (векторов) было задано отрицательное число. |DARKGRAY| Созданы векторы размерностью 2.");
            }
            Console.WriteLine("======================================================================================");

            ArrayVector vec1 = new ArrayVector(len1); ArrayVector vec2 = new ArrayVector(len2); IVectorable buffer = null;
            LinkedListVector linkedListVec = new LinkedListVector(len3);

            Console.WriteLine($"Созданы вектора размерностями {len1} и {len2} соответственно, односвязный список, содержащий {len3} узлов.");

            Console.Write("Введите значения компонент вектора 1 (через пробел): ");
            vec1.FillVal();
            Console.Write("Введите значения компонент вектора 2 (через пробел): ");
            vec2.FillVal();
            Console.Write("Введите значения узлов односвязного списка (через пробел): ");
            linkedListVec.FillVal();

            ArrayVector[] vectors = new ArrayVector[] { vec1, vec2 };

            Utils.ColoredWrite("|YELLOW| Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();

            Console.Clear();

            while (run)
            {
                Console.Clear();
                Program.PrintTitle();
                Utils.ColoredWriteLine("|YELLOW| Выберите раздел: |GRAY| \n[1] Действия над векторами \n[2] Операции над векторами \n[3] Односвязный список" +
                                                                        "\n[4] Клонирование вектора \n[5] Массив векторов \n[любой другой символ] выход из программы");
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

                            Utils.ColoredWrite("|YELLOW| \nВведите опцию: ");
                            char option1 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            Program.PrintTitle();

                            int selectNum;
                            try
                            {
                                switch (option1)
                                {
                                    case ('1'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| информацию о котором вы хотите получить: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Вектор равен: {vectors[selectNum - 1].ToString()}");
                                        break;

                                    case ('2'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| который вы хотите изменить, |YELLOW| желаемую координату |GRAY| и |YELLOW| новое значение (в указанном порядке): ");
                                        string[] values = Console.ReadLine().Split(' ');
                                        vectors[Convert.ToInt32(values[0]) - 1][Convert.ToInt32(values[1]) - 1] = Convert.ToInt32(values[2]);
                                        Console.WriteLine($"{values[1]}-ая координата {values[0]}-го вектора теперь равна {values[2]}");
                                        break;

                                    case ('3'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| норму которого вы хотите посчитать: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Норма вектора {selectNum} равна {vectors[selectNum - 1].GetNorm()}");
                                        break;

                                    case ('4'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| к которому вы хотите применить операцию: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Сумма положительных компонент вектора {selectNum} с четными номерами равна: {vectors[selectNum - 1].SumPositivesFromChetIndex()}");
                                        break;

                                    case ('5'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| к которому вы хотите применить операцию: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Сумма компонент вектора {selectNum} с нечетными номерами равна: {vectors[selectNum - 1].SumLessFromNechetIndex()}");
                                        break;

                                    case ('6'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| к которому вы хотите применить операцию: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Произведение четных положительных компонент вектора {selectNum} равна: {vectors[selectNum - 1].MultChet()}");
                                        break;

                                    case ('7'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| к которому вы хотите применить операцию: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine($"Произведение нечетных компонент вектора {selectNum} равна: {vectors[selectNum - 1].MultNechet()}");
                                        break;

                                    case ('8'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| к которому вы хотите применить операцию: ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        vectors[selectNum - 1].SortUp();
                                        Utils.ColoredWriteLine($"|YELLOW| Сортировка вектора {selectNum} по возрастанию |GRAY| проведена успешно.");
                                        break;

                                    case ('9'):
                                        Utils.ColoredWrite("Введите |YELLOW| номер вектора, |GRAY| который вы хотите отсортировать (по убыванию): ");
                                        selectNum = Convert.ToInt32(Console.ReadLine());
                                        vectors[selectNum - 1].SortDown();
                                        Utils.ColoredWriteLine($"|YELLOW| Сортировка вектора {selectNum} по убыванию |GRAY| проведена успешно.");
                                        break;

                                    default:
                                        flag1 = false;
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"|RED| Произошла ошибка. |GRAY| {e.Message}");
                            }
                            Utils.ColoredWriteLine("|YELLOW| \nЧтобы продолжить, нажмите любую клавишу...");
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
                                            "\n[5] Сложение векторов разных типов \n[любой другой символ] Выйти из раздела");

                            Utils.ColoredWrite(" |YELLOW| \nВведите опцию: ");
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
                                            ArrayVector result1 = (ArrayVector) Vectors.SumSt(vec1, vec2);
                                            Utils.ColoredWriteLine($"|YELLOW| Сложение векторов |GRAY| произведено |YELLOW| успешно. |GRAY| Результат сложения: {result1.ToString()}");
                                        }
                                        catch (Exception e)
                                        {
                                            Utils.ColoredWriteLine($" |RED| Ошибка. |DARKGRAY| {e.Message}");
                                        }
                                        break;

                                    case ('2'):
                                        try
                                        {
                                            int result2 = Vectors.ScalarSt(vec1, vec2);
                                            Utils.ColoredWriteLine($"|YELLOW| Скалярное произведение векторов |GRAY| произведено |YELLOW| успешно. Результат операции: {result2}");
                                        }
                                        catch (Exception e)
                                        {
                                            Utils.ColoredWriteLine($" |RED| Ошибка. |DARKGRAY| {e.Message}");
                                        }
                                        break;

                                    case ('3'):
                                        Console.Write("Введите номер вектора и число, на которое вы хотите умножить вектор: ");
                                        string[] options = Console.ReadLine().Split(' ');

                                        ArrayVector result3 = (ArrayVector) Vectors.MultNumberSt(vectors[Convert.ToInt32(options[0]) - 1], Convert.ToInt32(options[1]));
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
                                Utils.ColoredWriteLine("|RED| Ошибка. |DARKGRAY| Неверный формат ввода.");
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Utils.ColoredWriteLine("|RED| Ошибка. |DARKGRAY| Вектора с заданным номером не существует");
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"|RED| Ошибка. |DARKGRAY| {e.Message}");
                            }
                            Utils.ColoredWriteLine("|YELLOW| \nЧтобы продолжить, нажмите любую клавишу...");
                            Console.ReadKey();
                        }
                        break;

                    case ('3'):
                        bool flag3 = true;
                        while (flag3)
                        {
                            Console.Clear();
                            Program.PrintTitle();

                            Console.WriteLine("[1] Отобразить информацию об односвязном списке\n[2] Создать односвязный список\n[3] Добавить элемент в начало списка\n[4] Удалить элемент из начала списка" +
                                            "\n[5] Добавить элемент в конец списка\n[6] Удалить элемент из конца списка\n[7] Добавить элемент вовнутрь списка\n[8] Удалить элемент из списка\n[9] Вычислить норму вектора\n[Любой другой символ] Выйти из раздела");

                            Utils.ColoredWrite("|YELLOW| \nВведите опцию: ");
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
                                        Utils.ColoredWrite("Введите |YELLOW| количество координат |GRAY| списка: ");
                                        linkedListVec = new LinkedListVector(int.Parse(Console.ReadLine()));
                                        Utils.ColoredWrite($"|YELLOW| Инициализирован вектор: |GRAY| {linkedListVec.ToString()}");
                                        break;
                                    case ('3'):
                                        Utils.ColoredWrite("Введите |YELLOW| значение узла, |GRAY| который вы хотите добавить в начало списка: ");
                                        linkedListVec.AddToStart(int.Parse(Console.ReadLine()));
                                        Utils.ColoredWrite($"|YELLOW| Измененный вектор: |GRAY| {linkedListVec.ToString()}");
                                        break;
                                    case ('4'):
                                        Utils.ColoredWrite($"|YELLOW| Удалено значение: |GRAY| {linkedListVec[0]}");
                                        linkedListVec.DeleteFromStart();
                                        break;
                                    case ('5'):
                                        Utils.ColoredWrite("Введите |YELLOW| значение узла, |GRAY| который вы хотите добавить в конец списка: ");
                                        linkedListVec.AddToEnd(int.Parse(Console.ReadLine()));
                                        Utils.ColoredWrite($"|YELLOW| Измененный вектор: |GRAY| {linkedListVec.ToString()}");
                                        break;
                                    case ('6'):
                                        Utils.ColoredWrite($"|YELLOW| Удалено значение: |GRAY| {linkedListVec[linkedListVec.Length - 1]}");
                                        linkedListVec.DeleteFromEnd();
                                        break;
                                    case ('7'):
                                        Utils.ColoredWrite("Введите |YELLOW| значение узла |GRAY| и |YELLOW| индекс, |GRAY| по которому вы хотите добавить элемент: ");
                                        string[] input5 = Console.ReadLine().Split(' ');
                                        linkedListVec.AddInBetween(Convert.ToInt32(input5[0]), Convert.ToInt32(input5[1]));
                                        Utils.ColoredWrite($"|YELLOW| Измененный вектор: |GRAY| {linkedListVec.ToString()}");
                                        break;
                                    case ('8'):
                                        Utils.ColoredWrite("Введите |YELLOW| индекс, |GRAY| по которому вы хотите удалить элемент: ");
                                        string input51 = Console.ReadLine();
                                        linkedListVec.DeleteFromBetween(Convert.ToInt32(input51));
                                        break;
                                    case ('9'):
                                        Utils.ColoredWrite($"|YELLOW| Норма вектора |GRAY| равна: {linkedListVec.GetNorm()}");
                                        break;
                                    default:
                                        flag3 = false;
                                        break;
                                }
                            }
                            catch (FormatException)
                            {
                                Utils.ColoredWriteLine("|RED| Неправильный формат ввода. |DARKGRAY| Повторите ввод.");
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Utils.ColoredWriteLine("|RED| Ошибка. |DARKGRAY| Вектора с заданным номером не существует");
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"|RED| Ошибка. |GRAY| {e.Message}");
                            }
                            Utils.ColoredWriteLine("|YELLOW| \nЧтобы продолжить, нажмите любую клавишу...");
                            Console.ReadKey();
                        }
                        break;

                    case ('4'):
                        bool flag4 = true;
                        while (flag4)
                        {
                            Console.Clear();
                            Program.PrintTitle();

                            Console.WriteLine("[1] Клонировать вектор \n[2] Сравнить вектор с клонированным вектором \n[любой другой символ] Выйти из раздела");

                            Utils.ColoredWrite(" |YELLOW| \nВведите опцию: ");
                            char option4 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            Program.PrintTitle();
                            IVectorable[] allVecs = { vec1, vec2, linkedListVec };
                            try
                            {
                                switch (option4)
                                {
                                    case ('1'):
                                        Utils.ColoredWrite($" |YELLOW| [1] |GRAY| {vectors[0].ToString()} \n |YELLOW| [2] |GRAY| {vectors[1].ToString()} \n |YELLOW| [3] |GRAY| {linkedListVec.ToString()}\n" +
                                                           $"Введите |YELLOW| номер вектора, |GRAY| который вы хотите поместить в буфер клонирования: ");
                                        buffer = allVecs[int.Parse(Console.ReadLine()) - 1];
                                        Utils.ColoredWrite($"|YELLOW| Клонирование вектора было произведено успешно. |GRAY| В Буфер записан вектор: {buffer.ToString()}");
                                        break;

                                    case ('2'):
                                        foreach (IVectorable vec in allVecs)
                                            if (buffer != null)
                                            {
                                                if (vec.Equals(buffer))
                                                    Utils.ColoredWriteLine($"Клонированный вектор |YELLOW| равен |GRAY| вектору {vec.ToString()} ({vec.GetType()})");
                                                else
                                                    Utils.ColoredWriteLine($"Клонированный вектор |RED| не равен |GRAY| вектору {vec.ToString()} ({vec.GetType()})");
                                            }
                                            else
                                                Utils.ColoredWriteLine($"В буфер не был скопирован вектор. Сравнение векторов |RED| возможно только в случае, |GRAY| если в буфер был помещен вектор");
                                        break;

                                    default:
                                        flag4 = false;
                                        break;
                                }
                            }
                            catch (FormatException)
                            {
                                Utils.ColoredWriteLine("|RED| Неправильный формат ввода. |DARKGRAY| Повторите ввод.");
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Utils.ColoredWriteLine("|RED| Ошибка. |DARKGRAY| Вектора с заданным номером не существует");
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"|RED| Ошибка. |DARKGRAY| {e.Message}");
                            }
                            Utils.ColoredWriteLine("|YELLOW| \nЧтобы продолжить, нажмите любую клавишу...");
                            Console.ReadKey();
                        }
                        break;

                    case ('5'):
                        bool flag5 = true;
                        IVectorable[] arrVectors = null;
                        while (flag5)
                        {
                            Console.Clear();
                            Program.PrintTitle();

                            Console.WriteLine("[1] Отобразить содержимое массива векторов \n[2] Создать массив векторов \n[3] Сортировка массива векторов " +
                                              "\n[4] Отобразить вектора с максимальным и минимальным количеством координат\n[любой другой символ] Выйти из программы");

                            Utils.ColoredWrite(" |YELLOW| \nВведите опцию: ");
                            char option5 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            Program.PrintTitle();

                            try
                            {
                                switch (option5)
                                {
                                    case ('1'):
                                        Utils.ColoredWriteLine("Массив векторов:");
                                        for (int i = 0; i < arrVectors?.Length; i++)
                                            Utils.ColoredWriteLine($"|YELLOW| [{i + 1}] |GRAY| \t{arrVectors[i].ToString()}");
                                        break;
                                    case ('2'):
                                        Utils.ColoredWrite("Введите |YELLOW| длину массива векторов: ");
                                        arrVectors = Vectors.CreateArray(int.Parse(Console.ReadLine()));
                                        Console.WriteLine("Был создан массив векторов:");
                                        for (int i = 0; i < arrVectors.Length; i++)
                                            Utils.ColoredWriteLine($"|YELLOW| [{i + 1}] |GRAY| \t{arrVectors[i].ToString()}");
                                        break;

                                    case ('3'):
                                        arrVectors = Vectors.VectorsSort(arrVectors);
                                        Utils.ColoredWriteLine("Отсортированный |YELLOW| массив векторов:");
                                        for (int i = 0; i < arrVectors.Length; i++)
                                            Utils.ColoredWriteLine($"|YELLOW| [{i + 1}] |GRAY| (модуль: {Math.Round(arrVectors[i].GetNorm(), 3)})\t{arrVectors[i].ToString()}");
                                        Console.ReadLine();
                                        break;

                                    case ('4'):
                                        (IVectorable minCords, IVectorable maxCords) = Vectors.FindByMinMaxLength(arrVectors);
                                        Utils.ColoredWriteLine($"Вектор с максимальным числом координат: |YELLOW| {maxCords.ToString()} |GRAY| " +
                                                             $"\nВектор с минимальным числом координат: |YELLOW| {minCords.ToString()}");
                                        break;

                                    default:
                                        flag5 = false;
                                        break;
                                }
                                Utils.ColoredWriteLine("|YELLOW| \nЧтобы продолжить, нажмите любую клавишу...");
                                Console.ReadKey();
                            }
                            catch (Exception e)
                            {
                                Utils.ColoredWriteLine($"|RED| Ошибка. |DARKGRAY| {e.Message}");
                            }
                        }
                        break;

                    default:
                        Utils.ColoredWriteLine("|RED| \nВыход из программы...");
                        run = false;
                        break;
                }
            }

        }
    }
}
