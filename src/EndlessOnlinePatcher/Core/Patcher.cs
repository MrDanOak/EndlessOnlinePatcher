using EndlessOnlinePatcher.Extensions;
using System.Diagnostics;
using System.IO.Compression;
using System.Numerics;
using System.Text.RegularExpressions;

namespace EndlessOnlinePatcher.Core;

public sealed class Patcher : IPatcher, IDisposable
{
    private string _link { get; }

    private Action<string> _setPatchTextCallback;

    public Patcher(string downloadLink, Action<string> setPatchTextCallback)
    {
        _link = downloadLink;
        _setPatchTextCallback = setPatchTextCallback;
    }

    public async Task Patch(FileVersion version)
    {
        Clean();
        await DownloadPatch();
        await ApplyPatch(version);
    }

    private async Task DownloadPatch()
    {
        var progress = new Progress<int>(x => _setPatchTextCallback($"Downloading... {x}%"));
        using var httpClient = new HttpClient();
        using var fileStream = new FileStream("patch.zip", FileMode.Create, FileAccess.Write);
        await httpClient.DownloadAsync(_link, fileStream, progress);
        fileStream.Close();
    }

    private async Task ApplyPatch(FileVersion version)
    {
        var localDirectory = EndlessOnlineDirectory.Get().FullName;
        var patchFolder = $"patch-{version}/";

        ZipFile.ExtractToDirectory("patch.zip", patchFolder);
        var patchFiles = Directory.EnumerateFiles(patchFolder, "*", SearchOption.AllDirectories)
            .Select(x => x.Remove(0, patchFolder.Length))
            .ToList();

        var completed = 0;

        var extractTasks = patchFiles
            .Select(file => Task.Run(() =>
            {
                var relativeDirectory = $"{localDirectory}/{GetDirectoryFrom(file)}";
                if (!Directory.Exists(relativeDirectory))
                    Directory.CreateDirectory(relativeDirectory);

                File.Copy($"{patchFolder}/{file}", $"{localDirectory}/{file}", true);

                completed++;

                var percent = 100f * completed / patchFiles.Count;
#if DEBUG
                Debug.WriteLine($"Copying {file} to {localDirectory}/{file} {completed}/{patchFiles.Count} {percent}%");
#endif
                _setPatchTextCallback($"Extracting... {(int)percent}%");
            }));

        await Task.WhenAll(extractTasks);

        _setPatchTextCallback($"Patch applied! You are now on the latest version v{version}. Enjoy!");
    }

    private static string GetDirectoryFrom(string filePath)
    {
        var regex = new Regex("(.*)\\\\");
        return regex.Match(filePath).Value;
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