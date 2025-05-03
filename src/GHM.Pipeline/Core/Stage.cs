namespace GHM.Pipeline;

/// <summary>
/// Represents an abstract stage in a pipeline that processes data of type <typeparamref name="TData"/>.
/// </summary>
/// <typeparam name="TData">The type of data associated with the stage.</typeparam>
public abstract class Stage<TData>
    where TData : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Stage{TData}"/> class with the specified data.
    /// </summary>
    /// <param name="data">The data associated with the stage.</param>
    protected Stage(TData data)
    {
        Data = data;
    }

    /// <summary>
    /// Gets the collection of steps in the stage as a read-only collection.
    /// </summary>
    public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();

    /// <summary>
    /// Gets the most critical status among all steps in the stage.
    /// </summary>
    public Status Status => GetStatusMoreCritical();

    /// <summary>
    /// Gets a value indicating whether the stage is successful (i.e., no errors).
    /// </summary>
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Gets a value indicating whether the stage contains any steps with an error status.
    /// </summary>
    public bool IsError => _steps.Any(step => step.Status == Status.Error);

    /// <summary>
    /// Gets a value indicating whether the stage contains any steps with a canceled or error status.
    /// </summary>
    public bool IsErrorOrCanceled => _steps.Any(step => step.Status is Status.Canceled or Status.Error);

    /// <summary>
    /// Gets a value indicating whether the stage contains any steps with a canceled status.
    /// </summary>
    public bool IsCanceled => _steps.Any(step => step.Status is Status.Canceled);

    /// <summary>
    /// Gets the data associated with the stage.
    /// </summary>
    public TData Data { get; }

    /// <summary>
    /// Adds a collection of steps to the stage.
    /// </summary>
    /// <param name="steps">The steps to add.</param>
    public void AddRangeSteps(IEnumerable<Step> steps) => _steps.AddRange(steps);

    /// <summary>
    /// Adds a single step to the stage.
    /// </summary>
    /// <param name="step">The step to add.</param>
    public void AddStep(Step step) => _steps.Add(step);

    /// <summary>
    /// Adds a canceled step to the stage with the specified message and name.
    /// </summary>
    /// <param name="message">The message for the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    public void AddCanceled(string message, string name = "Generic.Step") => _steps.Add(Step.Canceled(message, name));

    /// <summary>
    /// Adds an error step to the stage with the specified message and name.
    /// </summary>
    /// <param name="message">The message for the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    public void AddError(string message, string name = "Generic.Step") => _steps.Add(Step.Error(message, name));

    /// <summary>
    /// Adds an in-adjustment step to the stage with the specified message and name.
    /// </summary>
    /// <param name="message">The message for the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    public void AddInAdjustment(string message, string name = "Generic.Step") =>
        _steps.Add(Step.InAdjustment(message, name));

    /// <summary>
    /// Adds an in-progress step to the stage with the specified message and name.
    /// </summary>
    /// <param name="message">The message for the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    public void AddInProgress(string message, string name = "Generic.Step") => _steps.Add(Step.InProgress(message, name));

    /// <summary>
    /// Adds a success step to the stage with the specified message and name.
    /// </summary>
    /// <param name="message">The message for the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    public void AddSuccess(string message, string name = "Generic.Step") => _steps.Add(Step.Success(message, name));

    /// <summary>
    /// Adds an informational step to the stage with the specified message and name.
    /// </summary>
    /// <param name="message">The message for the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    public void AddInformation(string message, string name = "Generic.Step") => _steps.Add(Step.Information(message, name));

    /// <summary>
    /// Gets all steps in the stage that match the specified status.
    /// </summary>
    /// <param name="status">The status to filter steps by.</param>
    /// <returns>An enumerable of steps with the specified status.</returns>
    public IEnumerable<Step> GetStepsByStatus(Status status) => _steps.Where(step => step.Status == status);

    /// <summary>
    /// Determines the most critical status among all steps in the stage.
    /// </summary>
    /// <returns>The most critical status, or <see cref="Status.Default"/> if there are no steps.</returns>
    private Status GetStatusMoreCritical()
    {
        if (_steps.Count == 0)
        {
            return Status.Default;
        }
        return _steps.MaxBy(x => (int)x.Status).Status;
    }

    private readonly List<Step> _steps = new();
}
