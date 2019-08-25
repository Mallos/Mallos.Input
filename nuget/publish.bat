:: Add NuGet Source
echo Add NuGet Source
./nuget.exe source Add -Name "GitHub - erictuvesson" -Source "https://nuget.pkg.github.com/erictuvesson/index.json" -UserName PACKAGE_REGISTRY_USERNAME -Password PACKAGE_REGISTRY_PASSWORD
echo Added NuGet Source

:: Publish Packages
echo Publish Packages
for %%f in (../release/*.nupkg) do (
  echo Publish Package %%~nf
  ./nuget.exe push %%~nf -Source "GitHub - erictuvesson"
)
echo Finished Publishing Packages

:: Publish Package Symbols
echo Publish Package Symbols
for %%f in (../release/*.snupkg) do (
  echo Publish Symbol %%~nf
  ./nuget.exe push %%~nf -Source "GitHub - erictuvesson"
)
echo Finished Publishing Package Symbols
