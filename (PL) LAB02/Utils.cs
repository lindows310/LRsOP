using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class Utils
    {
        public static void ColoredWrite(string message, params object[] info)
        {
            string[] words = message.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                bool printed = false;
                foreach (object[] o in info)
                {
                    int wordStart = (int)o[0];
                    int wordEnd = (int)o[1];
                    ConsoleColor color = (ConsoleColor)o[2];

                    if (wordStart <= i && wordEnd >= i)
                    {
                        Console.ForegroundColor = color;
                        Console.Write(words[i]);

                        if (i != words.Length - 1)
                            Console.Write(' ');

                        Console.ResetColor();
                        printed = true;
                    }
                }
                if (!printed)
                {
                    Console.Write(words[i]);
                    if (i != words.Length - 1)
                        Console.Write(' ');
                }
            }
        }
        public static void ColoredWriteLine(string message, params object[] info)
        {
            ColoredWrite(message, info);
            Console.WriteLine();
        }
    }
}
