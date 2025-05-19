using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Application.Tests.Services;

public sealed class ExplorationActionResultNarrationServiceTests
{
  #region Methods

  [Fact]
  public async Task GetExplorationActionResultNarrationAsync_ShouldReturnNarration()
  {
    // Arrange
    var narrationId = Guid.NewGuid();
    var resultId = Guid.NewGuid();
    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 50;

    var expectedNarration = ExplorationActionResultNarration.Load(
      narrationId, resultId, "You're tired but manage to pull through."
    );

    var repo = A.Fake<IExplorationActionResultNarrationRepository>();
    A.CallTo(() => repo.GetByExplorationActionResultIdAsync(resultId, character, A<CancellationToken>._))
      .Returns(expectedNarration);

    var service = new ExplorationActionResultNarrationService(repo);

    // Act
    var result = await service.GetExplorationActionResultNarrationAsync(resultId, character, CancellationToken.None);

    // Assert
    Assert.Equal(expectedNarration, result);
  }

  #endregion
}
