using User.Application.Common.Exceptions;
using User.Application.Common.Options;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace User.Application.Common.Utilities;

/// <summary>
/// کلاس کمکی برای نوع Enum
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// بدست آوردن DisplayName مقدار Enum
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static string GetEnumDisplayName(this Enum enumType) {
        var fieldInfo = enumType.GetType().GetField(enumType.ToString());
        var displayAttribute = fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[] ?? [];
        return (displayAttribute?.Length > 0 ? displayAttribute[0].Name : enumType.ToString())!;
    }

    /// <summary>
    /// متد هش SHA256
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetHa256Hash(this Guid value) {
        var algorithm = SHA256.Create();
        var byteValue = Encoding.UTF8.GetBytes(value.ToString());
        var byteHash = algorithm.ComputeHash(byteValue);      
        return Convert.ToBase64String(byteHash);
    }

    /// <summary>
    /// متد مورد استفاده برای کش
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="CommandFailedException"></exception>
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
