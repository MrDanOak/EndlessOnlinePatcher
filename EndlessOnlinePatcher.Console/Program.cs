using EndlessOnlinePatcher.Core;
using System.IO.Compression;

var path = "C:/Program Files (x86)/Endless Online/";
var clientVersionFetcher = new ClientVersionFetcher();
var localVersion = clientVersionFetcher.GetLocal($"{path}Endless.exe");
Console.WriteLine($"Local Version {localVersion}");
(var downloadLink, var remoteVersion) = await clientVersionFetcher.GetRemoteAsync("https://www.endless-online.com/client/download.html");
Console.WriteLine($"Remote Version {remoteVersion}");

if (remoteVersion > localVersion)
{
    var patcher = new Patcher();
    Console.WriteLine("Downloading remote version...");
    {
        var currentTop = Console.CursorTop;
        var progress = new Progress<int>(x =>
        {
            Console.SetCursorPosition(0, currentTop);
            Console.Write($"Progress {x}%");
        });

        await patcher.Patch(progress, downloadLink);
        Console.WriteLine();
    }
    Console.WriteLine("Download complete. Extracting...");
    ZipFile.ExtractToDirectory("patch.zip", path, System.Text.Encoding.UTF8, true);
    Console.WriteLine("Extract Complete. Starting Endless...");
    await Windows.StartEO(path);
}