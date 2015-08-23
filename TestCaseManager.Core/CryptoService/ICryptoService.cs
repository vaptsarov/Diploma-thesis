using System;

namespace TestCaseManager.Core.CryptoService
{
    public interface ICryptoService : IDisposable 
    { 
        string Encrypt(string value); 
        string Decrypt(string value); 
    } 
}
