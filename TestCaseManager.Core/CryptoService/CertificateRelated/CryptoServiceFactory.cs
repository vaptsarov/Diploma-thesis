using System;

namespace TestCaseManager.Core.CryptoService
{
    /// <summary>
    /// A dedicated component for resolving <see cref="ICryptoService"/> instances
    /// </summary>
    public static class CryptoServiceFactory
    {
        private static Func<ICryptoService> cryptoServiceInitializer = GetDefaultCryptoService;
        public static ICryptoService Get()
        {
            // Invoke the crypto service initializer that will resolve ICryptoService instance
            return CryptoServiceFactory.CryptoServiceInitializer.Invoke();
        }

        /// <summary>
        /// Gets or sets the function with which to resolve new <see cref="ICryptoService"/> instances.
        /// </summary>
        /// <value>The function for resolve new <see cref="ICryptoService"/> instances.</value>
        public static Func<ICryptoService> CryptoServiceInitializer
        {
            get
            {
                return cryptoServiceInitializer;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("The CryptoServiceInitializer cannot be null.");

                cryptoServiceInitializer = value;
            }
        }

        private static ICryptoService GetDefaultCryptoService()
        {
            return new NoEncryptionCryptoService();
        }
    }

}
