using System;
using System.Collections.Generic;

namespace Task3_FactoryMethod_PatternPrototype
{
    //Абстрактный класс фигуры
    public abstract class Shape : ICloneable
    {
        public abstract string Name { get; }
        public abstract int Cells { get; }
        public abstract object Clone();

        public void PrintInfo()
        {
            Console.WriteLine($"{Name} (Клеток: {Cells})");
        }
    }

    //Конкретные фигуры
    //Линия
    public class LineShape : Shape
    {
        public override string Name => "Линия";
        public override int Cells => 4;

        public override object Clone()
        {
            return new LineShape();
        }
    }

    //Квадрат
    public class SquareShape : Shape
    {
        public override string Name => "Квадрат";
        public override int Cells => 4;

        public override object Clone()
        {
            return new SquareShape();
        }
    }

    //Т-фигура
    public class TShape : Shape
    {
        public override string Name => "Т-фигура";
        public override int Cells => 4;

        public override object Clone()
        {
            return new TShape();
        }
    }

    //Супер-фигуры
    //Супер-линия
    public class SuperLineShape : Shape
    {
        public override string Name => "Супер-линия";
        public override int Cells => 6;

        public override object Clone()
        {
            return new SuperLineShape();
        }
    }

    //Крест
    public class CrossShape : Shape
    {
        public override string Name => "Крест";
        public override int Cells => 5;

        public override object Clone()
        {
            return new CrossShape();
        }
    }

    //Фабрика фигур
    public class ShapeFactory
    {
        private readonly List<Shape> _prototypes;
        private readonly Random _random;

        public ShapeFactory()
        {
            _random = new Random();
            _prototypes = new List<Shape>
        {
            new LineShape(),
            new SquareShape(),
            new TShape(),
            new SuperLineShape(),
            new CrossShape()
        };
        }

        public Shape CreateRandomShape()
        {
            int index = _random.Next(_prototypes.Count);
            return (Shape)_prototypes[index].Clone();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ShapeFactory factory = new ShapeFactory();

            for (int i = 0; i < 5; i++)
            {
                Shape shape = factory.CreateRandomShape();
                Shape clonedShape = (Shape)shape.Clone();

                Console.WriteLine("Оригинал:");
                shape.PrintInfo();

                Console.WriteLine("Клон:");
                clonedShape.PrintInfo();

                Console.WriteLine(new string('=', 30));
            }
        }
    }
}
