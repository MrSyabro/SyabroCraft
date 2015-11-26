using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace BuildCreator
{
    public partial class MainWindow : Form
    {
        public string gamePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SyabroCraft\\";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Show(object sender, EventArgs e)
        {
            if (Directory.Exists(gamePath + "builds\\")) foreach(string buildPath in Directory.GetDirectories(gamePath + "builds\\"))
            {
                string buildFolder = new DirectoryInfo(buildPath).Name;
                buildsSelector.Items.Add(buildFolder);
            }
            if (Directory.Exists(gamePath + "libraries\\")) libFileList.Items.AddRange(Directory.GetFiles(gamePath + "libraries\\", "*.jar"));
            if (Directory.Exists(gamePath + "assets\\")) assetsListBox.Items.AddRange(Directory.GetFiles(gamePath + "assets\\", "*", SearchOption.AllDirectories));
        }

        private void buildsSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildFileList.Items.Clear();
            
            foreach(string itemName in Directory.GetFiles(gamePath + "builds\\" + buildsSelector.Text + "\\mods\\", "*.jar", SearchOption.AllDirectories))
            {
                buildFileList.Items.Add(itemName);
            }
            foreach (string itemName in Directory.GetFiles(gamePath + "builds\\" + buildsSelector.Text + "\\mods\\", "*.litemod", SearchOption.AllDirectories))
            {
                buildFileList.Items.Add(itemName);
            }
        }

        private void saveFilesButton_Click(object sender, EventArgs e)
        {
            BuildInfo buildInfo = new BuildInfo();
            buildInfo.id = buildsSelector.Text;
            foreach (string item in buildFileList.Items)
            {
                Item itemInList = new Item();
                itemInList.name = new FileInfo(item).Name;
                itemInList.hash = ComputeMD5Checksum(item);
                itemInList.localPath = item.Replace(gamePath + "builds\\" + buildsSelector.Text, "").Replace(@"\", "\\");
                itemInList.sitePath = item.Replace(gamePath + "builds\\" + buildsSelector.Text, "").Replace("\\", "/");
                buildInfo.fileList.Add(itemInList);
            }
            string outPut = jss.Serialize(buildInfo);
            MessageBox.Show(POST("http://syabrocraft.xyz/wp-content/plugins/syabrocraft/build_editor.php", "key=" + keyTextBox.Text + "&json=" + outPut));
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

        private void allLibsList_Click(object sender, EventArgs e)
        {
            List<Lib> libList = new List<Lib>();
            foreach (string libPath in Directory.GetFiles(gamePath + "libraries\\", "*.jar"))
            {
                Lib libInfo = new Lib();
                libInfo.name = new FileInfo(libPath).Name;
                libInfo.hash = ComputeMD5Checksum(libPath);
                libList.Add(libInfo);
            }
            string outPut = jss.Serialize(libList);
            MessageBox.Show(outPut);
            File.WriteAllText(gamePath + "\\libraries.json", outPut);
        }

        private void createAssetsList_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(gamePath + "site_assets\\"))
                Directory.Delete(gamePath + "site_assets\\");
            Directory.CreateDirectory(gamePath + "site_assets\\");
            List<string> assetsList = new List<string>();
            foreach (string fileName in Directory.GetFiles(gamePath + "assets\\", "*", SearchOption.AllDirectories))
            {
                assetsList.Add(fileName.Replace(gamePath + "assets\\", "") + ".asset");
                Directory.CreateDirectory(new FileInfo(fileName).DirectoryName.Replace("assets", "site_assets"));
                File.Copy(fileName, gamePath + "site_assets\\" + fileName.Replace(gamePath + "assets\\", "") + ".asset");
            }
            File.WriteAllText(gamePath + "\\assets.json", jss.Serialize(assetsList));
            MessageBox.Show("OK");
        }
        public static string POST(string Url, string Data, string ContentType = "application/x-www-form-urlencoded")
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = ContentType;
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
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
    }

    public class BuildInfo
    {
        public string id;
        //public string assetIndex;
        public List<Item> fileList = new List<Item>();
    }
    public class Item
    {
        public string name;
        public string sitePath;
        public string localPath;
        public string hash;
    }
    public class Lib
    {
        public string name;
        public string hash;
    }
}
