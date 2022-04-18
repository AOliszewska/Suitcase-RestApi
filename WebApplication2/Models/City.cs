using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class City
    {
        public City()
        {
            Suitcases = new HashSet<Suitcase>();
        }
        public int IdCity { get; set; }
        public int IdCountry { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Suitcase> Suitcases { get; set; }
        public virtual Country IdCountryNavigation { get; set; }
    }
}