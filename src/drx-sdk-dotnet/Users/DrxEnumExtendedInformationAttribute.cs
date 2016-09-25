using System;

namespace Net.Dreceiptx.Users
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DrxEnumExtendedInformationAttribute : Attribute
    {
        public string Value { get; set; }
        public string Description { get; set; }

        public DrxEnumExtendedInformationAttribute(string value, string description)
        {
            Value = value;
            Description = description;
        }
    }
}