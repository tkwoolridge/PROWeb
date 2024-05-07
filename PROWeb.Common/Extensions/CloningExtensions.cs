using Newtonsoft.Json;

namespace PROWeb.Common.Extensions
{
    public static class CloningExtensions
    {
        public static T? DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
