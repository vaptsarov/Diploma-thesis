using System;
using System.Configuration;

namespace TestCaseManager.Utilities
{
    public class AppConfigManager
    {
        public string GetCertificateThumbprint
        {
            get
            {
                return this.TryGetConfigurationValue("CertificateThumbprint");
            }
        }

        private string TryGetConfigurationValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
                throw new NullReferenceException(string.Format("Missing {0} from configuration.", key));

            return value;
        }
    }
}
