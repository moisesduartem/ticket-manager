#tool "nuget:?package=ReportGenerator&version=5.1.3"

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");

var solutionFolder = "./";

Task("Restore")
    .Does(() =>
    {
        DotNetRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetBuild(solutionFolder, new DotNetBuildSettings
        {
            NoRestore = true,
            Configuration = configuration
        });
    });

Task("Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetTest(solutionFolder, new DotNetTestSettings
        {
            NoRestore = true,
            NoBuild = true,
            Verbosity = DotNetVerbosity.Minimal,
            Configuration = configuration,
            Collectors = new List<string> { "XPlat Code Coverage" }
        });
    });

Task("ReportCoverage")
    .IsDependentOn("Tests")
    .Does(() =>
    {
        ReportGenerator(report: "**/coverage.cobertura.xml", targetDir: ".coveragereport");
    });

RunTarget(target);