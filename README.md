# Hi-Lo Game

## How to play
Game URL: https://hi-lo-game.azurewebsites.net/

After connecting to the URL you can start guessing the mystery number.
Each player gets a random name that can be changed at will.
Any player can guess at any moment. Any player can also change the Game Engine at any time. Changing the Game Engine starts a new round.

## Overall architecture
The game is built using ASP.Net Core Razor Pages, with SignalR to enable multiplayer.
There is a Game Master responsible for the whole game. The Game Master can be configured with an IEngine (Default, Wrong or Random).
The randomness is implemented as a service because it is really important to be able to control how randomness is applied on games.

## LoggerAdapter
This project uses a custom LoggerAdapter to mitigate the drawback of using ILogger directly, as ILogger will create object in the heap for each call, even if the logger is not configured for that level. LoggerAdapter leverages generics to prevent heap alocation when dealing with value type. Less allocations means less time spent on GC. There is a benchmark to illustrate this.


``` text
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19045.2364)
Intel Xeon CPU E5-1620 v4 3.50GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2  [AttachedDebugger]
  DefaultJob : .NET 6.0.12 (6.0.1222.56807), X64 RyuJIT AVX2


|                          Method | Count |         Mean |      Error |     StdDev |       Median | Ratio | RatioSD |      Gen0 |     Gen1 |     Gen2 |  Allocated | Alloc Ratio |
|-------------------------------- |------ |-------------:|-----------:|-----------:|-------------:|------:|--------:|----------:|---------:|---------:|-----------:|------------:|
|     LoggerAdapter_WithValueType |   100 |     82.14 us |   1.634 us |   3.654 us |     81.28 us |  0.93 |    0.04 |   15.9912 |   0.8545 |        - |   81.91 KB |        0.85 |
| LoggerAdapter_WithReferenceType |   100 |     84.14 us |   1.678 us |   3.233 us |     83.19 us |  0.94 |    0.04 |   15.5029 |   0.7324 |        - |   79.56 KB |        0.82 |
|           ILogger_WithValueType |   100 |     90.16 us |   1.517 us |   1.345 us |     90.29 us |  1.00 |    0.00 |   18.7988 |   1.7090 |        - |   96.58 KB |        1.00 |
|       ILogger_WithReferenceType |   100 |     92.73 us |   1.339 us |   1.118 us |     93.16 us |  1.03 |    0.02 |   18.4326 |   1.5869 |        - |   94.24 KB |        0.98 |
|                                 |       |              |            |            |              |       |         |           |          |          |            |             |
|     LoggerAdapter_WithValueType |  1000 |    795.12 us |  13.559 us |  12.020 us |    798.17 us |  0.86 |    0.02 |  154.2969 |  42.9688 |        - |   792.3 KB |        0.84 |
| LoggerAdapter_WithReferenceType |  1000 |    809.43 us |  15.959 us |  24.846 us |    799.37 us |  0.88 |    0.03 |  150.3906 |  37.1094 |        - |  768.86 KB |        0.82 |
|           ILogger_WithValueType |  1000 |    927.77 us |  16.804 us |  15.718 us |    927.99 us |  1.00 |    0.00 |  176.7578 |  60.5469 |        - |  940.69 KB |        1.00 |
|       ILogger_WithReferenceType |  1000 |    923.30 us |  11.470 us |  10.168 us |    920.73 us |  0.99 |    0.02 |  172.8516 |  58.5938 |        - |  917.25 KB |        0.98 |
|                                 |       |              |            |            |              |       |         |           |          |          |            |             |
|     LoggerAdapter_WithValueType | 10000 | 12,360.32 us | 235.461 us | 231.254 us | 12,381.22 us |  0.81 |    0.02 | 1296.8750 | 546.8750 | 265.6250 | 7994.95 KB |        0.84 |
| LoggerAdapter_WithReferenceType | 10000 | 12,021.00 us | 240.273 us | 661.783 us | 11,753.21 us |  0.79 |    0.05 | 1234.3750 | 468.7500 | 234.3750 | 7760.56 KB |        0.82 |
|           ILogger_WithValueType | 10000 | 15,300.89 us | 279.522 us | 247.789 us | 15,250.01 us |  1.00 |    0.00 | 1546.8750 | 640.6250 | 312.5000 | 9479.68 KB |        1.00 |
|       ILogger_WithReferenceType | 10000 | 17,030.86 us | 336.816 us | 580.992 us | 17,074.59 us |  1.11 |    0.06 | 1625.0000 | 625.0000 | 312.5000 |  9245.3 KB |        0.98 |
```

To run the benchmark, execute the following from the LoggerAdapter.Benchmarks folder
```sh
dotnet run -c Release -- --job short --runtimes net6.0 --filter Benchmark
```

## Tests
There is 87% of code covereage.

## CI/CD
Continuous Delivery is configured using GitHub Actions.
See https://github.com/JeffersonAmori/HighLowGame/blob/master/.github/workflows/Hi-Lo-Game.yml

## Monitoring
App is monitored using Application Insights
![image](https://user-images.githubusercontent.com/9205694/209887531-c59747f4-0f85-4165-8ff8-0553968f375c.png)


## Deviation from C# style guidelines
The naming convention for private and private static differs from the Microsoft's C# style on purpose based on personal preference and industry standards.

## Out of scope
* Have a separate Hub project to support multiple clients.
* Validate the input on the front-end as it is desirable to keep open the possibility of the user type commands as well as guesses.
