using System.Text.RegularExpressions;

namespace Crm_Api.Shared.Constants;

public partial class RegexPatterns
{
    public readonly static Regex LetterOnlyRegex = LetterOnly();

    [GeneratedRegex("^[a-zA-Z]+$", RegexOptions.Compiled)]
    private static partial Regex LetterOnly();

    public readonly static Regex PhoneNumberRegex = PhoneNumber();

    [GeneratedRegex("^[0-9]{10}$", RegexOptions.Compiled)]
    private static partial Regex PhoneNumber();
}
