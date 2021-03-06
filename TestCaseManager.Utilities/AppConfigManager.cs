﻿namespace TestCaseManager.Utilities
{
    using System;
    using System.Configuration;

    public class AppConfigManager
    {
        public string GetCertificateThumbprint => TryGetConfigurationValue("CertificateThumbprint");

        private static string TryGetConfigurationValue(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
                throw new NullReferenceException($"Missing {key} from configuration.");

            return value;
        }
    }
}