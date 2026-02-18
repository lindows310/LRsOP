using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class LinkedListVector
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
        
        public int Lenght
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

        public double GetNorm()
        {
            double sum = 0;
            for (int i = 0; i <= Lenght; i++)
                sum += Math.Pow(this[i].value, 2);
            return Math.Sqrt(sum);
        }
        public void AddToEnd(int value)
        {
            Node nextNode = new Node(value);
            this[Lenght].nextNode = nextNode;
        }
        public void AddToStart(int value)
        {
            Node newStartNode = new Node(value, firstNode);
            firstNode = newStartNode;
        }
        public void AddInBetween(int value, int index)
        {
            if (index > 1 && index < Lenght)
            {
                Node tempNode = new Node(value, this[index]);
                this[index - 2].nextNode = tempNode;
            }
            else if (index == 1 || index == Lenght)
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
                throw new Exception("Ошибка. Не существует элемента, соответствующего индексу.");
        }
        public override string ToString()
        {
            string vector = " ";
            for (int i = 0; i <= Lenght; i++)
                vector += this[i].value + " ";
            return vector = "(" + vector + ")";
        }
    }
}
