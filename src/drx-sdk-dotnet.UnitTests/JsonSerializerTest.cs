using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Receipt.Serialization.Json;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    [TestFixture]
    public class JsonSerializerTest
    {
        [Test]
        public void TestDateTimeFormatting()
        {
            TestType test = new TestType();
            test.TestDateTime = DateTime.Now;
            string result = JsonSerializer.SerializeToString(test);
            Console.WriteLine(result);
        }

        public class TestType
        {
            public DateTime TestDateTime { get; set; }
        }
    }
}
