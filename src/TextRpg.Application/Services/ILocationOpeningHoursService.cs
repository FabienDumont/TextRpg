namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for managing a location's opening hours.
/// </summary>
public interface ILocationOpeningHoursService
{
  #region Methods

  /// <summary>
  ///   Checks whether a location is open at a given day and time.
  /// </summary>
  /// <param name="locationId">Identifier of the current location.</param>
  /// <param name="day">The day.</param>
  /// <param name="time">The time of the day.</param>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>A list of valid movements the player can perform.</returns>
  Task<bool> IsLocationOpenAsync(Guid locationId, DayOfWeek day, TimeSpan time, CancellationToken cancellationToken);

  #endregion
}
