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

using System;

namespace Net.Dreceiptx.Receipt.Document
{
    public class DocumentIdentification
    {
        // TODO: Why do we send this if always set to GS1 and if we send then lets
        // validate the value so it can only be GS1
        //@SerializedName("standard")
        /// <summary>
        /// Gets and sets the Standard. The name of the document standard contained in the payload
        /// The value of the element Standard MUST be set to the value 'GS1'
        /// </summary>
        public string Standard { get; set; } = "GS1";

        //@SerializedName("typeVersion")
        /// <summary>
        /// Gets and sets the TypeVersion. Version information of the document included in the
        /// payload of SBDH.This is the 'complete' version of the document itself and is 
        /// different than the HeaderVersion.The value of TypeVersion MUST be set the
        /// version number of the root schema of the XML business document contained
        /// in the payload of the message.Every com.digitalreceiptexchange.GS1 standard schema has version
        /// information in the �xsd:version? attribute of the �xsd:schema? tag of the schema
        /// and also in the schema annotation tag.
        /// The SBDH specification requires that all documents sent with one header have the
        /// same version number. To comply with this requirement; Only business documents
        /// belonging to the same BMS publication release and having the same version number 
        /// MUST be included in the payload if sending more than one document type.
        /// </summary>
        public string TypeVersion { get; set; }

        //@SerializedName("type")
        /// <summary>
        /// Gets and sets the Type. This element identifies the type of the document
        /// The value of the �Type? element of �DocumentIdentification? element MUST be set
        /// to the name of the XML element that defines the root of the business document.
        /// This is the name of the global XML element declared in the root schema for the
        /// business document in consideration.
        /// Example; order, invoice, debitCreditAdvice,
        /// </summary>
        public string Type { get; set; } = "DIGITALRECEIPT";

        /**
         * Gets the InstanceIdentifier. Description which contains reference information
         * which uniquely identifies this instance of the Standard Business Document (SBD)
         * between the Sender and the Receiver. This identifier identifies this document as being
         * distinct from others.
         * Example: MSG-1645000099
         * @return
         */
        ////@SerializedName("instanceIdentifier")
        /// <summary>
        /// Gets and sets the InstanceIdentifier.Description which contains reference information
        /// which uniquely identifies this instance of the Standard Business Document(SBD)
        /// between the Sender and the Receiver.This identifier identifies this document as being
        /// distinct from others.
        /// Example: MSG-1645000099
        /// </summary>
        public string InstanceIdentifier { get; set; }

        //@SerializedName("multipleType")
        /// <summary>
        /// Gets and sets the MultipleType property. Flag to indicate that there is more than one
        /// type of business document in the payload of the SBDH.
        /// The value of the MultiType element of DocumentIdentification element MUST be set
        /// false as the com.digitalreceiptexchange.GS1 XML design allows only one type of business documents to be sent
        /// within one message.
        /// TODO: Raised #6 on this as i think it should be a bool
        /// </summary>
        public string MultipleType { get; set; } = "true";

        //@SerializedName("creationDateAndTime")
        /// <summary>
        /// Gets and sets the CreationDateAndTime. DateTime and time of the SBDH document creation.
        /// The value of the �CreationDateAndTime? element MUST be set to the DateTime and time when
        /// the document originating application or the parser created the document.
        /// This value will typically be populated by the trading partner and will typically
        /// differ from the time stamping of the message by the communications software.
        /// </summary>
        public DateTime CreationDateAndTime { get; set; }
    }
}