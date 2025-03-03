using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing saves.
/// </summary>
public class SaveService(
  IGameSaveRepository saveRepository, IWorldService worldService, INarrationService narrationService
) : ISaveService
{
  #region Implementation of ISaveService

  /// <inheritdoc />
  public async Task SaveGameAsync(GameSave save, CancellationToken ct = default)
  {
    await saveRepository.SaveAsync(save, ct);
  }

  /// <inheritdoc />
  public GameSave? LoadGame(string json)
  {
    return saveRepository.Load(json);
  }

  /// <inheritdoc />
  public async Task<GameSave> CreateNewSaveAsync(
    DateTime date, Character playerCharacter, GameSettings gameSettings, CancellationToken cancellationToken
  )
  {
    var world = await worldService.CreateNewWorldAsync(date, playerCharacter, gameSettings, cancellationToken);

    var save = GameSave.Create($"{playerCharacter.Name}_{world.CurrentDate:yyyyMMdd_HHmmss}", playerCharacter, world);

    var narrationText = await narrationService.GetNarrationTextByKeyAsync("GameIntro", cancellationToken);

    var line = TextLineBuilder.BuildNarrationLine(narrationText, save.PlayerCharacter);

    save.AddText(line.TextParts);

    return save;
  }

  #endregion
}
