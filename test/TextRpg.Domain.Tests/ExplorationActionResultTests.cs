namespace TextRpg.Domain.Tests;

public class ExplorationActionResultTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var explorationActionId = Guid.NewGuid();
    var minEnergy = 10;
    var minMoney = 50;
    var description = "You dig up some old bones.";
    var energyChange = -5;
    var moneyChange = 100;

    // Act
    var result = ExplorationActionResult.Create(
      explorationActionId, minEnergy, minMoney, description, energyChange, moneyChange
    );

    // Assert
    result.Should().NotBeNull();
    result.Id.Should().NotBe(Guid.Empty);
    result.ExplorationActionId.Should().Be(explorationActionId);
    result.MinEnergy.Should().Be(minEnergy);
    result.MinMoney.Should().Be(minMoney);
    result.Description.Should().Be(description);
    result.EnergyChange.Should().Be(energyChange);
    result.MoneyChange.Should().Be(moneyChange);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var explorationActionId = Guid.NewGuid();
    var minEnergy = 20;
    var minMoney = 5;
    var description = "You find a hidden stash.";
    var energyChange = -2;
    var moneyChange = 200;

    // Act
    var result = ExplorationActionResult.Load(
      id, explorationActionId, minEnergy, minMoney, description, energyChange, moneyChange
    );

    // Assert
    result.Should().NotBeNull();
    result.Id.Should().Be(id);
    result.ExplorationActionId.Should().Be(explorationActionId);
    result.MinEnergy.Should().Be(minEnergy);
    result.MinMoney.Should().Be(minMoney);
    result.Description.Should().Be(description);
    result.EnergyChange.Should().Be(energyChange);
    result.MoneyChange.Should().Be(moneyChange);
  }

  #endregion
}
