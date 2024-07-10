using Amazon.SimpleNotificationService;

namespace TesteAnotaAiIfood.Application.Interfaces
{
    public interface IAwsService
    {
        Task PublishToTopic(string message);
    }
}
