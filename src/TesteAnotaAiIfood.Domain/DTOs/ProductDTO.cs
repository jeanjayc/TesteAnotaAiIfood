namespace TesteAnotaAiIfood.Domain.DTOs
{
    public record ProductDTO
        (
            string Title,
            string Owner,
            string? CategoryId,
            decimal Price,
            string Description
        );
}
