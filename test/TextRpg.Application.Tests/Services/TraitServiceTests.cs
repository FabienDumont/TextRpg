using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class TraitServiceTests
{
  #region Fields

  private readonly ITraitRepository _traitRepository = A.Fake<ITraitRepository>();
  private readonly TraitService _traitService;

  #endregion

  #region Ctors

  public TraitServiceTests()
  {
    _traitService = new TraitService(_traitRepository);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetAllTraitsAsync_ShouldCallRepositoryAndReturnResult()
  {
    // Arrange
    var traits = new List<Trait> {Trait.Load(Guid.NewGuid(), "Kind")};
    A.CallTo(() => _traitRepository.GetAllAsync(A<CancellationToken>._)).Returns(traits);

    // Act
    var result = await _traitService.GetAllTraitsAsync(CancellationToken.None);

    // Assert
    result.Should().BeEquivalentTo(traits);
    A.CallTo(() => _traitRepository.GetAllAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task GetCompatibleTraitsAsync_ShouldCallRepositoryAndReturnResult()
  {
    // Arrange
    var ids = new[] {Guid.NewGuid()};
    var traits = new List<Trait> {Trait.Load(Guid.NewGuid(), "Polite")};
    A.CallTo(() => _traitRepository.GetCompatibleTraitsAsync(ids, A<CancellationToken>._)).Returns(traits);

    // Act
    var result = await _traitService.GetCompatibleTraitsAsync(ids, CancellationToken.None);

    // Assert
    result.Should().BeEquivalentTo(traits);
    A.CallTo(() => _traitRepository.GetCompatibleTraitsAsync(ids, A<CancellationToken>._))
      .MustHaveHappenedOnceExactly();
  }

  #endregion
}
