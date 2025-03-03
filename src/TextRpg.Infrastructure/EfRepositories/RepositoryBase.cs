namespace TextRpg.Infrastructure.EfRepositories;

public abstract class RepositoryBase(ApplicationContext applicationContext)
{
  #region Properties

  /// <summary>
  ///   Gets the application context.
  /// </summary>
  protected ApplicationContext Context { get; } =
    applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

  #endregion

  #region Methods

  /// <summary>
  ///   Allows to save all changes into the <see cref="ApplicationContext" />.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token.</param>
  public Task SaveAsync(CancellationToken cancellationToken)
  {
    return Context.SaveChangesAsync(cancellationToken);
  }

  #endregion
}
