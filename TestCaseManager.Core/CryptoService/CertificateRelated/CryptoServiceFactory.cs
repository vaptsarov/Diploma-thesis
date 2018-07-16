using System;

namespace TestCaseManager.Core.CryptoService.CertificateRelated
{
    /// <summary>
    ///     A dedicated component for resolving <see cref="ICryptoService" /> instances
    /// </summary>
    public static class CryptoServiceFactory
    {
        private static Func<ICryptoService> _cryptoServiceInitializer = GetDefaultCryptoService;

        /// <summary>
        ///     Gets or sets the function with which to resolve new <see cref="ICryptoService" /> instances.
        /// </summary>
        /// <value>The function for resolve new <see cref="ICryptoService" /> instances.</value>
        public static Func<ICryptoService> CryptoServiceInitializer
        {
            get => _cryptoServiceInitializer;
            set => _cryptoServiceInitializer = value ?? throw new ArgumentNullException($"The CryptoServiceInitializer cannot be null.");
        }

        public static ICryptoService Get()
        {
            // Invoke the crypto service initializer that will resolve ICryptoService instance
            return CryptoServiceInitializer.Invoke();
        }

        private static ICryptoService GetDefaultCryptoService()
        {
            return new NoEncryptionCryptoService();
        }
    }
}