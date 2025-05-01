namespace TextRpg.Domain.Tests.Helpers;

public static class CharacterHelper
{
  #region Methods

  public static Character GetBasicPlayerCharacter()
  {
    return Character.Create("Player", 18, BiologicalSex.Male);
  }

  public static Character GetRandomCharacter(string? name = null)
  {
    var random = new Random();
    var age = random.Next(18, 60);
    var sex = (BiologicalSex) random.Next(Enum.GetValues<BiologicalSex>().Length);

    return Character.Create(name ?? $"NPC_{Guid.NewGuid()}", age, sex);
  }

  #endregion
}
