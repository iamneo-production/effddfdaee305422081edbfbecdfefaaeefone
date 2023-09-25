using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Ocelot.Tests
{
    [TestFixture]
    public class OcelotJsonTests
    {
        private JObject ocelotJson;

        [SetUp]
        public void Setup()
        {
            // Load the Ocelot.json file for testing (replace with your actual file path)
            string jsonFilePath = @"/home/coder/project/workspace/dotnetproject/dotnetapigateway/Ocelot.json";
            string jsonText = System.IO.File.ReadAllText(jsonFilePath);
            ocelotJson = JObject.Parse(jsonText);
        }

       [Test]
        public void VerifyArticleRoute()
        {
            var ArticleRoute = ocelotJson["Routes"][0];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Article"));
            Assert.That(ArticleRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("http"));
            Assert.That(ArticleRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(ArticleRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(8080));
        }

        [Test]
        public void VerifyArticleIDRoute()
        {
            var ReviewRoute = ocelotJson["Routes"][1];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Article/{id}"));
            Assert.That(ReviewRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("http"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(8080));
        }
[Test]
        public void VerifyReviewRoute()
        {
            var ReviewRoute = ocelotJson["Routes"][2];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Review"));
            Assert.That(ReviewRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("http"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(8081));
        }
                [Test]
        public void VerifyReviewNamesRoute()
        {
            var ReviewRoute = ocelotJson["Routes"][3];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Review/ReviewerNames"));
            Assert.That(ReviewRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("http"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(8081));
        }
        [Test]
        public void VerifyReviewIDRoute()
        {
            var ReviewRoute = ocelotJson["Routes"][4];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["DownstreamPathTemplate"].Value<string>(), Is.EqualTo("/api/Review/{id}"));
            Assert.That(ReviewRoute["DownstreamScheme"].Value<string>(), Is.EqualTo("http"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Host"].Value<string>(), Is.EqualTo("localhost"));
            Assert.That(ReviewRoute["DownstreamHostAndPorts"][0]["Port"].Value<int>(), Is.EqualTo(8081));
        }
        [Test]
        public void VerifyArticleRouteUpstreamPath()
        {
            var ArticleRoute = ocelotJson["Routes"][0];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Article"));
        }
        [Test]
        public void VerifyArticleIDRouteUpstreamPath()
        {
            var ArticleRoute = ocelotJson["Routes"][1];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Article/{id}"));
        }
        [Test]
        public void VerifyReviewRouteUpstreamPath()
        {
            var ArticleRoute = ocelotJson["Routes"][2];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Review"));
        }
        [Test]
        public void VerifyReviewNamesRouteUpstreamPath()
        {
            var ArticleRoute = ocelotJson["Routes"][3];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["UpstreamPathTemplate"].Value<string>(), Is.EqualTo("/gateway/Review/ReviewerNames"));
        }

        [Test]
        public void VerifyArticleRouteHttpMethods()
        {
            var ArticleRoute = ocelotJson["Routes"][0];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "POST", "GET" }));
        }
        [Test]
        public void VerifyArticleIDRouteHttpMethods()
        {
            var ArticleRoute = ocelotJson["Routes"][1];

            Assert.That(ArticleRoute, Is.Not.Null);
            Assert.That(ArticleRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "DELETE", "GET" }));
        }
        [Test]
        public void VerifyReviewRouteHttpMethods()
        {
            var ReviewRoute = ocelotJson["Routes"][2];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "POST", "GET" }));
        }
        [Test]
        public void VerifyReviewNamesRouteHttpMethods()
        {
            var ReviewRoute = ocelotJson["Routes"][3];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "GET" }));
        }
        [Test]
        public void VerifyReviewDeleteIDRouteHttpMethods()
        {
            var ReviewRoute = ocelotJson["Routes"][4];

            Assert.That(ReviewRoute, Is.Not.Null);
            Assert.That(ReviewRoute["UpstreamHttpMethod"].ToObject<string[]>(), Is.EquivalentTo(new[] { "DELETE" }));
        }
    }
}
