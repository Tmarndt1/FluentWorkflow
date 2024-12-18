namespace FluentWorkflow
{
    /// <summary>
    /// Helper class to create workflow steps
    /// </summary>
    public abstract class WorkflowStep
    {
        /// <summary>
        /// Creates a new workflow step
        /// </summary>
        public static WorkflowStep<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, TOutput> action)
        {
            return new WorkflowStep<TInput, TOutput>(action);
        }

        internal abstract object Execute(object input);
    }

    /// <summary>
    /// Represents a step in the workflow with a specific input and output type
    /// </summary>
    /// <typeparam name="TInput">The input type for the step</typeparam>
    /// <typeparam name="TOutput">The output type for the step</typeparam>
    public class WorkflowStep<TInput, TOutput> : WorkflowStep
    {
        private readonly Func<TInput, TOutput> _func;

        public WorkflowStep(Func<TInput, TOutput> func)
        {
            if (func is null) throw new ArgumentNullException(nameof(func));

            _func = func;
        }

        internal override object Execute(object input)
        {
            // Safe type casting with runtime check
            if (input is TInput typedInput)
            {
                return _func(typedInput);
            }

            throw new ArgumentException($"Invalid input type. Expected {typeof(TInput)}");
        }
    }
}
