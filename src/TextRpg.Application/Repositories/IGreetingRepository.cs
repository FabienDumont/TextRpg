using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for greetings.
/// </summary>
public interface IGreetingRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves a greeting given by a character.
  /// </summary>
  Task<Greeting?> GetAsync(
    Character character, CancellationToken cancellationToken
  );

  #endregion
}
