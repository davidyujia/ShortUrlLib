using NUnit.Framework;

namespace ShortUrlLib.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PicSeeApiGenerateTest()
        {
            var service = new ShortUrlLib.PicSeeApi();

            var shortUrl = service.Generate("https://github.com/");

            Assert.IsNotEmpty(shortUrl);
        }
    }
}