using TextRpg.Domain;

namespace TextRpg.Blazor.Stores;

public class GameSaveStore
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
