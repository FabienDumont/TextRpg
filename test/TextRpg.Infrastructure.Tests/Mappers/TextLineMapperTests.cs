using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class TextLineMapperTests
{
  #region Methods

  [Fact]
  public void ToDataModel_Should_Map_TextLine_To_TextLineDataModel()
  {
    // Arrange
    var textPart1 = new TextPart("blue", "Daniel: ");
    var textPart2 = new TextPart("white", "Hello!");
    var textLine = new TextLine([textPart1, textPart2]);

    // Act
    var dataModel = textLine.ToDataModel();

    // Assert
    dataModel.TextParts.Should().HaveCount(2);
    dataModel.TextParts[0].Color.Should().Be("blue");
    dataModel.TextParts[0].Text.Should().Be("Daniel: ");
    dataModel.TextParts[1].Color.Should().Be("white");
    dataModel.TextParts[1].Text.Should().Be("Hello!");
  }

  [Fact]
  public void ToDomain_Should_Map_TextLineDataModel_To_TextLine()
  {
    // Arrange
    var textPartDataModel1 = new TextPartDataModel {Color = "blue", Text = "Daniel: "};
    var textPartDataModel2 = new TextPartDataModel {Color = "white", Text = "Hello!"};
    var dataModel = new TextLineDataModel
    {
      TextParts = [textPartDataModel1, textPartDataModel2]
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.TextParts.Should().HaveCount(2);
    domain.TextParts[0].Color.Should().Be("blue");
    domain.TextParts[0].Text.Should().Be("Daniel: ");
    domain.TextParts[1].Color.Should().Be("white");
    domain.TextParts[1].Text.Should().Be("Hello!");
  }

  [Fact]
  public void ToDomainCollection_Should_Map_List_Of_TextLineDataModels_To_TextLines()
  {
    // Arrange
    var textPartDataModel1 = new TextPartDataModel {Color = "blue", Text = "Daniel: "};
    var textPartDataModel2 = new TextPartDataModel {Color = "white", Text = "Hello!"};
    var dataModel1 = new TextLineDataModel
    {
      TextParts = [textPartDataModel1, textPartDataModel2]
    };
    var dataModel2 = new TextLineDataModel
    {
      TextParts = [textPartDataModel1]
    };

    var dataModels = new List<TextLineDataModel> {dataModel1, dataModel2};

    // Act
    var domainLines = dataModels.ToDomainCollection();

    // Assert
    domainLines.Should().HaveCount(2);
    domainLines[0].TextParts.Should().HaveCount(2);
    domainLines[1].TextParts.Should().HaveCount(1);
  }

  [Fact]
  public void ToDataModelCollection_Should_Map_List_Of_TextLines_To_TextLineDataModels()
  {
    // Arrange
    var textPart1 = new TextPart("blue", "Daniel: ");
    var textPart2 = new TextPart("white", "Hello!");
    var textLine1 = new TextLine([textPart1, textPart2]);
    var textLine2 = new TextLine([textPart1]);

    var domainLines = new List<TextLine> {textLine1, textLine2};

    // Act
    var dataModels = domainLines.ToDataModelCollection();

    // Assert
    dataModels.Should().HaveCount(2);
    dataModels[0].TextParts.Should().HaveCount(2);
    dataModels[1].TextParts.Should().HaveCount(1);
  }

  #endregion
}
