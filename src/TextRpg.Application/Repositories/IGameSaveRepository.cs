using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

public interface IGameSaveRepository
{
  Task SaveAsync(GameSave save, CancellationToken cancellationToken);
  GameSave? Load(string json);
}
