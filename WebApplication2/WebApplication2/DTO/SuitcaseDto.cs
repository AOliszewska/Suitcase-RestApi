namespace WebApplication2.DTO
{
    public class SuitcaseDto
    {
        public int IdSuitcase { get; set; }
        public string CityName { get; set; }
        public string Name { get; set; }
        public ItemDto [] items { get; set; }
    }
}