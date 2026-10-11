# SenseHat.DotNetCore

## Overview

C# applications that connect a Raspberry Pi Sense HAT to a console interface and an ASP.NET Core Web API. The shared hardware service reads temperature, humidity and pressure, and controls the LED matrix. A second implementation generates simulated readings so the application workflow can be explored without the board.

> This repository is retained as a historical technical example. It reflects the .NET Core 3.0 preview-era environment and APIs used for the accompanying 2019 MSDN Magazine article.

## Technical focus

- C# and .NET Core 3.0
- Raspberry Pi Sense HAT and I2C device bindings
- Hardware abstraction and sensor emulation
- ASP.NET Core controllers and dependency injection
- HTTP sensor access and LED control, with Swagger UI

## Architecture

- `SenseHat.DotNetCore.Common`: `ISenseHatService`, the hardware and emulation implementations, and sensor-reading types.
- `SenseHat.DotNetCore.ConsoleApp`: displays readings in a loop and cycles LED colors in hardware mode.
- `SenseHat.DotNetCore.WebApp`: exposes `GET /api/SenseHat` for readings and `POST /api/SenseHat?colorName=Red` for LED control.

Both applications use the same service interface. The emulator returns randomized sensor values; LED writes have no effect in emulation mode.

## Getting started

### Historical prerequisites

The original article used Visual Studio 2019, .NET Core 3.0 Preview 5, and Raspberry Pi 2/3 with Sense HAT. The projects target `netcoreapp3.0` and retain prerelease dependencies, including `Iot.Device.Bindings` version `0.1.0-prerelease.19352.2` and ASP.NET Core preview packages.

Package restoration depends on the original dependencies being available. The article's .NET IoT feed (`https://dotnetfeed.blob.core.windows.net/dotnet-iot/index.json`) returned HTTP 403 during the documentation review, and the exact IoT package was unavailable from the nuget.org flat-container endpoint. The commands below describe the historical workflow; a successful build with a current SDK has not been verified.

From the repository root, using a compatible historical SDK and package sources:

```sh
dotnet restore SenseHat.DotNetCore.sln --source https://api.nuget.org/v3/index.json --source https://dotnetfeed.blob.core.windows.net/dotnet-iot/index.json
dotnet build SenseHat.DotNetCore.sln --no-restore
```

### Explore without hardware

Run the console application with its single `Y` argument:

```sh
dotnet run --project SenseHat.DotNetCore.ConsoleApp -- Y
```

It prints simulated readings. Stop with Ctrl+C.

For the Web API, select the project's development launch profile:

```sh
dotnet run --project SenseHat.DotNetCore.WebApp --launch-profile SenseHat.DotNetCore.WebApp
```

Open `https://localhost:5001/swagger` using the development HTTPS setup. `appsettings.Development.json` sets `Emulation` to `Y`; the base `appsettings.json` sets it to `N`. The console application enables emulation only when its sole argument is `Y` (case-insensitive).

### Historical hardware deployment

The article describes publishing for Windows 10 IoT Core on Arm:

```sh
dotnet publish SenseHat.DotNetCore.ConsoleApp -c Release -r win-arm
```

Copy the published output to the device and run from PowerShell:

```powershell
.\SenseHat.DotNetCore.ConsoleApp.exe N
```

The hardware mode reads the Sense HAT sensors and changes the LED matrix color. The related article also covers publishing the Web API, configuring its network endpoint, and accessing Swagger from another device.

## Related material

- [Cross-Platform IoT Programming with .NET Core 3.0](https://learn.microsoft.com/en-us/archive/msdn-magazine/2019/august/net-core-cross-platform-iot-programming-with-net-core-3-0) — Dawid Borycki, MSDN Magazine, August 2019. The article explains the service abstraction, emulation, and original hardware deployment.
