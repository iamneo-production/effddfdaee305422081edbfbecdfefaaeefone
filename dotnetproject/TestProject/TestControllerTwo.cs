using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetmicroservicetwo.Controllers;
using dotnetmicroservicetwo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace dotnetmicroservicetwo.Tests
{
    [TestFixture]
    public class ReviewControllerTests
    {
        private ReviewController _ReviewController;
        private ReviewDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize an in-memory database for testing
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ReviewDbContext(options);
            _context.Database.EnsureCreated(); // Create the database

            // Seed the database with sample data
            _context.Reviews.AddRange(new List<Review>
            {
                new Review { ReviewID = 1, ArticleID="1020", ReviewerName="Baskar", Comments="Old Comments",Rating=7 },
                new Review { ReviewID = 2, ArticleID="1027", ReviewerName="Clara", Comments="Bad Comments",Rating=3 },
                new Review { ReviewID = 3, ArticleID="1070", ReviewerName="Roystan", Comments="Good Comments",Rating=9 }
            });
            _context.SaveChanges();

            _ReviewController = new ReviewController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }
        [Test]
        public void ReviewClassExists()
        {
            // Arrange
            Type ReviewType = typeof(Review);

            // Act & Assert
            Assert.IsNotNull(ReviewType, "Review class not found.");
        }
        [Test]
        public void Review_Properties_ArticleID_ReturnExpectedDataTypes()
        {
            // Arrange
            Review review = new Review();
            PropertyInfo propertyInfo = review.GetType().GetProperty("ArticleID");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "ArticleID property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "ArticleID property type is not string.");
        }
[Test]
        public void Review_Properties_ReviewerName_ReturnExpectedDataTypes()
        {
            // Arrange
            Review review = new Review();
            PropertyInfo propertyInfo = review.GetType().GetProperty("ReviewerName");
            // Act & Assert
            Assert.IsNotNull(propertyInfo, "ReviewerName property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "ReviewerName property type is not string.");
        }

        [Test]
        public async Task GetAllReviews_ReturnsOkResult()
        {
            // Act
            var result = await _ReviewController.GetAllReviews();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetAllReviews_ReturnsAllReviews()
        {
            // Act
            var result = await _ReviewController.GetAllReviews();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Review>>(okResult.Value);
            var reviews = okResult.Value as IEnumerable<Review>;

            var ReviewCount = reviews.Count();
            Assert.AreEqual(3, ReviewCount); // Assuming you have 3 Reviews in the seeded data
        }


        [Test]
        public async Task AddReview_ValidData_ReturnsOkResult()
        {
            // Arrange
            var newReview = new Review
            {
 ArticleID="1020", ReviewerName="Alex", Comments="New Comments",Rating=3
            };

            // Act
            var result = await _ReviewController.AddReview(newReview);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task DeleteReview_ValidId_ReturnsNoContent()
        {
            // Arrange
              // var controller = new ReviewsController(context);

                // Act
                var result = await _ReviewController.DeleteReview(1) as NoContentResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteReview_InvalidId_ReturnsBadRequest()
        {
                   // Act
                var result = await _ReviewController.DeleteReview(0) as BadRequestObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                Assert.AreEqual("Not a valid Review id", result.Value);
        }
    }
}
