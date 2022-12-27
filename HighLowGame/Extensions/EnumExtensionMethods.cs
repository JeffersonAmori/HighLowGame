namespace HighLowGame.Extensions
{
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// Extension method to return an enum value of type T for the given string.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">String value.</param>
        /// <returns>The enum value.</returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
