using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace DentalCare.BillingWebAPI.Utility
{
    public static class ExtensionMethods
    {
        public static string SerializeToJson<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
