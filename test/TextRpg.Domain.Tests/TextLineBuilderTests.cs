using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Domain.Tests;

public class TextLineBuilderTests
{
  [Fact]
  public void BuildNarrationLine_ShouldReplacePlayerNameToken_WithCharacterName()
  {
    // Arrange
    var character = CharacterHelper.GetBasicPlayerCharacter();
    const string template = "Hello [PLAYERNAME], welcome back.";

    // Act
    var line = TextLineBuilder.BuildNarrationLine(template, character);

    // Assert
    Assert.Equal(3, line.TextParts.Count);

    Assert.Equal("Hello ", line.TextParts[0].Text);
    Assert.Null(line.TextParts[0].Color);

    Assert.Equal(character.Name, line.TextParts[1].Text);
    Assert.Equal("blue", line.TextParts[1].Color);

    Assert.Equal(", welcome back.", line.TextParts[2].Text);
    Assert.Null(line.TextParts[2].Color);
  }

  [Theory]
  [InlineData(BiologicalSex.Male, "blue")]
  [InlineData(BiologicalSex.Female, "pink")]
  [InlineData((BiologicalSex) 99, "purple")] // fallback case
  public void BuildNarrationLine_ShouldAssignCorrectColor_BasedOnSex(BiologicalSex sex, string expectedColor)
  {
    // Arrange
    var character = Character.Create("Jordan", 20, sex);
    const string template = "[PLAYERNAME] enters the room.";

    // Act
    var line = TextLineBuilder.BuildNarrationLine(template, character);

    // Assert
    var part = line.TextParts.First(p => p.Text == character.Name);
    Assert.Equal(expectedColor, part.Color);
  }

  [Fact]
  public void BuildNarrationLine_ShouldHandleTemplateWithoutTokens()
  {
    // Arrange
    var character = CharacterHelper.GetBasicPlayerCharacter();
    const string template = "No tokens here.";

    // Act
    var line = TextLineBuilder.BuildNarrationLine(template, character);

    // Assert
    Assert.Single(line.TextParts);
    Assert.Equal("No tokens here.", line.TextParts[0].Text);
    Assert.Null(line.TextParts[0].Color);
  }

  [Fact]
  public void BuildNarrationLine_ShouldPreserveUnknownTokensAsText()
  {
    // Arrange
    var character = CharacterHelper.GetBasicPlayerCharacter();
    const string template = "This is [UNKNOWN].";

    // Act
    var line = TextLineBuilder.BuildNarrationLine(template, character);

    // Assert
    Assert.Equal(3, line.TextParts.Count);
    Assert.Equal("This is ", line.TextParts[0].Text);
    Assert.Equal("[UNKNOWN]", line.TextParts[1].Text);
    Assert.Equal(".", line.TextParts[2].Text);
  }
}
