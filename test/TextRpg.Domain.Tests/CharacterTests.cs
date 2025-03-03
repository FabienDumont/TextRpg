using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Domain.Tests;

public class CharacterTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    const string name = "Player";
    const int age = 18;
    const BiologicalSex biologicalSex = BiologicalSex.Male;
    var character = Character.Create(name, age, biologicalSex);

    // Assert
    Assert.NotNull(character);
    Assert.NotEqual(Guid.Empty, character.Id);
    Assert.Equal(name, character.Name);
    Assert.Equal(age, character.Age);
    Assert.Equal(biologicalSex, character.BiologicalSex);
    Assert.Null(character.LocationId);
    Assert.Null(character.RoomId);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Player";
    const int age = 18;
    const BiologicalSex biologicalSex = BiologicalSex.Male;

    // Act
    var character = Character.Load(id, name, age, biologicalSex);

    // Assert
    Assert.Equal(id, character.Id);
    Assert.Equal(name, character.Name);
    Assert.Equal(age, character.Age);
    Assert.Equal(biologicalSex, character.BiologicalSex);
    Assert.Null(character.LocationId);
    Assert.Null(character.RoomId);
  }

  [Fact]
  public void AddTraits_ShouldAddGivenTraitIds()
  {
    // Arrange
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var trait1 = Guid.NewGuid();
    var trait2 = Guid.NewGuid();
    var traits = new List<Guid> {trait1, trait2};

    // Act
    playerCharacter.AddTraits(traits);

    // Assert
    Assert.Equal(2, playerCharacter.TraitsId.Count);
    Assert.Contains(trait1, playerCharacter.TraitsId);
    Assert.Contains(trait2, playerCharacter.TraitsId);
  }

  [Fact]
  public void MoveTo_ShouldChangeLocationAndRoom()
  {
    // Arrange
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();

    // Act
    playerCharacter.MoveTo(locationId, roomId);

    // Assert
    Assert.Equal(locationId, playerCharacter.LocationId);
    Assert.Equal(roomId, playerCharacter.RoomId);
  }

  #endregion
}
