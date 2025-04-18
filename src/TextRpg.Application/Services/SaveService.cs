using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

public class SaveService(IGameSaveRepository saveRepo) : ISaveService
{
  #region Implementation of ISaveService

  public async Task SaveGameAsync(GameSave save, CancellationToken ct = default)
  {
    await saveRepo.SaveAsync(save, ct);
  }

  public GameSave? LoadGame(string json)
  {
    return saveRepo.Load(json);
  }

  #endregion
}
