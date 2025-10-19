using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        /* Метод разворота матрицы (разворачивает матрицу в случае, если количество строк одной матрицы равно количеству
         * столбцов другой; соответственно для количества столбцов первой матрицы. Не производит перезаписи матриц во
         * всех остальных случаях). */
        public static int[,] Turn(int[,] matrix1, int[,] matrix2, int i, int j, int m, int n)
        {
            if (i - n - j + m == 0) // (альтернативная форма записи: i - j == n - m)
            {
                int[,] matrixTemp = new int[matrix1.GetLength(1), matrix1.GetLength(0)];

                for (int a = 0; a < matrix1.GetLength(0); a++)
                {
                    for (int b = 0; b < matrix1.GetLength(1); b++)
                    {
                        matrixTemp[b, a] = matrix1[a, b];
                    }
                }
                Console.WriteLine("Матрица была транспонирована.");

                matrix1 = matrixTemp;
                return matrix1;
            }
            else if (i - m - j + n == 0) // (альтернативная форма записи: i - m = j - n)
            {
                Console.WriteLine("Количества строк и столбцов матриц совпадают. В развороте нет необходимости.");
            }
            else
            {
                Console.WriteLine("Разворот матрицы не имеет смысла; к матрице невозможно будет применить бинарные арифметические и логический операторы.");
            }
            return matrix1;
        }

        // Метод подсчета суммы матриц.
        public static void MatrixSum(int[,] matrix1, int[,] matrix2)
        {
            for (int a = 0; a < matrix1.GetLength(0); a++)
            {
                for (int b = 0; b < matrix1.GetLength(1); b++)
                {
                    Console.Write("c({0}{1}) {2} {3}", a + 1, b + 1, matrix1[a, b] + matrix2[a, b], "\t");
                }
                Console.Write("\n");
            }
        }

        // Метод подсчета разницы матриц.
        public static void MatrixDif(int[,] matrix1, int[,] matrix2)
        {
            for (int a = 0; a < matrix2.GetLength(0); a++)
            {
                for (int b = 0; b < matrix2.GetLength(1); b++)
                {
                    Console.Write("c({0}{1}) {2} {3}", a + 1, b + 1, matrix1[a, b] - matrix2[a, b], "\t");
                }
                Console.WriteLine();
            }
        }

        // Метод умножения матрицы на матрицу.
        public static void MatXMat(int[,] matrix1, int[,] matrix2)
        {
            int result = 0;

            for (int a = 0; a < matrix2.GetLength(0); a++)
            {
                for (int b = 0; b < matrix2.GetLength(1); b++)
                {
                    for (int c = 0; c < matrix2.GetLength(1); c++)
                    {
                        result += (matrix1[a, c] * matrix2[c, b]);
                    }
                    Console.Write("c({0}{1}) {2} {3}", a, b, result, "\t");
                    result = 0;
                }
                Console.WriteLine();
            }
        }

        // Метод умножения матрицы на число.
        public static void MatrixMult(int[,] matrix1, int[,] matrix2)
        {
            string input;
            int mult, num;

            Console.Write("Введите номер матрицы и число, на которое желаете умножить эту матрицу, через пробел: ");
            input = Console.ReadLine();

            num = Convert.ToInt32(input.Split(' ')[0]);
            mult = Convert.ToInt32(input.Split(' ')[1]);

            for (int a = 0; a < matrix1.GetLength(0); a++)
            {
                for (int b = 0; b < matrix1.GetLength(1); b++)
                {
                    if (num == 1)
                    {
                        Console.Write("(c{0}{1}) {2} {3}", a + 1, b + 1, matrix1[a, b] * mult, "\t");
                    }
                    else if (num == 2)
                    {
                        Console.Write("(c{0}{1}) {2} {3}", a + 1, b + 1, matrix2[a, b] * mult, "\t");
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                        break;
                    }
                }
                Console.WriteLine();
            }
        }

        // Сравнение элементов матриц.
        public static void MatrixComparasion(int[,] matrix1, int[,] matrix2)
        {
            Console.WriteLine("Сравнение элементов матриц: ");
            for (int a = 0; a < matrix1.GetLength(0); a++)
            {
                for (int b = 0; b < matrix2.GetLength(1); b++)
                {
                    if (matrix1[a, b] == matrix2[a, b])
                    {
                        Console.Write("a{0}{1} = b{0}{1} {2}", a + 1, b + 1, "\t");
                    }
                    else
                    {
                        Console.Write("a{0}{1} != b{0}{1} {2}", a + 1, b + 1, "\t");
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            string option1, option2;
            bool key, run;

            run = true;
            key = true;

            Console.WriteLine("Лабораторная работа №3. Выполнил студент группы 6101-020302D Абросимов А.");

            while (key == true)
            {
                Console.WriteLine("\nВыберите раздел:\n(1) Действия с матрицами\n(2) Перевод из двоичной системы в десятичную \n(3) Выход");
                option1 = Console.ReadLine();
                switch (option1)
                {
                    case ("1"):
                        {
                            int i, j, m, n;
                            string inputIJ, inputMN;

                            Console.Write("Введите количество столбцов (i) и количество строк (j) матрицы через пробел: ");
                            inputIJ = Console.ReadLine();
                            i = Convert.ToInt32(inputIJ.Split(' ')[0]);
                            j = Convert.ToInt32(inputIJ.Split(' ')[1]);

                            Console.Write("\nВведите количество столбцов (m) и количество строк (n) матрицы через пробел: ");
                            inputMN = Console.ReadLine();
                            m = Convert.ToInt32(inputMN.Split(' ')[0]);
                            n = Convert.ToInt32(inputMN.Split(' ')[1]);

                            int[,] matrix1 = new int[i, j];
                            int[,] matrix2 = new int[m, n];

                            for (int a = 0; a < i; a++)
                            {
                                for (int b = 0; b < j; b++)
                                {
                                    Console.Write("Введите a{0}{1}: ", a + 1, b + 1);
                                    matrix1[a, b] = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                            Console.WriteLine();

                            for (int a = 0; a < m; a++)
                            {
                                for (int b = 0; b < n; b++)
                                {
                                    Console.Write("Введите b{0}{1}: ", a + 1, b + 1);
                                    matrix2[a, b] = Convert.ToInt32(Console.ReadLine());
                                }
                            }

                            matrix1 = Turn(matrix1, matrix2, i, j, m, n);
                            Console.WriteLine();

                            if (i - j - m + n == 0)
                            {
                                Console.WriteLine("Ваша первая матрица: ");
                                for (int a = 0; a < matrix1.GetLength(0); a++)
                                {
                                    for (int b = 0; b < matrix1.GetLength(1); b++)
                                    {
                                        Console.Write("(a{0}{1}) {2} \t", a + 1, b + 1, matrix1[a, b]);
                                    }
                                    Console.WriteLine();
                                }

                                Console.WriteLine("Ваша вторая матрица: ");
                                for (int a = 0; a < matrix2.GetLength(0); a++)
                                {
                                    for (int b = 0; b < matrix2.GetLength(1); b++)
                                    {
                                        Console.Write("(b{0}{1}) {2} \t", a + 1, b + 1, matrix2[a, b]);
                                    }
                                    Console.WriteLine();
                                }

                                while (run == true)
                                {
                                    Console.WriteLine("\nВыберите опцию: \n(1) Сложение матриц \n(2) Вычитание матриц \n(3) Умножение матриц \n(4) Умножение матрицы на число\n(5) Сравнение элементов матриц \n(6) Выход");
                                    option2 = Console.ReadLine();
                                    Console.WriteLine();

                                    switch (option2)
                                    {
                                        case ("1"): // Сложение.
                                            {
                                                MatrixSum(matrix1, matrix2);
                                            }
                                            break;

                                        case ("2"): // Вычитание.
                                            {
                                                MatrixDif(matrix1, matrix2);
                                            }
                                            break;

                                        case ("3"): // Умножение матрицы на матрицу.
                                            {
                                                MatXMat(matrix1, matrix2);
                                            }
                                            break;

                                        case ("4"): // Умножение матрицы на число.
                                            {
                                                MatrixMult(matrix1, matrix2);
                                            }
                                            break;

                                        case ("5"): // Сравнение элементов матриц.
                                            {
                                                MatrixComparasion(matrix1, matrix2);
                                            }
                                            break;

                                        case ("6"):
                                            {
                                                run = false;
                                                Console.WriteLine("Вы вышли из раздела.");
                                                Console.ReadLine();
                                            }
                                            break;

                                        default:
                                            {
                                                Console.WriteLine("Некорректный ввод. Введите номер функции повторно.");
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Матрицу невозможно транспонировать.");
                            }
                        }
                        break;

                    case ("2"):
                        {
                            int num;
                            bool flag;
                            string input, str;
                            flag = true;

                            while (flag)
                            {
                                Console.Write("Введите число, которое хотите перевести в двоичную систему счисления: ");
                                num = Convert.ToInt32(Console.ReadLine());

                                int mask1 = (num & 448) >> 6;        
                                int mask2 = (num & 7) << 6;
                                num = ((num & ~455) | mask1) | mask2;

                                str = Convert.ToString(num, 2);

                                if (str.Length < 9)
                                {
                                    for (int i = str.Length; i < 9; i++)
                                        str = "0" + str;
                                }

                                Console.WriteLine("Двоичное представление нового числа: {0}\nДесятичное представление нового числа: {1}", str, num);
                                Console.WriteLine("Хотите ввести еще одно число? (да/нет)");
                                input = Console.ReadLine();

                                if (input == "нет")
                                {
                                    flag = false;
                                    Console.WriteLine("Вы вышли из из раздела.");
                                }
                            }
                        }
                        break;

                    case ("3"):
                        {
                            key = false;
                            Console.WriteLine("Вы вышли из программы");
                            Console.ReadLine();
                        }
                        break;
                }
            }
        }
    }
}