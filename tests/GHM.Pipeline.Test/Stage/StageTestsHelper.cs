namespace GHM.Pipeline.Test;

public class FirstStageExemple : Stage<DataExemple, StageNameExemple>
{
    public FirstStageExemple()
        : base(new DataExemple(), StageNameExemple.FirstStage) { }
}

public class DataExemple { }

public enum StageNameExemple
{
    FirstStage,
    SecongStage,
    ThirdSatge
}
