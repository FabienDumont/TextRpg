namespace TextRpg.Domain.Tests;

public class CharacterTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    var name = "Player";
    var age = 18;
    var biologicalSex = BiologicalSex.Male;
    var character = Character.Create(name, age, biologicalSex);

    // Assert
    character.Should().NotBeNull();
    character.Id.Should().NotBe(Guid.Empty);
    character.Name.Should().Be(name);
    character.Age.Should().Be(age);
    character.BiologicalSex.Should().Be(biologicalSex);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var name = "Player";
    var age = 18;
    var biologicalSex = BiologicalSex.Male;

    // Act
    var character = Character.Load(id, name, age, biologicalSex);

    // Assert
    character.Id.Should().Be(id);
    character.Name.Should().Be(name);
    character.Age.Should().Be(age);
    character.BiologicalSex.Should().Be(biologicalSex);
  }

  [Fact]
  public void AddTraits_ShouldAddGivenTraitIds()
  {
    // Arrange
    var character = Character.Create("TestGuy", 18, BiologicalSex.Male);
    var trait1 = Guid.NewGuid();
    var trait2 = Guid.NewGuid();
    var traits = new List<Guid> {trait1, trait2};

    // Act
    character.AddTraits(traits);

    // Assert
    character.TraitsId.Should().HaveCount(2);
    character.TraitsId.Should().Contain(trait1);
    character.TraitsId.Should().Contain(trait2);
  }

  #endregion
}
