using LAB02;
using System;

namespace LAB02
{
    internal class LinkedListVector
    {
        public class Node
        {
            public int value; public Node nextNode;
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
            Length = 1;
            for (int i = 1; i < 5; i++)
            {
                currentNode.nextNode = new Node(0);
                currentNode = currentNode.nextNode;
                Length++;
            }
        }
        public LinkedListVector(int length)
        {
            Node currentNode = firstNode;
            Length = 1;
            for (int i = 0; i < length - 1; i++)
            {
                currentNode.nextNode = 0;
                currentNode = currentNode.nextNode;
                Length++;
            }
        }
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new Exception("Не существует элемента, соответствующего данному индексу.");

                Node currentNode = firstNode;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.nextNode;
                return currentNode.value;
            }
            set
            {
                if (index < 0 || index >= Length)
                    throw new Exception("Не существует элемента, соответствующего данному индексу.");

                Node currentNode = firstNode;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.nextNode;
                currentNode.value = value;
            }
        }

        public int Length { get; private set; }

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
                    Utils.ColoredWriteLine($"|RED| ({i + 1}) Неправильный формат ввода. |DARKGRAY| В координату записано значение 0.");
                    this[i] = 0;
                }
                catch (IndexOutOfRangeException)
                {
                    Utils.ColoredWriteLine($"|RED| ({i + 1}) Компоненте не было происвоено значение. |DARKGRAY| {i + 1}-ая координата равна 0.");
                    this[i] = 0;
                }
                catch (OverflowException)
                {
                    Utils.ColoredWriteLine($"|RED| ({i + 1}) Значение, присваиваемое компоненте, не принадлежит области определения типа int. |DARKGRAY| Координате присвоено значение 1");
                    this[i] = 1;
                }
            }
        }
        public double GetNorm()
        {
            double sum = 0;
            for (int i = 0; i < Length; i++)
                sum += Math.Pow(this[i], 2);
            return Math.Sqrt(sum);
        }
        public void DeleteFromEnd()
        {
            if (Length == 0)
                throw new Exception("Список пуст.");
            if (Length == 1)
            {
                firstNode = null;
                Length--;
                return;
            }
            Node currentNode = firstNode;
            while (currentNode.nextNode.nextNode != null)
                currentNode = currentNode.nextNode;

            currentNode.nextNode = null;
            Length--;
        }
        public void AddToEnd(int value)
        {
            Node currentNode = firstNode;
            while (currentNode.nextNode != null)
                currentNode = currentNode.nextNode;
            currentNode.nextNode = new Node(value);

            Length++;
        }
        public void DeleteFromStart()
        {
            firstNode = firstNode.nextNode;

            Length--;
        }
        public void AddToStart(int value)
        {
            Node prevFirstNode = firstNode;
            firstNode = new Node(value, prevFirstNode);

            Length++;
        }
        public void AddInBetween(int value, int index)
        {
            if (index < 1 || index > Length + 1)
            {
                throw new Exception("Не существует элемента, соответствующего индексу.");
            }
            if (index == 1)
            {
                AddToStart(value);
            }
            else if (index == Length + 1)
            {
                AddToEnd(value);
            }
            else
            {
                Node currentNode = firstNode;

                for (int i = 1; i < index - 1; i++)
                    currentNode = currentNode.nextNode;
                currentNode.nextNode = new Node(value, currentNode.nextNode);

                Length++;
            }
        }
        public void DeleteFromBetween(int index)
        {
            if (index < 1 || index > Length + 1)
            {
                throw new Exception("Не существует элемента, соответствующего индексу.");
            }
            if (index == 1)
            {
                DeleteFromStart();
            }
            else if (index == Length + 1)
            {
                DeleteFromEnd();
            }
            else
            {
                Node currentNode = firstNode;

                for (int i = 1; i < index - 1; i++)
                    currentNode = currentNode.nextNode;

                Node temp = currentNode.nextNode.nextNode;
                currentNode.nextNode = null;
                currentNode.nextNode = temp;

                Length--;
            }
        }
        public override string ToString()
        {
            string vector = " ";
            Node currentNode = firstNode;
            while (currentNode != null)
            {
                vector += currentNode.value + " ";
                currentNode = currentNode.nextNode;
            }
            return "(" + vector + ")";
        }
    }
}

