using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace ConsoleApp6
{
    public class AreaInfo
    {
        public static int sizeX { get; set; }
        public static int sizeY { get; set; }
    }

    public class Snake
    {
        public List<Vector2> cordsXY = new List<Vector2>(2);
        Random uniRand = new Random();

        public Snake()
        {
            cordsXY.Add(new Vector2(uniRand.Next(1, AreaInfo.sizeX - 2), uniRand.Next(1, AreaInfo.sizeY - 2)));
            cordsXY.Add(new Vector2(cordsXY[0].X + 1, cordsXY[0].Y));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<Vector2> varVecs = new List<Vector2>();

            Console.WriteLine("Введите размер поля:");
            string input = Console.ReadLine();

            AreaInfo.sizeX = int.Parse(input.Split(' ')[0]) + 2;
            AreaInfo.sizeY = int.Parse(input.Split(' ')[1]) + 2;
            int[,] area = new int[AreaInfo.sizeX, AreaInfo.sizeY];

            Snake snake = new Snake();
            while (true)
            {
                Vector2 pickup = new Vector2(rnd.Next(3, AreaInfo.sizeX - 2), (rnd.Next(3, AreaInfo.sizeY)) - 2);

                if (varVecs.Count < 1)
                    varVecs.Add(pickup);

                for (int i = 0; i < AreaInfo.sizeX; i++)
                {
                    for (int j = 0; j < AreaInfo.sizeY; j++)
                    {
                        Vector2 vecsBuffer = new Vector2(i, j);
                        if ((i == 0 || i == area.GetLength(0) - 1) || (j == 0 || j == area.GetLength(1) - 1))
                            Console.Write("П");
                        else if (snake.cordsXY.Contains(vecsBuffer))
                            Console.Write("О");
                        else if (varVecs.Contains(vecsBuffer))
                            Console.Write("%");
                        else
                            Console.Write('-');
                    }
                    Console.WriteLine();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                string key = keyInfo.KeyChar.ToString();
                Vector2 snCL = snake.cordsXY.Last();

                switch (key)
                {
                    case ("a"):
                            snake.cordsXY.Add(new Vector2(snCL.X, snCL.Y - 1));
                        if (!varVecs.Contains(snCL))
                            snake.cordsXY.Remove(snake.cordsXY.First());
                        else
                            varVecs.Clear();
                            break;

                    case ("d"):
                            snake.cordsXY.Add(new Vector2(snCL.X, snCL.Y + 1));
                        if (!varVecs.Contains(snCL))
                            snake.cordsXY.Remove(snake.cordsXY.First());
                        else
                            varVecs.Clear();
                            break;

                    case ("w"):
                            snake.cordsXY.Add(new Vector2(snCL.X - 1, snCL.Y));
                        if (!varVecs.Contains(snCL))
                            snake.cordsXY.Remove(snake.cordsXY.First());
                        else
                            varVecs.Clear();
                            break;

                    case ("s"):
                            snake.cordsXY.Add(new Vector2(snCL.X + 1, snCL.Y));
                        if (!varVecs.Contains(snCL))
                            snake.cordsXY.Remove(snake.cordsXY.First());
                        else
                            varVecs.Clear();
                            break;
                }
                snCL = snake.cordsXY.Last();
                if ((snCL.X == 0 || snCL.X == AreaInfo.sizeX - 1) || (snCL.Y == 0 || snCL.Y == AreaInfo.sizeY - 1) || snake.cordsXY.Distinct().Count() != snake.cordsXY.Count())
                {
                    Console.WriteLine("Вы умерли.");
                    break;
                }
                Console.Clear();
            }
        }
    }
}
