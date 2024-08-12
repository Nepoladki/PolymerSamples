using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using System.Text.RegularExpressions;

namespace PolymerSamples.Validation;

public partial class CodeValidation : ICodeValidator
{
    private const string BandRegexPattern = @"^([0-9]+\.){3}[0-9]+$";
    private const string FlatBeltRegexPattern = @"^[A-Z]+\.[0-9]+[A-Z]\.[0-9]+\.[0-9]+$";
    private readonly Regex BandCompiledRegex = BandRegex();
    private readonly Regex FlatBeltCompiledRegex = FlatBeltRegex();

    [GeneratedRegex(FlatBeltRegexPattern, RegexOptions.Compiled)]
    private static partial Regex FlatBeltRegex();
    [GeneratedRegex(BandRegexPattern, RegexOptions.Compiled)]
    private static partial Regex BandRegex();

    private bool ValidateBand(string input)
    {
        return BandCompiledRegex.IsMatch(input);
    }

    private bool ValidateFlatBelt(string input)
    {
        return FlatBeltCompiledRegex.IsMatch(input);
    }

    public bool ValidateCode(CodeDTO code)
    {
        return code.type_id switch
        {
            1 => ValidateBand(code.short_code_name),
            2 => ValidateBand(code.short_code_name),
            3 => ValidateFlatBelt(code.short_code_name),
            _ => true,
        };
    }
}
