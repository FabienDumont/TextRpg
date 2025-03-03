using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class IncompatibleTraitDataModelTests
{
  [Fact]
  public void Instanciation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var traitId = Guid.NewGuid();
    var incompatibleTraitId = Guid.NewGuid();

    // Act
    var dataModel = new IncompatibleTraitDataModel
    {
      TraitId = traitId,
      IncompatibleTraitId = incompatibleTraitId
    };

    // Assert
    dataModel.TraitId.Should().Be(traitId);
    dataModel.IncompatibleTraitId.Should().Be(incompatibleTraitId);
  }

}
