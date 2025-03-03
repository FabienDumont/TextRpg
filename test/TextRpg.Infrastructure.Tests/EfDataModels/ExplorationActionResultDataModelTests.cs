using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class ExplorationActionResultDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var resultId = Guid.NewGuid();
    var actionId = Guid.NewGuid();

    const int minEnergy = 10;
    const int maxEnergy = 50;
    const int minMoney = 5;
    const int maxMoney = 100;
    const int energyChange = -5;
    const int moneyChange = 10;

    var action = new ExplorationActionDataModel
    {
      Id = actionId,
      LocationId = Guid.NewGuid(),
      RoomId = null,
      Label = "Search the area",
      NeededMinutes = 15
    };

    // Act
    var result = new ExplorationActionResultDataModel
    {
      Id = resultId,
      ExplorationActionId = actionId,
      MinEnergy = minEnergy,
      MaxEnergy = maxEnergy,
      MinMoney = minMoney,
      MaxMoney = maxMoney,
      AddMinutes = true,
      EnergyChange = energyChange,
      MoneyChange = moneyChange,
      ExplorationAction = action
    };

    // Assert
    Assert.Equal(resultId, result.Id);
    Assert.Equal(actionId, result.ExplorationActionId);
    Assert.Equal(minEnergy, result.MinEnergy);
    Assert.Equal(maxEnergy, result.MaxEnergy);
    Assert.Equal(minMoney, result.MinMoney);
    Assert.Equal(maxMoney, result.MaxMoney);
    Assert.True(result.AddMinutes);
    Assert.Equal(energyChange, result.EnergyChange);
    Assert.Equal(moneyChange, result.MoneyChange);
    Assert.Equal(action, result.ExplorationAction);
  }

  [Fact]
  public void Instantiation_ShouldAllowNullOptionalValues()
  {
    // Arrange
    var result = new ExplorationActionResultDataModel
    {
      Id = Guid.NewGuid(),
      ExplorationActionId = Guid.NewGuid()
    };

    // Assert
    Assert.Null(result.MinEnergy);
    Assert.Null(result.MaxEnergy);
    Assert.Null(result.MinMoney);
    Assert.Null(result.MaxMoney);
    Assert.Null(result.EnergyChange);
    Assert.Null(result.MoneyChange);
    Assert.Null(result.ExplorationAction);
  }

  #endregion
}
