function Get-PackageVersion {
    return Get-Content .\version.txt -Tail 1
}

$version = Get-PackageVersion
$packageId = "QueryStringGenerator"
$basePath = "..\src\QueryStringGenerator\"
$project = $basePath + $packageId + ".csproj"
$nupkg = $basePath + "bin\Release\" + "$packageId.$version.nupkg"
$configuration = "Release"

dotnet clean -c $configuration $project
dotnet build -c $configuration $project -p:Version=$version -p:ContinuousIntegrationBuild=true
dotnet nuget push $nupkg -s https://api.nuget.org/v3/index.json -k ${env:NUGET_API_KEY}
