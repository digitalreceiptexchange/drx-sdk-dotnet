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
    /// <summary>
    /// Test class for the DigitalRecipt class
    /// </summary>
    [TestFixture]
    public class DigitalReceiptTest
    {

        //[Test]
        //public void TestCreation()
        //{
        //    DigitalReceipt digitalReceipt = new DigitalReceipt();



        //    //Assert.AreEqual("AUD", digitalReceipt.InvoiceCurrencyCode);
        //    //Assert.AreEqual("AU", digitalReceipt.CountryCode);
        //    //Assert.AreEqual("NOTSURE", digitalReceipt.MerchantGLN);

        //    LineItem lineItem = new LineItem(100, 10.01m, 3);
        //    lineItem.AddGeneralDiscount(1, "1 dollar discount for x", null);
        //    lineItem.AddMultibuyDiscount(2, "Buying 100 so slight discount", null);
        //    lineItem.AddTaxFee(new TaxFee(10));
        //    lineItem.GTIN = "00012345600012";
        //    lineItem.ISBN = "1234567890ABC";

        //    digitalReceipt.AddLineItem(lineItem);

        //}


        ///// <summary>
        ///// Test to ensure that all the errors are reported 
        ///// when nothing has been added to the DigitalReceipt
        ///// </summary>
        //[Test]
        //public void TestValidateReturnsCorrectErrorsWhenAllChecksFail()
        //{
        //    DigitalReceipt digitalReceipt = new DigitalReceipt();
        //    ReceiptValidation validationResult = digitalReceipt.Validate();
        //    Assert.IsFalse(validationResult.IsValid);
        //}
    }
}

             