using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class NarrationDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string key = "intro_scene";
    const string text = "You wake up in a strange place.";

    // Act
    var narration = new NarrationDataModel
    {
      Id = id,
      Key = key,
      Text = text
    };

    // Assert
    narration.Id.Should().Be(id);
    narration.Key.Should().Be(key);
    narration.Text.Should().Be(text);
  }

  #endregion
}
