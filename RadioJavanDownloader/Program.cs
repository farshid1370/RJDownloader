

while (true)
{
    Console.WriteLine("Please Insert Your PlayList Id :");
    var playListId = Console.ReadLine();


   var playList=await PlayListDownloader.DownloadPlayList(playListId,false);

    Console.WriteLine($"Playlist {playList.Name} has {playList.ItemCount} music for download press any key");
    Console.ReadLine();
    await PlayListDownloader.DownloadPlayList(playListId, true);
    Console.WriteLine($"All music in playlist {playListId} downloaded");

}

