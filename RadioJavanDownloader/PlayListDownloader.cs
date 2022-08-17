using RadioJavanDownloader;

public class PlayListDownloader
{
    private const string _baseUrl = @"https://www.radiojavan.com/playlists/playlist/mp3";
    private const string _baseDownloadUrl = @"https://host2.rj-mw1.com/media/mp3/mp3-320/{0}.mp3?playlist=20cac83f1179";
    public static async Task<bool> DownloadPlayList(string playListId)
    {
        var playListUrl = _baseUrl + "/" + playListId;
        var musicList = await HtmlUtils.GetMusicInfo(playListUrl, _baseDownloadUrl);
        foreach (var musicInfo in musicList)
        {
            var pathToDownload = $"{musicInfo.ArtistName} - {musicInfo.SongName} (320).mp3";
            var result = await Downloader.DownloadFileAsync(musicInfo.DownloadUrl, pathToDownload);
          
        }

        return true;
    }

}