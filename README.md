# Fluent Workflow Library

The **Fluent Workflow** library provides a flexible, type-safe way to construct and execute workflows with dynamically added steps. It ensures type safety across all steps and supports both synchronous and asynchronous execution.

---

## Features
- **Type-safe workflows**: Ensures input and output types are consistent across all steps.
- **Dynamic step addition**: Add steps dynamically to workflows while preserving type safety.
- **Synchronous and Asynchronous execution**: Execute workflows synchronously or asynchronously.

---

## Getting Started

### Installation
Include the `FluentWorkflow` namespace in your project:

```csharp
using FluentWorkflow;
```

### Workflow Concepts
- **Workflow**: Represents a sequence of steps to execute in order.
- **WorkflowStep**: Represents a single step in the workflow, processing input and producing output.

### Usage
#### Defining Steps
Each step in the workflow must inherit from `WorkflowStep<TInput, TOutput>`:

```csharp
public class StringToUpperStep : WorkflowStep<string, string>
{
    public override string Execute(string input) => input.ToUpper();
}

public class LengthStep : WorkflowStep<string, int>
{
    public override int Execute(string input) => input.Length;
}
```

#### Building a Workflow
Create and chain steps using the `AddStep` method:

```csharp
var workflow = new Workflow<string>()
    .AddStep(new StringToUpperStep())
    .AddStep(new LengthStep());
```

#### Executing the Workflow
Run the workflow synchronously:

```csharp
int result = workflow.Execute("hello world");
Console.WriteLine(result); // Output: 11
```

Or asynchronously:

```csharp
int result = await workflow.ExecuteAsync("hello world");
Console.WriteLine(result); // Output: 11
```

---

## API Reference

### Workflow<TInitialInput>
Represents the entry point of a workflow.

#### Methods
- `AddStep<TInput, TOutput>(WorkflowStep<TInput, TOutput> step)`: Adds a step to the workflow.

### Workflow<TInitialInput, TCurrentOutput>
Represents a workflow with a specific initial input and current output type.

#### Methods
- `AddStep<TInput, TOutput>(WorkflowStep<TInput, TOutput> step)`: Adds a step to the workflow.
- `TCurrentOutput Execute(TInitialInput initialInput)`: Executes the workflow synchronously.
- `Task<TCurrentOutput> ExecuteAsync(TInitialInput initialInput)`: Executes the workflow asynchronously.

### WorkflowStep<TInput, TOutput>
Base class for defining workflow steps.

#### Methods
- `TOutput Execute(TInput input)`: Processes the input and returns the output.

---

## Example
Below is a complete example combining all concepts:

```csharp
using FluentWorkflow;
using System;

public class StringToUpperStep : WorkflowStep<string, string>
{
    public override string Execute(string input) => input.ToUpper();
}

public class LengthStep : WorkflowStep<string, int>
{
    public override int Execute(string input) => input.Length;
}

class Program
{
    static void Main(string[] args)
    {
        var workflow = new Workflow<string>()
            .AddStep(new StringToUpperStep())
            .AddStep(new LengthStep());

        int result = workflow.Execute("hello world");
        Console.WriteLine(result); // Output: 11
    }
}
```

---

## License
This project is licensed under the MIT License. See `LICENSE` file for details.

