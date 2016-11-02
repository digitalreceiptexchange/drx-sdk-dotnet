
using System;
using Net.Dreceiptx.Receipt.Common;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Receipt.Common
{
    [TestFixture]
    public class CurrencyTest
    {
        [Test]
        public void TestCurrencyLookup()
        {
            foreach (Currency currency in Enum.GetValues(typeof(Currency)))
            {
                Currency result = EnumExtensions.Currency(currency.Value());
                Assert.AreEqual(currency, result);
            }
        }

        [Test]
        public void TestInvalidCurrencyThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => EnumExtensions.Currency("DUMMY"));
        }
    }
}
