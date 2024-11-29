using System.Text.RegularExpressions;
using static System.String;

namespace core_service.domain.valueobjects;

public record PhotoUrl
{
    public string? Url { get; init; }

    private PhotoUrl(string url)
    {
        Url = url == "null" ? null : url;
    }

    public static PhotoUrl Create(string url)
    {
        if(!IsValidUrl(url))
            throw new ArgumentException($"'{url}' is not a valid url");
        return new PhotoUrl(url);
    }

    public static PhotoUrl Empty => new PhotoUrl("null");
    public bool IsEmpty => this.Url == null;
    
    private static bool IsValidUrl(string url)
    {
        return Regex.IsMatch(url, "^http[s]?:\\/\\/[a-zA-Zа-яА-Яё-Ё0-9\\-_]{1,}\\.[a-zA-Zа-яА-Яё-Ё0-9\\-_]{1,}$");
    }
}