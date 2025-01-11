namespace PROWeb.Common.Extensions
{
    public static class StringExtensions
    {
        public static string? ToNullIfWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        public static string? ToNullIfEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }
    }
}
