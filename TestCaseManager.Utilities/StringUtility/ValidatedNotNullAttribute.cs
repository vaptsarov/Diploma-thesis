using System;

namespace TestCaseManager.Utilities.StringUtility
{
    /// <summary>
    ///     Allows our custom argument checking methods to avoid rising FxCop analysis error/warnings
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
        // intentionally empty
        // for details see http://esmithy.net/2011/03/15/suppressing-ca1062/
    }
}