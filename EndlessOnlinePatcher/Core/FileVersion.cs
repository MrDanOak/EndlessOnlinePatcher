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