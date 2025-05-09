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
        Assert.False(_stage.IsSuccess);
        Assert.True(_stage.IsCanceled);
        Assert.True(_stage.IsError);
        Assert.True(_stage.IsErrorOrCanceled);
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
        Assert.False(_stage.IsSuccess);
        Assert.False(_stage.IsCanceled);
        Assert.True(_stage.IsError);
        Assert.True(_stage.IsErrorOrCanceled);
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
        Assert.True(_stage.IsSuccess);
        Assert.False(_stage.IsCanceled);
        Assert.False(_stage.IsError);
        Assert.False(_stage.IsErrorOrCanceled);
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
        Assert.True(_stage.IsSuccess);
        Assert.False(_stage.IsCanceled);
        Assert.False(_stage.IsError);
        Assert.False(_stage.IsErrorOrCanceled);
    }

    [Fact]
    public void Status_Should_Be_Success_When_AddStepSuccess()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step2 = Step.Information("information test message");

        var stepList = new List<Step>(1) { step, step2 };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.Success, _stage.Status);
        Assert.True(_stage.IsSuccess);
        Assert.False(_stage.IsCanceled);
        Assert.False(_stage.IsError);
        Assert.False(_stage.IsErrorOrCanceled);
    }

    [Fact]
    public void Status_Should_Be_Success_When_AddStepInformation()
    {
        //Arrange
        var step = Step.Information("information test message");

        var stepList = new List<Step>(1) { step };

        //Act
        _stage.AddRangeSteps(stepList);

        //Assert
        Assert.Equal(Status.Information, _stage.Status);
        Assert.True(_stage.IsSuccess);
        Assert.False(_stage.IsCanceled);
        Assert.False(_stage.IsError);
        Assert.False(_stage.IsErrorOrCanceled);
    }

    [Fact]
    public void Status_Should_Be_Default_When_NotAddStep()
    {
        //Arrange
        //Act
        //Assert
        Assert.Equal(Status.Default, _stage.Status);
        Assert.True(_stage.IsSuccess);
        Assert.False(_stage.IsCanceled);
        Assert.False(_stage.IsError);
        Assert.False(_stage.IsErrorOrCanceled);
    }

    [Fact]
    public void GetStepsByStatus_Should_ReturnAllStepsByStatus()
    {
        //Arrange
        var step = Step.Success("success test message");
        var step1 = Step.Information("default test message");
        var step2 = Step.Information("default test message2");
        var stepList = new List<Step>(2) { step, step1, step2 };

        //Act
        _stage.AddRangeSteps(stepList);
        var steps = _stage.GetStepsByStatus(Status.Success);
        var steps2 = _stage.GetStepsByStatus(Status.Information);
        var steps3 = _stage.GetStepsByStatus(Status.Error);

        //Assert

        Assert.All(steps, step => Assert.Equal(Status.Success, step.Status));
        Assert.All(steps2, step => Assert.Equal(Status.Information, step.Status));
        Assert.All(steps3, step => Assert.Equal(Status.Error, step.Status));
    }
}
