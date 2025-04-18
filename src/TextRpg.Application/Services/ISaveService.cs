using TextRpg.Domain;

namespace TextRpg.Application.Services;

public interface ISaveService
{
  Task SaveGameAsync(GameSave save, CancellationToken ct = default);
  GameSave? LoadGame(string json);
}
