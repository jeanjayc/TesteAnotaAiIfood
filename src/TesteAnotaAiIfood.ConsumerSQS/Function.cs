using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using TesteAnotaAiIfood.Domain.DTOs;
using TesteAnotaAiIfood.Servesless.Models;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TesteAnotaAiIfood.Servesless;

public class Function
{
    private static MongoClient MongoClient { get; set; }
    private static readonly string mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
    private static readonly string databaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
    private static readonly string collectionCategory = Environment.GetEnvironmentVariable("COLLECTION_CATEGORY");
    private static readonly string collectionProduct = Environment.GetEnvironmentVariable("COLLECTION_PRODUCT");
    private static readonly MongoClient mongoClient;
    private static readonly IMongoDatabase database;
    private static readonly IMongoCollection<BsonDocument> categoryCollection;
    private static readonly IMongoCollection<BsonDocument> productCollection;
    static Function()
    {
        var settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConnectionString));
        settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

        mongoClient = new MongoClient(settings);
        database = mongoClient.GetDatabase(databaseName);
        categoryCollection = database.GetCollection<BsonDocument>(collectionCategory);
        productCollection = database.GetCollection<BsonDocument>(collectionProduct);
    }

    public async Task<BsonDocument> GetCategoryByOwnerId(string ownerId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("owner", ownerId);

        var document = await categoryCollection.Find(filter).FirstOrDefaultAsync();
        return document;
    }
    public async Task<BsonDocument> GetProductByOwnerId(string ownerId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("owner", ownerId);

        var document = await productCollection.Find(filter).FirstOrDefaultAsync();
        return document;
    }

    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            var messageBody = message.Body;
            var categoryResponse = await GetCategoryByOwnerId(messageBody);
            var productResponse = await GetProductByOwnerId(messageBody);

            var listProduct = new List<ProductDTO>();
            var listCatalog = new List<Catalog>();

            var product = new ProductDTO
                (
                    productResponse.GetValue("title").AsString,
                    productResponse.GetValue("owner").AsString,
                    productResponse.GetValue("price").AsString,
                    productResponse.GetValue("description").AsString
                );

            listProduct.Add(product);

            var catalog = new Catalog
            {
                Owner = categoryResponse.GetValue("owner").AsString,
                CategoryTitle = categoryResponse.GetValue("title").AsString,
                CategoryDescription = categoryResponse.GetValue("description").AsString,
                Itens = listProduct
            };

            listCatalog.Add(catalog);

            var catalogResult = new CatalogJson
            {
                Owner = productResponse.GetValue("owner").AsString,
                Catalog = listCatalog,
            };

            if(catalog != null)
            {
                var result = JsonSerializer.Serialize(catalogResult);

                context.Logger.LogInformation(result);
            }
            else
            {
                context.Logger.LogInformation("Documento não encontrado para o ownerId: " + messageBody);
            }
        }
    }
}