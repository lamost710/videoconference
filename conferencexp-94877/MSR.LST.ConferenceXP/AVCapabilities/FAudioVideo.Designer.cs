namespace MSR.LST.ConferenceXP
{
    partial class FAudioVideo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.Panel pbMiddleFill;
        private System.Windows.Forms.PictureBox pbVideoButton;
        private System.Windows.Forms.PictureBox pbAudioButton;
        private System.Windows.Forms.Label lblInfo;
        private CustomTrackBar tbVolume;
        private System.Windows.Forms.Panel pnlVideo;
        private System.Windows.Forms.Panel pnlAudio;
        private System.Windows.Forms.ToolTip toolTipAudioVideo;
        private System.Windows.Forms.Panel pnlLocalVideo;
        private System.Windows.Forms.PictureBox pbLocalVideoButton;
        private System.Windows.Forms.PictureBox pbVideoSeparation;
        private System.Windows.Forms.Panel pnlLocalAudio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pbLocalAudioButton;
        private System.Windows.Forms.ImageList imageListConfig;
        private System.Windows.Forms.Panel pnlLocalVideoConfig;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pbVideoTab;
        private System.Windows.Forms.PictureBox pbVideoButtonExt;
        private System.Windows.Forms.ImageList imageListSendButtons;
        private System.Windows.Forms.ImageList imageListPlayButtons;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
       
        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAudioVideo));
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.pbMiddleFill = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlAudio = new System.Windows.Forms.Panel();
            this.pbAudioButton = new System.Windows.Forms.PictureBox();
            this.tbVolume = new MSR.LST.ConferenceXP.CustomTrackBar();
            this.pnlVideo = new System.Windows.Forms.Panel();
            this.pbVideoButtonExt = new System.Windows.Forms.PictureBox();
            this.pbVideoSeparation = new System.Windows.Forms.PictureBox();
            this.pbVideoButton = new System.Windows.Forms.PictureBox();
            this.pnlLocalAudio = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbLocalAudioButton = new System.Windows.Forms.PictureBox();
            this.pnlLocalVideo = new System.Windows.Forms.Panel();
            this.pbLocalVideoButton = new System.Windows.Forms.PictureBox();
            this.imageListSendButtons = new System.Windows.Forms.ImageList(this.components);
            this.toolTipAudioVideo = new System.Windows.Forms.ToolTip(this.components);
            this.pbVideoTab = new System.Windows.Forms.PictureBox();
            this.imageListConfig = new System.Windows.Forms.ImageList(this.components);
            this.pnlLocalVideoConfig = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.imageListPlayButtons = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.pbMiddleFill.SuspendLayout();
            this.pnlAudio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAudioButton)).BeginInit();
            this.pnlVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoButtonExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoSeparation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoButton)).BeginInit();
            this.pnlLocalAudio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalAudioButton)).BeginInit();
            this.pnlLocalVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalVideoButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoTab)).BeginInit();
            this.pnlLocalVideoConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.Color.Black;
            this.pbVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbVideo.Location = new System.Drawing.Point(0, 0);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(230, 200);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVideo.TabIndex = 0;
            this.pbVideo.TabStop = false;
            this.pbVideo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbVideo_MouseDown);
            this.pbVideo.MouseEnter += new System.EventHandler(this.pbVideo_MouseEnter);
            this.pbVideo.MouseLeave += new System.EventHandler(this.pbVideo_MouseLeave);
            this.pbVideo.Resize += new System.EventHandler(this.pbVideo_Resize);
            // 
            // pbMiddleFill
            // 
            this.pbMiddleFill.BackColor = System.Drawing.Color.Transparent;
            this.pbMiddleFill.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbMiddleFill.BackgroundImage")));
            this.pbMiddleFill.Controls.Add(this.lblInfo);
            this.pbMiddleFill.Controls.Add(this.pnlAudio);
            this.pbMiddleFill.Controls.Add(this.pnlVideo);
            this.pbMiddleFill.Controls.Add(this.pnlLocalAudio);
            this.pbMiddleFill.Controls.Add(this.pnlLocalVideo);
            this.pbMiddleFill.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbMiddleFill.Location = new System.Drawing.Point(0, 174);
            this.pbMiddleFill.Name = "pbMiddleFill";
            this.pbMiddleFill.Size = new System.Drawing.Size(230, 26);
            this.pbMiddleFill.TabIndex = 65;
            this.pbMiddleFill.Visible = false;
            this.pbMiddleFill.MouseLeave += new System.EventHandler(this.pbMiddleFill_MouseLeave);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblInfo.Image = ((System.Drawing.Image)(resources.GetObject("lblInfo.Image")));
            this.lblInfo.Location = new System.Drawing.Point(232, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(300, 26);
            this.lblInfo.TabIndex = 72;
            this.lblInfo.Text = "Status: Normal";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTipAudioVideo.SetToolTip(this.lblInfo, "Status Information");
            this.lblInfo.MouseEnter += new System.EventHandler(this.menuEnter);
            // 
            // pnlAudio
            // 
            this.pnlAudio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlAudio.BackgroundImage")));
            this.pnlAudio.Controls.Add(this.pbAudioButton);
            this.pnlAudio.Controls.Add(this.tbVolume);
            this.pnlAudio.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAudio.Location = new System.Drawing.Point(120, 0);
            this.pnlAudio.Name = "pnlAudio";
            this.pnlAudio.Size = new System.Drawing.Size(112, 26);
            this.pnlAudio.TabIndex = 75;
            this.pnlAudio.Visible = false;
            this.pnlAudio.MouseEnter += new System.EventHandler(this.menuEnter);
            // 
            // pbAudioButton
            // 
            this.pbAudioButton.Image = ((System.Drawing.Image)(resources.GetObject("pbAudioButton.Image")));
            this.pbAudioButton.Location = new System.Drawing.Point(0, 2);
            this.pbAudioButton.Name = "pbAudioButton";
            this.pbAudioButton.Size = new System.Drawing.Size(23, 23);
            this.pbAudioButton.TabIndex = 68;
            this.pbAudioButton.TabStop = false;
            this.toolTipAudioVideo.SetToolTip(this.pbAudioButton, "Mute Audio");
            this.pbAudioButton.Click += new System.EventHandler(this.pbAudioButton_Click);
            this.pbAudioButton.MouseEnter += new System.EventHandler(this.pbAudioButton_MouseEnter);
            this.pbAudioButton.MouseLeave += new System.EventHandler(this.pbAudioButton_MouseLeave);
            // 
            // tbVolume
            // 
            this.tbVolume.BackColor = System.Drawing.SystemColors.Control;
            this.tbVolume.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbVolume.BackgroundImage")));
            this.tbVolume.CursorImage = ((System.Drawing.Image)(resources.GetObject("tbVolume.CursorImage")));
            this.tbVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbVolume.Location = new System.Drawing.Point(32, 6);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(74, 14);
            this.tbVolume.TabIndex = 73;
            this.toolTipAudioVideo.SetToolTip(this.tbVolume, "Volume");
            this.tbVolume.Value = 0;
            this.tbVolume.Visible = false;
            this.tbVolume.Scroll += new MSR.LST.ConferenceXP.CustomTrackBar.ScrollHandler(this.tbVolume_Scroll);
            // 
            // pnlVideo
            // 
            this.pnlVideo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlVideo.BackgroundImage")));
            this.pnlVideo.Controls.Add(this.pbVideoButtonExt);
            this.pnlVideo.Controls.Add(this.pbVideoSeparation);
            this.pnlVideo.Controls.Add(this.pbVideoButton);
            this.pnlVideo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlVideo.Location = new System.Drawing.Point(64, 0);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(56, 26);
            this.pnlVideo.TabIndex = 74;
            this.pnlVideo.Visible = false;
            this.pnlVideo.MouseEnter += new System.EventHandler(this.menuEnter);
            // 
            // pbVideoButtonExt
            // 
            this.pbVideoButtonExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbVideoButtonExt.Image = ((System.Drawing.Image)(resources.GetObject("pbVideoButtonExt.Image")));
            this.pbVideoButtonExt.Location = new System.Drawing.Point(24, 2);
            this.pbVideoButtonExt.Name = "pbVideoButtonExt";
            this.pbVideoButtonExt.Size = new System.Drawing.Size(22, 23);
            this.pbVideoButtonExt.TabIndex = 73;
            this.pbVideoButtonExt.TabStop = false;
            this.toolTipAudioVideo.SetToolTip(this.pbVideoButtonExt, "Pause Video");
            this.pbVideoButtonExt.Click += new System.EventHandler(this.pbVideoButtonExt_Click);
            this.pbVideoButtonExt.MouseEnter += new System.EventHandler(this.pbVideoButtonExt_MouseEnter);
            this.pbVideoButtonExt.MouseLeave += new System.EventHandler(this.pbVideoButtonExt_MouseLeave);
            // 
            // pbVideoSeparation
            // 
            this.pbVideoSeparation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbVideoSeparation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbVideoSeparation.BackgroundImage")));
            this.pbVideoSeparation.Location = new System.Drawing.Point(56, 4);
            this.pbVideoSeparation.Name = "pbVideoSeparation";
            this.pbVideoSeparation.Size = new System.Drawing.Size(32, 24);
            this.pbVideoSeparation.TabIndex = 72;
            this.pbVideoSeparation.TabStop = false;
            // 
            // pbVideoButton
            // 
            this.pbVideoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbVideoButton.Image = ((System.Drawing.Image)(resources.GetObject("pbVideoButton.Image")));
            this.pbVideoButton.Location = new System.Drawing.Point(0, 2);
            this.pbVideoButton.Name = "pbVideoButton";
            this.pbVideoButton.Size = new System.Drawing.Size(22, 23);
            this.pbVideoButton.TabIndex = 66;
            this.pbVideoButton.TabStop = false;
            this.toolTipAudioVideo.SetToolTip(this.pbVideoButton, "Play Video");
            this.pbVideoButton.Click += new System.EventHandler(this.pbVideoButton_Click);
            this.pbVideoButton.MouseEnter += new System.EventHandler(this.pbVideoButton_MouseEnter);
            this.pbVideoButton.MouseLeave += new System.EventHandler(this.pbVideoButton_MouseLeave);
            // 
            // pnlLocalAudio
            // 
            this.pnlLocalAudio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLocalAudio.BackgroundImage")));
            this.pnlLocalAudio.Controls.Add(this.pictureBox1);
            this.pnlLocalAudio.Controls.Add(this.pbLocalAudioButton);
            this.pnlLocalAudio.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLocalAudio.Location = new System.Drawing.Point(32, 0);
            this.pnlLocalAudio.Name = "pnlLocalAudio";
            this.pnlLocalAudio.Size = new System.Drawing.Size(32, 26);
            this.pnlLocalAudio.TabIndex = 76;
            this.pnlLocalAudio.Visible = false;
            this.pnlLocalAudio.MouseEnter += new System.EventHandler(this.menuEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(-48, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 24);
            this.pictureBox1.TabIndex = 74;
            this.pictureBox1.TabStop = false;
            // 
            // pbLocalAudioButton
            // 
            this.pbLocalAudioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLocalAudioButton.Image = ((System.Drawing.Image)(resources.GetObject("pbLocalAudioButton.Image")));
            this.pbLocalAudioButton.Location = new System.Drawing.Point(0, 2);
            this.pbLocalAudioButton.Name = "pbLocalAudioButton";
            this.pbLocalAudioButton.Size = new System.Drawing.Size(23, 23);
            this.pbLocalAudioButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLocalAudioButton.TabIndex = 68;
            this.pbLocalAudioButton.TabStop = false;
            this.toolTipAudioVideo.SetToolTip(this.pbLocalAudioButton, "Stop Sending My Audio");
            this.pbLocalAudioButton.Click += new System.EventHandler(this.pbLocalAudioButton_Click);
            this.pbLocalAudioButton.MouseEnter += new System.EventHandler(this.pbLocalAudioButton_MouseEnter);
            this.pbLocalAudioButton.MouseLeave += new System.EventHandler(this.pbLocalAudioButton_MouseLeave);
            // 
            // pnlLocalVideo
            // 
            this.pnlLocalVideo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLocalVideo.BackgroundImage")));
            this.pnlLocalVideo.Controls.Add(this.pbLocalVideoButton);
            this.pnlLocalVideo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLocalVideo.Location = new System.Drawing.Point(0, 0);
            this.pnlLocalVideo.Name = "pnlLocalVideo";
            this.pnlLocalVideo.Size = new System.Drawing.Size(32, 26);
            this.pnlLocalVideo.TabIndex = 76;
            this.pnlLocalVideo.Visible = false;
            this.pnlLocalVideo.MouseEnter += new System.EventHandler(this.menuEnter);
            // 
            // pbLocalVideoButton
            // 
            this.pbLocalVideoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLocalVideoButton.Image = ((System.Drawing.Image)(resources.GetObject("pbLocalVideoButton.Image")));
            this.pbLocalVideoButton.Location = new System.Drawing.Point(0, 2);
            this.pbLocalVideoButton.Name = "pbLocalVideoButton";
            this.pbLocalVideoButton.Size = new System.Drawing.Size(23, 24);
            this.pbLocalVideoButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLocalVideoButton.TabIndex = 73;
            this.pbLocalVideoButton.TabStop = false;
            this.toolTipAudioVideo.SetToolTip(this.pbLocalVideoButton, "Stop Sending My Video");
            this.pbLocalVideoButton.Click += new System.EventHandler(this.pbLocalVideoButton_Click);
            this.pbLocalVideoButton.MouseEnter += new System.EventHandler(this.pbLocalVideoButton_MouseEnter);
            this.pbLocalVideoButton.MouseLeave += new System.EventHandler(this.pbLocalVideoButton_MouseLeave);
            // 
            // imageListSendButtons
            // 
            this.imageListSendButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSendButtons.ImageStream")));
            this.imageListSendButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSendButtons.Images.SetKeyName(0, "");
            this.imageListSendButtons.Images.SetKeyName(1, "");
            this.imageListSendButtons.Images.SetKeyName(2, "");
            this.imageListSendButtons.Images.SetKeyName(3, "");
            this.imageListSendButtons.Images.SetKeyName(4, "");
            this.imageListSendButtons.Images.SetKeyName(5, "");
            this.imageListSendButtons.Images.SetKeyName(6, "");
            this.imageListSendButtons.Images.SetKeyName(7, "");
            // 
            // pbVideoTab
            // 
            this.pbVideoTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbVideoTab.Image = ((System.Drawing.Image)(resources.GetObject("pbVideoTab.Image")));
            this.pbVideoTab.Location = new System.Drawing.Point(2, 2);
            this.pbVideoTab.Name = "pbVideoTab";
            this.pbVideoTab.Size = new System.Drawing.Size(74, 21);
            this.pbVideoTab.TabIndex = 71;
            this.pbVideoTab.TabStop = false;
            this.toolTipAudioVideo.SetToolTip(this.pbVideoTab, "Camera Settings");
            this.pbVideoTab.Visible = false;
            this.pbVideoTab.Click += new System.EventHandler(this.pbVideoTab_Click);
            this.pbVideoTab.MouseEnter += new System.EventHandler(this.pbVideoTab_MouseEnter);
            this.pbVideoTab.MouseLeave += new System.EventHandler(this.pbVideoTab_MouseLeave);
            // 
            // imageListConfig
            // 
            this.imageListConfig.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListConfig.ImageStream")));
            this.imageListConfig.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListConfig.Images.SetKeyName(0, "");
            this.imageListConfig.Images.SetKeyName(1, "");
            // 
            // pnlLocalVideoConfig
            // 
            this.pnlLocalVideoConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLocalVideoConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLocalVideoConfig.BackgroundImage")));
            this.pnlLocalVideoConfig.Controls.Add(this.pictureBox3);
            this.pnlLocalVideoConfig.Controls.Add(this.pbVideoTab);
            this.pnlLocalVideoConfig.Location = new System.Drawing.Point(60, 0);
            this.pnlLocalVideoConfig.Name = "pnlLocalVideoConfig";
            this.pnlLocalVideoConfig.Size = new System.Drawing.Size(80, 24);
            this.pnlLocalVideoConfig.TabIndex = 78;
            this.pnlLocalVideoConfig.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Location = new System.Drawing.Point(80, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 24);
            this.pictureBox3.TabIndex = 72;
            this.pictureBox3.TabStop = false;
            // 
            // imageListPlayButtons
            // 
            this.imageListPlayButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPlayButtons.ImageStream")));
            this.imageListPlayButtons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPlayButtons.Images.SetKeyName(0, "");
            this.imageListPlayButtons.Images.SetKeyName(1, "");
            this.imageListPlayButtons.Images.SetKeyName(2, "");
            this.imageListPlayButtons.Images.SetKeyName(3, "");
            this.imageListPlayButtons.Images.SetKeyName(4, "");
            this.imageListPlayButtons.Images.SetKeyName(5, "");
            this.imageListPlayButtons.Images.SetKeyName(6, "");
            this.imageListPlayButtons.Images.SetKeyName(7, "");
            this.imageListPlayButtons.Images.SetKeyName(8, "");
            this.imageListPlayButtons.Images.SetKeyName(9, "");
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FAudioVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pbMiddleFill);
            this.Controls.Add(this.pbVideo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "FAudioVideo";
            this.Size = new System.Drawing.Size(230, 200);
            this.Load += new System.EventHandler(this.FAudioVideo_Load);
            this.MouseLeave += new System.EventHandler(this.FAudioVideo_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.pbMiddleFill.ResumeLayout(false);
            this.pnlAudio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAudioButton)).EndInit();
            this.pnlVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoButtonExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoSeparation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoButton)).EndInit();
            this.pnlLocalAudio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalAudioButton)).EndInit();
            this.pnlLocalVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalVideoButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideoTab)).EndInit();
            this.pnlLocalVideoConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;


    }
}
