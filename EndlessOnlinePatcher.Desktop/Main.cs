using EndlessOnlinePatcher.Core;
using EndlessOnlinePatcher.Desktop.Properties;
using System.Media;

namespace EndlessOnlinePatcher.Desktop;

public partial class Main : Form
{
    private FileVersion? _remoteVersion = default;
    private FileVersion? _localVersion = default;
    private const string _path = "C:/Program Files (x86)/Endless Online/";
    private const string _clientDownloadPage = "https://www.endless-online.com/client/download.html";
    private bool _patching = false;
    private bool _dragging;
    private Point _mouseDownLocation;
    private string _downloadLink = "";
    private readonly IClientVersionFetcher _clientVersionFetcher;
    private SoundPlayer _sndClickDown = new(Resources.click_down);
    private SoundPlayer _sndClickUp = new(Resources.click_up);

    public Main()
    {
        InitializeComponent();
        _clientVersionFetcher = new ClientVersionFetcher();
    }

    private async void Main_Shown(object sender, EventArgs e)
    {
        _localVersion = _clientVersionFetcher.GetLocal($"{_path}Endless.exe");
        (_downloadLink, _remoteVersion) = await _clientVersionFetcher.GetRemoteAsync(_clientDownloadPage);
        if (_localVersion == _remoteVersion)
        {
            lblMessage.Text = "You are already up to date with the latest version";
            pbxLaunch.Visible = true;
        }
        else
        {
            lblMessage.Text = $"A new version of the client is available (v{_localVersion} -> v{_remoteVersion})";
            pbxPatch.Visible = true;
        }
        pbxExit.Visible = true;
    }

    private void Main_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _dragging = true;
            _mouseDownLocation = new Point(e.X, e.Y);
        }
    }

    private void Main_MouseUp(object sender, MouseEventArgs e)
    {
        _dragging = false;
    }

    private void Main_MouseMove(object sender, MouseEventArgs e)
    {
        if (_dragging)
        {
            Location = new Point(Location.X + e.X - _mouseDownLocation.X, Location.Y + e.Y - _mouseDownLocation.Y);
        }
    }

    private void pbxLogout_MouseEnter(object sender, EventArgs e)
    {
        pbxLogout.Image = Resources.eo_logout_hover;
    }

    private void pbxLogout_MouseLeave(object sender, EventArgs e)
    {
        pbxLogout.Image = Resources.eo_logout;
    }

    private void pbxLogout_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void pbxPatch_MouseEnter(object sender, EventArgs e)
    {
        if (_patching) return;
        pbxPatch.Image = Resources.eo_patch_hover;
    }

    private void pbxPatch_MouseLeave(object sender, EventArgs e)
    {
        if (_patching) return;
        pbxPatch.Image = Resources.eo_patch;
    }

    private void pbxLaunch_MouseEnter(object sender, EventArgs e)
    {
        pbxLaunch.Image = Resources.eo_launch_hover;
    }

    private void pbxLaunch_MouseLeave(object sender, EventArgs e)
    {
        pbxLaunch.Image = Resources.eo_launch;
    }

    private async void pbxLaunch_Click(object sender, EventArgs e)
    {
        await Windows.StartEO(_path);
        Close();
    }

    private void pbxExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void pbxExit_MouseEnter(object sender, EventArgs e)
    {
        pbxExit.Image = Resources.eo_exit_hover;
    }

    private void pbxExit_MouseLeave(object sender, EventArgs e)
    {
        pbxExit.Image = Resources.eo_exit;
    }

    private async void pbxPatch_MouseClick(object sender, MouseEventArgs e)
    {
        if (_patching) return;

        _patching = true;
        pbxPatch.Image = Resources.eo_patching;
        var status = () => "Downloading Latest Patch";
        using var patcher = new Patcher(new Progress<int>(x =>
        {
            lblMessage.Text = $"{status()}... {x}%.";
        }), _downloadLink, _path);
        await patcher.Patch(_remoteVersion!);
        status = () => "Extracting and Applying Latest Patch";
        patcher.ApplyPatch(_remoteVersion!);
        _patching = false;
        pbxPatch.Visible = false;
        pbxLaunch.Visible = true;
    }

    private void pbxLaunch_MouseDown(object sender, MouseEventArgs e)
    {
        _sndClickDown.Play();
    }

    private void pbxExit_MouseDown(object sender, MouseEventArgs e)
    {
        _sndClickDown.Play();
    }

    private void pbxPatch_MouseDown(object sender, MouseEventArgs e)
    {
        _sndClickDown.Play();
    }

    private void pbxPatch_MouseUp(object sender, MouseEventArgs e)
    {
        _sndClickUp.Play();
    }

    private void pbxLaunch_MouseUp(object sender, MouseEventArgs e)
    {
        _sndClickUp.Play();
    }

    private void pbxExit_MouseUp(object sender, MouseEventArgs e)
    {
        _sndClickUp.Play();
    }

    private void pbxLogout_MouseDown(object sender, MouseEventArgs e)
    {
        _sndClickDown.Play();
    }

    private void pbxLogout_MouseUp(object sender, MouseEventArgs e)
    {
        _sndClickUp.Play();
    }
}
