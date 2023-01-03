namespace Jobby.Application.Static;
public static class DateTimeFormatter
{
    public static string FormatDateTime(DateTime dateTime)
    {
        TimeSpan timeSince = DateTime.Now - dateTime;
        if (timeSince.TotalDays > 365)
        {
            int years = (int)(timeSince.TotalDays / 365);
            return $"{years} year(s) ago";
        }
        else if (timeSince.TotalDays > 30)
        {
            int months = (int)(timeSince.TotalDays / 30);
            return $"{months} month(s) ago";
        }
        else if (timeSince.TotalDays > 7)
        {
            int weeks = (int)(timeSince.TotalDays / 7);
            return $"{weeks} week(s) ago";
        }
        else if (timeSince.TotalDays > 1)
        {
            int days = (int)timeSince.TotalDays;
            return $"{days} day(s) ago";
        }
        else if (timeSince.TotalHours > 1)
        {
            int hours = (int)timeSince.TotalHours;
            return $"{hours} hour(s) ago";
        }
        else if (timeSince.TotalMinutes > 1)
        {
            int minutes = (int)timeSince.TotalMinutes;
            return $"{minutes} minute(s) ago";
        }
        else
        {
            return "just now";
        }
    }
}
