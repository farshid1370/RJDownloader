using RadioJavanDownloader;

public class PlayListDownloader
{
    private const string _baseUrl = @"https://www.radiojavan.com/playlists/playlist/mp3";
    private const string _baseDownloadUrl = @"https://host2.rj-mw1.com/media/mp3/mp3-320/{0}.mp3";

    public static async Task<PlayListInfo> DownloadPlayList(string playListId, bool download)
    {

        var playListUrl = _baseUrl + "/" + playListId;
        var downloadUrl = $"{_baseDownloadUrl}?playlist={playListId}";
        var playListInfo = await HtmlUtils.GetPlayListInfo(playListUrl, downloadUrl);
        var musicList = await HtmlUtils.GetMusicInfo(playListUrl, downloadUrl);
        playListInfo.ItemCount = musicList.Count;

        if (!download) return playListInfo;

        foreach (var musicInfo in musicList)
        {
            var pathToDownload = @$"{playListInfo.Name}/{musicInfo.ArtistName} - {musicInfo.SongName} (320).mp3";
            await Downloader.DownloadFileAsync(musicInfo.DownloadUrl, pathToDownload);

        }
        return playListInfo;


    }
}