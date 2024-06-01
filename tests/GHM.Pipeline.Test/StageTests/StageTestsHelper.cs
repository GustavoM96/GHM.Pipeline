namespace GHM.Pipeline.Test;

public class FirstStageExemple : Stage<DataExemple>
{
    public FirstStageExemple(DataExemple dataExemple)
        : base(dataExemple) { }
}

public class DataExemple { }
