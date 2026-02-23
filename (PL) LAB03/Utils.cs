using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class Utils
    {
        private static Dictionary<string, ConsoleColor> keyColorPairs = new Dictionary<string, ConsoleColor>()
        {
            {"|RED|",    ConsoleColor.Red },
            {"|YELLOW|", ConsoleColor.Yellow },
            {"|DARKGRAY|",  ConsoleColor.DarkGray },
            {"|GRAY|",   ConsoleColor.Gray },

        };
        public static void ColoredWrite(string message)
        {
            string[] temp = message.Split(' ');
            for (int i = 0; i < temp.Length; i++)
                if (keyColorPairs.ContainsKey(temp[i]))
                    Console.ForegroundColor = keyColorPairs[temp[i]];
                else
                {
                    Console.Write(temp[i]);
                    if (i != temp.Length - 1)
                        Console.Write(' ');
                }
            Console.ResetColor();
        }
        public static void ColoredWriteLine(string message)
        {
            ColoredWrite(message);
            Console.WriteLine();
        }
    }
}
