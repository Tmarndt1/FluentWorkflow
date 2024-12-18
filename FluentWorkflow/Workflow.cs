using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentWorkflow
{
    /// <summary>
    /// Represents a workflow that can dynamically add steps with type safety
    /// </summary>
    public class Workflow<TInitialInput>
    {
        private readonly LinkedList<WorkflowStep> _steps = new LinkedList<WorkflowStep>();

        /// <summary>
        /// Adds a step to the workflow
        /// </summary>
        /// <typeparam name="TInput">The input type for this step</typeparam>
        /// <typeparam name="TOutput">The output type for this step</typeparam>
        /// <param name="step">The workflow step to add</param>
        /// <returns>A new workflow with the added step</returns>
        public Workflow<TInitialInput, TOutput> AddStep<TInput, TOutput>(
            WorkflowStep<TInput, TOutput> step)
        {
            return new Workflow<TInitialInput, TOutput>(_steps, step);
        }
    }

    /// <summary>
    /// Represents a workflow with a specific initial input and current step output type
    /// </summary>
    /// <typeparam name="TInitialInput">The initial input type for the workflow</typeparam>
    /// <typeparam name"TCurrentOutput">Current output type of the workflow</typeparam>
    public class Workflow<TInitialInput, TCurrentOutput>
    {
        private readonly LinkedList<WorkflowStep> _steps;

        internal Workflow(LinkedList<WorkflowStep> previousSteps, WorkflowStep newStep)
        {
            if (previousSteps.Count == 0)
            {
                previousSteps.AddFirst(newStep);
            }
            else
            {
                previousSteps.AddLast(newStep);
            }

            _steps = previousSteps;
        }

        /// <summary>
        /// Adds a new step to the workflow
        /// </summary>
        /// <typeparam name="TInput">The input type for this step</typeparam>
        /// <typeparam name="TOutput">The output type for this step</typeparam>
        /// <param name="step">The workflow step to add</param>
        /// <returns>A new workflow with the added step</returns>
        public Workflow<TInitialInput, TOutput> AddStep<TInput, TOutput>(WorkflowStep<TInput, TOutput> step)
            where TInput : TCurrentOutput
        {
            return new Workflow<TInitialInput, TOutput>(_steps, step);
        }

        public TCurrentOutput Execute(TInitialInput initialInput)
        {
            object input = initialInput;

            foreach (var step in _steps)
            {
                input = step.Execute(input);
            }

            return (TCurrentOutput)input;
        }

        /// <summary>
        /// Executes the entire workflow
        /// </summary>
        /// <param name="initialInput">The initial input to start the workflow</param>
        /// <returns>The final output of the workflow</returns>
        public Task<TCurrentOutput> ExecuteAsync(TInitialInput initialInput)
        {
            return Task.Run(() => Execute(initialInput));
        }
    }
}