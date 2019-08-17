using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace SnakeCaseConversionBenchmark
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class SnakeCaseConventioneerBenchmark
    {
        public static string name = "FirstName";

        [Benchmark]
        public string ToSnakeCaseRegex() => Regex.Replace(name, @"(\w)([A-Z])", "$1_$2").ToLower();

        [Benchmark]
        public string ToSnakeCaseLinq() =>
            string.Concat(name.Select((letter, index) =>
                index > 0 && char.IsUpper(letter) ? "_" + letter : letter.ToString())).ToLower();

        [Benchmark]
        public string ToSnakeCaseStringBuilderBySpan()
        {
            ReadOnlySpan<char> spanName = name;

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < spanName.Length; index++)
            {
                if (spanName[0] != spanName[index] && char.IsUpper(spanName[index]))
                {
                    sb.Append('_');
                    sb.Append(spanName[index]);
                }
                else
                {
                    sb.Append(spanName[index]);
                }
            }

            return sb.ToString().ToLower();
        }

        [Benchmark]
        public string ToSnakeCaseBySpan()
        {
            ReadOnlySpan<char> spanName = name.AsSpan();

            int upperCaseCount = name.Count(n => char.IsUpper(n) && n != name[0]);

            char[] snakeCaseName = new char[name.Length + upperCaseCount];

            int snakeCaseNameIndex = 0;

            int spanNameIndex = 0;

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

            return snakeCaseName.ToString();
        }


        [Benchmark]
        public string ToSnakeCaseNewtonsoftJsonBySpan()
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            var sb = new StringBuilder();
            var state = SnakeCaseState.Start;

            var nameSpan = name.AsSpan();

            for (int i = 0; i < nameSpan.Length; i++)
            {
                if (nameSpan[i] == ' ')
                {
                    if (state != SnakeCaseState.Start)
                    {
                        state = SnakeCaseState.NewWord;
                    }
                }
                else if (char.IsUpper(nameSpan[i]))
                {
                    switch (state)
                    {
                        case SnakeCaseState.Upper:
                            bool hasNext = (i + 1 < nameSpan.Length);
                            if (i > 0 && hasNext)
                            {
                                char nextChar = nameSpan[i + 1];
                                if (!char.IsUpper(nextChar) && nextChar != '_')
                                {
                                    sb.Append('_');
                                }
                            }

                            break;
                        case SnakeCaseState.Lower:
                        case SnakeCaseState.NewWord:
                            sb.Append('_');
                            break;
                    }

                    sb.Append(char.ToLowerInvariant(nameSpan[i]));
                    state = SnakeCaseState.Upper;
                }
                else if (nameSpan[i] == '_')
                {
                    sb.Append('_');
                    state = SnakeCaseState.Start;
                }
                else
                {
                    if (state == SnakeCaseState.NewWord)
                    {
                        sb.Append('_');
                    }

                    sb.Append(nameSpan[i]);
                    state = SnakeCaseState.Lower;
                }
            }

            return sb.ToString();
        }

        [Benchmark]
        public string ToSnakeCaseNewtonsoftJson()
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            StringBuilder sb = new StringBuilder();
            SnakeCaseState state = SnakeCaseState.Start;

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == ' ')
                {
                    if (state != SnakeCaseState.Start)
                    {
                        state = SnakeCaseState.NewWord;
                    }
                }
                else if (char.IsUpper(name[i]))
                {
                    switch (state)
                    {
                        case SnakeCaseState.Upper:
                            bool hasNext = (i + 1 < name.Length);
                            if (i > 0 && hasNext)
                            {
                                char nextChar = name[i + 1];
                                if (!char.IsUpper(nextChar) && nextChar != '_')
                                {
                                    sb.Append('_');
                                }
                            }

                            break;
                        case SnakeCaseState.Lower:
                        case SnakeCaseState.NewWord:
                            sb.Append('_');
                            break;
                    }

                    char c;
#if HAVE_CHAR_TO_LOWER_WITH_CULTURE
                    c = char.ToLower(s[i], CultureInfo.InvariantCulture);
#else
                    c = char.ToLowerInvariant(name[i]);
#endif
                    sb.Append(c);

                    state = SnakeCaseState.Upper;
                }
                else if (name[i] == '_')
                {
                    sb.Append('_');
                    state = SnakeCaseState.Start;
                }
                else
                {
                    if (state == SnakeCaseState.NewWord)
                    {
                        sb.Append('_');
                    }

                    sb.Append(name[i]);
                    state = SnakeCaseState.Lower;
                }
            }

            return sb.ToString();
        }
    }

    internal enum SnakeCaseState
    {
        Start,
        Lower,
        Upper,
        NewWord
    }
}