## Snake-Case Conversion 
In this repository I prepared some experimental implementation of snake-case with c# and .NET Core.

### What is Snake-Case?
The snake-case is a naming convention that developers use it for variable or key-value naming such as json, its format is like following json object. Splitting words with `_` and making them lowercase.
```json
{
    "first_name": "Soheil",
    "last_name": "Alizadeh"
}

```

## Benchmarks
``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview9-014004
  [Host]     : .NET Core 3.0.0-preview9-19423-09 (CoreCLR 4.700.19.42102, CoreFX 4.700.19.42104), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview9-19423-09 (CoreCLR 4.700.19.42102, CoreFX 4.700.19.42104), 64bit RyuJIT


```
|                          Method |        Mean |      Error |     StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------------- |------------:|-----------:|-----------:|-----:|-------:|------:|------:|----------:|
|               ToSnakeCaseBySpan |    27.47 ns |  0.4629 ns |  0.4330 ns |    1 | 0.0153 |     - |     - |      48 B |
|  ToSnakeCaseStringBuilderBySpan |    85.23 ns |  1.6495 ns |  1.3774 ns |    2 | 0.0637 |     - |     - |     200 B |
|       ToSnakeCaseNewtonsoftJson |    85.72 ns |  1.6418 ns |  1.4554 ns |    2 | 0.0484 |     - |     - |     152 B |
| ToSnakeCaseNewtonsoftJsonBySpan |    86.96 ns |  1.7060 ns |  1.5958 ns |    2 | 0.0484 |     - |     - |     152 B |
|                 ToSnakeCaseLinq |   353.42 ns |  3.9670 ns |  3.7108 ns |    3 | 0.1450 |     - |     - |     456 B |
|                ToSnakeCaseRegex | 2,056.69 ns | 29.5694 ns | 26.2125 ns |    4 | 0.1526 |     - |     - |   
