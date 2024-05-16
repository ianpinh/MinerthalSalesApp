namespace MinerthalSalesApp.Customs.CustomHelpers
{
    public static class ExtensionMethods
    {
        public static int DecimalDigits(this decimal n)
        {
            return n.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    .SkipWhile(c => c != '.')
                    .Skip(1)
                    .Count();
        }

        public static bool IsDeletion(this TextChangedEventArgs e)
        {
            return !string.IsNullOrEmpty(e.OldTextValue) && e.OldTextValue.Length > e.NewTextValue.Length;
        }
    }
    public static class ExtensionsFormatProvider
    {
        private static IFormatProvider inv
                       = System.Globalization.CultureInfo.InvariantCulture.NumberFormat;

        public static string ToStringInvariant<T>(this T obj, string format = null)
        {
            return (format == null) ? System.FormattableString.Invariant($"{obj}")
                                    : String.Format(inv, $"{{0:{format}}}", obj);
        }
    }

}
