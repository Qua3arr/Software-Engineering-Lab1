using System;
using System.Collections.Generic;

namespace Task4_SingletonPattern
{
    internal class ElevatorInTheBuilding
    {
        //Класс помещения
        public class Room
        {
            public string Name { get; set; }

            public Room(string name)
            {
                Name = name;
            }
        }

        //Класс этажа
        public class Floor
        {
            public int Number { get; set; }
            public List<Room> Rooms { get; set; }

            public Floor(int number, int roomsCount)
            {
                Number = number;
                Rooms = new List<Room>();

                for (int i = 1; i <= roomsCount; i++)
                {
                    Rooms.Add(new Room($"Комната {number}-{i}"));
                }
            }
        }

        //Singleton класс Здания
        public class Building
        {
            private static Building instance;

            public List<Floor> Floors { get; set; }
            public string Name { get; set; }

            private Building(int floorsCount, int roomsPerFloor)
            {
                Name = "Многоэтажное здание";
                Floors = new List<Floor>();

                for (int i = 1; i <= floorsCount; i++)
                {
                    Floors.Add(new Floor(i, roomsPerFloor));
                }

                Console.WriteLine("Создано здание с {0} этажами", floorsCount);
            }

            public static Building GetInstance(int floorsCount, int roomsPerFloor)
            {
                if (instance == null)
                    instance = new Building(floorsCount, roomsPerFloor);
                return instance;
            }

            public void DisplayInfo()
            {
                Console.WriteLine("\n=== {0} ===", Name);
                Console.WriteLine("Этажей: {0}", Floors.Count);
                foreach (var floor in Floors)
                {
                    Console.WriteLine("Этаж {0}: {1} помещений", floor.Number, floor.Rooms.Count);
                }
            }
        }

        //Singleton класс Лифта
        public class Elevator
        {
            private static Elevator instance;

            public int CurrentFloor { get; private set; }
            public bool IsMoving { get; private set; }

            private Elevator()
            {
                CurrentFloor = 1;
                IsMoving = false;
                Console.WriteLine("\nЛифт создан и находится на 1 этаже");
            }

            public static Elevator GetInstance()
            {
                if (instance == null)
                    instance = new Elevator();
                return instance;
            }

            public void MoveToFloor(int targetFloor)
            {
                if (IsMoving)
                {
                    Console.WriteLine("Лифт уже движется!");
                    return;
                }

                IsMoving = true;
                Console.WriteLine("Лифт едет с {0} этажа на {1} этаж...", CurrentFloor, targetFloor);

                // Имитация движения
                System.Threading.Thread.Sleep(1000);

                CurrentFloor = targetFloor;
                IsMoving = false;
                Console.WriteLine("Лифт прибыл на {0} этаж", CurrentFloor);
            }
        }

        static void Main(string[] args)
        {
            // Создаем здание
            Building building = Building.GetInstance(5, 3); // 5 этажей, 3 помещения на этаж
            building.DisplayInfo();

            // Работаем с лифтом
            Elevator elevator = Elevator.GetInstance();
            elevator.MoveToFloor(3);
            elevator.MoveToFloor(1);

            // Пытаемся создать еще одно здание (не получится)
            Building anotherBuilding = Building.GetInstance(10, 5);
            anotherBuilding.DisplayInfo(); // Параметры игнорируются, выводится первое здание
        }
    }
}
