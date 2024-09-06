using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Model;
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
    private static readonly string bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME");

    private static readonly MongoClient mongoClient;
    private static readonly IMongoDatabase database;
    private static readonly IMongoCollection<BsonDocument> categoryCollection;
    private static readonly IMongoCollection<BsonDocument> productCollection;

    private static readonly AmazonS3Client s3Client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
    static Function()
    {
        var settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConnectionString));
        settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

        mongoClient = new MongoClient(settings);
        database = mongoClient.GetDatabase(databaseName);
        categoryCollection = database.GetCollection<BsonDocument>(collectionCategory);
        productCollection = database.GetCollection<BsonDocument>(collectionProduct);
    }
        
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            var messageBody = message.Body;
            var categoryResponse = await GetCategoryByOwnerId(messageBody);
            var productResponse = await GetProductByOwnerId(messageBody);

            var catalog = await BuildEntityCatalog(categoryResponse, productResponse);

            if (catalog != null)
            {
                var document = JsonSerializer.Serialize(catalog);

                context.Logger.LogInformation(document);

                if (document != null)
                {
                    var ownerId = categoryResponse.GetValue("owner").AsString;
                    var jsonDocument = document.ToJson();
                    var key = $"{ownerId}.json";

                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        ContentBody = jsonDocument
                    };

                    await s3Client.PutObjectAsync(putRequest);
                }
                else
                {
                    context.Logger.LogInformation("Documento não encontrado para o ownerId: " + messageBody);
                }
            }
        }
    }

    private async Task<BsonDocument> GetCategoryByOwnerId(string ownerId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("owner", ownerId);

        var document = await categoryCollection.Find(filter).FirstOrDefaultAsync();
        return document;
    }
    private async Task<BsonDocument> GetProductByOwnerId(string ownerId)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("owner", ownerId);

        var document = await productCollection.Find(filter).FirstOrDefaultAsync();
        return document;
    }

    private async Task<CatalogJson> BuildEntityCatalog(BsonDocument categoryResponse, BsonDocument productResponse)
    {
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

        return new CatalogJson
        {
            Owner = productResponse.GetValue("owner").AsString,
            Catalog = listCatalog,
        };
    }
}