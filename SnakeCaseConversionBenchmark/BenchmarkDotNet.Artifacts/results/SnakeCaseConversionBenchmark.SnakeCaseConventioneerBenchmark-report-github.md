``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview8-013656
  [Host]     : .NET Core 3.0.0-preview8-28405-07 (CoreCLR 4.700.19.37902, CoreFX 4.700.19.40503), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview8-28405-07 (CoreCLR 4.700.19.37902, CoreFX 4.700.19.40503), 64bit RyuJIT


```
|                          Method |        Mean |      Error |     StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------------- |------------:|-----------:|-----------:|-----:|-------:|------:|------:|----------:|
|  ToSnakeCaseStringBuilderBySpan |    85.97 ns |  1.0500 ns |  0.9821 ns |    1 | 0.0637 |     - |     - |     200 B |
| ToSnakeCaseNewtonsoftJsonBySpan |    86.35 ns |  1.7816 ns |  1.4877 ns |    1 | 0.0484 |     - |     - |     152 B |
|       ToSnakeCaseNewtonsoftJson |    88.89 ns |  0.8417 ns |  0.7461 ns |    2 | 0.0484 |     - |     - |     152 B |
|               ToSnakeCaseBySpan |   122.01 ns |  1.9066 ns |  1.6902 ns |    3 | 0.0253 |     - |     - |      80 B |
|                 ToSnakeCaseLinq |   365.75 ns |  4.4839 ns |  3.5008 ns |    4 | 0.1450 |     - |     - |     456 B |
|                ToSnakeCaseRegex | 2,108.71 ns | 40.6829 ns | 31.7625 ns |    5 | 0.1564 |     - |     - |     496 B |
