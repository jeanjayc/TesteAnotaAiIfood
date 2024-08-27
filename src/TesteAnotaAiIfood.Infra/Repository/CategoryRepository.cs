using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MongoDB.Driver;
using System.Net;
using System.Text.Json;
using TesteAnotaAiIfood.Domain.Entities;
using TesteAnotaAiIfood.Infra.Interfaces;

namespace TesteAnotaAiIfood.Infra.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IAmazonDynamoDB _amazonDynamoDB;
        private readonly string _tableName = "category";
        public CategoryRepository(IAmazonDynamoDB amazonDynamoDB)
        {
            _amazonDynamoDB = amazonDynamoDB;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var scanRequest = new ScanRequest
            {
                TableName = _tableName,
            };

            var response = await _amazonDynamoDB.ScanAsync(scanRequest);
            var responseDesserializer = response.Items.Select(x =>
            {
                var json = Document.FromAttributeMap(x).ToJson();
                return JsonSerializer.Deserialize<Category>(json);
            });
            return responseDesserializer;
        }
        public async Task<Category> GetById(string id)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"pk", new AttributeValue{ S =  id }},
                    {"sk", new AttributeValue{ S =  id }}
                }
            };

            var response = await _amazonDynamoDB.GetItemAsync(getItemRequest);

            if (!response.Item.Any())
            {
                return null;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Item);

            return JsonSerializer.Deserialize<Category>(itemAsDocument.ToJson());
        }

        public async Task<Category> InsertCategory(Category category)
        {
            if (string.IsNullOrEmpty(category.ProductId))
            {
                category.ProductId = Guid.NewGuid().ToString(); 
            }

            var categoryAsJson = JsonSerializer.Serialize(category);

            var customerAsAttributes = new Dictionary<string, AttributeValue>()
            {
                ["pk"] = new AttributeValue { S = Guid.NewGuid().ToString() },
                ["sk"] = new AttributeValue { S = Guid.NewGuid().ToString() },
                ["Title"] = new AttributeValue { S = category.Title },
                ["Owner"] = new AttributeValue { S = category.Owner },
                ["Description"] = new AttributeValue { S = category.Description },
                ["ProductId"] = new AttributeValue { S = category.ProductId },
            };

            var createItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = customerAsAttributes
            };

            var response = await _amazonDynamoDB.PutItemAsync(createItemRequest);
            return category;
        }
        public async Task<Category> UpdateCategory(string id, Category category)
        {
            var categoryAsJson = JsonSerializer.Serialize(category);
            var customerAsAttributes = Document.FromJson(categoryAsJson).ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = customerAsAttributes
            };

            var response = await _amazonDynamoDB.PutItemAsync(createItemRequest);
            return category;
        }
        public async Task<bool> DeleteCategory(string id)
        {
            var deleteItemRequest = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "pk", new AttributeValue { S = id } },
                    { "sk", new AttributeValue { S = id } }
                }
            };
            var response = await _amazonDynamoDB.DeleteItemAsync(deleteItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
