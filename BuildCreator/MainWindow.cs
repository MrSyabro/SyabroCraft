using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
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
            foreach(string buildPath in Directory.GetDirectories(gamePath + "builds\\"))
            {
                string buildFolder = new DirectoryInfo(buildPath).Name;
                buildsSelector.Items.Add(buildFolder);
            }
            libFileList.Items.AddRange(Directory.GetFiles(gamePath + "libraries\\", "*.jar"));
            assetsListBox.Items.AddRange(Directory.GetFiles(gamePath + "assets\\", "*", SearchOption.AllDirectories));
        }

        private void buildsSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileListSelect.Items.Clear();
            
            foreach(string itemName in Directory.GetFiles(gamePath + "builds\\" + buildsSelector.Text, "*.*", SearchOption.AllDirectories))
            {
                fileListSelect.Items.Add(itemName);
            }
        }

        private void saveFilesButton_Click(object sender, EventArgs e)
        {
            BuildInfo buildInfo = new BuildInfo();
            buildInfo.id = buildsSelector.Text;
            foreach (string item in fileListSelect.CheckedItems)
            {
                Item itemInList = new Item();
                itemInList.name = new FileInfo(item).Name;
                itemInList.hash = ComputeMD5Checksum(item);
                itemInList.localPath = item.Replace(gamePath + "builds\\" + buildsSelector.Text, "").Replace(@"\", "\\");
                itemInList.sitePath = item.Replace(gamePath + "builds\\" + buildsSelector.Text, "").Replace("\\", "/");
                buildInfo.fileList.Add(itemInList);
            }
            string outPut = jss.Serialize(buildInfo);
            MessageBox.Show(outPut);
            File.WriteAllText(gamePath + "\\builds\\" + buildsSelector.Text + ".json", outPut);
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

        private void CBButton_Click(object sender, EventArgs e)
        {
            string item = fileListSelect.SelectedItem as string;
            Item itemInfo = new Item();
            itemInfo.name = new FileInfo(item).Name;
            itemInfo.hash = ComputeMD5Checksum(item);
            itemInfo.localPath = item.Replace(gamePath + "builds\\" + buildsSelector.Text, "").Replace(@"\", "\\");
            itemInfo.sitePath = item.Replace(gamePath + "builds\\" + buildsSelector.Text, "").Replace("\\", "/");
            string outPut = jss.Serialize(itemInfo);
            MessageBox.Show(outPut);
            File.WriteAllText(gamePath + "\\builds\\" + buildsSelector.Text + "_" + new FileInfo(item).Name + ".json", outPut);
        }

        private void allLibsList_Click(object sender, EventArgs e)
        {
            //LibList libList = new LibList();
            List<Lib> libList = new List<Lib>();
            foreach (string libPath in Directory.GetFiles(gamePath + "libraries\\", "*.jar"))
            {
                Lib libInfo = new Lib();
                libInfo.name = new FileInfo(libPath).Name;
                libInfo.hash = ComputeMD5Checksum(libPath);
                libList.Add(libInfo);
                //libList.itemList.Add(libInfo);
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
