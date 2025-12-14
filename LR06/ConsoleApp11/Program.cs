using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа No 6. Выполнил студент группы 6101-020302D Абросимов Артём");
            Console.Write("Введите строку: ");

            string str = Console.ReadLine();
            StringAnalyzer analyzer = new StringAnalyzer(str);

            bool run;
            run = true;

            while (run)
            {
                string input;

                Console.WriteLine("\nВведите опцию:\n" +
                                  "(1) Вывести информацию о количестве символов; подсчитать среднюю длину слова\n" +
                                  "(2) Заменить вхождения указанного слова новым словом\n" +
                                  "(3) Подсчитать количество вхождений подстроки\n" +
                                  "(4) Проверка на палиндром\n" +
                                  "(5) Проверка на дату\n" +
                                  "(6) Вывести текущую строку\n" +
                                  "(любой символ) Выход из программы");
                input = Console.ReadLine();
                switch(input)
                {
                    case ("1"):
                        Console.WriteLine($"Количество букв: {analyzer.LetCount}\n" +
                                          $"Количество цифр: {analyzer.NumCount}\n" +
                                          $"Средняя длина слова: {analyzer.AverageLength}");
                        break;
                    case ("2"):
                        Console.WriteLine("Введите слово, которое хотите заменить, и слово, которым вы хотите заменить вхождения:");
                        string[] inp = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        analyzer.ReplaceWith(inp[0], inp[1]);
                        break;
                    case ("3"):
                        Console.WriteLine("Введите подстроку, количество вхождений которой вы хотите высчитать:");
                        string userInp = Console.ReadLine();
                        Console.WriteLine($"Количество вхождений указанной подстроки: {analyzer.SubstringEntries(userInp)}");
                        break;
                    case ("4"):
                        string isPalindrom = analyzer.PalindromCheck() == true ? "Строка является палиндромом" : "Строка не является палиндромом";
                        Console.WriteLine(isPalindrom);
                        break;
                    case ("5"):
                        string isDate = analyzer.DateCheck() == true ? "Строка является датой" : "Строка не является датой";
                        Console.WriteLine(isDate);
                        break;
                    case ("6"):
                        Console.WriteLine($"Текущая строка:\n{analyzer.Str}");
                        break;
                    default:
                        Console.WriteLine("Вы вышли из программы.");
                        run = false;
                        break;
                }
            }
        }
    }
}
