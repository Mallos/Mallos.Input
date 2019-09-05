@echo off
@setlocal

dotnet restore ..\Mallos.Input.sln
dotnet build -c Release ..\Mallos.Input.sln

dotnet pack -c Release ..\src\Mallos.Input\Mallos.Input.csproj
dotnet pack -c Release ..\src\Mallos.Input.Veldrid.SDL2\Mallos.Input.Veldrid.SDL2.csproj
dotnet pack -c Release ..\src\Mallos.Input.Debug.ImGuiNET\Mallos.Input.Debug.ImGuiNET.csproj
