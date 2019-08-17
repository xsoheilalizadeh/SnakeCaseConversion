## Snake-Case Convertion 

### What is Snake-Case?
The snake-case is a naming convetion that developers use it for veriable or key-value naming such as json, its fomrat is like following json object. Spliting words with `_` and making them lowercase.
```json
{
    "first_name": "Soheil",
    "last_name":"alizadeh"
}

```

### Implemntation

In this repository I prepared three implementation of snake-case with c# and .NET Core.

1. Using Regex 
```C#
public string ToSnakeCase(string name) 
{
    return Regex.Replace(name, @"(\w)([A-Z])", "$1_$2").ToLower();
} 
```
2. Using Linq
```C#
public string ToSnakeCase(string name) 
{
    var letters = name.Select((letter, index) => index > 0 && char.IsUpper(letter) ? "_" + letter : letter.ToString());
    return  string.Concat().ToLower();
}
```
3. Custom algorithm + Span

```C#
public string ToSnakeCase(string name)
{
    var spanName = name.AsSpan();

    var upperCaseCount = name.Count(n => char.IsUpper(n) && n != name[0]);
   
    var snakeCaseName = new char[name.Length + upperCaseCount];
    var snakeCaseNameIndex = 0;
   
    var spanNameIndex = 0;

    while (snakeCaseNameIndex < snakeCaseName.Length)
    {
        if (spanNameIndex > 0 && char.IsUpper(spanName[spanNameIndex]))
        {
            snakeCaseName[snakeCaseNameIndex] = '_';
            snakeCaseName[snakeCaseNameIndex + 1] = spanName[spanNameIndex];
            snakeCaseNameIndex += 2;
            spanNameIndex++;
            continue;
        }
        snakeCaseName[snakeCaseNameIndex] = spanName[spanNameIndex];
        snakeCaseNameIndex++;
        spanNameIndex++;
    }

    return new string(snakeCaseName).ToLower();
}
```

## Benchmarks

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview8-013656
  [Host]     : .NET Core 3.0.0-preview8-28405-07 (CoreCLR 4.700.19.37902, CoreFX 4.700.19.40503), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview8-28405-07 (CoreCLR 4.700.19.37902, CoreFX 4.700.19.40503), 64bit RyuJIT


```
|          Method  |       Mean |     Error |     StdDev | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------  |-----------:|----------:|-----------:|-----:|-------:|------:|------:|----------:|
| ToSnakeCaseSpan  |   191.1 ns |  3.899 ns |   4.172 ns |    1 | 0.0560 |     - |     - |     176 B |
| ToSnakeCaseLinq  |   414.4 ns |  8.600 ns |  15.286 ns |    2 | 0.1450 |     - |     - |     456 B |
| ToSnakeCaseRegex | 2,611.7 ns | 52.147 ns | 147.932 ns |    3 | 0.1564 |     - |     - |     496 B |
