using EndlessOnlinePatcher.Core;

var path = "C:/Program Files (x86)/Endless Online/";
var clientVersionFetcher = new ClientVersionFetcher();
var localVersion = clientVersionFetcher.GetLocal($"{path}Endless.exe");
Console.WriteLine($"Local Version {localVersion}");
(var downloadLink, var remoteVersion) = await clientVersionFetcher.GetRemoteAsync("https://www.endless-online.com/client/download.html");
Console.WriteLine($"Remote Version {remoteVersion}");

if (remoteVersion > localVersion)
{
    Console.WriteLine("Downloading remote version...");
    {
        var currentTop = Console.CursorTop;
        var progress = new Progress<int>(x =>
        {
            Console.SetCursorPosition(0, currentTop);
            Console.Write($"Progress {x}%");
        });

        var patcher = new Patcher(progress, downloadLink, path);
        await patcher.Patch(remoteVersion);
        Console.WriteLine();
    }
    await Windows.StartEO(path);
}