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

using Net.Dreceiptx.Receipt.LineItem;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    /// <summary>
    /// Class for testing the LineItem class
    /// </summary>
    [TestFixture]
    public class LineItemTest
    {
        ///// <summary>
        ///// Test to ensure the properties of a LineItem instance
        ///// are correctly set after creating an instance.
        ///// </summary>
        //[Test]
        //public void TestCreationOfLineItemSetsPropertiesCorrectly()
        //{
        //    LineItem lineItem = new LineItem(10, 2.2m, 3);
        //    Assert.AreEqual(10, lineItem.Quantity);
        //    Assert.AreEqual(2.2m, lineItem.Price);
        //    Assert.AreEqual(3, lineItem.Total);
        //}

        ///// <summary>
        ///// Test to ensure the AllowancesTotal property on the LineItem
        ///// returns the correct amount when ading general discounts
        ///// to the LineItem instance.
        ///// </summary>
        //[Test]
        //public void TestAllowancesTotalReturnsCorrectResultWhenAddingGeneralDiscounts()
        //{
        //    LineItem lineItem = new LineItem("Brand", "Name", "Description", 10, 2.2m);
        //    lineItem.AddGeneralDiscount(5m, "Test General Discount", null);
        //    Assert.AreEqual(5, lineItem.AllowancesTotal);

        //    lineItem.AddGeneralDiscount(6m, "Test General Discount2", null);
        //    Assert.AreEqual(11, lineItem.AllowancesTotal);
        //}

        ///// <summary>
        ///// Test to ensure the AllowancesTotal property on the LineItem
        ///// returns the correct amount when adding multibuy discounts
        ///// to the LineItem instance.
        ///// </summary>
        //[Test]
        //public void TestAllowancesTotalReturnsCorrectResultWhenAddingMultibuyDiscounts()
        //{
        //    LineItem lineItem = new LineItem(10, 2.2m, 3);
        //    lineItem.AddMultibuyDiscount(5m, "Test multibuy Discount", null);
        //    Assert.AreEqual(5, lineItem.AllowancesTotal);

        //    lineItem.AddMultibuyDiscount(6m, "Test multibuy Discount2", null);
        //    Assert.AreEqual(11, lineItem.AllowancesTotal);
        //}


        ///// <summary>
        ///// Test to ensure the AllowancesTotal property on the LineItem
        ///// returns the correct amount when adding multibuy and general
        ///// discounts to the LineItem instance.
        ///// </summary>
        //[Test]
        //public void TestAllowancesTotalReturnsCorrectResultWhenAddingMultibuyDiscountsAndGeneralDiscounts()
        //{
        //    LineItem lineItem = new LineItem(10, 2.2m, 3);
        //    lineItem.AddMultibuyDiscount(5m, "Test multibuy Discount", null);
        //    Assert.AreEqual(5, lineItem.AllowancesTotal);

        //    lineItem.AddMultibuyDiscount(6m, "Test multibuy Discount2", null);
        //    Assert.AreEqual(11, lineItem.AllowancesTotal);

        //    lineItem.AddMultibuyDiscount(5m, "Test multibuy Discount", null);
        //    Assert.AreEqual(16, lineItem.AllowancesTotal);

        //    lineItem.AddMultibuyDiscount(6m, "Test multibuy Discount2", null);
        //    Assert.AreEqual(22, lineItem.AllowancesTotal);
        //}
    }
}