using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Build build = new Build();
        bool newB = false;
        Settings settings = AuthWindow.settings;
        WebClient webClient = new WebClient();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        Dictionary<string, Dictionary<string, ModFile>> globalMods;
        User userData = AuthWindow.userData;

        public Editor()
        {
            InitializeComponent();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler (WebClient_DownloadFileCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
            webClient.Encoding = System.Text.Encoding.UTF8;
            newB = true;
        }

        public Editor(Build eBuild)
        {
            InitializeComponent();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClient_DownloadFileCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
            webClient.Encoding = System.Text.Encoding.UTF8;
            build = eBuild;
            buildNameTextBox.Text = build.name;
            buildDirTextBox.Text = build.dir;
            gameVerComboBox.Text = build.gameVer;
            if (build.img)
            {
                if (!File.Exists(settings.GetBuildDir(build) + "image.png"))
                {
                    if (settings.lNET)
                    {
                        webClient.DownloadFile(Settings.site + "builds/" + build.dir + "/image.png", settings.GetBuildDir(build) + "image.png");
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = File.OpenRead(settings.GetBuildDir(build) + "image.png");
                        bi.EndInit();
                        image.Source = bi;
                    }
                }
                else
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = File.OpenRead(settings.GetBuildDir(build) + "image.png");
                    bi.EndInit();
                    image.Source = bi;
                }
            }

            foreach(string name in jss.Deserialize<List<string>>(webClient.DownloadString(Settings.site + "builds/" + build.dir + "/mods.json")))
                listBoxSelMods.Items.Add(name);
            globalMods = jss.Deserialize<Dictionary<string, Dictionary<string, ModFile>>>(webClient.DownloadString(Settings.site + "mods.json"));
            foreach (KeyValuePair<string, Dictionary<string, ModFile>> keyValue in globalMods)
            {
                listBoxUSelMods.Items.Add(keyValue.Key);
            }
            descriptionTab.IsEnabled = true;
            modsTab.IsEnabled = true;
            //newB = true;
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            (statusBar.Items[0] as Label).Content = e.Cancelled ? e.Error.Message : "Загрузка закончена.";
            (statusBar.Items[1] as ProgressBar).Visibility = Visibility.Hidden;
            (statusBar.Items[1] as ProgressBar).Value = 0;
            addMod.IsEnabled = true;
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            (statusBar.Items[1] as ProgressBar).Value = e.ProgressPercentage;
        }

        private void aplyButton_Click(object sender, RoutedEventArgs e)
        {
            build.name = buildNameTextBox.Text;
            build.dir = buildDirTextBox.Text;
            build.gameVer = gameVerComboBox.Text;

            globalMods = jss.Deserialize<Dictionary<string, Dictionary<string, ModFile>>>(webClient.DownloadString(Settings.site + "mods.json"));
            foreach (KeyValuePair<string, Dictionary<string, ModFile>> keyValue in globalMods)
            {
                listBoxUSelMods.Items.Add(keyValue.Key);
            }

            
            descriptionTab.IsEnabled = true;
            modsTab.IsEnabled = true;
        }

        private void addMod_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUSelMods.SelectedIndex != -1)
            {
                if (!Directory.Exists(settings.GetBuildDir(build) + "mods\\"))
                    Directory.CreateDirectory(settings.GetBuildDir(build) + "mods\\");
                webClient.DownloadFileAsync(new Uri(Settings.site + "mods/" + globalMods[listBoxUSelMods.SelectedItem as string][build.gameVer].name), settings.GetBuildDir(build) + "mods\\" + globalMods[listBoxUSelMods.SelectedItem as string][build.gameVer].name);
                (statusBar.Items[1] as ProgressBar).Visibility = Visibility.Visible;
                (statusBar.Items[0] as Label).Content = "Загрузка " + listBoxUSelMods.SelectedItem as string;
                addMod.IsEnabled = false;

                listBoxSelMods.Items.Add(listBoxUSelMods.SelectedItem as string);
            }
            else
            {
                (statusBar.Items[0] as Label).Content = "Выберите мод.";
            }
        }

        private void delMod_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSelMods.SelectedIndex != -1)
            {
                if (File.Exists(settings.GetBuildDir(build) + "mods\\" + globalMods[listBoxSelMods.SelectedItem as string][build.gameVer].name))
                    File.Delete(settings.GetBuildDir(build) + "mods\\" + globalMods[listBoxSelMods.SelectedItem as string][build.gameVer].name);
                int selected = listBoxSelMods.SelectedIndex;
                listBoxSelMods.Items.RemoveAt(selected);
            }
            else
            {
                (statusBar.Items[0] as Label).Content = "Выберите мод.";
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            build.name = buildNameTextBox.Text;
            build.dir = buildDirTextBox.Text;
            build.gameVer = gameVerComboBox.Text;

            File.WriteAllText(settings.GetBuildDir(build) + "mods.json", jss.Serialize(listBoxSelMods.Items));
            File.WriteAllText(settings.GetBuildDir(build) + "info.json", jss.Serialize(build));

            string request = jss.Serialize(new { type = newB ? "add" : "edit", clientToken = userData.clientToken, accessToken = userData.accessToken, build = build });
            
            MessageBox.Show(request);
            
            string responseStr = webClient.UploadString(Settings.site + "builds.php", request);
            MessageBox.Show(responseStr);
            //ServerError response = jss.Deserialize<ServerError>(responseStr);
            //MessageBox.Show(response.error, response.errorMessage);
            this.DialogResult = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void editor_Closing(object sender, CancelEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
