using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit.Abstractions;

namespace TesteAnotaAiIfood.Tests.IntegrationTest
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper _outputHelper;
        private readonly HttpClient _httpClient;

        public CategoryControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutput)
        {
            _factory = factory;
            _outputHelper = testOutput;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Category_GetAll_ShouldReturnAllCategorys()
        {

            //Arrange //Act
            var response = await _httpClient.GetAsync("api/Category/GetAll");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Category_GetByPK_ShouldReturnACategoryById()
        {
            //Arrange
            var pk = "231e3a24-ac5e-4f44-a34a-0b3ee37570cb";

            //Act
            var response = await _httpClient.GetAsync("api/Category/GetCategoryById/" + pk);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _outputHelper.WriteLine(pk);
        }
    }
}
