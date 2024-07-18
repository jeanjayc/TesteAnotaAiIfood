namespace TesteAnotaAiIfood.Servesless.Models
{
    public class CatalogJson
    {
        public string Owner { get; set; }
        public IEnumerable<Catalog> Catalog { get; set; }
    }
}