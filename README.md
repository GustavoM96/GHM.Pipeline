<p align="center">
<img src="logo.png" alt="logo" width="200px"/>
</p>

<h1 align="center"> GHM.Pipeline </h1>

[![Build & Test](https://github.com/GustavoM96/GHM.Pipeline/actions/workflows/build.yml/badge.svg)](https://github.com/GustavoM96/GHM.Pipeline/actions/workflows/build.yml)

GHM.Pipeline is a nuget package aims to manage pipelines in parts, where a pipeline has many stages and a stage has many steps.

## Install Package

.NET CLI

```sh
dotnet add package GHM.Pipeline
```

Package Manager

```sh
NuGet\Install-Package GHM.Pipeline
```

## Example

As an example, we will use an e-commerce pipeline in which the order is requested, validated, processed, shipped, and finalized. Within each stage, there are one or more steps.

Stages:

- Requested (`STAGE`)
  - requested by customer (`STEP`)
  - to insert in DataBase (`STEP`)
- Validation (`STAGE`)
  - customer has all data has been successfully validated (`STEP`)
  - seller has all data has been successfully validated (`STEP`)
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

public class RequestedStage : Stage<EcommerceData>
{
    public RequestedStage(EcommerceData data)
        : base(data) { }
}

public class ValidationStage : Stage<EcommerceData>
{
    public RequestedStage(EcommerceData data)
        : base(data) { }
}

public class EcommerceData { }

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
            stage.AddInformation("processing request stage") // this step is only a information

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
    }
}
```

## Classes

### Step

Step has many Status. The following list is ordered by more critical desc:

- Canceled
- Error
- InProgress
- InAdjustment
- Success
- Information
- Default

```csharp
Step step = Step.Success("success test","Step Name")

step.Status // Success
step.IsSuccess // true
step.IsError // false
step.IsCanceled // false
```

### Stage

Stage has:

- Many steps
- Data to execute the stage process
- Status more critical in step list

## Star

if you enjoy, don't forget the ⭐ and install the package 😊.
