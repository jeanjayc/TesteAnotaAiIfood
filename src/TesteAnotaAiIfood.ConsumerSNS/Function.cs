using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.SQS;
using Amazon.SQS.Model;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TesteAnotaAiIfood.Consumer;

public class Function
{
    private readonly AmazonSQSClient _sqsClient;
    private readonly string _queueUrl =Environment.GetEnvironmentVariable("SQS_QUEUE_URL");
    public Function()
    {
        _sqsClient = new AmazonSQSClient();
        _queueUrl = Environment.GetEnvironmentVariable("SQS_QUEUE_URL");
    }

    public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
    {
        foreach(var record in evnt.Records)
        {
            var message = record.Sns.Message;

            var sendMessage = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = message
            };

            await _sqsClient.SendMessageAsync(sendMessage);
        }
    }
}