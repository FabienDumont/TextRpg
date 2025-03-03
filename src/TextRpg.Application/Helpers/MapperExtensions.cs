namespace TextRpg.Application.Helpers;

public static class MapperExtensions
{
  #region Methods

  public static TResult Map<TInput, TResult>(this TInput input, Func<TInput, TResult> mapper)
  {
    return mapper(input);
  }

  public static List<TResult> MapCollection<TInput, TResult>(
    this IEnumerable<TInput> input, Func<TInput, TResult> mapper
  )
  {
    return input.Select(mapper).Distinct().ToList();
  }

  #endregion
}
