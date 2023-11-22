# GHM.Pipeline

GHM.Pipeline is a nuget package with the aim of pipeline management in parts.
One pipeline has many stages.
One stage has many steps.

## Install Package

.NET CLI

```sh
dotnet add package GHM.Pipeline --version 1.0.1
```

Package Manager

```sh
NuGet\Install-Package GHM.Pipeline -Version 1.0.1
```

## Example

### Step Object Structure

Step has many Status:

- Success
- Error
- Canceled
- InProgress
- InAdjustment

```csharp
var step = Step.Success("success test","Step Name")
step.Status // Success
step.IsSuccess // true
```

### Stage Object Structure

Stage has:

- Many steps
- Name as Enum
- Data to execute the stage process
- Status more critical in step list

```csharp
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
```

```csharp
public class StageTests
{
    private readonly FirstStageExemple _stage;

    public StageTests()
    {
        var data = new DataExemple();
        _stage = new(data);
    }
}
```

## Star

if you enjoy, don't forget the ⭐ and install the package 😊.
