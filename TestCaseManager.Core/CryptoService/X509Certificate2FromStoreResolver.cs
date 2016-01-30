using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TestCaseManager.Core.CryptoService
{
    public class X509Certificate2FromStoreResolver : IX509Certificate2Resolver
    {
        private readonly string thumbprint;

        public X509Certificate2FromStoreResolver(string thumbprint)
        {
            this.thumbprint = thumbprint;
        }

        public X509Certificate2 GetCertificate()
        {
            // Check for certificate in My store in the CurrentUser location 
            X509Store currentUserStore = this.GetStore(StoreLocation.CurrentUser);
            X509Certificate2 certificate = this.GetCertificateFromStore(currentUserStore);
            if (certificate != null)
                return certificate;

            // Check for certificate in My store in the LocalMachine location 
            X509Store localMachineStore = this.GetStore(StoreLocation.LocalMachine);
            certificate = this.GetCertificateFromStore(localMachineStore);
            if (certificate != null)
                return certificate;

            return null;
        }

        protected virtual X509Store GetStore(StoreLocation storeLocation)
        {
            return new X509Store(storeLocation);
        }

        protected virtual X509Store GetStore(StoreName storeName, StoreLocation? storeLocation = null)
        {
            if (!storeLocation.HasValue)
                return new X509Store(storeName);

            return new X509Store(storeName, storeLocation.GetValueOrDefault());
        }

        private X509Certificate2 GetCertificateFromStore(X509Store store)
        {
            X509Certificate2 certificate = null;

            store.Open(OpenFlags.ReadOnly);

            try
            {
                certificate = store.Certificates.Cast<X509Certificate2>().FirstOrDefault(item => string.Equals(item.Thumbprint, this.thumbprint, StringComparison.InvariantCultureIgnoreCase));
            }
            finally
            {
                store.Close();
            }

            return certificate;
        }
    }
}