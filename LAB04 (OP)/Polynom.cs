using System;

public class Polynom
{
    public double varA { get; set; }
    public double varB { get; set; }
    public double varC { get; set; }

    public Polynom()
    {
    }

    public Polynom(double varA, double varB, double varC)
    {
        this.varA = varA;
        this.varB = varB;
        this.varC = varC;
    }
    /// <summary>
    /// Метод нахожденния решений квадратного трехчлена с заданными вещественными коэффициентами a, b и c.
    /// </summary>
    public void FindSolution()
    {
        double x1, x2;
        if (Discriminant > 0)
        {
            x1 = (-(varB) - Math.Sqrt(Discriminant) / (varA * 2);
            x2 = (-(varB) + Math.Sqrt(Discriminant) / (varA * 2);
            Console.WriteLine("Уравнение имеет два решения: \nx(1) = {0}\nx(2) = {1}", x1, x2);
        }
        else if ((Discriminant == 0)
        {
            x1 = (-(varB) / (varA * 2));
            Console.WriteLine("Уравнение имеет единственное решение: \nx(1) = {0}", x1);
        }
        else
        {
            Console.WriteLine("Уравнение не имеет решений на множестве вещественных чмсел.");
        }
    }

    public double Discriminant()
    {
        return (Math.Pow(varB, 2) - 4 * (varA * varC));
    }
}
