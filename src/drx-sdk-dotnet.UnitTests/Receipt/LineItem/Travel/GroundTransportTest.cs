using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Receipt.LineItem.Travel;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Receipt.LineItem.Travel
{
    [TestFixture]
    public class GroundTransportTest
    {
        [Test]
        public void TestGetGroundTransportTypeReturnsCorrectValue()
        {
            GroundTransport g = new GroundTransport(GroundTransportType.Bus, "Provider", "sdf", 12.0);
            Assert.AreEqual(GroundTransportType.Bus, g.GetGroundTransportType());


        }
    }
}
