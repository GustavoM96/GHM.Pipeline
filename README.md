<p align="center">
<img src="logo.png" alt="logo" width="200px"/>
</p>

<h1 align="center"> GHM.HTTPResult </h1>

GHM.Pipeline is a nuget package aims to manage pipelines in parts, where a pipeline has many stages and a stage has many steps.

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

As an example, we will use an e-commerce pipeline in which the order is requested, validated, processed, shipped, and finalized. Within each stage, there are one or more steps.

Stages:

- Requested (`STAGE`)
  - requested by customer (`STEP`)
  - to insert in DataBase (`STEP`)
- Validation (`STAGE`)
  - customer has all data has been successfully validated (`STEP`)
  - seller as all data has been successfully validated (`STEP`)
- Processing (`STAGE`)
  - to Process credit card (`STEP`)
- Sending (`STAGE`)
  - to send the product (`STEP`)
- Finish (`STAGE`)
  - confirmation of product to customer's localization (`STEP`)

```csharp
using GHM.Pipeline;

public static class EcommercePipeline{

    public RequestedStage CreateRequestStage(EcommerceData data) => new(data);
    public ValidationStage CreateValidationStage(EcommerceData data) => new(data)
}

public class RequestedStage : Stage<EcommerceData, EcommerceStageEnum>
{
    public RequestedStage(EcommerceData data)
        : base(data, EcommerceStageEnum.Requested) { }
}

public class ValidationStage : Stage<EcommerceData, EcommerceStageEnum>
{
    public RequestedStage(EcommerceData data)
        : base(data, EcommerceStageEnum.Validation) { }
}

public class EcommerceData { }

public enum EcommerceStageEnum
{
    Requested,
    Validation,
    Processing,
    Sending,
    Finish
}
```

In services code

```csharp
using GHM.Pipeline;

public class EcommerceService
{
    public void Request(EcommerceData data)
    {

        RequestedStage stage = EcommercePipeline.CreateRequestedStage(data);
        try
        {
            if(data is null)
            {
                stage.AddCanceled("has no data to request") // this step canceled the stage
            }

            stage.AddSuccess("add data") // // this step validated the stage
        }
        catch (Exception ex)
        {
            stage.AddError("internal error: " + ex.Message) // // this step create a error to the stage
        }

        return
    }
}
```

## Classes

### Step

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

### Stage

Stage has:

- Many steps
- Name as Enum
- Data to execute the stage process
- Status more critical in step list

```csharp
using GHM.Pipeline;

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
using GHM.Pipeline;

public class StageService
{
    public CreateStage()
    {
        var data = new DataExemple();
        FirstStageExemple stage = new FirstStageExemple(data);
    }
}
```

## Star

if you enjoy, don't forget the ⭐ and install the package 😊.
