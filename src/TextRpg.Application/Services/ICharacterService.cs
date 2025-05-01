using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for managing characters.
/// </summary>
public interface ICharacterService
{
  #region Methods

  /// <summary>
  ///   Creates a new character with randomized attributes.
  /// </summary>
  /// <returns>A randomly generated character.</returns>
  Task<Character> CreateRandomCharacterAsync();

  #endregion
}
