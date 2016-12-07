using System;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.Builders
{
    public class StandardBusinessDocumentHeaderBuilder
    {
        private StandardBusinessDocumentHeader _header;
        private DocumentInformationBuilder _documentInformationBuilder;
        public StandardBusinessDocumentHeaderBuilder()
        {
            _header = new StandardBusinessDocumentHeader();
            _documentInformationBuilder = new DocumentInformationBuilder(this, _header);

        }

        public StandardBusinessDocumentHeaderBuilder MerchangeGLN(string merchantGLN)
        {
            _header.MerchantGLN.Value = merchantGLN;
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder DrxFLN(string DdxFLN)
        {
            _header.DrxFLN.Value = DdxFLN;
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder UserIdentifier(UserIdentifierType userIdentifierType, string userIdentifier)
        {
            _header.UserIdentifier.Value = $"{userIdentifierType.Value()}:{userIdentifier}";
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder AddMerchantContact(ReceiptContact contact)
        {
            _header.Sender[0].AddDocumentOwnerContact(contact);
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder AddRMSContact(ReceiptContact contact)
        {
            _header.Receiver[1].AddDocumentOwnerContact(contact);
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder AddReceiver(DocumentOwner receiver)
        {
            _header.Receiver.Add(receiver);
            return this;
        }

        public DocumentInformationBuilder DocumentInformation()
        {
            return _documentInformationBuilder;
        }

        public StandardBusinessDocumentHeader Build()
        {
            return _header;
        }

        public class DocumentInformationBuilder
        {
            private readonly StandardBusinessDocumentHeaderBuilder _builder;
            private readonly StandardBusinessDocumentHeader _header;

            public DocumentInformationBuilder(StandardBusinessDocumentHeaderBuilder builder, StandardBusinessDocumentHeader header)
            {
                _builder = builder;
                _header = header;
            }

            public DocumentInformationBuilder TypeVersion(string typeVersion)
            {
                _header.DocumentIdentification.TypeVersion = typeVersion;
                return this;
            }

            public DocumentInformationBuilder InstanceIdentifier(string instanceIdentifier)
            {
                _header.DocumentIdentification.InstanceIdentifier = instanceIdentifier;
                return this;
            }

            public DocumentInformationBuilder CreationDateAndTime(DateTime creationDateAndTime)
            {
                _header.DocumentIdentification.CreationDateAndTime = creationDateAndTime;
                return this;
            }

            public StandardBusinessDocumentHeaderBuilder Builder()
            {
                return _builder;
            }
        }
    }
}