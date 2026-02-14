using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LR05
{
    public class Fraction
    {
        int numerator;
        int denominator;
        public int Denominator
        {
            get { return denominator; }
            set 
            {
                if (value == 0)
                {
                    denominator = 1;
                    Console.WriteLine("Попытка присвоения нуля в знаменатель. В знаменатель присвоена единица.");
                }
                else
                    denominator = value;
            }
        }
        public int Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }

        public Fraction()
        {
            Numerator = 1;
            Denominator = 1;
        }
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        // Переопределение бинарных операторов.
        public static Fraction operator +(Fraction frac1, Fraction frac2)
        {
            Fraction fracMin = frac1.Denominator == Math.Min(frac1.Denominator, frac2.Denominator) ? frac1 : frac2;
            Fraction fracMax = frac1.Denominator == Math.Max(frac1.Denominator, frac2.Denominator) ? frac1 : frac2;

            if (fracMax.Denominator % fracMin.Denominator == 0)
                //return new Fraction(frac1.Numerator * frac2.Denominator, frac1.Denominator * frac1.Numerator);
            
                             return new Fraction(numerator: (fracMin.Numerator * (fracMax.Denominator / fracMin.Denominator)) + fracMax.Numerator, denominator: fracMax.Denominator); 
            else
            {
                int newDenom = 1;
                bool run = true;
                for (int i = Math.Max(frac1.Denominator, frac2.Denominator); run == true; i++)
                    if (i % frac1.Denominator == 0 && i % frac2.Denominator == 0)
                    {
                        newDenom = i;
                        run = false;
                    }
                return new Fraction(numerator: fracMin.Numerator * (newDenom / fracMin.Denominator) + fracMax.Numerator * (newDenom / fracMax.Denominator), denominator: newDenom);
            }
        }
        public static Fraction operator -(Fraction frac1, Fraction frac2)
        {
            int maxDenom = Math.Max(frac1.Denominator, frac2.Denominator);

            if (maxDenom % Math.Min(frac1.Denominator, frac2.Denominator) == 0)
                return new Fraction(numerator: frac1.Numerator * (maxDenom / frac1.Denominator) - frac2.Numerator * (maxDenom / frac2.Denominator), denominator: maxDenom);
            else
            {
                int newDenom = 1;
                bool run = true;
                for (int i = Math.Max(frac1.Denominator, frac2.Denominator); run == true; i++)
                    if (i % frac1.Denominator == 0 && i % frac2.Denominator == 0)
                    {
                        newDenom = i;
                        run = false;
                    }
                return new Fraction(numerator: frac1.Numerator * (newDenom / frac1.Denominator) - frac2.Numerator * (newDenom / frac2.Denominator), denominator: newDenom);
            }
        }
        public static Fraction operator *(Fraction frac1, Fraction frac2)
        {
            return new Fraction(numerator: frac1.Numerator * frac2.Numerator, denominator: frac1.Denominator * frac2.Denominator);
        }
        public static Fraction operator /(Fraction frac1, Fraction frac2)
        {
            return new Fraction(numerator: frac1.Numerator * frac2.Denominator, denominator: frac1.Denominator * frac2.Numerator);
        }

        // Методы, реализующие бинарные операторы (методы экземпляра).
        public void FracAdd(Fraction frac2)
        {
            Fraction result = this + frac2;
            Numerator = result.Numerator;
            Denominator = result.Denominator;
        }
        public void FracSub(Fraction frac2)
        {
            Fraction result = this - frac2;
            Numerator = result.Numerator;
            Denominator = result.Denominator;
        }
        public void FracMult(Fraction frac2)
        {
            Fraction result = this * frac2;
            Numerator = result.Numerator;
            Denominator = result.Denominator;
        }
        public void FracDiv(Fraction frac2)
        {
            Fraction result = this / frac2;
            Numerator = result.Numerator;
            Denominator = result.Denominator;
        }

        // Метод сокращения дроби.
        public static void FracReduction(Fraction frac)
        {
            int divider = 1;
            for (int i = 2; i <= frac.Numerator && i <= frac.Denominator; i++)
            {
                if (frac.Numerator % i == 0 && frac.Denominator % i == 0)
                    divider = i;
            }
            frac.Numerator /= divider;
            frac.Denominator /= divider;
        }

        // Метод вывода информации о дроби.
        public static void FracInf(Fraction frac)
        {
            Console.WriteLine("Дробь равна: {0}/{1}", frac.Numerator, frac.Denominator);
        }
    }
}
