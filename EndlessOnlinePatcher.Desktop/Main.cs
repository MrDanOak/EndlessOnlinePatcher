using EndlessOnlinePatcher.Core;
using EndlessOnlinePatcher.Desktop.Properties;
using System.IO.Compression;
using System.Text;

namespace EndlessOnlinePatcher.Desktop;

public partial class Main : Form
{
    private string _downloadLink = "";
    private FileVersion? _remoteVersion = default;
    private FileVersion? _localVersion = default;
    private const string _path = "C:/Program Files (x86)/Endless Online/";
    private bool _patched = false;
    private bool _dragging;
    private Point _mouseDownLocation;
    private IClientVersionFetcher _clientVersionFetcher;

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
        await UpdateVersionLabels();
        pnlPatch.Visible = ShowPatch();
        txtOutput.Visible = ShowPatch();
        pnlUpToDate.Visible = !ShowPatch();
    }

    private async Task UpdateVersionLabels()
    {
        lblLocal.Text = $"Local: v{_localVersion}";
        lblRemote.Text = "Remote: fetching...";
        (var downloadLink, var remoteVersion) = await _clientVersionFetcher.GetRemoteAsync("https://www.endless-online.com/client/download.html");
        lblRemote.Text = $"Remote: v{remoteVersion}";
        _downloadLink = downloadLink;
        _remoteVersion = remoteVersion;
    }

    private async void btnPatch_Click(object sender, EventArgs e)
    {
        btnPatch.Enabled = false;
        await Patch();
    }

    private async Task Patch()
    {
        var patcher = new Patcher();
        txtOutput.AppendText($"Beginning Patch...{Environment.NewLine}");
        var progress = new Progress<int>(x =>
        {
            prgPatch.Value = x;
        });

        await patcher.Patch(progress, _downloadLink);
        txtOutput.AppendText($"Patching Finished.{Environment.NewLine}");
        txtOutput.AppendText($"Extracting patch to install directory...{Environment.NewLine}");
        ZipFile.ExtractToDirectory("patch.zip", _path, Encoding.UTF8, true);
        txtOutput.AppendText($"Extraction finished.{Environment.NewLine}");
        _localVersion = _remoteVersion;

        if (chkAutoLaunch.Checked)
        {
            txtOutput.AppendText($"Auto-launching Endless Online.{Environment.NewLine}");
            await LaunchAndClose();
        }

        pnlPatch.Visible = ShowPatch();
        txtOutput.Visible = ShowPatch();
        pnlUpToDate.Visible = !ShowPatch();
    }

    private async Task LaunchAndClose()
    {
        await Windows.StartEO(_path);
        Close();
    }

    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _dragging = true;
            _mouseDownLocation = new Point(e.X, e.Y);
        }
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        _dragging = false;
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (_dragging)
        {
            Location = new Point(Location.X + e.X - _mouseDownLocation.X, Location.Y + e.Y - _mouseDownLocation.Y);
        }
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void pictureBox2_MouseEnter(object sender, EventArgs e)
    {
        pictureBox2.Image = Resources.eo_logout_hover;
    }

    private void pictureBox2_MouseLeave(object sender, EventArgs e)
    {
        pictureBox2.Image = Resources.eo_logout;
    }

    private async void btnLaunch_Click(object sender, EventArgs e)
    {
        await LaunchAndClose();
    }
}
