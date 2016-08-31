using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    [TestFixture]
    public class TaxFeeTest
    {

        [Test]
        public void TestEqualsReturnsTrueWhenObjectsEqual()
        {
            TaxFee taxFee = new TaxFee(0);
            TaxFee taxFee2 = taxFee;
            Assert.True(taxFee.Equals(taxFee2));
            Assert.AreEqual(taxFee.GetHashCode(), taxFee2.GetHashCode());
        }

        [Test]
        public void TestEqualsReturnsTrueWhenObjectsPropertiesEqual()
        {
            TaxFee taxFee = new TaxFee(0);
            TaxFee taxFe2 = new TaxFee(0);
            Assert.True(taxFee.Equals(taxFe2));
            Assert.AreEqual(taxFee.GetHashCode(), taxFe2.GetHashCode());
        }

        [Test]
        public void TestEqualsReturnsFalseWhenObjectsPropertiesNotEqual()
        {
            TaxFee taxFee = new TaxFee(0);
            TaxFee taxFee2 = new TaxFee(1);
            Assert.False(taxFee.Equals(taxFee2));
            Assert.AreNotEqual(taxFee.GetHashCode(), taxFee2.GetHashCode());
        }
    }
}
