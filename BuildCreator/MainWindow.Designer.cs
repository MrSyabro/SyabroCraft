namespace BuildCreator
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.buildsSelector = new System.Windows.Forms.ComboBox();
            this.saveFilesButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.modsTab = new System.Windows.Forms.TabPage();
            this.buildFileList = new System.Windows.Forms.ListBox();
            this.librariesTab = new System.Windows.Forms.TabPage();
            this.allLibsList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.libFileList = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.createAssetsList = new System.Windows.Forms.Button();
            this.assetsListBox = new System.Windows.Forms.ListBox();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.modsTab.SuspendLayout();
            this.librariesTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buildsSelector
            // 
            this.buildsSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buildsSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.buildsSelector.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buildsSelector.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buildsSelector.FormattingEnabled = true;
            this.buildsSelector.Location = new System.Drawing.Point(6, 460);
            this.buildsSelector.Name = "buildsSelector";
            this.buildsSelector.Size = new System.Drawing.Size(195, 26);
            this.buildsSelector.TabIndex = 0;
            this.buildsSelector.SelectedIndexChanged += new System.EventHandler(this.buildsSelector_SelectedIndexChanged);
            // 
            // saveFilesButton
            // 
            this.saveFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveFilesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.saveFilesButton.Location = new System.Drawing.Point(649, 458);
            this.saveFilesButton.Name = "saveFilesButton";
            this.saveFilesButton.Size = new System.Drawing.Size(162, 28);
            this.saveFilesButton.TabIndex = 4;
            this.saveFilesButton.Text = "Загрузить на сайт";
            this.saveFilesButton.UseVisualStyleBackColor = true;
            this.saveFilesButton.Click += new System.EventHandler(this.saveFilesButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.modsTab);
            this.tabControl1.Controls.Add(this.librariesTab);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(825, 523);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            // 
            // modsTab
            // 
            this.modsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.modsTab.Controls.Add(this.keyTextBox);
            this.modsTab.Controls.Add(this.buildFileList);
            this.modsTab.Controls.Add(this.buildsSelector);
            this.modsTab.Controls.Add(this.saveFilesButton);
            this.modsTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.modsTab.Location = new System.Drawing.Point(4, 27);
            this.modsTab.Name = "modsTab";
            this.modsTab.Padding = new System.Windows.Forms.Padding(3);
            this.modsTab.Size = new System.Drawing.Size(817, 492);
            this.modsTab.TabIndex = 0;
            this.modsTab.Text = "Моды";
            // 
            // buildFileList
            // 
            this.buildFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildFileList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.buildFileList.FormattingEnabled = true;
            this.buildFileList.ItemHeight = 18;
            this.buildFileList.Items.AddRange(new object[] {
            "Сборка не выбрана."});
            this.buildFileList.Location = new System.Drawing.Point(6, 6);
            this.buildFileList.Name = "buildFileList";
            this.buildFileList.Size = new System.Drawing.Size(803, 436);
            this.buildFileList.TabIndex = 5;
            // 
            // librariesTab
            // 
            this.librariesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.librariesTab.Controls.Add(this.allLibsList);
            this.librariesTab.Controls.Add(this.button1);
            this.librariesTab.Controls.Add(this.libFileList);
            this.librariesTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.librariesTab.Location = new System.Drawing.Point(4, 27);
            this.librariesTab.Name = "librariesTab";
            this.librariesTab.Padding = new System.Windows.Forms.Padding(3);
            this.librariesTab.Size = new System.Drawing.Size(817, 492);
            this.librariesTab.TabIndex = 1;
            this.librariesTab.Text = "Библиотеки";
            // 
            // allLibsList
            // 
            this.allLibsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.allLibsList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.allLibsList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.allLibsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.allLibsList.Location = new System.Drawing.Point(617, 458);
            this.allLibsList.Name = "allLibsList";
            this.allLibsList.Size = new System.Drawing.Size(138, 28);
            this.allLibsList.TabIndex = 2;
            this.allLibsList.Text = "Собрать список";
            this.allLibsList.UseVisualStyleBackColor = true;
            this.allLibsList.Click += new System.EventHandler(this.allLibsList_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(761, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Хеш";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // libFileList
            // 
            this.libFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.libFileList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.libFileList.FormattingEnabled = true;
            this.libFileList.ItemHeight = 18;
            this.libFileList.Location = new System.Drawing.Point(6, 6);
            this.libFileList.Name = "libFileList";
            this.libFileList.Size = new System.Drawing.Size(803, 436);
            this.libFileList.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.tabPage1.Controls.Add(this.createAssetsList);
            this.tabPage1.Controls.Add(this.assetsListBox);
            this.tabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(817, 492);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Assets";
            // 
            // createAssetsList
            // 
            this.createAssetsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createAssetsList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.createAssetsList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.createAssetsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.createAssetsList.Location = new System.Drawing.Point(673, 458);
            this.createAssetsList.Name = "createAssetsList";
            this.createAssetsList.Size = new System.Drawing.Size(138, 28);
            this.createAssetsList.TabIndex = 4;
            this.createAssetsList.Text = "Собрать список";
            this.createAssetsList.UseVisualStyleBackColor = true;
            this.createAssetsList.Click += new System.EventHandler(this.createAssetsList_Click);
            // 
            // assetsListBox
            // 
            this.assetsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assetsListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.assetsListBox.FormattingEnabled = true;
            this.assetsListBox.ItemHeight = 18;
            this.assetsListBox.Location = new System.Drawing.Point(6, 7);
            this.assetsListBox.Name = "assetsListBox";
            this.assetsListBox.Size = new System.Drawing.Size(805, 436);
            this.assetsListBox.TabIndex = 3;
            // 
            // keyTextBox
            // 
            this.keyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.keyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keyTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.keyTextBox.Location = new System.Drawing.Point(207, 461);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(436, 25);
            this.keyTextBox.TabIndex = 6;
            this.keyTextBox.Text = "Введи ключ";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(825, 523);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "BuildCreator";
            this.Shown += new System.EventHandler(this.MainWindow_Show);
            this.tabControl1.ResumeLayout(false);
            this.modsTab.ResumeLayout(false);
            this.modsTab.PerformLayout();
            this.librariesTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox buildsSelector;
        private System.Windows.Forms.Button saveFilesButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage modsTab;
        private System.Windows.Forms.TabPage librariesTab;
        private System.Windows.Forms.ListBox libFileList;
        private System.Windows.Forms.Button allLibsList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button createAssetsList;
        private System.Windows.Forms.ListBox assetsListBox;
        private System.Windows.Forms.ListBox buildFileList;
        private System.Windows.Forms.TextBox keyTextBox;
    }
}

