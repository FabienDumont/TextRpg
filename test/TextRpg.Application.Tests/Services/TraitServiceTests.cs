using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

public class TraitServiceTests
{
  private readonly ITraitRepository _repo = A.Fake<ITraitRepository>();
  private readonly ITraitService _service;

  public TraitServiceTests()
  {
    _service = new TraitService(_repo);
  }

  [Fact]
  public async Task GetAllTraitsAsync_ShouldCallRepositoryAndReturnResult()
  {
    // Arrange
    var traits = new List<Trait> {Trait.Load(Guid.NewGuid(), "Kind")};
    A.CallTo(() => _repo.GetAllAsync(A<CancellationToken>._)).Returns(traits);

    // Act
    var result = await _service.GetAllTraitsAsync(CancellationToken.None);

    // Assert
    result.Should().BeEquivalentTo(traits);
    A.CallTo(() => _repo.GetAllAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task GetCompatibleTraitsAsync_ShouldCallRepositoryAndReturnResult()
  {
    // Arrange
    var ids = new[] {Guid.NewGuid()};
    var traits = new List<Trait> {Trait.Load(Guid.NewGuid(), "Polite")};
    A.CallTo(() => _repo.GetCompatibleTraitsAsync(ids, A<CancellationToken>._)).Returns(traits);

    // Act
    var result = await _service.GetCompatibleTraitsAsync(ids, CancellationToken.None);

    // Assert
    result.Should().BeEquivalentTo(traits);
    A.CallTo(() => _repo.GetCompatibleTraitsAsync(ids, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }
}
