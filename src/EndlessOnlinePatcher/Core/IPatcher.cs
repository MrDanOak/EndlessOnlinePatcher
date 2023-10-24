namespace EndlessOnlinePatcher.Core;

internal interface IPatcher
{
    public Task Patch(FileVersion version);
    public void ApplyPatch(FileVersion version);
}
