namespace TextRpg.Domain.Tests;

public class CharacterTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    var character = Character.Create(string.Empty);

    // Assert
    character.Should().NotBeNull();
    character.Id.Should().NotBe(Guid.Empty);
    character.Name.Should().Be(string.Empty);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "John";

    // Act
    var character = Character.Load(id, name);

    // Assert
    character.Id.Should().Be(id);
    character.Name.Should().Be(name);
  }

  [Fact]
  public void AddTraits_ShouldAddGivenTraitIds()
  {
    // Arrange
    var character = Character.Create("TestGuy");
    var trait1 = Guid.NewGuid();
    var trait2 = Guid.NewGuid();
    var traits = new List<Guid> { trait1, trait2 };

    // Act
    character.AddTraits(traits);

    // Assert
    character.TraitsId.Should().HaveCount(2);
    character.TraitsId.Should().Contain(trait1);
    character.TraitsId.Should().Contain(trait2);
  }

  #endregion
}
