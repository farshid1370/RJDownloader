

while (true)
{
    Console.WriteLine("Please Insert Your PlayList Id :");
    var playListId = Console.ReadLine();


   await PlayListDownloader.DownloadPlayList(playListId);

   Console.WriteLine($"All music in playlist {playListId} downloaded");

}

