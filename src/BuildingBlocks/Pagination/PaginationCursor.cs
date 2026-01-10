using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace BuildingBlocks.Pagination;

public abstract class PaginationCursor
{
    public int PageIndex { get; set; }

    public string Encode()
    {
        var serializedCursor = JsonSerializer.Serialize(this);
        return Base64UrlEncoder.Encode(serializedCursor);
    }

    public static T Decode<T>(string encodedCursor) where T : PaginationCursor
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encodedCursor);
        var decodedStr = Base64UrlEncoder.Decode(encodedCursor);
        var cursor = JsonSerializer.Deserialize<T>(decodedStr);
        return cursor!;
    }
}