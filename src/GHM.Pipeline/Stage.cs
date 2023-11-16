using Ghm.Pipeline.Enum;

namespace GHM.Pipeline;

public abstract class Stage<TData, TStage>
    where TData : class
    where TStage : Enum
{
    protected Stage(TData data, TStage stage)
    {
        Data = data;
        Value = stage;
    }

    private readonly List<Step> _steps = new();

    public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();
    public Status Status => GetStatusMoreCritical();

    public TStage Value { get; }
    public TData Data { get; }

    public void AddStep(Step step) => _steps.Add(step);

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
