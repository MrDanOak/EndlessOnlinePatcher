using EoPatcher.Core.Services;
using EoPatcher.Services.VersionFetchers;

namespace EoPatcher.Services;

internal interface IPatchOrchestrator
{
    public Task Patch(Version version);
}

public sealed class PatchOrchestrator : IPatchOrchestrator, IDisposable
{
    private IHttpService _httpService;
    private IFileService _fileService;
    private ILocalVersionRepository _localVersionRepository;

    public PatchOrchestrator(Action<string> setPatchTextCallback)
    {
        _httpService = new HttpService(setPatchTextCallback);
        _fileService = new FileService(setPatchTextCallback);
        _localVersionRepository = new LocalVersionRepository();
    }

    public async Task Patch(Version version)
    {
        Clean();
        await _httpService.DownloadLatestPatchAsync(version);
        await _fileService.ExtractPatch(version);
        _localVersionRepository.Save(version);
    }

    public void Dispose()
    {
        Clean();
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
}