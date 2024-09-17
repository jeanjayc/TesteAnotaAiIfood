using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace TesteAnotaAiIfood.Tests.IntegrationTest
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CategoryControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Category_GetAll_ShouldReturnAllCategorys()
        {
            //Arrange
            var _client = _factory.CreateClient();

            //Act
            var response = await _client.GetAsync("api/Category/GetAll");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
