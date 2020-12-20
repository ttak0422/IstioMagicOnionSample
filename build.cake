#addin nuget:?package=Cake.Docker&version=0.11.1

var target = Argument("Target", "Default");
var builSettings = new DotNetCoreBuildSettings
{
    Configuration = "Debug"
};
class DockerBuildSettingsParameters
{
    public string Tag;
}
Setup<DockerBuildSettingsParameters>(ctx => new DockerBuildSettingsParameters()
    {
        Tag = ctx.Argument("tag", "onion-sample-app:1.0")
    });

Task("Restore")
    .Does(_ => DotNetCoreRestore());

Task("Clean")
    .Does(_ => DotNetCoreClean("."));

Task("BuildClient")
    .Does(_ => DotNetCoreBuild("./SampleApp.Client/SampleApp.Client.csproj"));

Task("BuildServer")
    .Does(_ => DotNetCoreBuild("./SampleApp.Server/SampleApp.Server.csproj"));

Task("Build")
    .IsDependentOn("BuildClient")
    .IsDependentOn("BuildServer");

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build");


Task("BuildServerDocker")
    .Does<DockerBuildSettingsParameters>(p => {
        Information($"docker tag: {p.Tag}");
        var settings = new DockerImageBuildSettings
        {
            File = "./SampleApp.Server/Dockerfile",
            Tag = new string[] { p.Tag }
        };
        DockerBuild(settings, ".");
    });

RunTarget(target);