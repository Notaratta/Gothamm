using System;
using System.Collections.Generic;

namespace Gotham.Objects
{
    public class Prison
    {
        public int Width { get; }
        public int Height { get; }
        public List<Thief> Inmates { get; }
        private Random random;

        public Prison(int width, int height)
        {
            Width = width;
            Height = height;
            Inmates = new List<Thief>();
            random = new Random();
        }

        public void AddInmate(Thief thief)
        {
            thief.Coordinates = (
                random.Next(1, Width - 1),
                random.Next(1, Height - 1)
            );
            thief.IsInPrison = true;
            Inmates.Add(thief);
        }

        public void ReleaseInmate(Thief thief)
        {
            thief.IsInPrison = false;
            Inmates.Remove(thief);
        }
        public void SimulateTurn()
        {
            foreach (var person in Inmates)
            {
                person.Move();
                WrapCoordinates(person);
            }
        }

        private void WrapCoordinates(Person person)
        {
            person.Coordinates = (
                (person.Coordinates.X + Width) % Width,
                (person.Coordinates.Y + Height) % Height
            );
        }
        public void DisplayPrison()
        {
            char[,] grid = new char[Height, Width];

            // Fill the grid with prison walls
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                    {
                        grid[y, x] = '#';
                    }
                    else
                    {
                        grid[y, x] = '.';
                    }
                }
            }

            // Display inmates
            foreach (var inmate in Inmates)
            {
                int x = inmate.Coordinates.X;
                int y = inmate.Coordinates.Y;

                if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
                {
                    grid[y, x] = 'i';
                }
            }

            // Print the prison grid
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(grid[y, x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}