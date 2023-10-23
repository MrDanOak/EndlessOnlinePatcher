using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EndlessOnlinePatcher.Core;

public record FileVersion(int Major = 0, int Minor = 0, int Build = 0, int Revision = 0)
{
    public override string ToString() => $"{Major}.{Minor}.{Build}.{Revision}";
    public static FileVersion FromString(string s)
    {
        var parts = s.Split('.').Select(int.Parse).ToArray();
        return new FileVersion(parts[0], parts[1], parts[2], parts[3]);
    }

    public static FileVersion GetLocal(string path)
    {
        var versionInfo = FileVersionInfo.GetVersionInfo(path);

        if (versionInfo == null) 
            return new FileVersion(0, 0, 0, 0);

        return FromString(versionInfo.FileVersion ?? throw new ArgumentNullException(versionInfo.FileVersion));
    }

    public static async Task<(string downloadLink, FileVersion)> GetRemoteAsync(string url)
    {
        var regexVersion = new Regex("href=\"(.*EndlessOnline(\\d*).zip)\"");
        using var httpClient = new HttpClient();
        var endlessHomePage = await httpClient.GetStringAsync(url);
        var regexVersionMatches = regexVersion.Match(endlessHomePage);
        var downloadLink = regexVersionMatches.Groups[1].Value;
        var major = int.Parse(regexVersionMatches.Groups[2].Value[0].ToString());
        var minor = int.Parse(regexVersionMatches.Groups[2].Value[1].ToString());
        var build = int.Parse(regexVersionMatches.Groups[2].Value[2..].ToString());

        return (downloadLink, new FileVersion(major, minor, build, 0));
    }

    public static bool operator >(FileVersion a, FileVersion b)
        => a.Major > b.Major ||
        a.Major == b.Major && a.Minor > a.Minor ||
        a.Major == b.Major && a.Minor == b.Minor && a.Build > b.Build ||
        a.Major == b.Major && a.Minor == b.Minor && a.Build == b.Build && a.Revision > b.Revision;

    public static bool operator <(FileVersion a, FileVersion b)
        => a.Major < b.Major ||
        a.Major == b.Major && a.Minor < a.Minor ||
        a.Major == b.Major && a.Minor == b.Minor && a.Build < b.Build ||
        a.Major == b.Major && a.Minor == b.Minor && a.Build == b.Build && a.Revision < b.Revision;
}