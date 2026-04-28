using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

abstract class Figure : IComparable<Figure>
{
    public abstract double Area();
    public abstract double Perimeter();
    public abstract void DisplayInfo();

    public int CompareTo(Figure other)
    {
        return this.Area().CompareTo(other.Area());
    }
}

class Rectangle : Figure
{
    private double width;
    private double height;

    public Rectangle(double w, double h)
    {
        width = w;
        height = h;
    }

    public override double Area() => width * height;
    public override double Perimeter() => 2 * (width + height);
    public override void DisplayInfo()
    {
        Console.WriteLine($"Rectangle: Width={width}, Height={height}, Area={Area()}, Perimeter={Perimeter()}");
    }
}

class Circle : Figure
{
    private double radius;

    public Circle(double r)
    {
        radius = r;
    }

    public override double Area() => Math.PI * radius * radius;
    public override double Perimeter() => 2 * Math.PI * radius;
    public override void DisplayInfo()
    {
        Console.WriteLine($"Circle: Radius={radius}, Area={Area():F2}, Perimeter={Perimeter():F2}");
    }
}

class Triangle : Figure
{
    private double a, b, c;

    public Triangle(double sideA, double sideB, double sideC)
    {
        a = sideA;
        b = sideB;
        c = sideC;
    }

    public override double Area()
    {
        double s = (a + b + c) / 2;
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    public override double Perimeter() => a + b + c;

    public override void DisplayInfo()
    {
        Console.WriteLine($"Triangle: Sides={a},{b},{c}, Area={Area():F2}, Perimeter={Perimeter()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Figure> figures = new List<Figure>();

        string[] lines = File.ReadAllLines("figures.txt");

        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            string type = parts[0].Trim();

            switch (type.ToLower())
            {
                case "rectangle":
                    double w = double.Parse(parts[1]);
                    double h = double.Parse(parts[2]);
                    figures.Add(new Rectangle(w, h));
                    break;
                case "circle":
                    double r = double.Parse(parts[1]);
                    figures.Add(new Circle(r));
                    break;
                case "triangle":
                    double sa = double.Parse(parts[1]);
                    double sb = double.Parse(parts[2]);
                    double sc = double.Parse(parts[3]);
                    figures.Add(new Triangle(sa, sb, sc));
                    break;
            }
        }

        figures.Sort();

        Console.WriteLine("Sorted by area:");
        foreach (var fig in figures)
        {
            fig.DisplayInfo();
        }
    }
}
