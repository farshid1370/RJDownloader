﻿namespace RadioJavanDownloader;

public static class Downloader
{
    private static readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };

    public static async Task DownloadFileAsync(string uri
        , string outputPath)
    {

        if (!Uri.TryCreate(uri, UriKind.Absolute, out var uriResult))
            throw new InvalidOperationException("URI is invalid.");
        var directoryName = outputPath.Split('/').First();
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"{outputPath} is exists");
            Console.WriteLine("---------------------------------------------------------");
          
        }
        Console.WriteLine($"{outputPath} downloading ...");
        var fileBytes = await _httpClient.GetByteArrayAsync(uri);
        if (fileBytes.Length <= 0)
        {
            Console.WriteLine($"{outputPath} failed!");
            Console.WriteLine("---------------------------------------------------------");
           
        }
        await File.WriteAllBytesAsync(outputPath, fileBytes);
       
       
    }
}