namespace GHM.Pipeline.Test;

public class StageTests
{
    private readonly FirstStageExemple _stage;

    public StageTests()
    {
        var data = new DataExemple();
        _stage = new(data);
    }

    [Fact]
    public void AddStep_Should_Add_To_List()
    {
        //Arrange
        var step = Step.Success("success test message");

        //Act
        _stage.AddStep(step);

        //Assert
        Assert.Contains(step, _stage.Steps);
    }

    [Fact]
    public void AddRangeSteps_Should_Add_To_List()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step1 = Step.Canceled("success test message");
        var stepList = new List<Step>(2) { step, step1 };
        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Contains(step, _stage.Steps);
        Assert.Contains(step1, _stage.Steps);
    }

    [Fact]
    public void Status_Should_Be_Canceled_When_AddStepCanceled()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step1 = Step.Canceled("canceled test message");
        var step2 = Step.Error("error test message");

        var stepList = new List<Step>(3) { step, step1, step2 };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.Canceled, _stage.Status);
    }

    [Fact]
    public void Status_Should_Be_Error_When_AddStepError()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step2 = Step.Error("error test message");
        var step1 = Step.InAdjustment("inAdjustment test message");

        var stepList = new List<Step>(3) { step, step1, step2 };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.Error, _stage.Status);
    }

    [Fact]
    public void Status_Should_Be_InAdjustment_When_AddStepInAdjustment()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step1 = Step.InAdjustment("inAdjustment test message");
        var step2 = Step.InProgress("inProgress test message");

        var stepList = new List<Step>(3) { step, step1, step2 };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.InAdjustment, _stage.Status);
    }

    [Fact]
    public void Status_Should_Be_InProgress_When_AddStepInProgress()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step2 = Step.InProgress("inProgress test message");

        var stepList = new List<Step>(2) { step, step2 };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.InProgress, _stage.Status);
        Assert.False(_stage.IsSuccess);
    }

    [Fact]
    public void Status_Should_Be_Success_When_AddStepSuccess()
    {
        //Arrange
        var step = Step.Success("success test message");

        var stepList = new List<Step>(1) { step };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.Success, _stage.Status);
        Assert.True(_stage.IsSuccess);
    }

    [Fact]
    public void Status_Should_Be_Success_When_Default()
    {
        //Arrange
        //Act
        //Assert
        Assert.Equal(Status.Success, _stage.Status);
        Assert.True(_stage.IsSuccess);
    }
}
