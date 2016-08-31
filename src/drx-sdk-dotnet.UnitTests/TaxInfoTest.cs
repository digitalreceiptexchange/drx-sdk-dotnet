using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    [TestFixture]
    public class TaxInfoTest
    {
        [Test]
        public void TestEqualsReturnsTrueWhenObjectsEqual()
        {
            TaxInfo taxInfo = new TaxInfo(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 0);
            TaxInfo taxInfo2 = taxInfo;
            Assert.True(taxInfo.Equals(taxInfo2));
            Assert.AreEqual(taxInfo.GetHashCode(), taxInfo2.GetHashCode());
        }

        [Test]
        public void TestEqualsReturnsTrueWhenObjectsPropertiesEqual()
        {
            TaxInfo taxInfo = new TaxInfo(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 0);
            TaxInfo taxInfo2 = new TaxInfo(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 0);
            Assert.True(taxInfo.Equals(taxInfo2));
            Assert.AreEqual(taxInfo.GetHashCode(), taxInfo2.GetHashCode());
        }

        [TestCase(TaxCategory.APPLICABLE, TaxCode.AAD, 0, 0, 0, Result=false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAF, 0, 0, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAD, 1, 0, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAD, 0, 1, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 1, Result = false)]
        public bool TestEqualsReturnsFalseWhenObjectsPropertiesNotEqual(TaxCategory taxCategory, TaxCode taxCode, decimal taxableAmount, 
            decimal percentage, decimal totalTax)
        {
            TaxInfo taxInfo = new TaxInfo(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 0);
            TaxInfo taxInfo2 = new TaxInfo(taxCategory, taxCode, taxableAmount,percentage,totalTax);
            return taxInfo.Equals(taxInfo2);
        }

        [TestCase(TaxCategory.APPLICABLE, TaxCode.AAD, 0, 0, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAF, 0, 0, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAD, 1, 0, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAD, 0, 1, 0, Result = false)]
        [TestCase(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 1, Result = false)]
        public bool TestGetHasCodeNotEqualForObjectsWithDifferentProperties(TaxCategory taxCategory, TaxCode taxCode, decimal taxableAmount,
            decimal percentage, decimal totalTax)
        {
            TaxInfo taxInfo = new TaxInfo(TaxCategory.EXEMPT, TaxCode.AAD, 0, 0, 0);
            TaxInfo taxInfo2 = new TaxInfo(taxCategory, taxCode, taxableAmount, percentage, totalTax);
            return taxInfo.GetHashCode() == taxInfo2.GetHashCode();
        }
    }
}
