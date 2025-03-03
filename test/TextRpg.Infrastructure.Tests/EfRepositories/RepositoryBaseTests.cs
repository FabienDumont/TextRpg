using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class RepositoryBaseTests
{
  private class TestRepository(ApplicationContext context) : RepositoryBase(context);

  [Fact]
  public void Ctor_ShouldThrowArgumentNullException_WhenContextIsNull()
  {
    // Act
    Action act = () => _ = new TestRepository(null!);

    // Assert
    act.Should().Throw<ArgumentNullException>().WithParameterName("context");
  }

  [Fact]
  public async Task SaveAsync_ShouldCallContextSaveChangesAsync()
  {
    // Arrange
    var context = A.Fake<ApplicationContext>();
    var cancellationToken = CancellationToken.None;

    var repo = new TestRepository(context);

    // Act
    await repo.SaveAsync(cancellationToken);

    // Assert
    A.CallTo(() => context.SaveChangesAsync(cancellationToken)).MustHaveHappenedOnceExactly();
  }
}
