$ErrorActionPreference = "Stop"

$IsWin = ($env:OS -like "*Windows*") -or $IsWindows

$RepoRoot = $PSScriptRoot
$SourceHeaders = Join-Path $RepoRoot "OpenALSoft/include/AL/al.h"
$ALCHeader = Join-Path $RepoRoot "OpenALSoft/include/AL/alc.h"
$GeneratedOutput = Join-Path $RepoRoot "Source/Native/OpenALNative.cs"
$OpenALSrcDir = Join-Path $RepoRoot "OpenALSoft"

Write-Host "Generating C# bindings..." -ForegroundColor Cyan

ClangSharpPInvokeGenerator `
  -f $SourceHeaders `
  -f $ALCHeader `
  -n "KumaEngine.OpenAL.Native" `
  -l "openal" `
  -m "ALNative" `
  -o $GeneratedOutput `
  --generate macro-bindings `
  --generate helper-types

Write-Host "Bindings generated at: $GeneratedOutput" -ForegroundColor Green

function Build-OpenALSoft {
    param (
        $TargetRID,
        $DestinationFileName,
        $CMakeArgs,
        $BuiltRelativePath
    )

    Write-Host "--> Building native target: $TargetRID..." -ForegroundColor Yellow

    $BuildDir = Join-Path $OpenALSrcDir "build-$TargetRID"
    $DestinationFolder = Join-Path $RepoRoot "runtimes/$TargetRID/native"

    if (Test-Path $BuildDir) { 
        Remove-Item -Path $BuildDir -Recurse -Force 
    }

    $FinalCMakeArgs = @("-DALSOFT_UTILS=OFF", "-DALSOFT_EXAMPLES=OFF") + $CMakeArgs

    cmake -S $OpenALSrcDir -B $BuildDir @FinalCMakeArgs
    cmake --build $BuildDir --config Release

    if (-not (Test-Path $DestinationFolder)) {
        New-Item -ItemType Directory -Path $DestinationFolder -Force | Out-Null
    }

    $BuiltBinaryPath = Join-Path $BuildDir $BuiltRelativePath
    $FinalBinaryPath = Join-Path $DestinationFolder $DestinationFileName

    Copy-Item -Path $BuiltBinaryPath -Destination $FinalBinaryPath -Force
    Write-Host "Binary packaged to: $FinalBinaryPath" -ForegroundColor Green
}

Write-Host "Building native OpenAL Soft binaries..." -ForegroundColor Cyan

if ($IsWin) {
    Build-OpenALSoft `
        -TargetRID "win-x64" `
        -DestinationFileName "openal.dll" `
        -CMakeArgs @("-A", "x64") `
        -BuiltRelativePath "Release/OpenAL32.dll"

} elseif ($IsLinux) {
    Build-OpenALSoft `
        -TargetRID "linux-x64" `
        -DestinationFileName "libopenal.so" `
        -CMakeArgs @("-DCMAKE_BUILD_TYPE=Release") `
        -BuiltRelativePath "libopenal.so"

    if (Get-Command "x86_64-w64-mingw32-gcc" -ErrorAction SilentlyContinue) {
        Build-OpenALSoft `
            -TargetRID "win-x64" `
            -DestinationFileName "openal.dll" `
            -CMakeArgs @(
                "-DCMAKE_BUILD_TYPE=Release", 
                "-DCMAKE_SYSTEM_NAME=Windows", 
                "-DCMAKE_C_COMPILER=x86_64-w64-mingw32-gcc", 
                "-DCMAKE_CXX_COMPILER=x86_64-w64-mingw32-g++"
            ) `
            -BuiltRelativePath "OpenAL32.dll"
    } else {
        Write-Warning "MinGW compiler (x86_64-w64-mingw32-gcc) not found. Skipping Windows cross-compilation."
    }
} else {
    Write-Error "Unsupported operating system."
}