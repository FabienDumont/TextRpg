﻿using TextRpg.Domain;
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
    var textLine = new TextLine(new List<TextPart> {textPart1, textPart2});

    // Act
    var dataModel = textLine.ToDataModel();

    // Assert
    Assert.NotNull(dataModel.TextParts);
    Assert.Equal(2, dataModel.TextParts.Count);
    Assert.Equal("blue", dataModel.TextParts[0].Color);
    Assert.Equal("Daniel: ", dataModel.TextParts[0].Text);
    Assert.Equal("white", dataModel.TextParts[1].Color);
    Assert.Equal("Hello!", dataModel.TextParts[1].Text);
  }

  [Fact]
  public void ToDomain_Should_Map_TextLineDataModel_To_TextLine()
  {
    // Arrange
    var textPartDataModel1 = new TextPartDataModel {Color = "blue", Text = "Daniel: "};
    var textPartDataModel2 = new TextPartDataModel {Color = "white", Text = "Hello!"};
    var dataModel = new TextLineDataModel
    {
      TextParts = new List<TextPartDataModel> {textPartDataModel1, textPartDataModel2}
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    Assert.NotNull(domain.TextParts);
    Assert.Equal(2, domain.TextParts.Count);
    Assert.Equal("blue", domain.TextParts[0].Color);
    Assert.Equal("Daniel: ", domain.TextParts[0].Text);
    Assert.Equal("white", domain.TextParts[1].Color);
    Assert.Equal("Hello!", domain.TextParts[1].Text);
  }

  [Fact]
  public void ToDomainCollection_Should_Map_List_Of_TextLineDataModels_To_TextLines()
  {
    // Arrange
    var textPartDataModel1 = new TextPartDataModel {Color = "blue", Text = "Daniel: "};
    var textPartDataModel2 = new TextPartDataModel {Color = "white", Text = "Hello!"};
    var dataModel1 = new TextLineDataModel
    {
      TextParts = new List<TextPartDataModel> {textPartDataModel1, textPartDataModel2}
    };
    var dataModel2 = new TextLineDataModel
    {
      TextParts = new List<TextPartDataModel> {textPartDataModel1}
    };

    var dataModels = new List<TextLineDataModel> {dataModel1, dataModel2};

    // Act
    var domainLines = dataModels.ToDomainCollection();

    // Assert
    Assert.Equal(2, domainLines.Count);
    Assert.Equal(2, domainLines[0].TextParts.Count);
    Assert.Single(domainLines[1].TextParts);
  }

  [Fact]
  public void ToDataModelCollection_Should_Map_List_Of_TextLines_To_TextLineDataModels()
  {
    // Arrange
    var textPart1 = new TextPart("blue", "Daniel: ");
    var textPart2 = new TextPart("white", "Hello!");
    var textLine1 = new TextLine(new List<TextPart> {textPart1, textPart2});
    var textLine2 = new TextLine(new List<TextPart> {textPart1});

    var domainLines = new List<TextLine> {textLine1, textLine2};

    // Act
    var dataModels = domainLines.ToDataModelCollection();

    // Assert
    Assert.Equal(2, dataModels.Count);
    Assert.Equal(2, dataModels[0].TextParts.Count);
    Assert.Single(dataModels[1].TextParts);
  }

  #endregion
}
