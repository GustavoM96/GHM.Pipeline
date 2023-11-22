using Ghm.Pipeline.Enum;

namespace GHM.Pipeline;

public abstract class Stage<TData, TName>
    where TData : class
    where TName : Enum
{
    protected Stage(TData data, TName stage)
    {
        Data = data;
        Name = stage;
    }

    private readonly List<Step> _steps = new();

    public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();
    public Status Status => GetStatusMoreCritical();
    public bool IsSuccess => Status == Status.Success;

    public TName Name { get; }
    public TData Data { get; }

    public void AddStep(Step step) => _steps.Add(step);

    public void AddRangeSteps(IEnumerable<Step> steps) => _steps.AddRange(steps);

    private Status GetStatusMoreCritical()
    {
        if (_steps.Any(s => s.Status == Status.Canceled))
        {
            return Status.Canceled;
        }

        if (_steps.Any(s => s.Status == Status.Error))
        {
            return Status.Error;
        }

        if (_steps.Any(s => s.Status == Status.InAdjustment))
        {
            return Status.InAdjustment;
        }

        if (_steps.Any(s => s.Status == Status.InProgress))
        {
            return Status.InProgress;
        }

        return Status.Success;
    }
}
