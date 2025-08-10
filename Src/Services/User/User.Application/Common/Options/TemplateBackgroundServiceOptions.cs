namespace User.Application.Common.Options;

/// <summary>
/// تنظیمات سرویس پس زمینه
/// </summary>
public sealed class TemplateBackgroundServiceOptions
{
    /// <summary>
    /// زمان تکرار حلقه بر اساس دقیقه
    /// </summary>
    public int PeriodTimeSpanMinute { get; init; }
}
