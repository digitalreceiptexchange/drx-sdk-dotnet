using System.Collections.Generic;

namespace Net.Dreceiptx
{
    public class ReceiptValidation
    {
        private readonly List<string> _errors = new List<string>();

        public ReceiptValidation()
        {
            IsValid = true;
        }

        public bool IsValid { get; private set; }

        public List<string> Errors
        {
            get { return _errors; }
        }

        public void AddError(string error)
        {
            IsValid = false;
            Errors.Add(error);
        }
    }
}