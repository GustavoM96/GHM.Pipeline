namespace GHM.Pipeline.Test;

public class FirstStageExemple : Stage<DataExemple, StageNameExemple>
{
    public FirstStageExemple(DataExemple dataExemple)
        : base(dataExemple, StageNameExemple.FirstStage) { }
}

public class DataExemple { }

public enum StageNameExemple
{
    FirstStage,
    SecongStage,
    ThirdSatge
}
