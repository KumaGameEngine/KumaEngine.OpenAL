# KumaEngine.OpenAL
OpenAL bindings for the Kuma game engine.

## Building

> [!warning]
> Generating the bindings on windows will only generate native binaries for windows, if linux binaries are needed, build on linux or use WSL

```bash
./build.ps1
dotnet publish
```

the resulting `nupkg` file will be in `bin/Release`