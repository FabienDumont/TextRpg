using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

public class LocationOpeningHoursService(ILocationOpeningHoursRepository locationOpeningHoursRepository)
  : ILocationOpeningHoursService
{
  #region Implemention of ILocationOpeningHoursService

  public async Task<bool> IsLocationOpenAsync(
    Guid locationId, DayOfWeek day, TimeSpan time, CancellationToken cancellationToken
  )
  {
    var openingHoursList = await locationOpeningHoursRepository.GetByLocationIdAsync(locationId, cancellationToken);

    foreach (var hours in openingHoursList)
    {
      if (hours.DayOfWeek != day)
      {
        // Handle cross-midnight: e.g., 22:00-02:00 on Saturday covers into Sunday
        if (CrossesMidnight(hours))
        {
          var previousDay = GetPreviousDayOfWeek(day);
          if (hours.DayOfWeek == previousDay && time >= TimeSpan.Zero && time <= hours.ClosesAt)
          {
            return true;
          }
        }

        continue;
      }

      // Normal within-the-day opening hours
      if (hours.OpensAt <= hours.ClosesAt)
      {
        if (time >= hours.OpensAt && time <= hours.ClosesAt)
        {
          return true;
        }
      }
      else
      {
        // Cross-midnight (e.g., 22:00–02:00 on same day definition)
        if (time >= hours.OpensAt || time <= hours.ClosesAt)
        {
          return true;
        }
      }
    }

    return false;
  }

  private static bool CrossesMidnight(LocationOpeningHours hours)
  {
    return hours.OpensAt > hours.ClosesAt;
  }

  private static DayOfWeek GetPreviousDayOfWeek(DayOfWeek day)
  {
    return day == DayOfWeek.Sunday ? DayOfWeek.Saturday : (DayOfWeek) ((int) day - 1);
  }

  #endregion
}
