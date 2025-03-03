using TextRpg.Domain;

namespace TextRpg.Blazor.Stores;

/// <summary>
///   Stores and manages the current game save state for the UI layer.
/// </summary>
public class GameSaveStore
{
  /// <summary>
  ///   Event triggered when the game state changes.
  /// </summary>
  public event Action? OnChange;

  /// <summary>
  ///   The currently loaded game save.
  /// </summary>
  public GameSave? CurrentSave { get; private set; }

  /// <summary>
  ///   Loads the given game save and notifies listeners.
  /// </summary>
  public void LoadGame(GameSave save)
  {
    CurrentSave = save;
    NotifyStateChanged();
  }

  /// <summary>
  ///   Unloads the current game save and notifies listeners.
  /// </summary>
  public void UnloadGame()
  {
    CurrentSave = null;
    NotifyStateChanged();
  }

  /// <summary>
  ///   Triggers the <see cref="OnChange"/> event to notify subscribers.
  /// </summary>
  public void NotifyStateChanged() => OnChange?.Invoke();
}
