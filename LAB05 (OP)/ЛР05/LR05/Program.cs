using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LR05
{
    internal class Program
    {
        public static void FracGen(out Fraction frac1, out Fraction frac2)
        {
            string input, fracinp;

            Console.Write("Введите дробь 1 (в порядке числитель, знаменатель - через пробел): ");
            input = Console.ReadLine();
            frac1 = FracInput(input);

            Console.Write("Введите дробь 2 (в порядке числитель, знаменатель - через пробел): ");
            input = Console.ReadLine();
            frac2 = FracInput(input);
        }
        public static Fraction FracInput(string input)
        {
            try
            {
                return new Fraction(numerator: int.Parse(input.Split(' ')[0]), denominator: int.Parse(input.Split(' ')[1]));
            }
            catch (FormatException e)
            {
                Console.WriteLine("Неправильный формат ввода. Инициализирована дробь: 1/1");
                return new Fraction(numerator: 1, denominator: 1);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа No 5. Выполнил студент группы 6101-020302D Абросимов Артём\n");

            string option1, option2;
            bool runMainMenu, runMenu2;
            runMainMenu = true;

            while (runMainMenu)
            {
                Console.WriteLine("Выберите опцию:\n" +
                                  "(1) Работа с дробями\n" +
                                  "(Любой символ) Выход из программы");

                runMenu2 = true;

                option1 = Console.ReadLine();
                switch (option1)
                {
                    case ("1"):
                        {
                            FracGen(out Fraction frac1, out Fraction frac2);
                            while (runMenu2)
                            {
                                Console.WriteLine("Выберите опцию: \n" +
                                                  "(1) Результат сложения дробей\n" +
                                                  "(2) Результат вычитания дробей\n" +
                                                  "(3) Результат умножения дробей\n" +
                                                  "(4) Результат деления дробей \n" +
                                                  "(5) Сложить дробь\n" +
                                                  "(6) Вычесть из дроби\n" +
                                                  "(7) Умножить дробь\n" +
                                                  "(8) Поделить дробь\n" +
                                                  "(9) Сократить дробь\n" +
                                                  "(10) Информация о дроби\n" +
                                                  "(11) Изменить дроби\n" +
                                                  "(Любой символ) Выход из раздела");

                                option2 = Console.ReadLine();
                                switch (option2)
                                {
                                    case ("1"):
                                        Console.WriteLine("Результат сложения дробей: {0}/{1}", FractionStatic.FracAdd(frac1, frac2).Numerator, FractionStatic.FracAdd(frac1, frac2).Denominator);
                                        break;
                                    case ("2"):
                                        Console.WriteLine("Результат вычитания дробей: {0}/{1}", FractionStatic.FracSub(frac1, frac2).Numerator, FractionStatic.FracSub(frac1, frac2).Denominator);
                                        break;
                                    case ("3"):
                                        Console.WriteLine("Результат умножение дробей: {0}/{1}", FractionStatic.FracMult(frac1, frac2).Numerator, FractionStatic.FracMult(frac1, frac2).Denominator);
                                        break;
                                    case ("4"):
                                        Console.WriteLine("Результат деление дробей: {0}/{1}", FractionStatic.FracDiv(frac1, frac2).Numerator, FractionStatic.FracDiv(frac1, frac2).Denominator);
                                        break;
                                    case ("5"):
                                        frac1.FracAdd(frac2);
                                        Console.WriteLine("Сложение произведено. Первая дробь равна: {0}/{1}", frac1.Numerator, frac1.Denominator);
                                        break;
                                    case ("6"):
                                        frac1.FracSub(frac2);
                                        Console.WriteLine("Вычитание произведено. Первая дробь равна: {0}/{1}", frac1.Numerator, frac1.Denominator);
                                        break;
                                    case ("7"):
                                        frac1.FracMult(frac2);
                                        Console.WriteLine("Умножение произведено. Первая дробь равна: {0}/{1}", frac1.Numerator, frac1.Denominator);
                                        break;
                                    case ("8"):
                                        frac1.FracDiv(frac2);
                                        Console.WriteLine("Деление произведено. Первая дробь равна: {0}/{1}", frac1.Numerator, frac1.Denominator);
                                        break;
                                    case ("9"):
                                        Console.WriteLine("Выберите дробь.");
                                        string decision1 = Console.ReadLine();
                                        Fraction.FracReduction(decision1 == "1" ? frac1 : frac2);
                                        break;
                                    case ("10"):
                                        Console.WriteLine("Выберите дробь.");
                                        string decision2 = Console.ReadLine();
                                        Fraction.FracInf(decision2 == "1" ? frac1 : frac2);
                                        break;
                                    case ("11"):
                                        FracGen(out frac1, out frac2);
                                        break;
                                    default:
                                        runMenu2 = false;
                                        break;
                                }
                                Console.WriteLine();
                            }
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Вы вышли из программы.");
                            runMainMenu = false;
                        }
                        break;
                }
            }
            }
    }
}
