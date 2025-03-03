using System.Text.Json;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.JsonRepositories;

public class GameSaveJsonRepository(string? customPath = null) : IGameSaveRepository
{
  private readonly string _saveDirectory = customPath ?? GetSavePath();

  public async Task SaveAsync(GameSave save, CancellationToken cancellationToken)
  {
    Directory.CreateDirectory(_saveDirectory);
    var path = Path.Combine(_saveDirectory, $"{save.Name}.json");

    var dataModel = save.ToDataModel();

    var json = JsonSerializer.Serialize(dataModel, new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync(path, json, cancellationToken);
  }

  public async Task<GameSave?> LoadAsync(string saveName, CancellationToken cancellationToken)
  {
    var path = Path.Combine(_saveDirectory, $"{saveName}.json");
    if (!File.Exists(path)) return null;

    var json = await File.ReadAllTextAsync(path, cancellationToken);
    var dataModel = JsonSerializer.Deserialize<GameSaveDataModel>(json);
    return dataModel?.ToDomain();
  }

  internal static string GetSavePath(string? exePathOverride = null)
  {
    var exePath = exePathOverride ?? AppContext.BaseDirectory;

    if (exePath.Contains(Path.Combine("resources", "app")))
    {
      var parent = Path.GetFullPath(Path.Combine(exePath, @"..\..\.."));
      return Path.Combine(parent, "Saves");
    }

    return Path.Combine(exePath, "Saves");
  }

}
