using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Net.Dreceiptx
{
    [DataContract]
    public class Delivery
    {
        private List<Contact> _contacts = new List<Contact>();

        /// <summary> Required for XML serialization </summary>
        protected Delivery()
        {}

        public Delivery(DateTime deliveryDateTime, string deliveryInstructions)
        {
            DeliveryDateTime = deliveryDateTime;
            DeliveryInstructions = deliveryInstructions;
        }

        /// <summary>
        /// TODO: Should we indicate the timezone this time is in?
        /// </summary>
        [DataMember]
        public DateTime DeliveryDateTime { get; set; }
        [DataMember]
        public string DeliveryInstructions { get; set; }
        [DataMember]
        public Address DeliveryAddress { get; set; }
        //TODO: Is this needed here?
        [DataMember]
        public ReceiptAllowanceCharge DeliveryFee { get; set; }

        [DataMember]
        public List<Contact> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }

        [XmlIgnore]
        public bool HasDeliveryFee
        {
            get { return DeliveryFee != null; }
        }

        [XmlIgnore]
        public bool HasDeliveryAddress
        {
            get { return DeliveryAddress != null; }
        }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        //TODO: Is this needed here?
        public void AddDeliveryFee(decimal amount, string description, TaxFee tax)
        {
            //TODO: What is ADZ?
            ReceiptAllowanceCharge deliveryFee = new ReceiptAllowanceCharge(
                "CHARGE", "ADZ", 
                "CHARGE_TO_BE_PAID_BY_CUSTOMER", amount, 
                description, tax);
            DeliveryFee = deliveryFee;
        }
    }

    public static class ReceiptBuilder
    {
        public static Address Name(this Address address, string name)
        {
            address.Name = name;
            return address;
        }

        public static Address StreetAddress1(this Address address, string value)
        {
            address.StreetAddress1 = value;
            return address;
        }

        public static Address StreetAddress2(this Address address, string value)
        {
            address.StreetAddress2 = value;
            return address;
        }

        public static Address StreetAddress3(this Address address, string value)
        {
            address.StreetAddress3 = value;
            return address;
        }

        public static Address City(this Address address, string value)
        {
            address.City = value;
            return address;
        }

        public static Address PostalCode(this Address address, string value)
        {
            address.PostalCode = value;
            return address;
        }

        public static Address State(this Address address, string value)
        {
            address.State = value;
            return address;
        }

        public static Address CountryCode(this Address address, string value)
        {
            address.CountryCode = value;
            return address;
        }
    }
}