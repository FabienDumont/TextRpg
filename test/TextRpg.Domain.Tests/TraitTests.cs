namespace TextRpg.Domain.Tests;

public class TraitTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    var trait = Trait.Create(string.Empty);

    // Assert
    trait.Should().NotBeNull();
    trait.Id.Should().NotBe(Guid.Empty);
    trait.Name.Should().Be(string.Empty);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "careful";

    // Act
    var trait = Trait.Load(id, name);

    // Assert
    trait.Id.Should().Be(id);
    trait.Name.Should().Be(name);
  }

  #endregion
}
