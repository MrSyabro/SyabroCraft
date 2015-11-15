namespace Launcher
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.StartGame = new System.Windows.Forms.Button();
            this.webNews = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.proc = new System.Diagnostics.Process();
            this.buildsSelector = new System.Windows.Forms.ComboBox();
            this.serverSelector = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartGame
            // 
            this.StartGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.StartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartGame.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.StartGame.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.StartGame.Location = new System.Drawing.Point(388, 343);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(61, 28);
            this.StartGame.TabIndex = 0;
            this.StartGame.Text = "Старт";
            this.StartGame.UseVisualStyleBackColor = false;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // webNews
            // 
            this.webNews.IsWebBrowserContextMenuEnabled = false;
            this.webNews.Location = new System.Drawing.Point(12, 34);
            this.webNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.webNews.Name = "webNews";
            this.webNews.Size = new System.Drawing.Size(533, 303);
            this.webNews.TabIndex = 1;
            this.webNews.Url = new System.Uri("http://syabrocraft.esy.es/wp-content/plugins/syabrocraft/news.php", System.UriKind.Absolute);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(537, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(31, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "SyabroCraft";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Teal;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(455, 343);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 28);
            this.button2.TabIndex = 8;
            this.button2.Text = "Настройки";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 29);
            this.panel1.TabIndex = 9;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.pictureBox1.Image = global::Launcher.Properties.Resources.skin;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
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
            // 
            // buildsSelector
            // 
            this.buildsSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buildsSelector.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buildsSelector.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.buildsSelector.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buildsSelector.FormattingEnabled = true;
            this.buildsSelector.Location = new System.Drawing.Point(12, 343);
            this.buildsSelector.Name = "buildsSelector";
            this.buildsSelector.Size = new System.Drawing.Size(220, 26);
            this.buildsSelector.TabIndex = 10;
            this.buildsSelector.SelectedIndexChanged += new System.EventHandler(this.buildsSelector_SelectedIndexChanged);
            // 
            // serverSelector
            // 
            this.serverSelector.BackColor = System.Drawing.Color.RoyalBlue;
            this.serverSelector.Cursor = System.Windows.Forms.Cursors.No;
            this.serverSelector.Enabled = false;
            this.serverSelector.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.serverSelector.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.serverSelector.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.serverSelector.FormattingEnabled = true;
            this.serverSelector.Location = new System.Drawing.Point(238, 343);
            this.serverSelector.Name = "serverSelector";
            this.serverSelector.Size = new System.Drawing.Size(144, 26);
            this.serverSelector.TabIndex = 11;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(557, 383);
            this.Controls.Add(this.serverSelector);
            this.Controls.Add(this.buildsSelector);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.webNews);
            this.Controls.Add(this.StartGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "SyabroCraft";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartGame;
        private System.Windows.Forms.WebBrowser webNews;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Diagnostics.Process proc;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox serverSelector;
        public System.Windows.Forms.ComboBox buildsSelector;
    }
}

