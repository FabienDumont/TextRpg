using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class TraitRepositoryTests
{
  #region Fields

  private readonly ApplicationContext _context;

  private readonly List<IncompatibleTraitDataModel> _incompatibilities =
    [new() {TraitId = Guid.NewGuid(), IncompatibleTraitId = Guid.NewGuid()}];

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
    var result = await _repository.GetAllAsync(CancellationToken.None);

    Assert.NotNull(result);
    Assert.Equal(_traitDataModels.Count, result.Count);
    foreach (var trait in _traitDataModels)
    {
      Assert.Contains(result, r => r.Id == trait.Id);
    }
  }

  [Fact]
  public async Task GetCompatibleTraitsAsync_ShouldReturnAll_WhenNoSelectedTraits()
  {
    var result = await _repository.GetCompatibleTraitsAsync([], CancellationToken.None);

    Assert.NotNull(result);
    Assert.Equal(_traitDataModels.Count, result.Count);
  }

  [Fact]
  public async Task GetCompatibleTraitsAsync_ShouldExcludeIncompatible()
  {
    var traitA = _traitDataModels[0];
    var traitB = _traitDataModels[1];

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

    var result = await _repository.GetCompatibleTraitsAsync([traitA.Id], CancellationToken.None);

    Assert.NotNull(result);
    Assert.DoesNotContain(result, r => r.Id == traitA.Id);
    Assert.DoesNotContain(result, r => r.Id == traitB.Id);
  }

  #endregion
}
