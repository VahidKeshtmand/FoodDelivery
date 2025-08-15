using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Options;

namespace Users.Application.Common.Utilities;

public static class ExtensionMethods
{
    public static string GetEnumDisplayName(this Enum enumType) {
        var fieldInfo = enumType.GetType().GetField(enumType.ToString());
        var displayAttribute = fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[] ?? [];
        return (displayAttribute?.Length > 0 ? displayAttribute[0].Name : enumType.ToString())!;
    }

    public static string GetHa256Hash(this Guid value) {
        var algorithm = SHA256.Create();
        var byteValue = Encoding.UTF8.GetBytes(value.ToString());
        var byteHash = algorithm.ComputeHash(byteValue);      
        return Convert.ToBase64String(byteHash);
    }

    public static async Task<TItem> GetOrCreateAsync<TItem>(
        this IMemoryCache cache, 
        object key,
        Func<ICacheEntry, Task<TItem>> factory,
        InMemoryCacheOptions options) {

        return await cache.GetOrCreateAsync(key, async entry => {

            entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(options.AbsoluteExpirationSeconds));
            entry.SetSlidingExpiration(TimeSpan.FromSeconds(options.SlidingExpirationSeconds));

            return await factory(entry);

        }) ?? throw new CommandFailedException("خطا در حافظه ی کش");
    }
}
