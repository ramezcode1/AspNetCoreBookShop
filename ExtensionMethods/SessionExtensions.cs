using System.Text.Json;

namespace BookStore.ExtensionMethods
{
    // these methods make it easier to get and set objects in session

    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return (value == null) ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
