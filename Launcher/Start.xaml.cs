using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Windows;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для Start.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        Build selectedBuild;
        BackgroundWorker bWorker;

        /// <summary>
        /// Иницыализация элементов StartWindow
        /// </summary>
        /// <param name="sBuild">Запускаемая сборка</param>
        public StartWindow(Build sBuild)
        {
            selectedBuild = sBuild;
            InitializeComponent();
            bWorker = new BackgroundWorker();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bWorker.RunWorkerAsync(selectedBuild);
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.ProgressChanged += new ProgressChangedEventHandler(bWorker_PChanged);
        }

        private void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Settings settings = AuthWindow.settings;
            WebClient webClient = new WebClient();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            Build sBuild = (Build)e.Argument;
            User userData = AuthWindow.userData;

            try
            {
                // Проверка клиента
                if (!File.Exists(settings.GetGameEXEFile(sBuild.gameVer)))
                {
                    if (!Directory.Exists(settings.GetLPath() + "versions\\" + sBuild.gameVer + "\\"))
                        Directory.CreateDirectory(settings.GetLPath() + "versions\\" + sBuild.gameVer + "\\");
                    bWorker.ReportProgress(0, "Загрузка клиента..");
                    ZipArchive zip = new ZipArchive(webClient.OpenRead(Settings.site + "versions/" + sBuild.gameVer));
                    zip.ExtractToDirectory(settings.Dir + Settings.lDir + "versions\\");
                    zip.Dispose();
                }

                // Проверка библиотек
                if (!Directory.Exists(settings.GetLPath() + "libraries\\"))
                    Directory.CreateDirectory(settings.GetLPath() + "libraries\\");
                Dictionary<string, string> libraries = jss.Deserialize<Dictionary<string, string>>(webClient.DownloadString(Settings.site + "libraries/" + sBuild.gameVer + (Environment.Is64BitOperatingSystem ? "64" : "32") + ".json"));
                int step = 100 / libraries.Count;
                int progress = 0;
                foreach (KeyValuePair<string, string> lib in libraries)
                {
                    bWorker.ReportProgress(progress, lib.Key);
                    if (!File.Exists(settings.GetLPath() + "libraries\\" + lib.Key) ||
                        !(BitConverter.ToString(sha1.ComputeHash(File.ReadAllBytes(settings.GetLPath() + "libraries\\" + lib.Key))).Replace("-", string.Empty) == lib.Value))
                    {
                        webClient.DownloadFile(Settings.site + "libraries/" + lib.Key, settings.GetLPath() + "libraries\\" + lib.Key);
                    }
                    bWorker.ReportProgress(progress += step, lib.Key);
                }

                // Загрузка ресурсов клиента
                if (!Directory.Exists(settings.GetLPath() + "assets\\indexes\\"))
                    Directory.CreateDirectory(settings.GetLPath() + "assets\\indexes");
                if (!Directory.Exists(settings.GetLPath() + "assets\\objects\\"))
                    Directory.CreateDirectory(settings.GetLPath() + "assets\\objects");
                webClient.DownloadFile(Settings.site + "assets/indexes/" + sBuild.gameVer + ".json", settings.GetLPath() + "assets/indexes/" + sBuild.gameVer + ".json");
                GameAssets gAssets = jss.Deserialize<GameAssets>(File.ReadAllText(settings.GetLPath() + "assets/indexes/" + sBuild.gameVer + ".json"));
                foreach (KeyValuePair<string, Asset> asset in gAssets.objects)
                {
                    bWorker.ReportProgress(progress, asset.Key);
                    if (!File.Exists(settings.GetLPath() + "assets\\objects\\" + asset.Value.hash.Substring(0, 2) + "\\" + asset.Value.hash) ||
                        !(BitConverter.ToString(sha1.ComputeHash(File.ReadAllBytes(settings.GetLPath() + "assets\\objects\\" + asset.Value.hash.Substring(0, 2) + "\\" + asset.Value.hash))).Replace("-", string.Empty) == asset.Value.hash.ToUpper()))
                    {
                        if (!Directory.Exists(settings.GetLPath() + "assets\\objects\\" + asset.Value.hash.Substring(0, 2)))
                            Directory.CreateDirectory(settings.GetLPath() + "assets\\objects\\" + asset.Value.hash.Substring(0, 2));
                        webClient.DownloadFile(Settings.site + "assets\\objects\\" + asset.Value.hash.Substring(0, 2) + "/" + asset.Value.hash, settings.GetLPath() + "assets\\objects\\" + asset.Value.hash.Substring(0, 2) + "\\" + asset.Value.hash);
                    }
                    bWorker.ReportProgress(progress += step, asset.Key);
                }

                // Загрузка модификаций
                //if (!Directory.Exists($"{settings.GetLPath()}builds\\{sBuild.dir}\\mods"))
                //Directory.CreateDirectory($"{settings.GetLPath()}builds\\{sBuild.dir}\\mods");
                if (!sBuild.local && settings.lNET)
                    webClient.DownloadFile(Settings.site + "builds/" + sBuild.dir + "/mods.json", settings.GetBuildDir(sBuild) + "mods.json");
                ModFile modFile;
                List<string> buildMods = jss.Deserialize<List<string>>(File.ReadAllText(settings.GetBuildDir(sBuild) + "mods.json"));
                Dictionary<string, Dictionary<string, ModFile>> globalMods = jss.Deserialize<Dictionary<string, Dictionary<string, ModFile>>>(webClient.DownloadString(Settings.site + "mods.json"));
                step = 100 / buildMods.Count;
                progress = 0;
                foreach (string modName in buildMods)
                {
                    bWorker.ReportProgress(progress, modName);
                    modFile = globalMods[modName][sBuild.gameVer];
                    if (!File.Exists(settings.GetBuildDir(sBuild.dir) + "mods\\" + modFile.name) ||
                        !(BitConverter.ToString(sha1.ComputeHash(File.ReadAllBytes(settings.GetBuildDir(sBuild.dir) + "mods\\" + modFile.name))).Replace("-", string.Empty) == modFile.hash))
                    {
                        Directory.CreateDirectory((new FileInfo(settings.GetBuildDir(sBuild.dir) + "mods\\" + modFile.name)).DirectoryName);
                        webClient.DownloadFile(Settings.site + "mods/" + modFile.name, settings.GetBuildDir(sBuild.dir) + "mods\\" + modFile.name);
                    }
                    bWorker.ReportProgress(progress += step, modName);
                }

                // Загрузка дополнительных файлов
                if (sBuild.zip)
                {
                    bWorker.ReportProgress(0, "Загрузка доп.файлов..");
                    ZipArchive zip = new ZipArchive(webClient.OpenRead(Settings.site + "builds/" + sBuild.dir + "/files.zip"));
                    zip.ExtractToDirectory(settings.GetBuildDir(sBuild.dir));
                    zip.Dispose();
                }

                // Запуск игры
                bWorker.ReportProgress(0, "Запуск игры..");
                string mineLib = "";
                foreach (KeyValuePair<string, string> lib in libraries)
                { mineLib += "\"" + settings.Dir + Settings.lDir + "\";"; }
                Process minecraft = new Process();
                minecraft.StartInfo.Arguments = $"{settings.jArg} -Xmx{settings.jRAM}" + // Дополнительные аргументы и RAM
                    " -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true" + // Обязытельные аргументы
                    $" -Djava.library.path=\"{settings.GetGameDLLFiles(sBuild.dir)}\"" + // Путь к папке natives
                    $" -cp {mineLib}\"{settings.GetGameEXEFile(sBuild)}\";" + // Список файлов библиотек и исполняемый файл
                    $" --version \"{sBuild.gameVer}\" --gameDir \"{settings.GetBuildDir(sBuild)}\"" + // Версия, путь к папке игры
                    @" --assetsDir " + settings.Dir + Settings.lDir + @"assets\" + $" --assetIndex {sBuild.gameVer}" + // Ресурсы
                    $" --accessToken {userData.accessToken} --uuid {userData.uuid} --userProperties [] --userType majang " + // Ключи
                    $" --username {userData.username}"; // Имя игрока
                minecraft.StartInfo.UseShellExecute = false;
                minecraft.StartInfo.WorkingDirectory = settings.GetBuildDir(sBuild.dir);
                minecraft.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void bWorker_PChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 0)
            {
                progressBar.IsIndeterminate = false;
                progressBar.Value = e.ProgressPercentage;
            } else { progressBar.IsIndeterminate = true; }
            stateLabel.Content = e.UserState;
        }
    }

    class GameAssets
    {
        public Dictionary<string, Asset> objects { get; set; }
    }

    class Asset
    {
        public string hash { get; set; }
        public int size { get; set; }
    }

    class ModFile
    {
        public string name { get; set; }
        public string hash { get; set; }
    }
}
