namespace TestCaseManager.Core.CryptoService
{
    public class NoEncryptionCryptoService : ICryptoService
    {
        public string Encrypt(string value)
        {
            return value;
        }


        public string Decrypt(string value)
        {
            return value;
        }


        public void Dispose()
        {
        }
    }
}
