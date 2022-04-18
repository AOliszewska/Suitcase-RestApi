namespace WebApplication2.Models
{
    public class SuitcaseItem
    {
        public int IdSuitcase { get; set; }
        public int IdItem { get; set; }
        public virtual Suitcase IdSuitcaseNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}