namespace WashingMachineManagementApi.Application.Common.Extensions;

public static class StringExtensions
{
    public static string FirstCharacterToLower(this string input)
    {
        return string.Concat(input[0].ToString().ToLower(), input.AsSpan(1));
    }

    public static string FirstCharacterToUpper(this string input)
    {
        return string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1));
    }
}
