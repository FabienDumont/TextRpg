namespace TextRpg.Domain.Tests;

public class TextLineTests
{
  [Fact]
  public void Constructor_ShouldInitializeWithTextParts()
  {
    // Arrange
    var initialParts = new List<TextPart>
    {
      new("red", "Hello"),
      new("green", " world")
    };

    // Act
    var textLine = new TextLine(initialParts);

    // Assert
    textLine.TextParts.Should().BeEquivalentTo(initialParts);
  }

  [Fact]
  public void AddTextPart_ShouldAddPartWithSpecifiedColor()
  {
    // Arrange
    var textLine = new TextLine(new List<TextPart>());

    // Act
    textLine.AddTextPart("Hello", "blue");

    // Assert
    textLine.TextParts.Should().ContainSingle();
    textLine.TextParts[0].Text.Should().Be("Hello");
    textLine.TextParts[0].Color.Should().Be("blue");
  }

  [Fact]
  public void AddTextPart_ShouldAddPartWithNullColor()
  {
    // Arrange
    var textLine = new TextLine([]);

    // Act
    textLine.AddTextPart("Narration");

    // Assert
    textLine.TextParts.Should().ContainSingle();
    textLine.TextParts[0].Text.Should().Be("Narration");
    textLine.TextParts[0].Color.Should().BeNull();
  }
}
