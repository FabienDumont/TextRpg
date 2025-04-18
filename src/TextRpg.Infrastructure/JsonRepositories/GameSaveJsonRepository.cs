using System.Text.Json;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.JsonRepositories;

public class GameSaveJsonRepository(string? customPath = null) : IGameSaveRepository
{

  public async Task SaveAsync(GameSave save, CancellationToken cancellationToken)
  {
    var saveDirectory = customPath ?? GetSavePath(save.PlayerCharacter.Name);
    Directory.CreateDirectory(saveDirectory);
    var path = Path.Combine(saveDirectory, $"{save.Name}.json");

    var dataModel = save.ToDataModel();

    var json = JsonSerializer.Serialize(dataModel, new JsonSerializerOptions {WriteIndented = true});
    await File.WriteAllTextAsync(path, json, cancellationToken);
  }

  public GameSave? Load(string json)
  {
    try
    {
      var dataModel = JsonSerializer.Deserialize<GameSaveDataModel>(json);
      return dataModel?.ToDomain();
    }
    catch (JsonException)
    {
      return null;
    }
  }

  internal static string GetSavePath(string characterName, string? exePathOverride = null)
  {
    var exePath = exePathOverride ?? AppContext.BaseDirectory;

    if (exePath.Contains(Path.Combine("resources", "app")))
    {
      var parent = Path.GetFullPath(Path.Combine(exePath, @"..\..\.."));
      return Path.Combine(parent, $@"Saves\{characterName}");
    }

    return Path.Combine(exePath, $@"Saves\{characterName}");
  }
}
