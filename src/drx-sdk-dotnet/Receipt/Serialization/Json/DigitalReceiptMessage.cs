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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Settlement;

namespace Net.Dreceiptx.Receipt.Serialization.Json
{
    /// <summary>
    /// Class representing a full DigitalReceipt. It containts the header information about the
    /// receipt along with the invoice and payment details.
    /// </summary>
    [DataContract]
    public class DigitalReceipt
    {
        public DigitalReceipt()
        {
            Invoice = new Invoice.Invoice();
        }

        /// <summary>
        /// Gets and sets the StandardBusinessDocumentHeader.
        /// </summary>
        [DataMember]
        public StandardBusinessDocumentHeader StandardBusinessDocumentHeader { get; set; }

        /// <summary>
        /// Gets and sets the receipt Invoice details
        /// </summary>
        [DataMember]
        public Invoice.Invoice Invoice { get; set; }

        /// <summary>
        /// Gets and sets the receipt PaymentReceipts
        /// </summary>
        [DataMember]
        public List<PaymentReceipt> PaymentReceipts { get; set; }
    }
}