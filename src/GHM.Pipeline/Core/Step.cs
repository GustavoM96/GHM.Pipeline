namespace GHM.Pipeline;

/// <summary>
/// Represents a step in a pipeline with a specific status, message, and name.
/// </summary>
public readonly struct Step
{
    /// <summary>E
    /// Gets the name of the step.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the message associated with the step.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the status of the step.
    /// </summary>
    public Status Status { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Step"/> struct with the specified status, message, and name.
    /// </summary>
    /// <param name="status">The status of the step.</param>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step.</param>
    private Step(Status status, string message, string name)
    {
        Status = status;
        Message = message;
        Name = name;
    }

    /// <summary>
    /// Creates a step with a status of <see cref="Status.Canceled"/>.
    /// </summary>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    /// <returns>A new <see cref="Step"/> instance with a canceled status.</returns>
    public static Step Canceled(string message, string name = "Generic.Step") => new(Status.Canceled, message, name);

    /// <summary>
    /// Creates a step with a status of <see cref="Status.Error"/>.
    /// </summary>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    /// <returns>A new <see cref="Step"/> instance with an error status.</returns>
    public static Step Error(string message, string name = "Generic.Step") => new(Status.Error, message, name);

    /// <summary>
    /// Creates a step with a status of <see cref="Status.InAdjustment"/>.
    /// </summary>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    /// <returns>A new <see cref="Step"/> instance with an in-adjustment status.</returns>
    public static Step InAdjustment(string message, string name = "Generic.Step") => new(Status.InAdjustment, message, name);

    /// <summary>
    /// Creates a step with a status of <see cref="Status.InProgress"/>.
    /// </summary>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    /// <returns>A new <see cref="Step"/> instance with an in-progress status.</returns>
    public static Step InProgress(string message, string name = "Generic.Step") => new(Status.InProgress, message, name);

    /// <summary>
    /// Creates a step with a status of <see cref="Status.Success"/>.
    /// </summary>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    /// <returns>A new <see cref="Step"/> instance with a success status.</returns>
    public static Step Success(string message, string name = "Generic.Step") => new(Status.Success, message, name);

    /// <summary>
    /// Creates a step with a status of <see cref="Status.Information"/>.
    /// </summary>
    /// <param name="message">The message associated with the step.</param>
    /// <param name="name">The name of the step. Defaults to "Generic.Step".</param>
    /// <returns>A new <see cref="Step"/> instance with an information status.</returns>
    public static Step Information(string message, string name = "Generic.Step") => new(Status.Information, message, name);
}
