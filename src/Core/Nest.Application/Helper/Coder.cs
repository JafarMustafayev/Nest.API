namespace Nest.Application.Helper;

public static class Coder
{
    public static string Encode(this string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static string Decode(this string value)
    {
        byte[] bytes = WebEncoders.Base64UrlDecode(value);
        return Encoding.UTF8.GetString(bytes);
    }
}