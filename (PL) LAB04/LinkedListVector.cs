using System;

namespace LAB01
{
    internal class LinkedListVector : IVectorable, ICloneable, IComparable<IVectorable>
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
            for (int i = 1; i < 5; i++)
            {
                currentNode.nextNode = new Node(0);
                currentNode = currentNode.nextNode;
            }
            Length += 5;
        }
        public LinkedListVector(int length)
        {
            Node currentNode = firstNode;
            for (int i = 0; i < length - 1; i++)
            {
                currentNode.nextNode = 0;
                currentNode = currentNode.nextNode;
            }
            Length += length - 1;
        }
        public int this[int index]
        {
            get
            {
                Node currentNode = firstNode;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.nextNode;
                if (currentNode == null)
                    throw new Exception("Не существует элемента, соответствующего данному индексу.");
                return currentNode.value;
            }
            set
            {
                Node currentNode = firstNode;
                for (int i = 0; i < index; i++)
                    currentNode = currentNode.nextNode;
                if (currentNode == null)
                    throw new Exception("Ошибка. Не существует элемента, соответствующего данному индексу.");
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
            for (int i = 0; i <= Length; i++)
                sum += Math.Pow(this[i], 2);
            return Math.Sqrt(sum);
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
        }
        public void AddToStart(int value)
        {
            Node prevFirstNode = firstNode;
            firstNode = new Node(value, prevFirstNode);

            Length++;
        }
        public void AddInBetween(int value, int index)
        {
            if (index > 1 && index < Length)
            {
                Node currentNode = firstNode;
                for (int i = 0; i < index - 1; i++)
                    currentNode = currentNode.nextNode;
                currentNode.nextNode = new Node(value, currentNode.nextNode);
            }
            else if (index == 1 || index >= Length)
            {
                if (index == 1)
                {
                    Node temp = firstNode.nextNode;
                    firstNode = new Node(value, temp);
                    Length++;
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
            Node currentNode = firstNode;
            while (currentNode != null)
            {
                vector += currentNode.value + " ";
                currentNode = currentNode.nextNode;
            }
            return "(" + vector + ")";
        }
        public override bool Equals(object obj)
        {
            if (obj is IVectorable vector)
            {
                for (int i = 0; i < Length; i++)
                    if (this[i] != vector[i])
                        return false;
                return true;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public int CompareTo(IVectorable linkedListVec)
        {
            if (this.Length == linkedListVec.Length)      return 0;
            else if (this.Length > linkedListVec.Length)  return 1;
            else                                          return -1;
        }
        public object Clone()
        {
            LinkedListVector vec = new LinkedListVector(this.Length);
            for (int i = 0; i < this.Length; i++)
                vec[i] = this[i];
            return vec;
        }
    }
}
