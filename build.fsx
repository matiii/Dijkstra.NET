// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake
open Fake.MSTest
open System

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
Fake.MSBuildHelper.MSBuildLoggers <- []

let dir = "./build/"
let nugetDir = "./NuGet/"

Target "Clean" (fun _ ->
    trace "Clean directory."
    CleanDir dir
)

Target "BuildApp" (fun _ ->
    trace "Build app"
    RestorePackages()
    !! "src/**/*.csproj"
        |> MSBuildRelease dir "Build"
        |> Log "AppBuild-Output: "
) 

Target "Tests" (fun _ ->
    trace "Run tests"
    !! (dir+"*.Tests.dll") 
            |> MSTest (fun p -> p)
)

Target "CreatePackage" (fun _ ->
    trace "Create Nuget package"

    CreateDir nugetDir
    CleanDir nugetDir

    NuGetPack (fun p -> {p with Version = "1.0.5"}) "src/Dijkstra.NET/Dijkstra.NET/Dijkstra.NET.csproj"
)

"Clean"
     ==>
      "BuildApp"
        ==>
            "Tests"
                ==>
                  "CreatePackage"

// start build
RunTargetOrDefault "CreatePackage"
