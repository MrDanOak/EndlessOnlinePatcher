using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EndlessOnlinePatcher.Core;
public partial class ClientVersionFetcher : IClientVersionFetcher
{
    [GeneratedRegex("href=\"(.*EndlessOnline(\\d*)([a-zA-Z])*.zip)\"")]
    private static partial Regex EndlessOnlineZipRegex();

    public FileVersion GetLocal()
    {
        if (!File.Exists(EndlessOnlineDirectory.GetExe()))
            return new FileVersion(0, 0, 0, 0);

        var versionInfo = FileVersionInfo.GetVersionInfo(EndlessOnlineDirectory.GetExe());

        if (versionInfo == null)
            return new FileVersion(0, 0, 0, 0);

        return FileVersion.FromString(versionInfo.FileVersion ?? throw new ArgumentNullException(versionInfo.FileVersion));
    }

    public async Task<(string downloadLink, FileVersion)> GetRemoteAsync()
    {
        using var httpClient = new HttpClient();
        var endlessHomePage = await httpClient.GetStringAsync("https://www.endless-online.com/client/download.html");
        var regexVersionMatches = EndlessOnlineZipRegex().Match(endlessHomePage);
        var downloadLink = regexVersionMatches.Groups[1].Value;
        var major = int.Parse(regexVersionMatches.Groups[2].Value[0].ToString());
        var minor = int.Parse(regexVersionMatches.Groups[2].Value[1].ToString());
        var build = int.Parse(regexVersionMatches.Groups[2].Value[2..].ToString());

        if (regexVersionMatches.Groups.Count > 3 && !string.IsNullOrWhiteSpace(regexVersionMatches.Groups[3].Value))
        {
            var patchAlpha = regexVersionMatches.Groups[3].Value[0];
            var patch = patchAlpha switch
            {
                var p when patchAlpha >= 'a' && patchAlpha <= 'z' => p - 'a',
                var p when patchAlpha >= 'A' && patchAlpha <= 'Z' => p - 'A',
                _ => throw new ArgumentException("Patch must be alphabetical")
            };

            return (downloadLink, new FileVersion(major, minor, build, patch));
        }

        return (downloadLink, new FileVersion(major, minor, build, 0));
    }
}