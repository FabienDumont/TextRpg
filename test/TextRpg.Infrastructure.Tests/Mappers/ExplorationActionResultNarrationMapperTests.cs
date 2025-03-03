using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class ExplorationActionResultNarrationMapperTests
{
  #region Methods

  [Fact]
  public void ToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var resultId = Guid.NewGuid();

    var dataModel = new ExplorationActionResultNarrationDataModel
    {
      Id = id,
      ExplorationActionResultId = resultId,
      MinEnergy = 15,
      MaxEnergy = 75,
      Text = "You feel halfway drained, but you sleep well."
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    Assert.NotNull(domain);
    Assert.Equal(id, domain.Id);
    Assert.Equal(resultId, domain.ExplorationActionResultId);
    Assert.Equal(15, domain.MinEnergy);
    Assert.Equal(75, domain.MaxEnergy);
    Assert.Equal("You feel halfway drained, but you sleep well.", domain.Text);
  }

  [Fact]
  public void ToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<ExplorationActionResultNarrationDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = Guid.NewGuid(),
        MinEnergy = null,
        MaxEnergy = 30,
        Text = "You crash hard into your bed."
      },
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = Guid.NewGuid(),
        MinEnergy = 31,
        MaxEnergy = null,
        Text = "You're not very tired but still lie down."
      }
    };

    // Act
    var domains = dataModels.ToDomainCollection();

    // Assert
    Assert.Equal(2, domains.Count);
    for (var i = 0; i < domains.Count; i++)
    {
      Assert.Equal(dataModels[i].Id, domains[i].Id);
      Assert.Equal(dataModels[i].ExplorationActionResultId, domains[i].ExplorationActionResultId);
      Assert.Equal(dataModels[i].MinEnergy, domains[i].MinEnergy);
      Assert.Equal(dataModels[i].MaxEnergy, domains[i].MaxEnergy);
      Assert.Equal(dataModels[i].Text, domains[i].Text);
    }
  }

  [Fact]
  public void ToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var resultId = Guid.NewGuid();
    var domain = ExplorationActionResultNarration.Load(id, resultId, 5, 95, "You're exhausted but find rest.");

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    Assert.NotNull(dataModel);
    Assert.Equal(id, dataModel.Id);
    Assert.Equal(resultId, dataModel.ExplorationActionResultId);
    Assert.Equal(5, dataModel.MinEnergy);
    Assert.Equal(95, dataModel.MaxEnergy);
    Assert.Equal("You're exhausted but find rest.", dataModel.Text);
  }

  [Fact]
  public void ToDataModelCollection_ShouldMapDomainCollectionToDataModelCollection()
  {
    // Arrange
    var domains = new List<ExplorationActionResultNarration>
    {
      ExplorationActionResultNarration.Load(Guid.NewGuid(), Guid.NewGuid(), 0, 25, "Dead tired."),
      ExplorationActionResultNarration.Load(Guid.NewGuid(), Guid.NewGuid(), 26, 100, "Mildly tired.")
    };

    // Act
    var dataModels = domains.ToDataModelCollection();

    // Assert
    Assert.Equal(2, dataModels.Count);
    for (var i = 0; i < dataModels.Count; i++)
    {
      Assert.Equal(domains[i].Id, dataModels[i].Id);
      Assert.Equal(domains[i].ExplorationActionResultId, dataModels[i].ExplorationActionResultId);
      Assert.Equal(domains[i].MinEnergy, dataModels[i].MinEnergy);
      Assert.Equal(domains[i].MaxEnergy, dataModels[i].MaxEnergy);
      Assert.Equal(domains[i].Text, dataModels[i].Text);
    }
  }

  #endregion
}
