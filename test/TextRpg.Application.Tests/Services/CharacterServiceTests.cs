using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class CharacterServiceTests
{
  #region Fields

  private readonly CharacterService _characterService;
  private readonly INameRepository _nameRepository = A.Fake<INameRepository>();

  #endregion

  #region Ctors

  public CharacterServiceTests()
  {
    _characterService = new CharacterService(_nameRepository);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task CreateRandomCharacter_ShouldReturnValidCharacter()
  {
    // Arrange
    A.CallTo(() => _nameRepository.GetFemaleNamesAsync(A<CancellationToken>._)).Returns(new List<string> {"Alice"});
    A.CallTo(() => _nameRepository.GetMaleNamesAsync(A<CancellationToken>._)).Returns(new List<string> {"Bob"});

    // Act
    var character = await _characterService.CreateRandomCharacterAsync();

    // Assert
    Assert.NotNull(character);
    Assert.False(string.IsNullOrWhiteSpace(character.Name));
    Assert.InRange(character.Age, 18, 59);
    Assert.True(character.BiologicalSex == BiologicalSex.Male || character.BiologicalSex == BiologicalSex.Female);
  }

  #endregion
}
