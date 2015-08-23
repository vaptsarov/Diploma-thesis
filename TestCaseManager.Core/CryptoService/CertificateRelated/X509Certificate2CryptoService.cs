using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TestCaseManager.Core.CryptoService
{
    public class X509Certificate2CryptoService : ICryptoService
    {
        private X509Certificate2 certificate = null;
        private RSACryptoServiceProvider rsaPublicCryptoProvider = null;
        private RSACryptoServiceProvider rsaPrivateCryptoProvider = null;

        public X509Certificate2CryptoService(IX509Certificate2Resolver certificateResolver)
        {
            if (certificateResolver == null)
                throw new ArgumentNullException(@"The 'certificateResolver' parameter is null.");

            this.certificate = certificateResolver.GetCertificate();
            if (this.certificate == null)
                throw new InvalidOperationException(@"The CertificateResolver returned null certificate.");

            this.rsaPublicCryptoProvider = (RSACryptoServiceProvider)this.certificate.PublicKey.Key;
            this.rsaPrivateCryptoProvider = (RSACryptoServiceProvider)this.certificate.PrivateKey;
        }

        public virtual string Encrypt(string value)
        {
            byte[] bytestoEncrypt = UTF8Encoding.UTF8.GetBytes(value);
            byte[] encryptedBytes = this.rsaPublicCryptoProvider.Encrypt(bytestoEncrypt, false);

            string encryptedValue = Convert.ToBase64String(encryptedBytes);
            return encryptedValue;
        }

        public virtual string Decrypt(string value)
        {
            if (this.certificate.HasPrivateKey == false)
                throw new InvalidOperationException(@"The certificate in use does not have a Private Key.");

            byte[] bytestodecrypt = Convert.FromBase64String(value);
            byte[] decryptedByptes = this.rsaPrivateCryptoProvider.Decrypt(bytestodecrypt, false);

            string decryptedValue = UTF8Encoding.UTF8.GetString(decryptedByptes);
            return decryptedValue;
        }

        public void Dispose()
        {
            if (this.certificate != null)
            {
                this.certificate.Reset();
                this.certificate = null;
            }

            if (this.rsaPublicCryptoProvider != null)
            {
                this.rsaPublicCryptoProvider.Clear();
                this.rsaPublicCryptoProvider = null;
            }

            if (this.rsaPrivateCryptoProvider != null)
            {
                this.rsaPrivateCryptoProvider.Clear();
                this.rsaPrivateCryptoProvider = null;
            }
        }
    }

}
