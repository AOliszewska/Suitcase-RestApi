using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class Item
    {

        public Item()
        {
            SuitcaseItems = new HashSet<SuitcaseItem>();
        }
        public int IdItem { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public byte IsPacked { get; set; }
        public virtual ICollection<SuitcaseItem> SuitcaseItems { get; set; }

    }
}