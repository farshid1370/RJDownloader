using HtmlAgilityPack;

namespace RadioJavanDownloader;

public static class HtmlUtils
{
    public static async Task<List<MusicInfo>> GetMusicInfo(string playListUrl,string baseDownloadUrl)
    {
        var web = new HtmlWeb();
        var htmlDoc = await web.LoadFromWebAsync(playListUrl);

        var playListHtmlDoc = htmlDoc.DocumentNode.SelectNodes("//body/div").First(x => x.Id == "playlist").InnerHtml;
        htmlDoc.LoadHtml(playListHtmlDoc);

        var sidePanelHtmlDoc = htmlDoc.DocumentNode.SelectSingleNode("//div").SelectNodes("//div").First(x => x.Attributes["class"].Value == "sidePanel").InnerHtml;
        htmlDoc.LoadHtml(sidePanelHtmlDoc);

        var listMusicHtmlDoc = htmlDoc.DocumentNode.SelectSingleNode("//ul").SelectNodes("//li").Select(x=>x.InnerHtml).ToList();


        var musicList = new List<MusicInfo>();

        foreach (var item in listMusicHtmlDoc.Where(item => !string.IsNullOrEmpty(item)))
        {
            htmlDoc.LoadHtml(item);
            var songInfo = htmlDoc.DocumentNode.SelectSingleNode("//img").Attributes["data-src"].Value.Split('/')[5];
            var artistName = htmlDoc.DocumentNode.SelectNodes("//div/span").First(x => x.Attributes["class"].Value == "artist").InnerText;
            var songName = htmlDoc.DocumentNode.SelectNodes("//div/span").First(x => x.Attributes["class"].Value == "song").InnerText;
            var downloadUrl =string.Format(baseDownloadUrl, songInfo) ;

            musicList.Add(new MusicInfo
            {
                ArtistName = artistName, SongName = songName, DownloadUrl = downloadUrl
            });
        }

        return musicList;
    }
}