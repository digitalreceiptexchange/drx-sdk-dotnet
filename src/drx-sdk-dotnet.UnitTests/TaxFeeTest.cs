#region copyright
// Copyright 2016 Digital Receipt Exchange Limited
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion
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
