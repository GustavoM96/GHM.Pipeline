namespace GHM.Pipeline;

public readonly struct Step
{
    public string Name { get; }
    public string Message { get; }
    public Status Status { get; }

    private Step(Status status, string message, string name)
    {
        Status = status;
        Message = message;
        Name = name;
    }

    public static Step Canceled(string message, string name = "Generic.Step") => new(Status.Canceled, message, name);

    public static Step Error(string message, string name = "Generic.Step") => new(Status.Error, message, name);

    public static Step InAdjustment(string message, string name = "Generic.Step") => new(Status.InAdjustment, message, name);

    public static Step InProgress(string message, string name = "Generic.Step") => new(Status.InProgress, message, name);

    public static Step Success(string message, string name = "Generic.Step") => new(Status.Success, message, name);

    public static Step Information(string message, string name = "Generic.Step") => new(Status.Information, message, name);
}
