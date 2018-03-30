using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings settings = AuthWindow.settings;
        WebClient webClient = new WebClient();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        Dictionary<string, Build> builds;

        public MainWindow()
        {
            InitializeComponent();
            webClient.Encoding = System.Text.Encoding.UTF8;
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string respStr = webClient.UploadString(Settings.site + "builds.php", "{\"type\":\"list\"}" /*jss.Serialize(new { type = "list" })*/);
                File.WriteAllText(settings.GetLPath() + "builds.json", respStr);
                BuildsListItem item;
                MessageBox.Show(respStr);
                builds = jss.Deserialize<Dictionary<string, Build>>(respStr);
                foreach (KeyValuePair<string, Build> build in builds)
                {
                    item = new BuildsListItem { BName = build.Value.name, ImageData = build.Value.img ? webClient.DownloadData(Settings.site + "builds/" + build.Key + "/image.png") : null, Path = build.Key };
                    buildsListView.Items.Add(item);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            RAMComboBox.Text = settings.jRAM;
            javaPathLabel.Content = settings.jPath;
            showConsoleCheckBox.IsChecked = settings.jShowClonsole;
            argsTextBox.Text = settings.jArg;

            autoCloseCheckBox.IsChecked = settings.lAutoClose;
            autoAuthCheckBox.IsChecked = settings.lAutoLogin;
            onlineCheckBox.IsChecked = settings.lNET;
        }

        private void buildsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (buildsListView.SelectedIndex != -1)
                buildsWebBrowser.Source = new Uri(Settings.site + "builds/" + ((BuildsListItem)buildsListView.SelectedItem).Path);
        }

        private void buildsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StartWindow startWindow = new StartWindow(builds[((BuildsListItem)buildsListView.SelectedItem).Path]);
            startWindow.Show();
        }

        private void addBuild_Click(object sender, RoutedEventArgs e)
        {
            Editor editor = new Editor();
            editor.ShowDialog();
            updateBuildList();
        }

        private void editBuild_Click(object sender, RoutedEventArgs e)
        {
            if (buildsListView.SelectedIndex != -1)
            {
                Editor editor = new Editor(builds[((BuildsListItem)buildsListView.SelectedItem).Path]);
                editor.ShowDialog();
                if (editor.DialogResult.Value)
                {
                    builds[((BuildsListItem)buildsListView.SelectedItem).Path] = editor.build;
                    buildsListView.Items[buildsListView.SelectedIndex] = new BuildsListItem
                    {
                        BName = editor.build.name,
                        ImageData = (editor.build.img ? File.ReadAllBytes(settings.GetBuildDir(editor.build.dir) + "image.png") : null),
                        Path = editor.build.dir
                    };
                }
            }
            updateBuildList();
        }

        private void openJavaButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openJava = new OpenFileDialog();
            openJava.Filter = "Java (java.exe)|java.exe|All executable|*.exe";
            if (openJava.ShowDialog().Value)
            {
                javaPathLabel.Content = openJava.FileName;
            }
        }

        private void saveSetingButton_Click(object sender, RoutedEventArgs e)
        {
            settings.jRAM = RAMComboBox.Text;
            settings.jPath = javaPathLabel.Content as string;
            settings.jShowClonsole = showConsoleCheckBox.IsChecked.Value;
            settings.jArg = argsTextBox.Text;

            settings.lAutoClose = autoCloseCheckBox.IsChecked.Value;
            settings.lAutoLogin = autoAuthCheckBox.IsChecked.Value;
            settings.lNET = onlineCheckBox.IsChecked.Value;

            settings.Save();
        }

        private void onlineCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (onlineCheckBox.IsChecked.Value)
            {
                Cursor = Cursors.Wait;
                IsEnabled = false;
                try
                {

                    string respStr = webClient.UploadString(Settings.site + "builds.php", "{\"type\":\"list\"}" /*jss.Serialize(new { type = "list" })*/);
                    BuildsListItem item;
                    builds = jss.Deserialize<Dictionary<string, Build>>(respStr);
                    foreach (KeyValuePair<string, Build> build in builds)
                    {
                        item = new BuildsListItem { BName = build.Value.name, ImageData = build.Value.img ? webClient.DownloadData(Settings.site + "builds/" + build.Key + "/image.png") : null, Path = build.Key };
                        buildsListView.Items.Add(item);
                    }
                    settings.lNET = true;
                    //onlineCheckBox.IsChecked = false;
                    IsEnabled = true;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        settings.lNET = false;
                        onlineCheckBox.IsChecked = false;
                        IsEnabled = true;
                    }
                    else MessageBox.Show(ex.Message, "Не ебу, че за ошибка..");
                }
            }

        }

        private void deleteBuild_Click(object sender, RoutedEventArgs e)
        {
            if (buildsListView.SelectedIndex != -1)
            {
                string request = jss.Serialize(new { type = "remove", clientToken = AuthWindow.userData.clientToken, accessToken = AuthWindow.userData.accessToken, build = builds[((BuildsListItem)buildsListView.SelectedItem).Path] });
                MessageBox.Show(webClient.UploadString(Settings.site + "builds.php", request));
                updateBuildList();
                //MessageBox.Show(error.errorMessage, error.error);
            }
        }

        private void updateBuildList()
        {
            try
            {
                buildsListView.Items.Clear();

                string respStr = webClient.UploadString(Settings.site + "builds.php", "{\"type\":\"list\"}" /*jss.Serialize(new { type = "list" })*/);
                File.WriteAllText(settings.GetLPath() + "builds.json", respStr);
                BuildsListItem item;
                MessageBox.Show(respStr);
                builds = jss.Deserialize<Dictionary<string, Build>>(respStr);
                foreach (KeyValuePair<string, Build> build in builds)
                {
                    item = new BuildsListItem { BName = build.Value.name, ImageData = build.Value.img ? webClient.DownloadData(Settings.site + "builds/" + build.Key + "/image.png") : null, Path = build.Key };
                    buildsListView.Items.Add(item);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void updateBuilds_Click(object sender, RoutedEventArgs e)
        {
            updateBuildList();
        }
    }
    public class BuildsListItem : ListItem
    {
        public string BName { get; set; }
        public byte[] ImageData { get; set; }
        public string Path { get; set; }
    }
}
