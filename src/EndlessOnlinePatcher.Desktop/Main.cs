using EndlessOnlinePatcher.Core;
using EndlessOnlinePatcher.Desktop.Properties;
using System.Media;

namespace EndlessOnlinePatcher.Desktop;

public partial class Main : Form
{
    private FileVersion? _remoteVersion = default;
    private FileVersion? _localVersion = default;
    private bool _patching = false;
    private bool _dragging;
    private Point _mouseDownLocation;
    private string _downloadLink = "";
    private readonly IClientVersionFetcher _clientVersionFetcher;
    private readonly SoundPlayer _sndClickDown = new(Resources.click_down);
    private readonly SoundPlayer _sndClickUp = new(Resources.click_up);

    public Main()
    {
        InitializeComponent();
        _clientVersionFetcher = new ClientVersionFetcher();
    }

    private async void Main_Shown(object sender, EventArgs e)
    {
        _localVersion = _clientVersionFetcher.GetLocal();
        (_downloadLink, _remoteVersion) = await _clientVersionFetcher.GetRemoteAsync();
        if (_localVersion == _remoteVersion)
        {
            lblMessage.Text = "You are already up to date with the latest version";
            pbxLaunch.Visible = true;
        }
        else
        {
            lblMessage.Text = $"A new version of the client is available{Environment.NewLine}(v{_localVersion} -> v{_remoteVersion})";
            pbxPatch.Visible = true;
            pbxSkip.Visible = true;
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
        await Core.Windows.StartEO();
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

    private void pbxSkip_MouseEnter(object sender, EventArgs e)
    {
        pbxSkip.Image = Resources.skip_hover;
    }

    private void pbxSkip_MouseLeave(object sender, EventArgs e)
    {
        pbxSkip.Image = Resources.skip;
    }

    private async void pbxPatch_MouseClick(object sender, MouseEventArgs e)
    {
        if (_patching) return;

        _patching = true;
        pbxPatch.Image = Resources.eo_patching;
        var status = () => "Downloading Latest Patch";
        using var patcher = new Patcher(new Progress<int>(x =>
        {
            if (x >= 99 && status().Contains("Extracting"))
            {
                // weird multithreading bug because of this I think
                lblMessage.Text = "Patch applied! You are now on the latest version and can launch the game";
                return;
            }

            lblMessage.Text = $"{status()}... {x}%.";
        }), _downloadLink);
        await patcher.Patch(_remoteVersion!);
        status = () => "Extracting and Applying Latest Patch";
        patcher.ApplyPatch(_remoteVersion!);
        _patching = false;
        pbxPatch.Visible = false;
        pbxLaunch.Visible = true;
        Thread.Sleep(10);
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

    private void pbxSkip_MouseDown(object sender, MouseEventArgs e)
    {
        _sndClickDown.Play();
    }

    private void pbxSkip_MouseUp(object sender, MouseEventArgs e)
    {
        _sndClickUp.Play();
    }
}
