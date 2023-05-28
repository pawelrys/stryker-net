using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Spectre.Console;
using Stryker.Core.Mutants;
using Stryker.Core.Options;
using Stryker.Core.ProjectComponents;
using Stryker.Core.ProjectComponents.TestProjects;
using Stryker.Core.Reporters.Json;

namespace Stryker.Core.Reporters.Csv;

public class CsvReporter : IReporter
{
    private readonly StrykerOptions _options;
    private readonly IAnsiConsole _console;

    public CsvReporter(StrykerOptions strykerOptions, IAnsiConsole console = null)
    {
        _options = strykerOptions;
        _console = console ?? AnsiConsole.Console;
    }

    public void OnMutantsCreated(IReadOnlyProjectComponent reportComponent, TestProjectsInfo testProjectsInfo)
    {
        // This reporter does not report during the testrun
    }

    public void OnStartMutantTestRun(IEnumerable<IReadOnlyMutant> mutantsToBeTested)
    {
        // This reporter does not report during the testrun
    }

    public void OnMutantTested(IReadOnlyMutant result)
    {
        // This reporter does not report during the testrun
    }

    public void OnAllMutantsTested(IReadOnlyProjectComponent reportComponent, TestProjectsInfo testProjectsInfo)
    {

        var tests = testProjectsInfo.TestFiles.ToList();
        var alltests = from testFile in tests select testFile.Tests.ToList();
        var finalList = alltests.SelectMany(d => d).ToList();

        _console.WriteLine();
        _console.WriteLine();
        _console.WriteLine("TEEEEEEEEEEEEST____________________");
        _console.WriteLine();
        foreach (var mutant in reportComponent.Mutants)
        {
            if (mutant.AssessingTests.GetGuids().Count() == 0)
            {
                _console.WriteLine("ID: " + mutant.DisplayName);
                _console.WriteLine("Klasa Mutacyjna: " + mutant.Mutation.OriginalNode.GetLocation());
                _console.WriteLine("Status Mutanta: " + mutant.ResultStatus);
                _console.WriteLine("Nr_Lini: "+ mutant.Line);
                _console.WriteLine("Opis_Uzycia_Operatora: " + mutant.Mutation.DisplayName);
                _console.WriteLine("Operator mutacyjny: " + mutant.Mutation.Type);
            }
            foreach (var testId in mutant.AssessingTests.GetGuids())
            {
                var test = finalList.Find(obj => obj.Id.Equals(testId));
                _console.WriteLine("ID: " + mutant.DisplayName);
                _console.WriteLine("Klasa Mutacyjna: " + mutant.Mutation.OriginalNode.GetLocation());
                _console.WriteLine("Status Mutanta: " + mutant.ResultStatus);
                _console.WriteLine("Nr_Lini: "+ mutant.Line);
                _console.WriteLine("Opis_Uzycia_Operatora: " + mutant.Mutation.DisplayName);
                _console.WriteLine("Operator mutacyjny: " + mutant.Mutation.Type);
                _console.WriteLine("Nazwa Testu: "  + test.Name);

                // if (mutant.Mutation.OriginalNode is BinaryExpressionSyntax)
                // {
                //     _console.WriteLine("TEST1: " + ((BinaryExpressionSyntax)mutant.Mutation.OriginalNode).GetText());
                //     _console.WriteLine("TEST2: " + ((BinaryExpressionSyntax)mutant.Mutation.OriginalNode).OperatorToken);
                //     _console.WriteLine("TEST2: " + ((BinaryExpressionSyntax)mutant.Mutation.ReplacementNode).OperatorToken);
                //     break;
                // }
            }

            // if (mutant.Mutation.OriginalNode is BinaryExpressionSyntax)
            // {
            //     break;
            // }
        }



        // print empty line for readability
        _console.WriteLine();
        _console.WriteLine();
        _console.WriteLine("All mutants have been tested, and your mutation score has been calculated");
    }
}
