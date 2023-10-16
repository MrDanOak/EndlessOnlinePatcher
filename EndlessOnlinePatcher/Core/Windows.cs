using System.Diagnostics;

namespace EndlessOnlinePatcher.Core;

public static class Windows
{

    public static async Task StartEO(string path)
    {
        await Task.Run(() =>
        {
            var processStartInfo = new ProcessStartInfo(path + "Endless.exe") { Verb = "" };
            Directory.SetCurrentDirectory(path);
            Process.Start(processStartInfo);
        });
    }
}
