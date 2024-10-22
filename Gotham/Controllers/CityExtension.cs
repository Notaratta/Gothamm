using Gotham.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Controllers
{
    public static class CityExtension
    {
        public static int CountThiefsInCity(this City city)
        {
            return city.People.OfType<Thief>().Count(thief => !thief.IsInPrison);
        }
        public static int CountThiefsInPrison(this City city)
        {
            return city.People.OfType<Thief>().Count(thief => thief.IsInPrison);
        }
        public static int CountCitizenssInCity(this City city)
        {
            return city.People.OfType<Citizen>().Count();
        }
        public static int CountPolicesInCity(this City city)
        {
            return city.People.OfType<Police>().Count();
        }
        public static int CountRobedCitizens(this City city)
        {
            return city.People.OfType<Citizen>().Count(citizen => citizen.Items.Count() < 4);
        }
        public static void AnonceTheAction(this City city, Thief thief, Citizen citizen, Item item)
        {

            Console.WriteLine($"Thief {thief.Id} stole {item.Name} from citizen {citizen.Id}");
        }
        public static void AnonceTheAction(this City city, Thief thief, Police police)
        {
            Console.WriteLine($"Thief {thief.Id} was caught by police {police.Id} and was sent to prison");
        }
    }
}
