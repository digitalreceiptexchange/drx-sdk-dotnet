namespace Net.Dreceiptx.Receipt.Config
{
    public class StronlyTypedConfigManager : IConfigManager
    {
        private readonly DictionaryConfigManager _dictionaryConfigManager = new DictionaryConfigManager();

        public string ExchangeHostname
        {
            get { return GetOrNull("exchange.hostname"); }
            set { SetConfigValue("exchange.hostname", value); }
        }

        public string DirectoryHostname
        {
            get { return GetOrNull("directory.hostname"); }
            set { SetConfigValue("directory.hostname", value); }
        }

        public string ApiRequesterId
        {
            get { return GetOrNull("api.requesterId"); }
            set { SetConfigValue("api.requesterId", value); }
        }

        public string ReceiptVersion
        {
            get { return GetOrNull("receipt.version"); }
            set { SetConfigValue("receipt.version", value); }
        }

        public string UserVersion
        {
            get { return GetOrNull("user.version"); }
            set { SetConfigValue("user.version", value); }
        }

        /// <summary>
        /// Gets and sets the ExchangeProtocol. This should be http
        /// or https
        /// </summary>
        public string ExchangeProtocol
        {
            get { return GetOrNull("exchange.protocol"); }
            set { SetConfigValue("exchange.protocol", value); }
        }

        /// <summary>
        /// Gets and sets the DirectoryProtocol. This should be http or 
        /// https
        /// </summary>
        public string DirectoryProtocol
        {
            get { return GetOrNull("directory.protocol"); }
            set { SetConfigValue("directory.protocol", value); }
        }

        private string GetOrNull(string key)
        {
            if (_dictionaryConfigManager.Exists(key))
            {
                return _dictionaryConfigManager.GetConfigValue(key);
            }
            return null;
        }

        public string GetConfigValue(string key)
        {
            return _dictionaryConfigManager.GetConfigValue(key);
        }

        public void SetConfigValue(string key, string value)
        {
            _dictionaryConfigManager.SetConfigValue(key, value);
        }

        public void SetConfigValue(string key, string value, bool commit)
        {
            _dictionaryConfigManager.SetConfigValue(key, value, commit);
        }

        public bool Exists(string key)
        {
            return _dictionaryConfigManager.Exists(key);
        }
    }
}