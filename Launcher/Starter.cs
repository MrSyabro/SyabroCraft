using Launcher;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace System
{
    public partial class Starter : Form
    {
        public Starter()
        {
            InitializeComponent();
        }
        public string forge = "";
        public string server = "";
        public string shaders = "";
        public string liteMod = "";
        WebClient client = new WebClient();
        JavaScriptSerializer jss = new JavaScriptSerializer();

        private void Starter_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID))
            {
                label2.Text = "Проверка целосности сборки";
                if (SetingClass.buildAutoSync)
                {
                    CheckBuild.RunWorkerAsync();
                } else
                {
                    startGame();
                }
            } else {
                label2.Text = "Загрузка сборки...";
                DownloadBuild.RunWorkerAsync();
            }
        }

        private void startGame()
        {
            try
            {
                if (SetingClass.javaPath == "")
                    proc.StartInfo.FileName = "java";
                else
                    proc.StartInfo.FileName = SetingClass.javaPath;

                if (SetingClass.selectedServer != null)
                {
                    string IP = SetingClass.selectedServer.IP;
                    string port = SetingClass.selectedServer.Port;
                    server = " --server " + IP + " --port " + port;
                }

                if (SetingClass.forge)
                    forge = " --tweakClass cpw.mods.fml.common.launcher.FMLTweaker";

                if (SetingClass.shaders)
                    shaders = " --tweakClass shadersmodcore.loading.SMCTweaker";

                if (SetingClass.liteMod)
                    shaders = " --tweakClass com.mumfrey.liteloader.launch.LiteLoaderTweaker";

                string MineLib = string.Join("\";\"", Directory.GetFiles(SetingClass.pDir + "\\" + SetingClass.gDir + "\\libraries", "*.jar", SearchOption.AllDirectories));
                proc.StartInfo.Arguments = SetingClass.dopArguments + " -Xmx" + SetingClass.sram + " -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:+UseConcMarkSweepGC -XX:+CMSIncrementalMode -XX:-UseAdaptiveSizePolicy -Xmn128M -Djava.library.path=\"" + SetingClass.pDir + "\\" + SetingClass.gDir + "\\bin\\natives\" -cp \"" + MineLib + "\";\"" + SetingClass.pDir + "\\" + SetingClass.gDir + "\\bin\\minecraft.jar\" net.minecraft.launchwrapper.Launch --username " + SetingClass.login + " --gameDir \"" + SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + "\" --version \"" + SetingClass.selectedBuild.Name + "\" --assetsDir \"" + SetingClass.pDir + "\\" + SetingClass.gDir + "\\assets\" --assetIndex " + "1.7.10" + " --accessToken " + SetingClass.accessToken + " --uuid " + SetingClass.uuid + " --userProperties [] --userType mojang " + liteMod + forge + server + shaders;
                if (!SetingClass.showConsole)
                {
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.CreateNoWindow = true;
                }
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WorkingDirectory = SetingClass.pDir + "\\" + SetingClass.gDir;
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка, пожалуйста, отправте файл логов \"...\\Roaming\\SyabroCraft\\LauncherLog.log\" на форум разработчика.");
                AuthWindow.writeToLog("[" + DateTime.Today + "]: Произошал ошибка: ");
                AuthWindow.writeToLog("Сообщение: " + ex.Message);
                AuthWindow.writeToLog("Исключение из: " + ex.TargetSite);
                AuthWindow.writeToLog("Информация: " + ex.Data);
                AuthWindow.writeToLog("Стек: " + ex.StackTrace);
                Process.Start(SetingClass.pDir + "\\" + SetingClass.gDir);
                Process.Start("http://syabrocraft.xyz/forums/forum/%D0%B2%D0%BE%D0%BF%D1%80%D0%BE%D1%81-%D0%BE%D1%82%D0%B2%D0%B5%D1%82/");
                AuthWindow.logFile.Close();
                Close();
            }
            //label2.Text = "Читаю логи.. :)";
            //AuthWindow.writeToLog(proc.StandardError.ReadToEnd());
            //AuthWindow.writeToLog(proc.StandardOutput.ReadToEnd());
        }

        private void backgroundWorker1_DoWork_downloadBuild(object sender, ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (!Directory.Exists(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds"))
                    Directory.CreateDirectory(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds");
                Directory.CreateDirectory(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID);

                HttpWebRequest buildFileWebRequest = (HttpWebRequest)WebRequest.Create(SetingClass.repoLink + "/builds/" + SetingClass.selectedBuild.ID + ".json");
                HttpWebResponse buildFileWebResponse = (HttpWebResponse)buildFileWebRequest.GetResponse();
                Stream ReceiveStream = buildFileWebResponse.GetResponseStream();
                StreamReader buildsListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                string buildsListJSON = buildsListSR.ReadToEnd();
                BuildInfo buildFiles = jss.Deserialize<BuildInfo>(buildsListJSON);

                foreach (Item item in buildFiles.fileList)
                {
                    if (!Directory.Exists(new FileInfo(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath).DirectoryName))
                        Directory.CreateDirectory(new FileInfo(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath).DirectoryName);
                    DownloadBuild.ReportProgress(0, "Загрузка " + item.name);
                    client.DownloadFile(SetingClass.repoLink + item.sitePath, SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка", "Произошла ошибка, пожалуйста, отправте файл логов \"...\\Roaming\\SyabroCraft\\LauncherLog.log\" на форум разработчика.");
                AuthWindow.writeToLog("[" + DateTime.Today + "]: Произошал ошибка: ");
                AuthWindow.writeToLog("Сообщение: " + ex.Message);
                AuthWindow.writeToLog("Исключение из: " + ex.TargetSite);
                AuthWindow.writeToLog("Информация: " + ex.Data);
                AuthWindow.writeToLog("Стек: " + ex.StackTrace);
                Process.Start(SetingClass.pDir + "\\" + SetingClass.gDir);
                Process.Start("http://syabrocraft.xyz/forums/forum/%D0%B2%D0%BE%D0%BF%D1%80%D0%BE%D1%81-%D0%BE%D1%82%D0%B2%D0%B5%D1%82/");
                AuthWindow.logFile.Close();
                Close();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Point location = new Point(Cursor.Position.X, Cursor.Position.Y);
                this.Location = location;
            }
        }

        private string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }

        private void CheckBuild_DoWork(object sender, ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                HttpWebRequest buildFileWebRequest = (HttpWebRequest)WebRequest.Create(SetingClass.repoLink + "builds/" + SetingClass.selectedBuild.ID + ".json");
                HttpWebResponse buildFileWebResponse = (HttpWebResponse)buildFileWebRequest.GetResponse();
                Stream ReceiveStream = buildFileWebResponse.GetResponseStream();
                StreamReader buildsListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                string buildsListJSON = buildsListSR.ReadToEnd();
                BuildInfo buildInfo = jss.Deserialize<BuildInfo>(buildsListJSON);

                foreach (Item item in buildInfo.fileList)
                {
                    if (!File.Exists(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath))
                    {
                        if (!Directory.Exists(new FileInfo(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath).DirectoryName))
                            Directory.CreateDirectory(new FileInfo(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath).DirectoryName);
                        CheckBuild.ReportProgress(0, "Загрузка " + item.name);
                        client.DownloadFile(SetingClass.repoLink + item.sitePath, SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath);
                    }
                    else if (item.hash != ComputeMD5Checksum(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + "\\" + item.localPath))
                    {
                        File.Delete(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath);
                        CheckBuild.ReportProgress(0, "Загрузка " + item.name);
                        client.DownloadFile(SetingClass.repoLink + item.sitePath, SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + item.localPath);
                    }
                }

                foreach (string fileName in Directory.GetFiles(SetingClass.pDir + "\\" + SetingClass.gDir + "\\builds\\" + SetingClass.selectedBuild.ID + "\\mods\\", "*.jar", SearchOption.AllDirectories))
                {
                    if (!searchMods(buildInfo, fileName))
                    {
                        CheckBuild.ReportProgress(0, "Удаление " + fileName);
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке сборки");
            }
        }

        bool searchMods(BuildInfo buildInfo, string fileName)
        {
            foreach (Item item in buildInfo.fileList)
            {
                if (new FileInfo(fileName).Name == item.name)
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckBuild_RunWorkerCompleted(object sender, ComponentModel.RunWorkerCompletedEventArgs e)
        {
            startGame();
            Close();
        }

        private void DownloadBuild_ProgressChanged(object sender, ComponentModel.ProgressChangedEventArgs e)
        {
            label2.Text = e.UserState as String;
        }

        private void proc_ErrorDataReceived(object sender, Diagnostics.DataReceivedEventArgs e)
        {
            AuthWindow.writeToLog(sender as string);
        }
    }
    class BuildInfo
    {
        public string id;
        //public string assetIndex;
        public Item[] fileList;
    }
    class Item
    {
        public string name;
        public string sitePath;
        public string localPath;
        public string hash;
    }
}
