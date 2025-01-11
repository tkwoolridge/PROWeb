namespace PROWeb.Data.Models.Reports
{
    internal static class PredefinedFilterTypes
    {
        private static readonly Type[] _predefinedTypes =
        [
            typeof(object),
            typeof(bool),
            typeof(char),
            typeof(string),
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateOnly),
            typeof(TimeOnly),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert)
        ];

        public static bool IsPredefinedType(Type type)
        {
            return _predefinedTypes.Contains(type);
        }

        public static bool IsPredefinedType(string typeName)
        {
            return _predefinedTypes.Any(t => t.FullName!.Equals(typeName));
        }
    }
}
