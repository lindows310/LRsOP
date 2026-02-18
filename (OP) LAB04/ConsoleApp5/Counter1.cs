using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Counter
    {
        public int minVal { get; set; }
        public int maxVal { get; set; }
        public int curVal { get; set; }

        public Counter()
        {
        }

        public Counter(int maxValue, int minValue, int currentValue)
        {
            this.curVal = currentValue;
            this.minVal = minValue;
            this.maxVal = maxValue;
        }

        /// <summary>
        /// Добавляет единицу к десятичному счетчику.
        /// </summary>
        public void Addition()
        {
            if (IsUpLimited())
                curVal = minVal;
            else
                curVal += 1;
        }

        /// <summary>
        /// Вычитает единицу из десятичного счетчика.
        /// </summary>
        public void Difference()
        {
            if (IsDownLimited())
                curVal = maxVal;
            else
                curVal -= 1;
        }

        public void ShowCounter()
        {
            Console.WriteLine("Текущее значение счетчика: {0}. Нажмите Enter, чтобы продолжить", curVal);
            Console.ReadLine();
        }

        /// <summary>
        /// Проверяет достижение счетчиком его предельных значений.
        /// </summary>
        private bool IsUpLimited()
        {
            bool status;
            if (curVal == maxVal)
                status = true;
            else
                status = false;
            return status;
        }

        private bool IsDownLimited()
        {
            bool status;
            if (curVal == minVal)
                status = true;
            else
                status = false;
            return status;
        }
    }
}
