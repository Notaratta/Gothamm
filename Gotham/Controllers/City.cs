using Gotham.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gotham.Objects
{
    public class City
    {
        public int Width { get; }
        public int Height { get; }
        public List<Person> People { get; }
        public Prison Prison { get; }
        private Random random;

        public City(int width, int height, int numCitizens, int numThieves, int numPolice)
        {
            Width = width;
            Height = height;
            People = new List<Person>();
            Prison = new Prison(10, 10); 
            random = new Random();

            for (int i = 0; i < numCitizens; i++)
            {
                People.Add(new Citizen(i, RandomCoordinates(), RandomDirection()));
            }
            for (int i = 0; i < numThieves; i++)
            {
                People.Add(new Thief(numCitizens + i, RandomCoordinates(), RandomDirection()));
            }
            for (int i = 0; i < numPolice; i++)
            {
                People.Add(new Police(numCitizens + numThieves + i, RandomCoordinates(), RandomDirection()));
            }
        }

        private (int X, int Y) RandomCoordinates()
        {
            return (random.Next(Width), random.Next(Height));
        }

        private (int X, int Y) RandomDirection()
        {
            int[] directions = { -1, 0, 1 };
            int x, y;
            do
            {
                x = directions[random.Next(3)];
                y = directions[random.Next(3)];
            } while (x == 0 && y == 0);
            return (x, y);
        }

        public void SimulateTurn()
        {
            foreach (var person in People.Where(p => !(p is Thief thief) || !thief.IsInPrison))
            {
                person.Move();
                WrapCoordinates(person);
                Prison.SimulateTurn();

                switch (person)
                {
                    case Thief thiefPerson:
                        var citizen = FindNearbyPerson<Citizen>(thiefPerson);
                        if (citizen != null)
                        {
                            Item? stolenItem = thiefPerson.RobCitizen(citizen);
                            if (stolenItem != null) {
                                this.AnonceTheAction(thiefPerson, citizen, stolenItem);
                                Thread.Sleep(2000);
                            }
                        }
                        break;

                    case Police policePerson:
                        var nearbyThief = FindNearbyPerson<Thief>(policePerson);
                        if (nearbyThief != null && !nearbyThief.IsInPrison)
                        {
                            if (nearbyThief.StolenGoods.Count > 0)
                            {
                                policePerson.CatchThief(nearbyThief);
                                Prison.AddInmate(nearbyThief);

                                this.AnonceTheAction(nearbyThief, policePerson);
                                Thread.Sleep(2000);
                            }
                        }
                        break;
                }
            }
        }

        private void WrapCoordinates(Person person)
        {
            person.Coordinates = (
                (person.Coordinates.X + Width) % Width,
                (person.Coordinates.Y + Height) % Height
            );
        }

        private T FindNearbyPerson<T>(Person person) where T : Person
        {
            return People.OfType<T>().FirstOrDefault(p =>
                p.Coordinates.X == person.Coordinates.X &&
                p.Coordinates.Y == person.Coordinates.Y &&
                !(p is Thief thief && thief.IsInPrison));
        }

   

        public bool CityIsNotSafe()
        {
            if (this.CountThiefsInCity() > 0) 
            { 
                return true;
            }
            
            return false;
        }

        public void DisplayCity()
        {
            char[,] grid = new char[Height, Width];

            // Fill the grid with empty spaces
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    grid[y, x] = '.';
                }
            }

            // Display people
            foreach (var person in People)
            {
                if (person is Thief thief && thief.IsInPrison) continue;

                char symbol = person switch
                {
                    Citizen => 'C',
                    Thief => 'T',
                    Police => 'P',
                    _ => '?'
                };

                int x = person.Coordinates.X;
                int y = person.Coordinates.Y;

                if (x >= 0 && x < Width && y >= 0 && y < Height)
                {
                    grid[y, x] = symbol;
                }
            }

            // Print the city grid
            Console.WriteLine("City:");
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(grid[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Display the prison grid
            Console.WriteLine("Prison:");
            Prison.DisplayPrison();


            Console.WriteLine("Thiefs in City: " + this.CountThiefsInCity());
            Console.WriteLine("Thiefs in Prison: " + this.CountThiefsInPrison());
            Console.WriteLine("Citizens in City: " + this.CountCitizenssInCity());
            Console.WriteLine("Robbed Citizens in City: " + this.CountRobedCitizens());
            Console.WriteLine("Police in City: " + this.CountPolicesInCity());
            // Display legend
            Console.WriteLine("Legend: C - Citizen, T - Thief, P - Police, i - Imprisoned Thief, # - Prison Wall, . - Empty Space");
        }
    }
}