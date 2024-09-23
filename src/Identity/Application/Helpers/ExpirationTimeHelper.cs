namespace Identity.Application.Helpers;

public static class ExpirationTimeHelper
{
    public static DateTime GetExpirationTime(int lifetimeInDays)
    {
        return DateTime.UtcNow.AddDays(lifetimeInDays).Date.AddDays(1).AddMinutes(-1);
    }
}