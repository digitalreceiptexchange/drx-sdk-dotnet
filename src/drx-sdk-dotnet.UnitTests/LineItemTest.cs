using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    /// <summary>
    /// Class for testing the LineItem class
    /// </summary>
    [TestFixture]
    public class LineItemTest
    {
        /// <summary>
        /// Test to ensure the properties of a LineItem instance
        /// are correctly set after creating an instance.
        /// </summary>
        [Test]
        public void TestCreationOfLineItemSetsPropertiesCorrectly()
        {
            LineItem lineItem = new LineItem(10, 2.2m, 3);
            Assert.AreEqual(10, lineItem.Quantity);
            Assert.AreEqual(2.2m, lineItem.Price);
            Assert.AreEqual(3, lineItem.Total);
        }

        /// <summary>
        /// Test to ensure the AllowancesTotal property on the LineItem
        /// returns the correct amount when ading general discounts
        /// to the LineItem instance.
        /// </summary>
        [Test]
        public void TestAllowancesTotalReturnsCorrectResultWhenAddingGeneralDiscounts()
        {
            LineItem lineItem = new LineItem(10, 2.2m, 3);
            lineItem.AddGeneralDiscount(5m, "Test General Discount", null);
            Assert.AreEqual(5, lineItem.AllowancesTotal);

            lineItem.AddGeneralDiscount(6m, "Test General Discount2", null);
            Assert.AreEqual(11, lineItem.AllowancesTotal);
        }

        /// <summary>
        /// Test to ensure the AllowancesTotal property on the LineItem
        /// returns the correct amount when adding multibuy discounts
        /// to the LineItem instance.
        /// </summary>
        [Test]
        public void TestAllowancesTotalReturnsCorrectResultWhenAddingMultibuyDiscounts()
        {
            LineItem lineItem = new LineItem(10, 2.2m, 3);
            lineItem.AddMultibuyDiscount(5m, "Test multibuy Discount", null);
            Assert.AreEqual(5, lineItem.AllowancesTotal);

            lineItem.AddMultibuyDiscount(6m, "Test multibuy Discount2", null);
            Assert.AreEqual(11, lineItem.AllowancesTotal);
        }


        /// <summary>
        /// Test to ensure the AllowancesTotal property on the LineItem
        /// returns the correct amount when adding multibuy and general
        /// discounts to the LineItem instance.
        /// </summary>
        [Test]
        public void TestAllowancesTotalReturnsCorrectResultWhenAddingMultibuyDiscountsAndGeneralDiscounts()
        {
            LineItem lineItem = new LineItem(10, 2.2m, 3);
            lineItem.AddMultibuyDiscount(5m, "Test multibuy Discount", null);
            Assert.AreEqual(5, lineItem.AllowancesTotal);

            lineItem.AddMultibuyDiscount(6m, "Test multibuy Discount2", null);
            Assert.AreEqual(11, lineItem.AllowancesTotal);

            lineItem.AddMultibuyDiscount(5m, "Test multibuy Discount", null);
            Assert.AreEqual(16, lineItem.AllowancesTotal);

            lineItem.AddMultibuyDiscount(6m, "Test multibuy Discount2", null);
            Assert.AreEqual(22, lineItem.AllowancesTotal);
        }
    }
}