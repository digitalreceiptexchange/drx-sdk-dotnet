using Net.Dreceiptx.Client;
using Net.Dreceiptx.Receipt;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Builders;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.LineItem.Travel;
using Net.Dreceiptx.Receipt.Merchant;
using Net.Dreceiptx.Receipt.Settlement;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.Users;
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

            builder.SetMerchantReference("12345678910"); ;
            builder.SetUserGUID(UserIdentifierType.Email, "test@test.com");
            builder.AddLineItem("Brand", "Name", "Description", quantity: 5, price: 12.99m,
                tax: new Tax(taxableAmount: 12.99m, totalTax: 1.29m));

            //StandardLineItem lineItem = new StandardLineItem("Brand", "Name", "Description", quantity: 5, price: 12.99m);
            //lineItem.AddTax(new Tax(taxableAmount: 12.99m, totalTax: 1.29m));
            //builder.AddLineItem(lineItem);

            Tax otherTax = new Tax(TaxCategory.APPLICABLE, TaxCode.OtherTaxes, taxableAmount: 10.00m, totalTax: 1.00m);

            builder.AddAdminFee(20.00m, "Admin Processing Fee");
            builder.AddGeneralDiscount(20.00m, "Bulk buy discount");
            builder.AddBookingFee(20.00m, "5% Booking Fee");
            builder.AddDeliveryFee(20.00m, "Local/Metro Delivery");
            builder.AddTip(20.00m, "Included Gratuity");

            builder.AddDeliveryFee(20.00m, "Local/Metro Delivery", new Tax(20.00m, 2.00m));

            builder.AddPaymentReceipt(PaymentMethodType.Cash, 57.00m);

            /*PaymentReceipt paymentReceipt = new PaymentReceipt(PaymentMethodType.CreditCard, 57.00m);
            paymentReceipt.SettlementCurrency = Currency.AustralianDollar;
            paymentReceipt.PaymentAuthorisation = new PaymentAuthorisation("Credit Card Provider", "AuthNumbner", "AuthCode");
            builder.AddPaymentReceipt(paymentReceipt);*/

            IExchangeClient dRxClient = new DRXClient(Region.Australasia);
            builder.DryRunReceipt = true;
            dRxClient.SendDryRunReceipt(builder.BuildReceiptPostRequest());

            builder.DryRunReceipt = false;
            dRxClient.SendProductionReceipt(builder.BuildReceiptPostRequest());

            string digitalReceiptAPIJson = "";
            DigitalReceipt digitalReceipt = DigitalReceipt.FromJson(digitalReceiptAPIJson);
            string merchantName = digitalReceipt.MerchantName;
            string merchantGSTNumber = digitalReceipt.GetCompanyTaxNumber(TaxCode.GoodsAndServicesTax);

            Merchant merchant = dRxClient.LookupMerchant(digitalReceipt.MerchantLocationReference);

            decimal total = digitalReceipt.Total;
            decimal totalGST = digitalReceipt.GetTaxTotal(TaxCode.GoodsAndServicesTax);
            decimal totalCharge = digitalReceipt.ChargesTotal;
            decimal totalAllowances = digitalReceipt.AllowancesTotal;
            string purchaseOrderNumber = digitalReceipt.PurchaseOrderNumber;
            Address deliveryAddress = digitalReceipt.DeliveryAddress;
            string receiptReference = digitalReceipt.ReceiptReference;

            string userGUID = digitalReceipt.UserGUID;

            foreach (LineItem lineItem in digitalReceipt.LineItems)
            {
                string brandName = lineItem.BrandName;
                string name = lineItem.Name;
                string description = lineItem.Description;
                string note = lineItem.Note;
                string serialNumber = lineItem.SerialNumber;
                decimal price = lineItem.Price;
                decimal quantity = lineItem.Quantity;
                //decimal total = lineItem.Total;
                //decimal totalGST = lineItem.GetTaxTotal(TaxCode.GoodsAndServicesTax);

                if (lineItem.GetType() == typeof(Flight))
                {
                    //Get Flight related data
                }
                else if (lineItem.GetType() == typeof(GroundTransport))
                {
                    //Get GroundTransport related data
                }
                else
                {
                    //Get generic Interface data
                }

                if (lineItem.GetType() == typeof(GroundTransport)){
                    GroundTransport groundTransport = ((GroundTransport)lineItem);
                    GroundTransportType type = groundTransport.GetGroundTransportType();
                    //string description = groundTransport.Description;
                    System.DateTime? departureDate = groundTransport.DepartureDate;
                    System.DateTime? arrivalDate = groundTransport.ArrivalDate;
                    string serviceProvider = groundTransport.Provider;
                    string bookingNumber = groundTransport.BookingNumber;
                    string passengerName = groundTransport.PassengerName;
                    GeographicalCoordinates departureGeoLocation = groundTransport.DepartureGeoLocation;
                    GeographicalCoordinates arrivalGeoLocation = groundTransport.ArrivalGeoLocation;
                    string tripCode = groundTransport.TripCode;
                    string tripReason = groundTransport.TripReason;
                    decimal? distrance = groundTransport.TripDistance;
                }

                if (lineItem.GetType() == typeof(Flight)){
                    Flight flight = ((Flight)lineItem);
                    FlightType type = flight.GetFlightType();
                    //string description = flight.Description;
                    System.DateTime? departureDate = flight.DepartureDate;
                    FlightDestinationType? domesticOrInternationalFlight = flight.FlightDestinationType;
                    string airline = flight.Airline;
                    string ticketNumber = flight.TicketNumber;
                    string pnr = flight.PassengerNameRecord;
                    string passengerName = flight.PassengerName;
                    string itinerary = flight.Itinerary;
                }

                if (lineItem.GetType() == typeof(Accommodation)){
                    Accommodation accommodation = ((Accommodation)lineItem);
                    AccommodationType type = accommodation.GetAccommodationType();
                    //string description = accommodation.Description;
                    System.DateTime? arrivalDate = accommodation.ArrivalDate;
                    System.DateTime? departureDate = accommodation.DepartureDate;
                    string serviceProvider = accommodation.ProviderName;
                }

                if (lineItem.GetType() == typeof(Entertainment)){
                    Entertainment entertainment = ((Entertainment)lineItem);
                    if (entertainment.IncludesAlcohol.HasValue && entertainment.IncludesAlcohol == true)
                    {
                        //Includes Alcohol so determine if internal policy applies
                    }
                }



            }

            foreach (ReceiptAllowanceCharge allowance in digitalReceipt.Allowances)
            {
                string type = allowance.SettlementType.Value();
                string description = allowance.Description;
                //decimal total = allowance.Total;
                //decimal totalGST = allowance.GetTaxTotal(TaxCode.GoodsAndServicesTax);
            }

            foreach (PaymentReceipt paymentReceipt in digitalReceipt.PaymentReceipts)
            {
                string type = paymentReceipt.MethodType.Value();
                string currency = paymentReceipt.SettlementCurrency.Value();
                //decimal total = paymentReceipt.PaymentAmount;
                if (paymentReceipt.PaymentAuthorisation != null)
                {
                    string authorisationCode = paymentReceipt.PaymentAuthorisation.AuthorisationCode;
                    string authorisationNumber = paymentReceipt.PaymentAuthorisation.AuthorisationNumber;
                    string paymentProvider = paymentReceipt.PaymentAuthorisation.PaymentProvider;
                }
            }

            NewUser newUser = new NewUser("{users primary email address}");
            NewUserRegistrationResult result = dRxClient.RegisterNewUser(newUser);
            //string userGUID = result.Guid;

            newUser.AddConfigOption(UserConfigOptionType.EndPointId, "companyXXX");
            newUser.AddConfigOption(UserConfigOptionType.DryRunEndPointId, "testXXX");

            newUser.AddIdentifier(UserIdentifierType.Email, "{another user email}");
            newUser.AddIdentifier(UserIdentifierType.Mobile, "{+614*********}");
            newUser.AddIdentifier(UserIdentifierType.AccorLeClub, "{308*****************}");

        }
    }
}
