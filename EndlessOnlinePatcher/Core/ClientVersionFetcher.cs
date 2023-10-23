using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EndlessOnlinePatcher.Core;
public partial class ClientVersionFetcher : IClientVersionFetcher
{
    [GeneratedRegex("href=\"(.*EndlessOnline(\\d*).zip)\"")]
    private static partial Regex EndlessOnlineZipRegex();

    public FileVersion GetLocal(string path)
    {
        var versionInfo = FileVersionInfo.GetVersionInfo(path);

        if (versionInfo == null)
            return new FileVersion(0, 0, 0, 0);

        return FileVersion.FromString(versionInfo.FileVersion ?? throw new ArgumentNullException(versionInfo.FileVersion));
    }

    public async Task<(string downloadLink, FileVersion)> GetRemoteAsync(string url)
    {
        using var httpClient = new HttpClient();
        var endlessHomePage = await httpClient.GetStringAsync(url);
        var regexVersionMatches = EndlessOnlineZipRegex().Match(endlessHomePage);
        var downloadLink = regexVersionMatches.Groups[1].Value;
        var major = int.Parse(regexVersionMatches.Groups[2].Value[0].ToString());
        var minor = int.Parse(regexVersionMatches.Groups[2].Value[1].ToString());
        var build = int.Parse(regexVersionMatches.Groups[2].Value[2..].ToString());

        return (downloadLink, new FileVersion(major, minor, build, 0));
    }
}