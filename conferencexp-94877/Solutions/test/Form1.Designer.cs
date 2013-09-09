namespace test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.fAudioVideo1 = new MSR.LST.ConferenceXP.FAudioVideo();
            this.SuspendLayout();
            // 
            // fAudioVideo1
            // 
            this.fAudioVideo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fAudioVideo1.Location = new System.Drawing.Point(3, 29);
            this.fAudioVideo1.Name = "fAudioVideo1";
            this.fAudioVideo1.Size = new System.Drawing.Size(529, 393);
            this.fAudioVideo1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 434);
            this.Controls.Add(this.fAudioVideo1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MSR.LST.ConferenceXP.FAudioVideo fAudioVideo1;
    }
}

