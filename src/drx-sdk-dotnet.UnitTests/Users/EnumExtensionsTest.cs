using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Users;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Users
{
    [TestFixture]
    public class EnumExtensionsTest
    {
        [Test]
        public void TestEnumAttribute()
        {
            Assert.AreEqual("ENDPOINTID", UserConfigOptionType.EndPointId.Value());
        }

        public enum TestEnum
        {
            [DrxEnumExtendedInformationAttribute("ONE", "ONEDESCRIPTION")]
            One,
            [DrxEnumExtendedInformationAttribute("TWO", "TWODESCRIPTION")]
            Two,
            [DrxEnumExtendedInformationAttribute("THREE", "THREEDESCRIPTION")]
            Three
        }
    }
}
