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
            this.fileListSelect = new System.Windows.Forms.CheckedListBox();
            this.CBButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.modsTab = new System.Windows.Forms.TabPage();
            this.librariesTab = new System.Windows.Forms.TabPage();
            this.allLibsList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.libFileList = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.createAssetsList = new System.Windows.Forms.Button();
            this.assetsListBox = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.modsTab.SuspendLayout();
            this.librariesTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buildsSelector
            // 
            this.buildsSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(207)))), ((int)(((byte)(250)))));
            this.buildsSelector.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buildsSelector.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buildsSelector.FormattingEnabled = true;
            this.buildsSelector.Location = new System.Drawing.Point(6, 427);
            this.buildsSelector.Name = "buildsSelector";
            this.buildsSelector.Size = new System.Drawing.Size(195, 26);
            this.buildsSelector.TabIndex = 0;
            this.buildsSelector.SelectedIndexChanged += new System.EventHandler(this.buildsSelector_SelectedIndexChanged);
            // 
            // saveFilesButton
            // 
            this.saveFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveFilesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.saveFilesButton.Location = new System.Drawing.Point(658, 427);
            this.saveFilesButton.Name = "saveFilesButton";
            this.saveFilesButton.Size = new System.Drawing.Size(129, 28);
            this.saveFilesButton.TabIndex = 4;
            this.saveFilesButton.Text = "Собрать список";
            this.saveFilesButton.UseVisualStyleBackColor = true;
            this.saveFilesButton.Click += new System.EventHandler(this.saveFilesButton_Click);
            // 
            // fileListSelect
            // 
            this.fileListSelect.AllowDrop = true;
            this.fileListSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileListSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.fileListSelect.HorizontalScrollbar = true;
            this.fileListSelect.Location = new System.Drawing.Point(6, 6);
            this.fileListSelect.Name = "fileListSelect";
            this.fileListSelect.Size = new System.Drawing.Size(781, 402);
            this.fileListSelect.TabIndex = 6;
            // 
            // CBButton
            // 
            this.CBButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CBButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.CBButton.Location = new System.Drawing.Point(571, 427);
            this.CBButton.Name = "CBButton";
            this.CBButton.Size = new System.Drawing.Size(81, 28);
            this.CBButton.TabIndex = 7;
            this.CBButton.Text = "1 Мод";
            this.CBButton.UseVisualStyleBackColor = true;
            this.CBButton.Click += new System.EventHandler(this.CBButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.modsTab);
            this.tabControl1.Controls.Add(this.librariesTab);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(801, 499);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            // 
            // modsTab
            // 
            this.modsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(234)))), ((int)(((byte)(245)))));
            this.modsTab.Controls.Add(this.fileListSelect);
            this.modsTab.Controls.Add(this.CBButton);
            this.modsTab.Controls.Add(this.buildsSelector);
            this.modsTab.Controls.Add(this.saveFilesButton);
            this.modsTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.modsTab.Location = new System.Drawing.Point(4, 27);
            this.modsTab.Name = "modsTab";
            this.modsTab.Padding = new System.Windows.Forms.Padding(3);
            this.modsTab.Size = new System.Drawing.Size(793, 468);
            this.modsTab.TabIndex = 0;
            this.modsTab.Text = "Моды";
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
            this.librariesTab.Size = new System.Drawing.Size(793, 468);
            this.librariesTab.TabIndex = 1;
            this.librariesTab.Text = "Библиотеки";
            // 
            // allLibsList
            // 
            this.allLibsList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.allLibsList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.allLibsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.allLibsList.Location = new System.Drawing.Point(593, 432);
            this.allLibsList.Name = "allLibsList";
            this.allLibsList.Size = new System.Drawing.Size(138, 28);
            this.allLibsList.TabIndex = 2;
            this.allLibsList.Text = "Собрать список";
            this.allLibsList.UseVisualStyleBackColor = true;
            this.allLibsList.Click += new System.EventHandler(this.allLibsList_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(737, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Хеш";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // libFileList
            // 
            this.libFileList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.libFileList.FormattingEnabled = true;
            this.libFileList.ItemHeight = 18;
            this.libFileList.Location = new System.Drawing.Point(6, 6);
            this.libFileList.Name = "libFileList";
            this.libFileList.Size = new System.Drawing.Size(781, 418);
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
            this.tabPage1.Size = new System.Drawing.Size(793, 468);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Assets";
            // 
            // createAssetsList
            // 
            this.createAssetsList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.createAssetsList.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.createAssetsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.createAssetsList.Location = new System.Drawing.Point(649, 434);
            this.createAssetsList.Name = "createAssetsList";
            this.createAssetsList.Size = new System.Drawing.Size(138, 28);
            this.createAssetsList.TabIndex = 4;
            this.createAssetsList.Text = "Собрать список";
            this.createAssetsList.UseVisualStyleBackColor = true;
            this.createAssetsList.Click += new System.EventHandler(this.createAssetsList_Click);
            // 
            // assetsListBox
            // 
            this.assetsListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.assetsListBox.FormattingEnabled = true;
            this.assetsListBox.ItemHeight = 18;
            this.assetsListBox.Location = new System.Drawing.Point(6, 7);
            this.assetsListBox.Name = "assetsListBox";
            this.assetsListBox.Size = new System.Drawing.Size(781, 418);
            this.assetsListBox.TabIndex = 3;
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
            this.librariesTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox buildsSelector;
        private System.Windows.Forms.Button saveFilesButton;
        private System.Windows.Forms.CheckedListBox fileListSelect;
        private System.Windows.Forms.Button CBButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage modsTab;
        private System.Windows.Forms.TabPage librariesTab;
        private System.Windows.Forms.ListBox libFileList;
        private System.Windows.Forms.Button allLibsList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button createAssetsList;
        private System.Windows.Forms.ListBox assetsListBox;
    }
}

