using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Services;

namespace TextRpg.Application;

public static class ServiceCollectionExtensions
{
  #region Methods

  public static void AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ITraitService, TraitService>();
    services.AddScoped<ISaveService, SaveService>();
  }

  #endregion
}
