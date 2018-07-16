using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TestCaseManager.Core.CryptoService
{
    public class X509Certificate2FromStoreResolver : IX509Certificate2Resolver
    {
        private readonly string _thumbprint;

        public X509Certificate2FromStoreResolver(string thumbprint)
        {
            this._thumbprint = thumbprint;
        }

        public X509Certificate2 GetCertificate()
        {
            // Check for certificate in My store in the CurrentUser location 
            var currentUserStore = GetStore(StoreLocation.CurrentUser);
            var certificate = GetCertificateFromStore(currentUserStore);
            if (certificate != null)
                return certificate;

            // Check for certificate in My store in the LocalMachine location 
            var localMachineStore = GetStore(StoreLocation.LocalMachine);
            certificate = GetCertificateFromStore(localMachineStore);
            return certificate;
        }

        protected virtual X509Store GetStore(StoreLocation storeLocation)
        {
            return new X509Store(storeLocation);
        }

        protected virtual X509Store GetStore(StoreName storeName, StoreLocation? storeLocation = null)
        {
            return !storeLocation.HasValue ? new X509Store(storeName) : new X509Store(storeName, storeLocation.GetValueOrDefault());
        }

        private X509Certificate2 GetCertificateFromStore(X509Store store)
        {
            X509Certificate2 certificate = null;

            store.Open(OpenFlags.ReadOnly);

            try
            {
                certificate = store.Certificates.Cast<X509Certificate2>().FirstOrDefault(item =>
                    string.Equals(item.Thumbprint, _thumbprint, StringComparison.InvariantCultureIgnoreCase));
            }
            finally
            {
                store.Close();
            }

            return certificate;
        }
    }
}