namespace TestCaseManager.Core.CryptoService.CertificateRelated
{
    using System;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class X509Certificate2CryptoService : ICryptoService
    {
        private X509Certificate2 _certificate;
        private RSACryptoServiceProvider _rsaPrivateCryptoProvider;
        private RSACryptoServiceProvider _rsaPublicCryptoProvider;

        public X509Certificate2CryptoService(IX509Certificate2Resolver certificateResolver)
        {
            if (certificateResolver == null)
                throw new ArgumentNullException($"The {(IX509Certificate2Resolver) null} parameter is null.");

            _certificate = certificateResolver.GetCertificate();
            if (_certificate == null)
                throw new InvalidOperationException(@"The CertificateResolver returned null certificate.");

            _rsaPublicCryptoProvider = (RSACryptoServiceProvider) _certificate.PublicKey.Key;
            _rsaPrivateCryptoProvider = (RSACryptoServiceProvider) _certificate.PrivateKey;
        }

        public virtual string Encrypt(string value)
        {
            var bytestoEncrypt = Encoding.UTF8.GetBytes(value);
            var encryptedBytes = _rsaPublicCryptoProvider.Encrypt(bytestoEncrypt, false);

            var encryptedValue = Convert.ToBase64String(encryptedBytes);
            return encryptedValue;
        }

        public virtual string Decrypt(string value)
        {
            if (_certificate.HasPrivateKey == false)
                throw new InvalidOperationException(@"The certificate in use does not have a Private Key.");

            var bytestodecrypt = Convert.FromBase64String(value);
            var decryptedByptes = _rsaPrivateCryptoProvider.Decrypt(bytestodecrypt, false);

            var decryptedValue = Encoding.UTF8.GetString(decryptedByptes);
            return decryptedValue;
        }

        public void Dispose()
        {
            if (_certificate != null)
            {
                _certificate.Reset();
                _certificate = null;
            }

            if (_rsaPublicCryptoProvider != null)
            {
                _rsaPublicCryptoProvider.Clear();
                _rsaPublicCryptoProvider = null;
            }

            if (_rsaPrivateCryptoProvider != null)
            {
                _rsaPrivateCryptoProvider.Clear();
                _rsaPrivateCryptoProvider = null;
            }
        }
    }
}