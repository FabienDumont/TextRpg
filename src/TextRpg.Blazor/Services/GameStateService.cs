using TextRpg.Domain;

namespace TextRpg.Blazor.Services;

public class GameStateService
{
  public GameSave? CurrentSave { get; private set; }

  public void LoadGame(GameSave save)
  {
    CurrentSave = save;
  }

  public void UnloadGame()
  {
    CurrentSave = null;
  }
}
