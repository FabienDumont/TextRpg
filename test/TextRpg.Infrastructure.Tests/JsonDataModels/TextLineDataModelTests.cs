using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Tests.JsonDataModels;

public class TextLineDataModelTests
{
  #region Tests

  [Fact]
  public void TextLineDataModel_Should_InitializeWithDefaultValues()
  {
    // Act
    var model = new TextLineDataModel();

    // Assert
    model.TextParts.Should().NotBeNull();
    model.TextParts.Should().BeEmpty();
  }

  [Fact]
  public void TextLineDataModel_Should_Allow_Adding_TextParts()
  {
    // Arrange
    var textPart1 = new TextPartDataModel {Color = "blue", Text = "Daniel:"};
    var textPart2 = new TextPartDataModel {Color = "white", Text = "Hello, how are you?"};
    var model = new TextLineDataModel();

    // Act
    model.TextParts.Add(textPart1);
    model.TextParts.Add(textPart2);

    // Assert
    model.TextParts.Should().HaveCount(2);
    model.TextParts[0].Color.Should().Be("blue");
    model.TextParts[0].Text.Should().Be("Daniel:");
    model.TextParts[1].Color.Should().Be("white");
    model.TextParts[1].Text.Should().Be("Hello, how are you?");
  }

  [Fact]
  public void TextLineDataModel_Should_Initialize_TextParts_WithGivenValues()
  {
    // Arrange
    var textPart1 = new TextPartDataModel {Color = "green", Text = "NPC:"};
    var textPart2 = new TextPartDataModel {Color = "yellow", Text = "Greetings!"};

    // Act
    var model = new TextLineDataModel
    {
      TextParts = [textPart1, textPart2]
    };

    // Assert
    model.TextParts.Should().HaveCount(2);
    model.TextParts[0].Color.Should().Be("green");
    model.TextParts[0].Text.Should().Be("NPC:");
    model.TextParts[1].Color.Should().Be("yellow");
    model.TextParts[1].Text.Should().Be("Greetings!");
  }

  #endregion
}
