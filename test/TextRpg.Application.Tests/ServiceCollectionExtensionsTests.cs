using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Repositories;
using TextRpg.Application.Services;

namespace TextRpg.Application.Tests;

public class ServiceCollectionExtensionsTests
{
  [Fact]
  public void AddApplication_ShouldRegisterTraitServiceAsSingleton()
  {
    // Arrange
    IServiceCollection services = new ServiceCollection();
    var fakeRepo = A.Fake<ITraitRepository>();
    services.AddSingleton(fakeRepo);

    // Act
    services.AddApplication();
    var provider = services.BuildServiceProvider();

    // Assert
    var service1 = provider.GetService<ITraitService>();
    var service2 = provider.GetService<ITraitService>();

    service1.Should().NotBeNull();
    service1.Should().BeOfType<TraitService>();
    service1.Should().BeSameAs(service2); // Singleton check
  }

}
