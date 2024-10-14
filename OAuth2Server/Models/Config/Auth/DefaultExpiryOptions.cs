namespace OAuth2Server.Models.Config.Auth;

public class DefaultExpiryOptions
{
    public int? Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
    public int? Hour { get; set; }
    public int? Minute { get; set; }
    public int? Second { get; set; }

    public DateTimeOffset GetExpireOn(DateTimeOffset? startOn = null)
    {
        var expireOn = startOn.HasValue ? startOn.Value : DateTimeOffset.UtcNow.ToLocalTime();

        if (Year.HasValue)
            expireOn = expireOn.AddYears(Year.Value);
        if (Month.HasValue)
            expireOn = expireOn.AddMonths(Month.Value);
        if (Day.HasValue)
            expireOn = expireOn.AddDays(Day.Value);
        if (Hour.HasValue)
            expireOn = expireOn.AddHours(Hour.Value);
        if (Minute.HasValue)
            expireOn = expireOn.AddMinutes(Minute.Value);
        if (Second.HasValue)
            expireOn = expireOn.AddSeconds(Second.Value);

        return expireOn;
    }
}
