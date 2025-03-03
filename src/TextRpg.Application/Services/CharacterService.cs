using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing characters.
/// </summary>
public class CharacterService(INameRepository nameRepository) : ICharacterService
{
  #region Implementation of ICharacterService

  /// <inheritdoc />
  public async Task<Character> CreateRandomCharacterAsync()
  {
    var rng = new Random();

    var sex = rng.Next(0, 2) == 0 ? BiologicalSex.Male : BiologicalSex.Female;
    var age = rng.Next(18, 60);

    var names = sex == BiologicalSex.Female
      ? await nameRepository.GetFemaleNamesAsync()
      : await nameRepository.GetMaleNamesAsync();

    var name = names[rng.Next(names.Count)];

    return Character.Create(name, age, sex);
  }

  #endregion
}
