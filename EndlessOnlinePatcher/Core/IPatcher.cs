namespace EndlessOnlinePatcher.Core;

internal interface IPatcher
{
    public Task Patch(IProgress<int> progress, string downloadLink);
}
