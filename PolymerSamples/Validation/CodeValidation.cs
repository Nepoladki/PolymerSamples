using PolymerSamples.Interfaces;
using System.Text.RegularExpressions;

namespace PolymerSamples.Validation;

public partial class CodeValidation : ICodeValidator
{
    private const string BandRegexPattern = @"^([0-9]+\.)+[0-9]+$";
    private const string FlatBeltRegexPattern = @"^[A-Z]+\.[0-9]+[A-Z]\.[0-9]+\.[0-9]+$";
    private readonly Regex BandCompiledRegex = BandRegex();
    private readonly Regex FlatBeltCompiledRegex = FlatBeltRegex();

    [GeneratedRegex(FlatBeltRegexPattern, RegexOptions.Compiled)]
    private static partial Regex FlatBeltRegex();
    [GeneratedRegex(BandRegexPattern, RegexOptions.Compiled)]
    private static partial Regex BandRegex();

    public bool ValidateBand(string input)
    {
        return BandCompiledRegex.IsMatch(input);
    }

    public bool ValidateFlatBelt(string input)
    {
        return FlatBeltCompiledRegex.IsMatch(input);
    }
}
