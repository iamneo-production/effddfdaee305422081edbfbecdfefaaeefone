using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetmicroserviceone.Controllers;
using dotnetmicroserviceone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace dotnetmicroserviceone.Tests
{
    [TestFixture]
    public class ArticleControllerTests
    {
        private ArticleController _ArticleController;
        private ArticleDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize an in-memory database for testing
            var options = new DbContextOptionsBuilder<ArticleDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ArticleDbContext(options);
            _context.Database.EnsureCreated(); // Create the database

            // Seed the database with sample data
            _context.Articles.AddRange(new List<Article>
            {
                new Article { ArticleID = 1, Title="One Title", Author="One Author", Content="One huge Content",PublicationDate=DateTime.Parse("2023-02-02") },
                new Article { ArticleID = 2, Title="Two Title", Author="Two Author", Content="two huge Content",PublicationDate=DateTime.Parse("2023-04-02") },
                new Article { ArticleID = 3, Title="Three Title", Author="Three Author", Content="Three huge Content",PublicationDate=DateTime.Parse("2023-06-02") },
            });
            _context.SaveChanges();

            _ArticleController = new ArticleController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }
        [Test]
        public void ArticleClassExists()
        {
            // Arrange
            Type ArticleType = typeof(Article);

            // Act & Assert
            Assert.IsNotNull(ArticleType, "Article class not found.");
        }
        [Test]
        public void Article_Properties_Title_ReturnExpectedDataTypes()
        {
            // Arrange
            Article article = new Article();
            PropertyInfo propertyInfo = article.GetType().GetProperty("Title");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Title property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Title property type is not string.");
        }
[Test]
        public void Article_Properties_Author_ReturnExpectedDataTypes()
        {
            // Arrange
            Article article = new Article();
            PropertyInfo propertyInfo = article.GetType().GetProperty("Author");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Author property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Author property type is not string.");
        }
        [Test]
        public void Article_Properties_Content_ReturnExpectedDataTypes()
        {
            // Arrange
            Article article = new Article();
            PropertyInfo propertyInfo = article.GetType().GetProperty("Content");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Content property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Content property type is not string.");
        }

        [Test]
        public async Task GetAllArticles_ReturnsOkResult()
        {
            // Act
            var result = await _ArticleController.GetAllArticles();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetAllArticles_ReturnsAllArticles()
        {
            // Act
            var result = await _ArticleController.GetAllArticles();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Article>>(okResult.Value);
            var articles = okResult.Value as IEnumerable<Article>;

            var ArticleCount = articles.Count();
            Assert.AreEqual(3, ArticleCount); // Assuming you have 3 Articles in the seeded data
        }

        [Test]
        public async Task GetArticleById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _ArticleController.GetArticleById(existingId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetArticleById_ExistingId_ReturnsArticle()
        {
            // Arrange
            var existingId = 1;

            // Act
            var result = await _ArticleController.GetArticleById(existingId);

            // Assert
            Assert.IsNotNull(result);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            var article = okResult.Value as Article;
            Assert.IsNotNull(article);
            Assert.AreEqual(existingId, article.ArticleID);
        }

        [Test]
        public async Task GetArticleById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingId = 99; // Assuming this ID does not exist in the seeded data

            // Act
            var result = await _ArticleController.GetArticleById(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task AddArticle_ValidData_ReturnsOkResult()
        {
            // Arrange
            var newArticle = new Article
            {
Title="New Title", Author="New Author", Content="new huge Content",PublicationDate=DateTime.Parse("2023-02-02")
            };

            // Act
            var result = await _ArticleController.AddArticle(newArticle);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task DeleteArticle_ValidId_ReturnsNoContent()
        {
            // Arrange
              // var controller = new ArticlesController(context);

                // Act
                var result = await _ArticleController.DeleteArticle(1) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteArticle_InvalidId_ReturnsBadRequest()
        {
                   // Act
                var result = await _ArticleController.DeleteArticle(0) as BadRequestObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                Assert.AreEqual("Not a valid Article id", result.Value);
        }
    }
}
