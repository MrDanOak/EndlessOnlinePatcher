using EndlessOnlinePatcher.Extensions;

namespace EndlessOnlinePatcher.Core;
public class Patcher : IPatcher
{
    public async Task Patch(IProgress<int> progress, string downloadLink)
    {
        using var httpClient = new HttpClient();
        using var download = await httpClient.GetStreamAsync(downloadLink);
        using var fileStream = new FileStream("patch.zip", FileMode.Create, FileAccess.Write);
        await httpClient.DownloadAsync(downloadLink, fileStream, progress);
    }
}