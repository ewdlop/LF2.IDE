# .NET 8 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 8 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 8 upgrade.
3. Upgrade LF2.IDE\LF2.IDE.csproj to .NET 8

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

No projects are excluded from this upgrade.

### Aggregate NuGet packages modifications across all projects

No NuGet package modifications are required based on the analysis results.

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### LF2.IDE\LF2.IDE.csproj modifications

Project properties changes:
  - Project file needs to be converted to SDK-style format
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0-windows`

Other changes:
  - Convert from legacy .NET Framework project format to modern SDK-style project format
  - Update project references and dependencies to be compatible with .NET 8