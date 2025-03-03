using TextRpg.Domain;

namespace TextRpg.Application.Services;

public interface ISaveService
{
  Task SaveGameAsync(GameSave save, CancellationToken ct = default);
  Task<GameSave?> LoadGameAsync(string saveName, CancellationToken ct = default);
}
