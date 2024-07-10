using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using TesteAnotaAiIfood.Application.Interfaces;

namespace TesteAnotaAiIfood.Application.Services
{
    public class AwsService : IAwsService
    {
        private IAmazonSimpleNotificationService _client;
        private readonly IConfiguration _configuration;

        public AwsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task PublishToTopic(string message)
        {
            _client = new AmazonSimpleNotificationServiceClient(region: Amazon.RegionEndpoint.USEast1, 
                credentials: new Amazon.Runtime.BasicAWSCredentials(_configuration["AwsConfig:AcessKey"], _configuration["AwsConfig:SecretKey"]));

            var topicArn = _configuration.GetSection("AwsConfig").GetSection("ARNTopic").Value;

            var request = new PublishRequest
            {
                TopicArn = topicArn,
                Message = message
            };

            var response = await _client.PublishAsync(request);
        }
    }
}
