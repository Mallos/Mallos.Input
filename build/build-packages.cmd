@echo off
@setlocal

dotnet restore ..\OpenInput.sln
dotnet build -c Release ..\OpenInput.sln

dotnet pack -c Release ..\src\OpenInput\OpenInput.csproj
dotnet pack -c Release ..\src\OpenInput.Veldrid.SDL2\OpenInput.Veldrid.SDL2.csproj
dotnet pack -c Release ..\src\OpenInput.Debug.ImGuiNET\OpenInput.Debug.ImGuiNET.csproj
