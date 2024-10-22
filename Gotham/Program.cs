using System;
using System.Threading;
using Gotham.Objects;

class Program
{
    static void Main(string[] args)
    {
        City gotham = new City(120, 20, 30, 20, 20);
        int turns = 100;
        int counter = 0;

        //for (int i = 0; i < turns; i++)
        //{
        //    Console.Clear();
        //    Console.WriteLine($"Turn {i + 1}");
        //    gotham.DisplayCity();
        //    gotham.SimulateTurn();
        //    //Thread.Sleep(500); // Pause for half a second between turns
        //}

        do
        {
            Console.Clear();
            Console.WriteLine($"Turn {counter += 1}");
            gotham.DisplayCity();
            gotham.SimulateTurn();
            Thread.Sleep(500); // Pause for half a second between turns
        } while (gotham.CityIsNotSafe() && counter <= 1000);


        Console.WriteLine("Simulation ended. Final statistics:");
        foreach (var person in gotham.People)
        {
            if (person is Citizen citizen)
            {
                Console.WriteLine($"Citizen {citizen.Id} has {citizen.Items.Count} items left.");
            }
            else if (person is Thief thief)
            {
                Console.WriteLine($"Thief {thief.Id} has stolen {thief.StolenGoods.Count} items. In prison: {thief.IsInPrison}");
            }
            else if (person is Police police)
            {
                Console.WriteLine($"Police {police.Id} has confiscated {police.ConfiscatedItems.Count} items.");
            }
        }
        Console.WriteLine($"Inmates in prison: {gotham.Prison.Inmates.Count}");
    }
}