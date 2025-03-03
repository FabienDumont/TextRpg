using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class RoomRepositoryTests
{
  #region Fields

  private readonly RoomRepository _repository;
  private readonly ApplicationContext _context;
  private readonly List<RoomDataModel> _roomDataModels;
  private readonly Guid _locationId = Guid.NewGuid();

  #endregion

  #region Ctors

  public RoomRepositoryTests()
  {
    _context = A.Fake<ApplicationContext>();

    _roomDataModels =
    [
      new() {Id = Guid.NewGuid(), LocationId = _locationId, Name = "Bedroom", IsEntryPoint = false},
      new() {Id = Guid.NewGuid(), LocationId = _locationId, Name = "Living room", IsEntryPoint = true}
    ];

    var roomDbSet = _roomDataModels.AsQueryable().BuildMockDbSet();

    A.CallTo(() => _context.Rooms).Returns(roomDbSet);
    A.CallTo(() => _context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new RoomRepository(_context);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetLocationEntryPointAsync_ShouldReturnEntryPoint()
  {
    var result = await _repository.GetLocationEntryPointAsync(_locationId, CancellationToken.None);
    result.Should().NotBeNull();
  }

  [Fact]
  public async Task GetLocationEntryPointAsync_ShouldReturnNull()
  {
    var result = await _repository.GetLocationEntryPointAsync(Guid.NewGuid(), CancellationToken.None);
    result.Should().BeNull();
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnRoom_WhenRoomExists()
  {
    var roomId = _roomDataModels.First().Id;

    A.CallTo(() => _context.Rooms.FindAsync(
        A<object[]>.That.Matches(keys => (Guid) keys[0] == roomId), A<CancellationToken>._
      )
    ).Returns(new ValueTask<RoomDataModel?>(_roomDataModels.First(r => r.Id == roomId)));

    var result = await _repository.GetByIdAsync(roomId, CancellationToken.None);

    result.Should().NotBeNull();
    result.Id.Should().Be(roomId);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldThrow_WhenRoomDoesNotExist()
  {
    var randomId = Guid.NewGuid();

    A.CallTo(() => _context.Rooms.FindAsync(
        A<object[]>.That.Matches(keys => (Guid) keys[0] == randomId), A<CancellationToken>._
      )
    ).Returns(new ValueTask<RoomDataModel?>((RoomDataModel?) null));

    Func<Task> act = async () => await _repository.GetByIdAsync(randomId, CancellationToken.None);

    await act.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Room with ID {randomId} was not found.");
  }

  #endregion
}
