namespace GHM.Pipeline;

public abstract class Stage<TData>
    where TData : class
{
    protected Stage(TData data)
    {
        Data = data;
    }

    private readonly List<Step> _steps = new();

    public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();
    public Status Status => GetStatusMoreCritical();
    public bool IsSuccess => !IsError;
    public bool IsError => _steps.Any(step => step.Status is Status.Canceled or Status.Error);
    public bool IsCanceled => _steps.Any(step => step.Status is Status.Canceled);

    public TData Data { get; }

    public void AddRangeSteps(IEnumerable<Step> steps) => _steps.AddRange(steps);

    public void AddStep(Step step) => _steps.Add(step);

    public void AddCanceled(string message, string name = "Generic.Step") => _steps.Add(Step.Canceled(message, name));

    public void AddError(string message, string name = "Generic.Step") => _steps.Add(Step.Error(message, name));

    public void AddInAdjustment(string message, string name = "Generic.Step") =>
        _steps.Add(Step.InAdjustment(message, name));

    public void AddInProgress(string message, string name = "Generic.Step") => _steps.Add(Step.InProgress(message, name));

    public void AddSuccess(string message, string name = "Generic.Step") => _steps.Add(Step.Success(message, name));

    public void AddInformation(string message, string name = "Generic.Step") => _steps.Add(Step.Information(message, name));

    private Status GetStatusMoreCritical()
    {
        if (_steps.Count == 0)
        {
            return Status.Default;
        }
        return _steps.MaxBy(x => (int)x.Status).Status;
    }
}
