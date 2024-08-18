using System.Text.Json.Serialization;

namespace TesteAnotaAiIfood.Domain.Entities
{
    public abstract class BaseEntity
    {
        [JsonPropertyName("pk")]
        public string Pk => Id;

        [JsonPropertyName("sk")]
        public string SK => Id;
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
