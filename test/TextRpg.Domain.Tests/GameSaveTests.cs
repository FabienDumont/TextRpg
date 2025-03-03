using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Domain.Tests;

public class GameSaveTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeCorrectly()
  {
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var world = World.Create(DateTime.Now, [playerCharacter]);

    var save = GameSave.Create("New Save", playerCharacter, world);

    Assert.NotNull(save);
    Assert.NotEqual(Guid.Empty, save.Id);
    Assert.Equal("New Save", save.Name);
    Assert.Equal(playerCharacter.Id, save.PlayerCharacterId);
    Assert.Equal(world, save.World);
    Assert.Equal(playerCharacter, save.PlayerCharacter);
    Assert.True(DateTime.UtcNow - save.SavedAt < TimeSpan.FromSeconds(1));
  }

  [Fact]
  public void Load_ShouldInitializeCorrectly()
  {
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var id = Guid.NewGuid();
    var name = "Save 1";
    var world = World.Create(DateTime.Now, [playerCharacter]);

    var save = GameSave.Load(id, name, playerCharacter.Id, world, []);

    Assert.Equal(id, save.Id);
    Assert.Equal(name, save.Name);
    Assert.Equal(playerCharacter.Id, save.PlayerCharacterId);
    Assert.Equal(world, save.World);
    Assert.Equal(playerCharacter, save.PlayerCharacter);
  }

  [Fact]
  public void Load_ShouldThrow_WhenPlayerCharacterNotFound()
  {
    var id = Guid.NewGuid();
    var name = "Save X";
    var wrongPlayerId = Guid.NewGuid();
    var world = World.Create(DateTime.Now, []);

    var ex = Assert.Throws<InvalidOperationException>(() => GameSave.Load(id, name, wrongPlayerId, world, []));

    Assert.Equal("Player character not found in character list.", ex.Message);
  }

  [Fact]
  public void Create_ShouldThrow_WhenPlayerIsNull()
  {
    var world = World.Create(DateTime.Now, []);

    var ex = Assert.Throws<ArgumentNullException>(() => GameSave.Create("Save Null", null!, world));

    Assert.Equal("playerCharacter", ex.ParamName);
  }

  [Fact]
  public void AddText_Should_Add_Text_With_Color_To_Save()
  {
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var id = Guid.NewGuid();
    const string name = "Save 1";
    var world = World.Create(DateTime.Now, [playerCharacter]);
    var save = GameSave.Load(id, name, playerCharacter.Id, world, []);

    save.AddText(
      [
        new TextPart("blue", "Daniel: "),
        new TextPart("white", "Hello!")
      ]
    );

    Assert.Single(save.TextLines);
    var line = save.TextLines.First();
    Assert.Equal(2, line.TextParts.Count);
    Assert.Equal("blue", line.TextParts[0].Color);
    Assert.Equal("Daniel: ", line.TextParts[0].Text);
    Assert.Equal("white", line.TextParts[1].Color);
    Assert.Equal("Hello!", line.TextParts[1].Text);
  }

  [Fact]
  public void ResetText_Should_Clear_All_TextLines()
  {
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var id = Guid.NewGuid();
    const string name = "Save 1";
    var world = World.Create(DateTime.Now, [playerCharacter]);
    var save = GameSave.Load(id, name, playerCharacter.Id, world, []);

    save.AddText(
      [
        new TextPart("blue", "Daniel: "),
        new TextPart("white", "Hello!")
      ]
    );

    save.ResetText();

    Assert.Empty(save.TextLines);
  }

  #endregion
}
