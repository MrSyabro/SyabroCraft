using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public string gPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SyabroCraft";
        WebClient client = new WebClient();
        Process proc = new Process();
        INI ini = new INI();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public string repoLinc = @"http://syabrocraft.xyz/wp-content/minecraft/";
        public string versLinc = @"http://syabrocraft.xyz/wp-content/plugins/syabrocraft/version_change.php";
        public Uri libLinc;
        public Form1()
        {
            InitializeComponent();
            ini.path = gPath + "\\seting.ini";
            
        }

        //public string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(gPath))
            {
                label2.Text = "Загрузка файлов...";
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                label2.Text = "Проверка...";
                backgroundWorker2.RunWorkerAsync();
            }
        }
        
        class INI
        { 
             public string path;

            [DllImport("kernel32")] 
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32")] 
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath); 

            public void IniWriteValue(string Section, string Key, string Value) 
            { 
                   if(!Directory.Exists(Path.GetDirectoryName(path))) 
                         Directory.CreateDirectory(Path.GetDirectoryName(path)); 
                   if(!File.Exists(path)) 
                          using (File.Create(path)) { }; 

                   WritePrivateProfileString(Section, Key, Value, this.path); 
            } 

           public string IniReadValue(string Section, string Key) 
           { 
                 StringBuilder temp = new StringBuilder(255); 
                 int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path); 
                 return temp.ToString(); 
           } 
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    libLinc = new Uri(repoLinc + "libraries64.json");
                }
                else
                {
                    libLinc = new Uri(repoLinc + "libraries32.json");
                }
                Directory.CreateDirectory(gPath);
                Directory.CreateDirectory(gPath + "\\libraries\\");

                HttpWebRequest libsListWebRequest = (HttpWebRequest)WebRequest.Create(libLinc);
                HttpWebResponse libsListWebResponse = (HttpWebResponse)libsListWebRequest.GetResponse();
                Stream ReceiveStream = libsListWebResponse.GetResponseStream();
                StreamReader libListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                string libListJSON = libListSR.ReadToEnd();
                List<Lib> libList = jss.Deserialize<List<Lib>>(libListJSON);
                foreach (Lib lib in libList)
                {
                    backgroundWorker1.ReportProgress(0, lib.name);
                    client.DownloadFile(repoLinc + "libs/" + lib.name, gPath + "\\libraries\\" + lib.name);
                }

                backgroundWorker1.ReportProgress(0, "Загрузка ресурсов...");
                HttpWebRequest assetsListWebRequest = (HttpWebRequest)WebRequest.Create(repoLinc + "/assets.json");
                HttpWebResponse assetsListWebResponse = (HttpWebResponse)assetsListWebRequest.GetResponse();
                ReceiveStream = assetsListWebResponse.GetResponseStream();
                StreamReader assetListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                string assetListJSON = assetListSR.ReadToEnd();
                List<string> assetList = jss.Deserialize<List<string>>(assetListJSON);
                foreach (string asset in assetList)
                {
                    if (!Directory.Exists(new FileInfo(gPath + "\\assets\\" + asset).DirectoryName))
                        Directory.CreateDirectory(new FileInfo(gPath + "\\assets\\" + asset).DirectoryName);
                    client.DownloadFile(repoLinc + "assets/" + asset.Replace(@"\\", "/"), gPath + "\\assets\\" + asset.Replace(".asset", ""));
                }

                backgroundWorker1.ReportProgress(0, "Загрузка архивов...");
                client.DownloadFile(new Uri(repoLinc + "mCore.zip"), gPath + "\\mCore.zip");
                client.DownloadFile(new Uri(repoLinc + "lCore.zip"), gPath + "\\lCore.zip");
                backgroundWorker1.ReportProgress(0, "Расспаковка...");
                ZipFile.ExtractToDirectory(gPath + "\\mCore.zip", gPath);
                ZipFile.ExtractToDirectory(gPath + "\\lCore.zip", gPath);
                File.Delete(gPath + "\\mCore.zip");
                File.Delete(gPath + "\\lCore.zip");

                string launcherver = POST(versLinc, "name=launcher");
                string corever = POST(versLinc, "name=core");

                SerSetingsClass setings = new SerSetingsClass();
                setings.sram = "1G";
                setings.core_version = Convert.ToInt32( corever );
                setings.launcher_version = Convert.ToInt32(launcherver);
                setings.dopArguments = "";
                setings.oldBuild = "";
                setings.oldServer = "";
                setings.clientToken = Convert.ToString(Guid.NewGuid());
                setings.accessToken = "";
                setings.shaders = false;
                setings.forge = true;
                File.WriteAllText(gPath + "\\seting.json", jss.Serialize(setings));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при попытке установки");}
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

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    libLinc = new Uri(repoLinc + "libraries64.json");
                } else
                {
                    libLinc = new Uri(repoLinc + "libraries32.json");
                }
                if (!Directory.Exists(gPath + "\\libraries"))
                {
                    HttpWebRequest libsListWebRequest = (HttpWebRequest)WebRequest.Create(libLinc);
                    HttpWebResponse libsListWebResponse = (HttpWebResponse)libsListWebRequest.GetResponse();
                    Stream ReceiveStream = libsListWebResponse.GetResponseStream();
                    StreamReader libListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                    string libListJSON = libListSR.ReadToEnd();
                    List<Lib> libList = jss.Deserialize<List<Lib>>(libListJSON);
                    foreach (Lib lib in libList)
                    {
                        backgroundWorker2.ReportProgress(0, lib.name);
                        client.DownloadFile(repoLinc + "libs/" + lib.name, gPath + "\\libraries\\" + lib.name);
                    }
                }
                else
                {
                    HttpWebRequest libsListWebRequest = (HttpWebRequest)WebRequest.Create(libLinc);
                    HttpWebResponse libsListWebResponse = (HttpWebResponse)libsListWebRequest.GetResponse();
                    Stream ReceiveStream = libsListWebResponse.GetResponseStream();
                    StreamReader libListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                    string libListJSON = libListSR.ReadToEnd();
                    List<Lib> libList = jss.Deserialize<List<Lib>>(libListJSON);
                    foreach (Lib lib in libList)
                    {
                        if (File.Exists(gPath + "\\libraries\\" + lib.name) && ComputeMD5Checksum(gPath + "\\libraries\\" + lib.name) != lib.hash)
                        {
                            File.Delete(gPath + "\\libraries\\" + lib.name);
                            backgroundWorker2.ReportProgress(0, lib.name);
                            client.DownloadFile(repoLinc + "libs/" + lib.name, gPath + "\\libraries\\" + lib.name);
                        }
                        else if (!File.Exists(gPath + "\\libraries\\" + lib.name))
                        {
                            backgroundWorker2.ReportProgress(0, lib.name);
                            client.DownloadFile(repoLinc + "libs/" + lib.name, gPath + "\\libraries\\" + lib.name);
                        }
                            
                    }
                }
                if (!Directory.Exists(gPath + "\\assets"))
                {
                    backgroundWorker2.ReportProgress(0, "Загрузка ресурсов...");
                    HttpWebRequest assetsListWebRequest = (HttpWebRequest)WebRequest.Create(repoLinc + "/assets.json");
                    HttpWebResponse assetsListWebResponse = (HttpWebResponse)assetsListWebRequest.GetResponse();
                    Stream ReceiveStream = assetsListWebResponse.GetResponseStream();
                    StreamReader assetListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                    string assetListJSON = assetListSR.ReadToEnd();
                    List<string> assetList = jss.Deserialize<List<string>>(assetListJSON);
                    foreach (string asset in assetList)
                    {
                        if (!Directory.Exists(new FileInfo(gPath + "\\assets\\" + asset).DirectoryName))
                            Directory.CreateDirectory(new FileInfo(gPath + "\\assets\\" + asset).DirectoryName);
                        client.DownloadFile(repoLinc + "assets/" + asset.Replace(@"\\", "/"), gPath + "\\assets\\" + asset.Replace(".asset", ""));
                    }
                }
                if (!Directory.Exists(gPath + "\\bin"))
                {
                    backgroundWorker2.ReportProgress(0, "Загрузка архива...");
                    client.DownloadFile(new Uri(repoLinc + "mCore.zip"), gPath + "\\mCore.zip");
                    backgroundWorker2.ReportProgress(0, "Расспаковка...");
                    ZipFile.ExtractToDirectory(gPath + "\\mCore.zip", gPath);
                    File.Delete(gPath + "\\mCore.zip");
                }
                if (!File.Exists(gPath + "\\Launcher.exe"))
                {
                    backgroundWorker2.ReportProgress(0, "Загрузка архива...");
                    client.DownloadFile(new Uri(repoLinc + "lCore.zip"), gPath + "\\lCore.zip");
                    backgroundWorker2.ReportProgress(0, "Расспаковка...");
                    ZipFile.ExtractToDirectory(gPath + "\\lCore.zip", gPath);
                    File.Delete(gPath + "\\lCore.zip");
                }
                int siteLauncherVer = Convert.ToInt32(POST(versLinc, "name=launcher"));
                int siteCoreVer = Convert.ToInt32(POST(versLinc, "name=core"));
                if (!File.Exists(gPath + "\\seting.json"))
                {
                    SerSetingsClass setings = new SerSetingsClass();
                    setings.sram = "1G";
                    setings.core_version = Convert.ToInt32(siteCoreVer);
                    setings.launcher_version = Convert.ToInt32(siteLauncherVer);
                    setings.dopArguments = "";
                    setings.oldBuild = "";
                    setings.oldServer = "";
                    setings.clientToken = Convert.ToString(Guid.NewGuid());
                    setings.accessToken = "";
                    setings.shaders = false;
                    setings.forge = true;
                    File.WriteAllText(gPath + "\\seting.json", jss.Serialize(setings));
                }
                else
                {
                    SerSetingsClass setings = jss.Deserialize<SerSetingsClass>(File.ReadAllText(gPath + "\\seting.json"));
                    if (siteLauncherVer < setings.launcher_version)
                    {
                        backgroundWorker2.ReportProgress(0, "Обновление лаунчера...");
                        File.Delete(gPath + "\\Launcher.exe");
                        client.DownloadFile(new Uri(repoLinc + "/lCore.zip"), gPath + "\\lCore.zip");
                        ZipFile.ExtractToDirectory(gPath + "\\lCore.zip", gPath);
                        File.Delete(gPath + "\\lCore.zip");
                        setings.launcher_version = siteLauncherVer;
                        File.WriteAllText(gPath + "\\seting.json", jss.Serialize(setings));
                    }
                    if (siteCoreVer < setings.core_version)
                    {
                        backgroundWorker2.ReportProgress(0, "Обновления Minecraft...");
                        Directory.Delete(gPath + "\\bin", true);
                        client.DownloadFile(new Uri(repoLinc + "/mCore.zip"), gPath + "\\mCore.zip");
                        ZipFile.ExtractToDirectory(gPath + "\\mCore.zip", gPath);
                        File.Delete(gPath + "\\mCore.zip");
                        setings.launcher_version = siteCoreVer;
                        File.WriteAllText(gPath + "\\seting.json", jss.Serialize(setings));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при попытке обновления"); }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Process.Start(gPath + "\\Launcher.exe");
            this.Close();
        }

        private static string POST(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
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

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            label2.Text = e.UserState as String;
        }
    }

    public class Lib
    {
        public string name;
        public string hash;
    }

    class SerSetingsClass
    {
        public string sram = "1G";
        public string dopArguments = "";
        public string javaPath;
        public string clientToken;
        public string accessToken;
        public string oldBuild;
        public string oldServer;
        public bool forge;
        public bool shaders;
        public int launcher_version;
        public int core_version;
        public int updater_version;
    }
}
