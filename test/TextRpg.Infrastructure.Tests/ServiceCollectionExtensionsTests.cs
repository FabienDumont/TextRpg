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
    context.Should().NotBeNull();
    context.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");

    var options = provider.GetRequiredService<DbContextOptions<ApplicationContext>>();
    var extension = options.Extensions.FirstOrDefault(e => e.GetType().Name.Contains(
        "SqliteOptionsExtension", StringComparison.OrdinalIgnoreCase
      )
    );
    extension.Should().NotBeNull();
  }

  [Fact]
  public void AddInfrastructure_ShouldRegisterAllRepositories()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddInfrastructure("test.db");
    var provider = services.BuildServiceProvider();

    // Assert TraitRepository
    var traitRepo = provider.GetService<ITraitRepository>();
    traitRepo.Should().NotBeNull().And.BeOfType<TraitRepository>();

    // Assert GreetingRepository
    var greetingRepo = provider.GetService<IGreetingRepository>();
    greetingRepo.Should().NotBeNull().And.BeOfType<GreetingRepository>();

    // Assert GameSaveJsonRepository
    var saveRepo = provider.GetService<IGameSaveRepository>();
    saveRepo.Should().NotBeNull().And.BeOfType<GameSaveJsonRepository>();

    // Assert NameJsonRepository
    var nameRepo = provider.GetService<INameRepository>();
    nameRepo.Should().NotBeNull().And.BeOfType<NameJsonRepository>();
  }

  #endregion
}
