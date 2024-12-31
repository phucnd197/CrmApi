namespace Crm.Api.Test.TestCommon;
public static class Helper
{
    public static string RandomLetterOnlyString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var newString = new char[length];
        for (var i = 0; i < length; i++)
        {
            newString[i] = characters[Random.Shared.Next(0, characters.Length)];
        }
        return new string(newString);
    }
}
