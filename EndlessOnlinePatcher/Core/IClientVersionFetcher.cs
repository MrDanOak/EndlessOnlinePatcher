namespace EndlessOnlinePatcher.Core;

public interface IClientVersionFetcher
{
    FileVersion GetLocal();
    Task<(string downloadLink, FileVersion)> GetRemoteAsync();
}