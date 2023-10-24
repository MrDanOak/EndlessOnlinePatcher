using EndlessOnlinePatcher.Extensions;
using System.IO.Compression;

namespace EndlessOnlinePatcher.Core;
public sealed class Patcher : IPatcher, IDisposable
{
    private IProgress<int> _progress { get; }
    private string _link { get; }

    public Patcher(IProgress<int> progress, string downloadLink)
    {
        _progress = progress;
        _link = downloadLink;
    }

    public async Task Patch(FileVersion version)
    {
        Clean();
        using var httpClient = new HttpClient();
        using var fileStream = new FileStream("patch.zip", FileMode.Create, FileAccess.Write);
        await httpClient.DownloadAsync(_link, fileStream, _progress);
        fileStream.Close();
    }

    public void ApplyPatch(FileVersion version)
    {
        var patchFolder = $"patch-{version}/";
        ZipFile.ExtractToDirectory("patch.zip", patchFolder);
        var allExceptConfig = Directory.EnumerateFiles(patchFolder, "*", SearchOption.AllDirectories)
            .Select(x => x.Remove(0, patchFolder.Length))
            .Where(x => !x.StartsWith("config"));

        var i = 0;
        var localDirectory = EndlessOnlineDirectory.Get().FullName;
        foreach (var file in allExceptConfig)
        {
            File.Copy($"{patchFolder}/{file}", $"{localDirectory}/{file}", true);
            i++;
            _progress.Report(i / allExceptConfig.Count() * 100);
        }
    }

    private static void Clean()
    {
        var patchDirectories = Directory.EnumerateDirectories("./").Where(x => x.Contains("patch"));
        foreach (var directory in patchDirectories)
        {
            Directory.Delete(directory, true);
        }

        var patchZips = Directory.EnumerateFiles("./", "*.zip");
        foreach (var zip in patchZips)
        {
            File.Delete(zip);
        }
    }

    public void Dispose()
    {
        Clean();
    }
}