namespace EndlessOnlinePatcher.Desktop;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        pictureBox1 = new PictureBox();
        label1 = new Label();
        flowLayoutPanel1 = new FlowLayoutPanel();
        pictureBox2 = new PictureBox();
        pnlVersions = new Panel();
        lblLocal = new Label();
        lblRemote = new Label();
        pnlPatch = new Panel();
        chkAutoLaunch = new CheckBox();
        btnPatch = new Button();
        prgPatch = new ProgressBar();
        pnlUpToDate = new Panel();
        label2 = new Label();
        btnLaunch = new Button();
        txtOutput = new TextBox();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        flowLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        pnlVersions.SuspendLayout();
        pnlPatch.SuspendLayout();
        pnlUpToDate.SuspendLayout();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(0, 27);
        pictureBox1.Margin = new Padding(0);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(300, 100);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        pictureBox1.MouseDown += pictureBox1_MouseDown;
        pictureBox1.MouseMove += pictureBox1_MouseMove;
        pictureBox1.MouseUp += pictureBox1_MouseUp;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(55, 144);
        label1.Name = "label1";
        label1.Size = new Size(0, 15);
        label1.TabIndex = 1;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.AutoSize = true;
        flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flowLayoutPanel1.Controls.Add(pictureBox2);
        flowLayoutPanel1.Controls.Add(pictureBox1);
        flowLayoutPanel1.Controls.Add(pnlVersions);
        flowLayoutPanel1.Controls.Add(pnlPatch);
        flowLayoutPanel1.Controls.Add(pnlUpToDate);
        flowLayoutPanel1.Controls.Add(txtOutput);
        flowLayoutPanel1.Dock = DockStyle.Fill;
        flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
        flowLayoutPanel1.Location = new Point(0, 0);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(300, 420);
        flowLayoutPanel1.TabIndex = 2;
        // 
        // pictureBox2
        // 
        pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pictureBox2.Image = Properties.Resources.eo_logout;
        pictureBox2.Location = new Point(267, 3);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new Size(33, 21);
        pictureBox2.TabIndex = 4;
        pictureBox2.TabStop = false;
        pictureBox2.Click += pictureBox2_Click;
        pictureBox2.MouseEnter += pictureBox2_MouseEnter;
        pictureBox2.MouseLeave += pictureBox2_MouseLeave;
        // 
        // pnlVersions
        // 
        pnlVersions.Controls.Add(lblLocal);
        pnlVersions.Controls.Add(lblRemote);
        pnlVersions.Location = new Point(3, 130);
        pnlVersions.Name = "pnlVersions";
        pnlVersions.Size = new Size(297, 56);
        pnlVersions.TabIndex = 1;
        // 
        // lblLocal
        // 
        lblLocal.AutoSize = true;
        lblLocal.Location = new Point(2, 10);
        lblLocal.Name = "lblLocal";
        lblLocal.Size = new Size(48, 15);
        lblLocal.TabIndex = 1;
        lblLocal.Text = "lblLocal";
        // 
        // lblRemote
        // 
        lblRemote.AutoSize = true;
        lblRemote.Location = new Point(2, 33);
        lblRemote.Name = "lblRemote";
        lblRemote.Size = new Size(61, 15);
        lblRemote.TabIndex = 0;
        lblRemote.Text = "lblRemote";
        // 
        // pnlPatch
        // 
        pnlPatch.Controls.Add(chkAutoLaunch);
        pnlPatch.Controls.Add(btnPatch);
        pnlPatch.Controls.Add(prgPatch);
        pnlPatch.Location = new Point(3, 192);
        pnlPatch.Name = "pnlPatch";
        pnlPatch.Size = new Size(297, 50);
        pnlPatch.TabIndex = 2;
        pnlPatch.Visible = false;
        // 
        // chkAutoLaunch
        // 
        chkAutoLaunch.AutoSize = true;
        chkAutoLaunch.CheckAlign = ContentAlignment.MiddleRight;
        chkAutoLaunch.Location = new Point(183, 28);
        chkAutoLaunch.Name = "chkAutoLaunch";
        chkAutoLaunch.Size = new Size(111, 19);
        chkAutoLaunch.TabIndex = 2;
        chkAutoLaunch.Text = "Auto-launch EO";
        chkAutoLaunch.UseVisualStyleBackColor = true;
        // 
        // btnPatch
        // 
        btnPatch.Location = new Point(208, 3);
        btnPatch.Name = "btnPatch";
        btnPatch.Size = new Size(86, 23);
        btnPatch.TabIndex = 1;
        btnPatch.Text = "Patch";
        btnPatch.UseVisualStyleBackColor = true;
        btnPatch.Click += btnPatch_Click;
        // 
        // prgPatch
        // 
        prgPatch.Location = new Point(3, 3);
        prgPatch.Name = "prgPatch";
        prgPatch.Size = new Size(199, 23);
        prgPatch.TabIndex = 0;
        // 
        // pnlUpToDate
        // 
        pnlUpToDate.Controls.Add(label2);
        pnlUpToDate.Controls.Add(btnLaunch);
        pnlUpToDate.Location = new Point(3, 248);
        pnlUpToDate.Name = "pnlUpToDate";
        pnlUpToDate.Size = new Size(294, 27);
        pnlUpToDate.TabIndex = 5;
        pnlUpToDate.Visible = false;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(3, 7);
        label2.Name = "label2";
        label2.Size = new Size(65, 15);
        label2.TabIndex = 2;
        label2.Text = "Up to date!";
        // 
        // btnLaunch
        // 
        btnLaunch.Location = new Point(208, 3);
        btnLaunch.Name = "btnLaunch";
        btnLaunch.Size = new Size(86, 23);
        btnLaunch.TabIndex = 2;
        btnLaunch.Text = "Launch!";
        btnLaunch.UseVisualStyleBackColor = true;
        btnLaunch.Click += btnLaunch_Click;
        // 
        // txtOutput
        // 
        txtOutput.Location = new Point(3, 281);
        txtOutput.Multiline = true;
        txtOutput.Name = "txtOutput";
        txtOutput.ReadOnly = true;
        txtOutput.Size = new Size(294, 89);
        txtOutput.TabIndex = 3;
        txtOutput.Visible = false;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ClientSize = new Size(300, 420);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.None;
        Name = "Main";
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Endless Online Patcher";
        Shown += Main_Shown;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        pnlVersions.ResumeLayout(false);
        pnlVersions.PerformLayout();
        pnlPatch.ResumeLayout(false);
        pnlPatch.PerformLayout();
        pnlUpToDate.ResumeLayout(false);
        pnlUpToDate.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private Label label1;
    private FlowLayoutPanel flowLayoutPanel1;
    private Panel pnlVersions;
    private Label lblRemote;
    private Label lblLocal;
    private Panel pnlPatch;
    private Button btnPatch;
    private ProgressBar prgPatch;
    private TextBox txtOutput;
    private CheckBox chkAutoLaunch;
    private PictureBox pictureBox2;
    private Panel pnlUpToDate;
    private Label label2;
    private Button btnLaunch;
}
