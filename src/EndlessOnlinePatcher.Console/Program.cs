using EndlessOnlinePatcher.Core;

var clientVersionFetcher = new ClientVersionFetcher();
var localVersion = clientVersionFetcher.GetLocal();
Console.WriteLine($"Local Version {localVersion}");
(var downloadLink, var remoteVersion) = await clientVersionFetcher.GetRemoteAsync();
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

        var patcher = new Patcher(progress, downloadLink);
        await patcher.Patch(remoteVersion);
        Console.WriteLine();
        patcher.ApplyPatch(remoteVersion);
        Console.WriteLine("Patch Applied");
    }

    await Windows.StartEO();
}