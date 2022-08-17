namespace RadioJavanDownloader;

public static class Downloader
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task<bool> DownloadFileAsync(string uri
        , string outputPath)
    {

        if (!Uri.TryCreate(uri, UriKind.Absolute, out var uriResult))
            throw new InvalidOperationException("URI is invalid.");

        if (File.Exists(outputPath))
        {
            Console.WriteLine($"{outputPath} is exists");
            Console.WriteLine("---------------------------------------------------------");
            return true;
        }
        Console.WriteLine($"{outputPath} downloading ...");
        var fileBytes = await _httpClient.GetByteArrayAsync(uri);
        await File.WriteAllBytesAsync(outputPath, fileBytes);
        Console.WriteLine($"{outputPath} downloaded!");
        Console.WriteLine("---------------------------------------------------------");
        return true;
    }
}