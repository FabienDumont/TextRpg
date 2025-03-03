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
    character.Should().NotBeNull();
    character.Id.Should().NotBe(Guid.Empty);
    character.Name.Should().Be(name);
    character.Age.Should().Be(age);
    character.BiologicalSex.Should().Be(biologicalSex);
    character.LocationId.Should().BeNull();
    character.RoomId.Should().BeNull();
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
    character.Id.Should().Be(id);
    character.Name.Should().Be(name);
    character.Age.Should().Be(age);
    character.BiologicalSex.Should().Be(biologicalSex);
    character.LocationId.Should().BeNull();
    character.RoomId.Should().BeNull();
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
    playerCharacter.TraitsId.Should().HaveCount(2);
    playerCharacter.TraitsId.Should().Contain(trait1);
    playerCharacter.TraitsId.Should().Contain(trait2);
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
    playerCharacter.LocationId.Should().Be(locationId);
    playerCharacter.RoomId.Should().Be(roomId);
  }

  #endregion
}
