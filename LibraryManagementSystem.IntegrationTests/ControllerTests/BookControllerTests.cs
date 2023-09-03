namespace LibraryManagementSystem.IntegrationTests.ControllerTests
{
    public class BookControllerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetById_ShouldReturnOk(int bookId)
        {
            // Act
            using var response = await HttpClient.GetAsync($"/api/v1/books/{bookId}");
            var result = response.StatusCode;

            // Assert
            result.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(9)]
        public async Task GetById_ShouldReturnBookWithSpecifiedId(int bookId)
        {
            // Act
            using var response = await HttpClient.GetAsync($"/api/v1/books/{bookId}");
            var result = JsonSerializer.Deserialize<BookViewModel>(await response.Content.ReadAsStringAsync());

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(bookId);
        }

        [Theory]
        [InlineData(1500)]
        [InlineData(20000)]
        public async Task GetById_ShouldReturnNotFound(int bookId)
        {
            // Act
            using var response = await HttpClient.GetAsync($"/api/v1/books/{bookId}");
            var result = response.StatusCode;

            // Assert
            result.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public async Task GetById_ShouldReturnBadRequest(int bookId)
        {
            // Act
            using var response = await HttpClient.GetAsync($"api/v1/books/{bookId}");
            var result = response.StatusCode;

            // Assert
            result.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}