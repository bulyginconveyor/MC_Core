using System.Text.RegularExpressions;

namespace core_service.domain.models.valueobjects;

public partial record PhotoUrl
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
        if(string.IsNullOrEmpty(url))
            return false;
        
        var pattern = @"^http[s]?://([a-zA-Zа-яА-ЯёЁ0-9\._\-/]){1,}\.+([a-zA-Z0-9]){1,}$";
        return Regex.IsMatch(url, pattern);
    }
}