using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Repositories;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests;

public class ServiceCollectionExtensionsTests
{
  [Fact]
  public void AddInfrastructure_ShouldRegisterApplicationContext_WithSqliteProvider()
  {
    // Arrange
    var services = new ServiceCollection();
    var dbFileName = "test.db";

    // Act
    services.AddInfrastructureEf(dbFileName);
    var provider = services.BuildServiceProvider();

    var context = provider.GetRequiredService<ApplicationContext>();
    context.Should().NotBeNull();

    context.Database.ProviderName.Should().Be("Microsoft.EntityFrameworkCore.Sqlite");

    var options = provider.GetRequiredService<DbContextOptions<ApplicationContext>>();
    var extension = options.Extensions.FirstOrDefault(e =>
      e.GetType().Name.Contains("SqliteOptionsExtension", StringComparison.OrdinalIgnoreCase));

    extension.Should().NotBeNull("EF should configure SqliteOptionsExtension internally");
  }


  [Fact]
  public void AddInfrastructure_ShouldRegisterRepositories()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddInfrastructureEf("test.db");
    var provider = services.BuildServiceProvider();

    // Assert: trait repo
    var traitRepo = provider.GetService<ITraitRepository>();
    traitRepo.Should().NotBeNull();
    traitRepo.Should().BeOfType<TraitRepository>();

    // Assert: greeting repo
    var greetingRepo = provider.GetService<IGreetingRepository>();
    greetingRepo.Should().NotBeNull();
    greetingRepo.Should().BeOfType<GreetingRepository>();
  }
}