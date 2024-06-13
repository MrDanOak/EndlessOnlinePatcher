using EoPatcher.Extensions;
using System.Text.RegularExpressions;

namespace EoPatcher.Core.Services;

public interface IHttpService
{
    public Task DownloadLatestPatchAsync(Version version);
}

public partial class HttpService : IHttpService
{
    private readonly Action<string> _setPatchTextCallback;

    public HttpService(Action<string> setPatchTextCallback)
    {
        _setPatchTextCallback = setPatchTextCallback;
    }

    private async Task<string> GetLatestDownloadLinkAsync()
    {
        using var httpClient = new HttpClient();
        var endlessHomePage = await httpClient.GetStringAsync("https://www.endless-online.com/client/download.html");
        var regexVersionMatches = new Regex("href=\"(.*EndlessOnline(\\d*)([a-zA-Z])*.zip)\"").Match(endlessHomePage);
        var downloadLink = regexVersionMatches.Groups[1].Value;
        return downloadLink;
    }

    public async Task DownloadLatestPatchAsync(Version version)
    {
        var progress = new Progress<int>(x => _setPatchTextCallback($"Downloading... {x}%"));
        using var httpClient = new HttpClient();
        using var fileStream = new FileStream("patch.zip", FileMode.Create, FileAccess.Write);
        var link = await GetLatestDownloadLinkAsync();
        await httpClient.DownloadAsync(link, fileStream, progress);
        fileStream.Close();
    }
}