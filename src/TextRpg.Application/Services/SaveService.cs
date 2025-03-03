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

  public async Task<GameSave?> LoadGameAsync(string saveName, CancellationToken ct = default)
  {
    return await saveRepo.LoadAsync(saveName, ct);
  }

  #endregion
}
