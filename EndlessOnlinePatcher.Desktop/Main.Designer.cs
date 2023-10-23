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
        pictureBox1 = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.ErrorImage = Properties.Resources.text_window;
        pictureBox1.Image = Properties.Resources.text_window;
        pictureBox1.Location = new Point(28, 63);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(250, 12);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        BackgroundImage = Properties.Resources.eo_popup;
        BackgroundImageLayout = ImageLayout.Center;
        ClientSize = new Size(290, 125);
        Controls.Add(pictureBox1);
        FormBorderStyle = FormBorderStyle.None;
        Name = "Main";
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Endless Online Patcher";
        Shown += Main_Shown;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBox1;
}
