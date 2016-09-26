/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package net.dreceiptx.receipt.validation;

public class ValidationErrors {
    public static final String ReceiptMustHaveALeastLineItem = "Digital Receipt must have a least 1 line item.";
    public static final String ReceiptTypeMustBeSet = "Receipt Type must be set.";
    public static final String CurrencyCodeMustBeSet = "Currency Code must be set.";
    public static final String CountryCodeMustBeSet = "Country Code must be set.";
    public static final String MerchantGLNMustBeSet = "Merchant GLN must be set.";
    public static final String UserGUIDMustBeSet = "User GUID must be set.";
    public static final String MerchantReferencenumberMustBeSet = "Merchant referenceNumber must be set.";
    public static final String ReceiptTransactionDateMustBeSet = "Receipt transactionDateTime must be set.";
}
