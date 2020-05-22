namespace TestCaseManager.Core.CryptoService
{
    using System;

    public interface ICryptoService : IDisposable
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}