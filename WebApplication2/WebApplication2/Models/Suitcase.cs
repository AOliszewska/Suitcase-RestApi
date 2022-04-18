using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class Suitcase
    {
        public Suitcase()
        {
            SuitcaseItems = new HashSet<SuitcaseItem>();
        }
        public int IdSuitcase { get; set; }
        public int IdUser { get; set; }
        public int IdCity { get; set; }
        public string Name { get; set; }
        
        public virtual City IdCityNavigation { get; set; }
        public virtual UserVacation IdUserNavigation { get; set; }
        public virtual ICollection<SuitcaseItem> SuitcaseItems { get; set; }
    }
    }
