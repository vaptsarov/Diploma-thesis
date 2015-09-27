using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace TestCaseManager.Utilities
{
    public static class UtilityExtensions
    {
        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString ConvertToSecureString(this string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            unsafe
            {
                fixed (char* passwordChars = password)
                {
                    var securePassword = new SecureString(passwordChars, password.Length);
                    securePassword.MakeReadOnly();
                    return securePassword;
                }
            }
        }

        /// <summary>
        /// Removes new lines, starting/ending whitespaces on each line and tabs.
        /// </summary>
        /// <param name="value">String value to trim</param>
        /// <returns>String value without new lines, tabs and starting/ending whitespaces for each line.</returns>
        public static string RemoveWhitespace(this string value)
        {
            string[] lines = value.Split('\r', '\n').Where(l => l.Length > 0).ToArray();
            StringBuilder builder = new StringBuilder(value.Length);
            foreach (string line in lines)
            {
                builder.Append(line.Trim());
            }
            builder.Replace("\t", string.Empty);

            return builder.ToString();
        }

        /// <summary>
        /// Trims whitespace characters from start and end of a string. Can handle properly NULL values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimNullSafe(this string value)
        {
            return string.IsNullOrEmpty(value) ? value : value.Trim();
        }

        private const string exceptionDetailsFormat = @"{0}
    InnerException: {1}";

        /// <summary>
        /// Returns exceptions details including details for all inner exceptions (if any).
        /// </summary>
        /// <param name="exception">Current exception</param>
        /// <returns>String representation of the current exception and all inner exceptions</returns>
        public static string ToDetailedString(this Exception exception)
        {
            return string.Format(exceptionDetailsFormat,
                                 exception.ToString(),
                                 exception.InnerException != null ? exception.InnerException.ToDetailedString() : "None");
        }

        public static void GuardAgainstNull([ValidatedNotNull] this object target, string parameterName)
        {
            if (target == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void GuardAgainstNullOrEmpty([ValidatedNotNull] this string target, string parameterName)
        {
            if (target == null)
                throw new System.ArgumentNullException(parameterName);

            if (target.Length == 0)
                throw new System.ArgumentException(string.Format("Argument must not be empty: {0}.", parameterName));
        }
    }

    /// <summary>
    /// Allows our custom argument checking methods to avoid rising FxCop analysis error/warnings
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
        // intentionally empty
        // for details see http://esmithy.net/2011/03/15/suppressing-ca1062/
    }
}
