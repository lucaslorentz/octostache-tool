# Introduction

.NET Tool to replace octopus variables (Octostache) by environment variable values.

# Instructions

Install:
```bash
dotnet tool install --global OctostacheTool
```

Use:
```bash
octostache substitute test.txt
```

# Development
Pack:
```bash
dotnet pack -c Release
```

Install:
```bash
dotnet tool install --global --add-source ./nupkg OctostacheTool
```

Use:
```bash
octostache substitute test.txt
```

Uninstall:
```bash
dotnet tool uninstall -g OctostacheTool
```
