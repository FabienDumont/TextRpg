namespace TextRpg.Domain.Tests;

public class GameSaveTests
{
    [Fact]
    public void Create_ShouldInitializeCorrectly()
    {
        // Arrange
        var player = Character.Create("Hero");

        // Act
        var save = GameSave.Create("New Save", player);

        // Assert
        save.Should().NotBeNull();
        save.Id.Should().NotBe(Guid.Empty);
        save.Name.Should().Be("New Save");
        save.PlayerCharacterId.Should().Be(player.Id);
        save.Characters.Should().ContainSingle().Which.Should().Be(player);
        save.PlayerCharacter.Should().Be(player);
        save.SavedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Load_ShouldInitializeCorrectly()
    {
        // Arrange
        var player = Character.Create("Player");
        var characters = new List<Character> { player };
        var id = Guid.NewGuid();
        var name = "Save 1";

        // Act
        var save = GameSave.Load(id, name, player.Id, characters);

        // Assert
        save.Id.Should().Be(id);
        save.Name.Should().Be(name);
        save.PlayerCharacterId.Should().Be(player.Id);
        save.Characters.Should().ContainSingle().Which.Should().Be(player);
        save.PlayerCharacter.Should().Be(player);
    }

    [Fact]
    public void Load_ShouldThrow_WhenPlayerCharacterNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Save X";
        var wrongPlayerId = Guid.NewGuid();
        var characters = new List<Character> { Character.Create("NPC") };

        // Act
        var act = () => GameSave.Load(id, name, wrongPlayerId, characters);

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("Player character not found in character list.");
    }

    [Fact]
    public void Create_ShouldThrow_WhenPlayerIsNull()
    {
        // Act
        var act = () => GameSave.Create("Save Null", null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("playerCharacter");
    }
}
