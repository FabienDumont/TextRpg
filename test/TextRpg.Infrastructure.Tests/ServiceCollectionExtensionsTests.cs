using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Repositories;
using TextRpg.Infrastructure.EfRepositories;
using TextRpg.Infrastructure.JsonRepositories;

namespace TextRpg.Infrastructure.Tests;

public class ServiceCollectionExtensionsTests
{
  #region Methods

  [Fact]
  public void AddInfrastructure_ShouldRegisterApplicationContext_WithSqliteProvider()
  {
    // Arrange
    var services = new ServiceCollection();
    var dbFileName = "test.db";

    // Act
    services.AddInfrastructure(dbFileName);
    var provider = services.BuildServiceProvider();

    // Assert DbContext
    var context = provider.GetRequiredService<ApplicationContext>();
    Assert.NotNull(context);
    Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", context.Database.ProviderName);

    var options = provider.GetRequiredService<DbContextOptions<ApplicationContext>>();
    var extension = options.Extensions.FirstOrDefault(e => e.GetType().Name.Contains(
        "SqliteOptionsExtension", StringComparison.OrdinalIgnoreCase
      )
    );
    Assert.NotNull(extension);
  }

  [Fact]
  public void AddInfrastructure_ShouldRegisterAllRepositories()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddInfrastructure("test.db");
    var provider = services.BuildServiceProvider();

    // TraitRepository
    var traitRepo = provider.GetService<ITraitRepository>();
    Assert.NotNull(traitRepo);
    Assert.IsType<TraitRepository>(traitRepo);

    // GreetingRepository
    var greetingRepo = provider.GetService<IGreetingRepository>();
    Assert.NotNull(greetingRepo);
    Assert.IsType<GreetingRepository>(greetingRepo);

    // GameSaveJsonRepository
    var saveRepo = provider.GetService<IGameSaveRepository>();
    Assert.NotNull(saveRepo);
    Assert.IsType<GameSaveJsonRepository>(saveRepo);

    // NameJsonRepository
    var nameRepo = provider.GetService<INameRepository>();
    Assert.NotNull(nameRepo);
    Assert.IsType<NameJsonRepository>(nameRepo);
  }

  #endregion
}
