namespace FluentWorkflow.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Define some example steps
            var parseStringToInt = WorkflowStep.Create<string, int>(input =>
                int.TryParse(input, out int result) ? result : throw new InvalidOperationException("Invalid number"));

            var multiplyByTwo = WorkflowStep.Create<int, int>(input => input * 2);

            var convertToString = WorkflowStep.Create<int, string>(input => input.ToString());

            // Create and execute a workflow
            var workflow = new Workflow<string>()
                .AddStep(parseStringToInt)
                .AddStep(multiplyByTwo)
                .AddStep(convertToString);

            var result = await workflow.ExecuteAsync("42");

            Assert.Equal("84", result);
        }

        [Fact]
        public async Task Test2()
        {
            var result = await Task.Run(() =>
            {
                var integer = int.Parse("42");

                integer = integer * 2;

                return integer.ToString();
            });

            Assert.Equal("84", result);
        }
    }
}