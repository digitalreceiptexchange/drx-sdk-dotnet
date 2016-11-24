using Net.Dreceiptx.Receipt.LineItem.Travel;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Receipt.LineItem.Travel
{
    [TestFixture]
    public class ClientTest
    {

        [Test]
        public void TestFlight()
        {
            Flight flight = new Flight(FlightType.COMMERCIAL, "QANTAS", "SYDLON_SHORT", "SYDLON_LONG",1, 1800);
            Assert.AreEqual("QANTAS", flight.Airline);
            Assert.AreEqual("SYDLON_SHORT", flight.Itinerary);
            Assert.AreEqual("SYDLON_LONG", flight.ItineraryDescription);
            Assert.AreEqual(1, flight.Quantity);
            Assert.AreEqual(1800, flight.Price);
        }

        [Test]
        public void TestFlightDestinationType()
        {
            Flight flight = new Flight(FlightType.COMMERCIAL, "QANTAS", "SYDLON_SHORT", "SYDLON_LONG", 1, 1800);
            flight.FlightDestinationType = FlightDestinationType.DOMESTIC;
            Assert.AreEqual(FlightDestinationType.DOMESTIC, flight.FlightDestinationType);

            flight.FlightDestinationType = FlightDestinationType.DOMESTIC;
            Assert.AreEqual(FlightDestinationType.DOMESTIC, flight.FlightDestinationType);
        }

        [Test]
        public void TestFlightDestinationTypeNull()
        {
            Flight flight = new Flight(FlightType.COMMERCIAL, "QANTAS", "SYDLON_SHORT", "SYDLON_LONG", 1, 1800);
            flight.FlightDestinationType = FlightDestinationType.DOMESTIC;
            Assert.AreEqual(FlightDestinationType.DOMESTIC, flight.FlightDestinationType);

            flight.FlightDestinationType = null;
            Assert.AreEqual(null, flight.FlightDestinationType);
        }
    }
}
