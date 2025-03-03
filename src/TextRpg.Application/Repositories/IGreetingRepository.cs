using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for greetings.
/// </summary>
public interface IGreetingRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves a greeting that matches the specified relationship level and optionally considers traits.
  /// </summary>
  Task<Greeting?> GetByRelationshipLevelAsync(
    int relationshipLevel, IEnumerable<Trait> traits, CancellationToken cancellationToken
  );

  #endregion
}
