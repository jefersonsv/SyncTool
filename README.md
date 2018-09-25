# Synctool
Command line tool that sync files and folders recursively

[![License](http://img.shields.io/:license-mit-blue.svg)](http://gep13.mit-license.org) [![NuGet version (synctool)](https://img.shields.io/nuget/v/synctool.svg?style=flat-square)](https://www.nuget.org/packages/synctool/)

# Usage

```
Usage:
    synctool.exe sync <source-folder> <destiny-folder> [--silent]
    synctool.exe version

Commands:
    sync             Syncronize folders, adding, deleting and updating files
	version          Show version

Options:
    -s, --silent     Silent mode
    -h, --help       Show this screen
```
## Install

```
dotnet tool install -g synctool
```

## Uninstall

```
dotnet tool uninstall -g synctool
```

# Thanks

- [Colorful.Console](https://github.com/tomakita/Colorful.Console) C# library that wraps around the System.Console class, exposing enhanced styling functionality
- [docopt](https://github.com/docopt/docopt.net) Port of docopt to .net 
- [Microsoft.Extensions.FileSystemGlobbing](https://www.nuget.org/packages/Microsoft.Extensions.FileSystemGlobbing/) File system globbing to find files matching a specified pattern
- [Contessa](http://www.textfiles.com/art/contessa.flf) Font by Christopher Joseph Pirillo