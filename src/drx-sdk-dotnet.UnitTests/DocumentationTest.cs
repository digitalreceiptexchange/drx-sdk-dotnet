using Net.Dreceiptx.Receipt;
using Net.Dreceiptx.Receipt.Config;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    /// <summary>
    /// This class contains the code that is documented within the DigitalReceiptExchange
    /// API Documentation (pptx)
    /// </summary>
    [TestFixture]
    public class DocumentationTest
    {
        [Test]
        public void TestDocumentation()
        {
            DigitalReceiptBuilder builder = new DigitalReceiptBuilder(Location.Australia);

        }
    }
}
