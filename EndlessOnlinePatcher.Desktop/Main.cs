using EndlessOnlinePatcher.Core;
using EndlessOnlinePatcher.Desktop.Properties;
using System.IO.Compression;
using System.Text;

namespace EndlessOnlinePatcher.Desktop;

public partial class Main : Form
{
    private FileVersion? _remoteVersion = default;
    private FileVersion? _localVersion = default;
    private const string _path = "C:/Program Files (x86)/Endless Online/";
    private bool _patched = false;
    private readonly IClientVersionFetcher _clientVersionFetcher;
    private bool ShowPatch() => _localVersion! < _remoteVersion! && !_patched;

    public Main()
    {
        InitializeComponent();
        _clientVersionFetcher = new ClientVersionFetcher();
    }

    private async void Main_Shown(object sender, EventArgs e)
    {
        var localVersion = _clientVersionFetcher.GetLocal($"{_path}Endless.exe");
        _localVersion = localVersion;
    }
}
