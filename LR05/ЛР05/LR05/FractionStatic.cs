using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR05
{
    internal static class FractionStatic
    {
        // Методы, реализующие бинарные операторы (статические методы).
        public static Fraction FracAdd(Fraction frac1, Fraction frac2)
        {
            return (frac1 + frac2);
        }
        public static Fraction FracSub(Fraction frac1, Fraction frac2)
        {
            return (frac1 - frac2);
        }
        public static Fraction FracMult(Fraction frac1, Fraction frac2)
        {
            return (frac1 * frac2);
        }
        public static Fraction FracDiv(Fraction frac1, Fraction frac2)
        {
            return (frac1 / frac2);
        }
    }
}
