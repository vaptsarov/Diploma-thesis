namespace TestCaseManager.Core.CryptoService
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    ///     Contract for X509Certificate2 certificate resolvers.
    /// </summary>
    public interface IX509Certificate2Resolver
    {
        /// <summary>
        ///     Returns a <see cref="X509Certificate2" /> instance.
        /// </summary>
        /// <returns>The resolved <see cref="X509Certificate2" /> instance.</returns>
        X509Certificate2 GetCertificate();
    }
}