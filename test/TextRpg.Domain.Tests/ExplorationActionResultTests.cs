namespace TextRpg.Domain.Tests;

public class ExplorationActionResultTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var explorationActionId = Guid.NewGuid();

    // Act
    var result = ExplorationActionResult.Create(explorationActionId, null, null, null, null, false, null, null);

    // Assert
    Assert.NotNull(result);
    Assert.NotEqual(Guid.Empty, result.Id);
    Assert.Equal(explorationActionId, result.ExplorationActionId);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var explorationActionId = Guid.NewGuid();
    const int minEnergy = 20;
    const int maxEnergy = 30;
    const int minMoney = 5;
    const int maxMoney = 10;
    const int energyChange = -2;
    const int moneyChange = 200;

    // Act
    var result = ExplorationActionResult.Load(
      id, explorationActionId, minEnergy, maxEnergy, minMoney, maxMoney, true, energyChange, moneyChange
    );

    // Assert
    Assert.NotNull(result);
    Assert.Equal(id, result.Id);
    Assert.Equal(explorationActionId, result.ExplorationActionId);
    Assert.Equal(minEnergy, result.MinEnergy);
    Assert.Equal(maxEnergy, result.MaxEnergy);
    Assert.Equal(minMoney, result.MinMoney);
    Assert.Equal(maxMoney, result.MaxMoney);
    Assert.True(result.AddMinutes);
    Assert.Equal(energyChange, result.EnergyChange);
    Assert.Equal(moneyChange, result.MoneyChange);
  }

  #endregion
}
