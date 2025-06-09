using NUnit.Framework;
using Reqnroll;
using Reqnroll.Infrastructure;
using Reqnroll.NUnit;

namespace Mars_Project_Automation.Drivers.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Cucumberrunner(ITestExecutionEngine executionEngine) : TestRunner(executionEngine)
    {
    }
}
