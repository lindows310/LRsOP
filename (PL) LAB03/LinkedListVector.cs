using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class LinkedListVector : IVectorable<LinkedListVector.Node>
    {
        public class Node
        {
            public int value;
            public Node nextNode;
            public Node()
            {
                this.value = 0; nextNode = null;
            }
            public Node(int value)
            {
                this.value = value; nextNode = null;
            }
            public Node(int value, Node linkedNode)
            {
                this.value = value; nextNode = linkedNode;
            }
            public static implicit operator Node(int value)
            {
                return new Node(value);
            }
        }
        private Node firstNode = new Node(0);

        public LinkedListVector()
        {
            Node currentNode = firstNode;
            for (int i = 1; i < 5; i++)
            {
                currentNode.nextNode = new Node(0);
                currentNode = currentNode.nextNode;
            }
        }
        public LinkedListVector(int length)
        {
            Node currentNode = firstNode;
            for (int i = 0; i < length - 1; i++)
            {
                currentNode.nextNode = 0;
                currentNode = currentNode.nextNode;
            }
        }
        public Node this[int index]
        {
            get
            {
                Node currentNode = firstNode;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.nextNode;
                if (currentNode == null)
                    throw new Exception("Ошибка. Не существует элемента, соответствующего данному индексу.");
                return currentNode;
            }
            set
            {
                Node currentNode = firstNode;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.nextNode;
                if (currentNode.nextNode == null)
                    throw new Exception("Ошибка. Не существует элемента, соответствующего данному индексу.");
                currentNode.nextNode = value;
            }
        }

        public int Length
        {
            get
            {
                Node currentNode = firstNode;
                for (int i = 1; ; i++)
                {
                    currentNode = currentNode.nextNode;
                    if (currentNode.nextNode == null)
                        return i;
                }
            }
        }

        public void FillVal()
        {
            string[] temp = Console.ReadLine().Split(' ');
            for (int i = 0; i < Length; i++)
            {
                try
                {
                    this[i] = int.Parse(temp[i]);
                }
                catch (FormatException)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Неправильный формат ввода. В координату записано значение 0.", new object[] { 0, 3, ConsoleColor.Red }, new object[] { 4, 8, ConsoleColor.DarkGray });
                    this[i] = 0;
                }
                catch (IndexOutOfRangeException)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Компоненте не было происвоено значение. {i + 1}-ая координата равна 0.", new object[] { 0, 5, ConsoleColor.Red }, new object[] { 6, 9, ConsoleColor.DarkGray });
                    this[i] = 0;
                }
                catch (OverflowException)
                {
                    Utils.ColoredWriteLine($"({i + 1}) Значение, присваиваемое компоненте, не принадлежит области определения типа int. Координате присвоено значение 1", new object[] { 0, 9, ConsoleColor.Red }, new object[] { 10, 13, ConsoleColor.DarkGray });
                    this[i] = 1;
                }
            }
        }

        public double GetNorm()
        {
            double sum = 0;
            for (int i = 0; i <= Length; i++)
                sum += Math.Pow(this[i].value, 2);
            return Math.Sqrt(sum);
        }
        public void AddToEnd(int value)
        {
            Node nextNode = new Node(value);
            this[Length].nextNode = nextNode;
        }
        public void AddToStart(int value)
        {
            Node newStartNode = new Node(value, firstNode);
            firstNode = newStartNode;
        }
        public void AddInBetween(int value, int index)
        {
            if (index > 1 && index < Length)
            {
                Node tempNode = new Node(value, this[index]);
                this[index - 2].nextNode = tempNode;
            }
            else if (index == 1 || index == Length)
            {
                if (index == 1)
                {
                    Node temp = firstNode.nextNode;
                    firstNode = new Node(value, temp);
                }
                else
                    AddToEnd(value);
            }
            else
                throw new Exception("Не существует элемента, соответствующего индексу.");
        }
        public override string ToString()
        {
            string vector = " ";
            for (int i = 0; i <= Length; i++)
                vector += this[i].value + " ";
            return $"Кол-во коорд.: [{Length}]; " + "(" + vector + ")";
        }
    }
}
