namespace System
{
    partial class Starter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Starter));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DownloadBuild = new System.ComponentModel.BackgroundWorker();
            this.proc = new System.Diagnostics.Process();
            this.CheckBuild = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 28);
            this.panel1.TabIndex = 0;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(31, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "SyabroCraft Starter";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            // 
            // DownloadBuild
            // 
            this.DownloadBuild.WorkerReportsProgress = true;
            this.DownloadBuild.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork_downloadBuild);
            this.DownloadBuild.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DownloadBuild_ProgressChanged);
            this.DownloadBuild.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CheckBuild_RunWorkerCompleted);
            // 
            // proc
            // 
            this.proc.StartInfo.Domain = "";
            this.proc.StartInfo.LoadUserProfile = false;
            this.proc.StartInfo.Password = null;
            this.proc.StartInfo.StandardErrorEncoding = null;
            this.proc.StartInfo.StandardOutputEncoding = null;
            this.proc.StartInfo.UserName = "";
            this.proc.SynchronizingObject = this;
            this.proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(this.proc_ErrorDataReceived);
            this.proc.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(this.proc_ErrorDataReceived);
            // 
            // CheckBuild
            // 
            this.CheckBuild.WorkerReportsProgress = true;
            this.CheckBuild.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CheckBuild_DoWork);
            this.CheckBuild.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DownloadBuild_ProgressChanged);
            this.CheckBuild.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CheckBuild_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // Starter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(371, 63);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Starter";
            this.Text = "Starter";
            this.Shown += new System.EventHandler(this.Starter_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.Panel panel1;
        private Windows.Forms.PictureBox pictureBox1;
        private Windows.Forms.Label label1;
        private ComponentModel.BackgroundWorker DownloadBuild;
        private Diagnostics.Process proc;
        private ComponentModel.BackgroundWorker CheckBuild;
        private Windows.Forms.Label label2;
    }
}