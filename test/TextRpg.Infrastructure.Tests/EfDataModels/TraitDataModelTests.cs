using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class TraitDataModelTests
{
  [Fact]
  public void Instanciation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Brave";

    // Act
    var trait = new TraitDataModel
    {
      Id = id,
      Name = name
    };

    // Assert
    trait.Id.Should().Be(id);
    trait.Name.Should().Be(name);
  }
}
