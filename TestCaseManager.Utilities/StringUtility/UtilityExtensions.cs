namespace TestCaseManager.Utilities.StringUtility
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public static class UtilityExtensions
    {
        private const string ExceptionDetailsFormat = @"{0}
    InnerException: {1}";

        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException(nameof(securePassword));

            var unmanagedString = IntPtr.Zero;
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
                throw new ArgumentNullException(nameof(password));

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
        ///     Removes new lines, starting/ending whitespaces on each line and tabs.
        /// </summary>
        /// <param name="value">String value to trim</param>
        /// <returns>String value without new lines, tabs and starting/ending whitespaces for each line.</returns>
        public static string RemoveWhitespace(this string value)
        {
            var lines = value.Split('\r', '\n').Where(l => l.Length > 0).ToArray();
            var builder = new StringBuilder(value.Length);
            foreach (var line in lines) builder.Append(line.Trim());
            builder.Replace("\t", string.Empty);

            return builder.ToString();
        }

        /// <summary>
        ///     Trims whitespace characters from start and end of a string. Can handle properly NULL values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimNullSafe(this string value)
        {
            return string.IsNullOrEmpty(value) ? value : value.Trim();
        }

        /// <summary>
        ///     Returns exceptions details including details for all inner exceptions (if any).
        /// </summary>
        /// <param name="exception">Current exception</param>
        /// <returns>String representation of the current exception and all inner exceptions</returns>
        public static string ToDetailedString(this Exception exception)
        {
            return string.Format(ExceptionDetailsFormat,
                exception,
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
                throw new ArgumentNullException(parameterName);

            if (target.Length == 0)
                throw new ArgumentException($"Argument must not be empty: {parameterName}.");
        }
    }
}