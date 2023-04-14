using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Buildalyzer;
using Moq;
using Shouldly;
using Stryker.Core.Initialisation;
using Stryker.Core.Mutants;
using Stryker.Core.MutationTest;
using Stryker.Core.Options;
using Stryker.Core.ProjectComponents.TestProjects;
using Stryker.Core.Reporters;
using Stryker.Core.TestRunners;
using Stryker.Core.TestRunners.VsTest;
using Xunit;
using VsTest = Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Stryker.Core.UnitTest.Initialisation
{
    public class ProjectMutatorTests : TestBase
    {
        private readonly Mock<IMutationTestProcess> _mutationTestProcessMock = new Mock<IMutationTestProcess>(MockBehavior.Strict);
        private readonly Mock<IInitialisationProcess> _initialisationProcessMock = new Mock<IInitialisationProcess>(MockBehavior.Strict);
        private readonly Mock<IReporter> _reporterMock = new Mock<IReporter>(MockBehavior.Strict);
        private readonly MutationTestInput _mutationTestInput;
        private readonly IFileSystem _fileSystemMock = new MockFileSystem();
        private readonly string _testFilePath = "c:\\mytestfile.cs";
        private readonly string _testFileContents = @"using Xunit;

namespace ExtraProject.XUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // example test
        }
    }
}
";

        public ProjectMutatorTests()
        {
            _mutationTestProcessMock.Setup(x => x.Mutate());
            _fileSystemMock.File.WriteAllText(_testFilePath, _testFileContents);

            _mutationTestInput = new MutationTestInput()
            {
                TestProjectsInfo = new TestProjectsInfo(_fileSystemMock)
                {
                    TestProjects = new List<TestProject>
                    {
                        new TestProject(_fileSystemMock, TestHelper.SetupProjectAnalyzerResult(
                            sourceFiles: new string[] { _testFilePath }).Object)
                    }
                }
            };
        }

        [Fact]
        public void ShouldInitializeEachProjectInSolution()
        {
            // arrange
            var options = new StrykerOptions();
            var target = new ProjectMutator(_initialisationProcessMock.Object, _mutationTestProcessMock.Object);
            var failedTest = Guid.NewGuid();
            var successfulTest = Guid.NewGuid();
            var testCase1 = new VsTest.TestCase("mytestname", new Uri(_testFilePath), _testFileContents)
            {
                CodeFilePath = _testFilePath,
                LineNumber = 7,
            };
            var tests = new List<VsTestDescription> { new VsTestDescription(testCase1) };
            var initialTestRunResult = new TestRunResult(
                vsTestDescriptions: tests,
                executedTests: new TestGuidsList(failedTest, successfulTest),
                failedTests: new TestGuidsList(failedTest),
                timedOutTest: TestGuidsList.NoTest(),
                message: "testrun succesful",
                timeSpan: TimeSpan.FromSeconds(2));

            var initialTestrun = new InitialTestRun(initialTestRunResult, new TimeoutValueCalculator(500));

            _initialisationProcessMock.Setup(x => x.Initialize(It.IsAny<StrykerOptions>(), It.IsAny<IEnumerable<IAnalyzerResult>>())).Returns(_mutationTestInput);
            _initialisationProcessMock.Setup(x => x.InitialTest(options))
                .Returns(initialTestrun);

            // act
            var result = target.MutateProject(options, _reporterMock.Object);

            // assert
            result.ShouldNotBeNull();
            var testFile = _mutationTestInput.TestProjectsInfo.TestFiles.ShouldHaveSingleItem();
            testFile.Tests.Count().ShouldBe(1);
        }
    }
}
