using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class TraitRepositoryTests
{
  #region Fields

  private readonly ApplicationContext _context;

  private readonly List<IncompatibleTraitDataModel> _incompatibilities =
  [
    new() {TraitId = Guid.NewGuid(), IncompatibleTraitId = Guid.NewGuid()}
  ];

  private readonly TraitRepository _repository;

  private readonly List<TraitDataModel> _traitDataModels =
  [
    new() {Id = Guid.NewGuid(), Name = "Brave"},
    new() {Id = Guid.NewGuid(), Name = "Shy"},
    new() {Id = Guid.NewGuid(), Name = "Aggressive"}
  ];

  #endregion

  #region Ctors

  public TraitRepositoryTests()
  {
    _context = A.Fake<ApplicationContext>();

    // Fake DbSet<T>
    var traitDbSet = _traitDataModels.AsQueryable().BuildMockDbSet();
    var incompatibleDbSet = _incompatibilities.AsQueryable().BuildMockDbSet();

    A.CallTo(() => _context.Traits).Returns(traitDbSet);
    A.CallTo(() => _context.IncompatibleTraits).Returns(incompatibleDbSet);
    A.CallTo(() => _context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new TraitRepository(_context);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetAllAsync_ShouldReturnMappedTraits()
  {
    // Act
    var result = await _repository.GetAllAsync(CancellationToken.None);

    // Assert
    result.Should().HaveCount(_traitDataModels.Count);
    result.Select(x => x.Id).Should().BeEquivalentTo(_traitDataModels.Select(t => t.Id));
  }

  [Fact]
  public async Task GetCompatibleTraitsAsync_ShouldReturnAll_WhenNoSelectedTraits()
  {
    // Act
    var result = await _repository.GetCompatibleTraitsAsync([], CancellationToken.None);

    // Assert
    result.Should().HaveCount(_traitDataModels.Count);
  }

  [Fact]
  public async Task GetCompatibleTraitsAsync_ShouldExcludeIncompatible()
  {
    // Arrange
    var traitA = _traitDataModels[0]; // selected
    var traitB = _traitDataModels[1]; // incompatible

    _incompatibilities.Clear();
    _incompatibilities.Add(
      new IncompatibleTraitDataModel
      {
        TraitId = traitA.Id,
        IncompatibleTraitId = traitB.Id
      }
    );

    var newTraitDb = _traitDataModels.AsQueryable().BuildMockDbSet();
    var newIncompatibleDb = _incompatibilities.AsQueryable().BuildMockDbSet();

    A.CallTo(() => _context.Traits).Returns(newTraitDb);
    A.CallTo(() => _context.IncompatibleTraits).Returns(newIncompatibleDb);

    // Act
    var result = await _repository.GetCompatibleTraitsAsync([traitA.Id], CancellationToken.None);

    // Assert
    result.Should().NotContain(t => t.Id == traitA.Id || t.Id == traitB.Id);
  }

  #endregion
}
