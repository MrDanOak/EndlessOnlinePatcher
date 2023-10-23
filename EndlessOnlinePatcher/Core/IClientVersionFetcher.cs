namespace EndlessOnlinePatcher.Core;

public interface IClientVersionFetcher
{
    FileVersion GetLocal(string path);
    Task<(string downloadLink, FileVersion)> GetRemoteAsync(string url);
}