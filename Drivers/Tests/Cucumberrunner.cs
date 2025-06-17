
using NUnit.Framework;
using Reqnroll;
using Reqnroll.Infrastructure;
using Reqnroll.NUnit;

namespace MarsProjectAutomation.Drivers.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Cucumberrunner(ITestExecutionEngine executionEngine) : TestRunner(executionEngine)
    {
    }
}

