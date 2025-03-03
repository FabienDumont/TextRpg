using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class IncompatibleTraitDataModelTests
{
  #region Methods

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
    Assert.Equal(traitId, dataModel.TraitId);
    Assert.Equal(incompatibleTraitId, dataModel.IncompatibleTraitId);
  }

  #endregion
}
