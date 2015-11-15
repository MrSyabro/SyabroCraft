using setings;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Text;

namespace Launcher
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Setings setingsWindow = new Setings();
        Starter starter = new Starter();
        WebClient client = new WebClient();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public Builds buildsList = new Builds();

        private void StartGame_Click(object sender, EventArgs e)
        {
            if (serverSelector.Text != "")
            {
                SetingClass.selectedServer = buildsList.builds[buildsSelector.SelectedIndex].servers[serverSelector.SelectedIndex];
                SetingClass.selectedBuild = buildsList.builds[buildsSelector.SelectedIndex];
                SetingClass.oldBuild = serverSelector.Text;
                SetingClass.oldServer = buildsSelector.Text;
                saveSetings();
            }
            else
            {
                SetingClass.selectedBuild = buildsList.builds[buildsSelector.SelectedIndex];
                SetingClass.oldBuild = buildsSelector.Text;
                saveSetings();
            }

            starter.ShowDialog();
            Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest buildsListWebRequest = (HttpWebRequest)WebRequest.Create(SetingClass.repoLink + "builds.json");
                HttpWebResponse buildsListWebResponse = (HttpWebResponse)buildsListWebRequest.GetResponse();
                Stream ReceiveStream = buildsListWebResponse.GetResponseStream();
                StreamReader buildsListSR = new StreamReader(ReceiveStream, Encoding.UTF8);
                string buildsListJSON = buildsListSR.ReadToEnd();
                buildsList = jss.Deserialize<Builds>(buildsListJSON);
                foreach (Build build in buildsList.builds)
                {
                    buildsSelector.Items.Add(build.Name);
                }

                if (SetingClass.oldBuild != "")
                {
                    buildsSelector.Text = SetingClass.oldBuild;
                    if (SetingClass.oldServer != "")
                        serverSelector.Text = SetingClass.oldServer;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

            
        }
        public void ungzip(string path, string decomPath, bool overwrite)
        {
            if (File.Exists(decomPath))
            {
                if (overwrite)
                {
                    File.Delete(decomPath);
                }
                else
                {
                    throw new IOException("The decompressed path you specified already exists and cannot be overwritten.");
                }
            }
            GZipStream stream = new GZipStream(new FileStream(path, FileMode.Open, FileAccess.ReadWrite), CompressionMode.Decompress);
            FileStream decompressedFile = new FileStream(decomPath, FileMode.OpenOrCreate, FileAccess.Write);
            int data;
            while ((data = stream.ReadByte()) != -1)
            {
                decompressedFile.WriteByte((byte)data);
            }
            decompressedFile.Close();
            stream.Close();
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
        private void button2_Click(object sender, EventArgs e)
        {
            setingsWindow.ShowDialog();
            if (setingsWindow.DialogResult == DialogResult.OK)
            {
                AuthWindow.saveSetings();
            }
        }
        private Server SearchServer(string name)
        {
            foreach (Build build in buildsList.builds)
            {
                foreach (Server server in build.servers)
                {
                    if (server.Name == name)
                        return server;
                }
            }
            return null;
        }
        private Build SearchBuild(string name)
        {
            foreach (Build build in buildsList.builds)
            {
                if (build.Name == name)
                    return build;
            }
            return null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buildsSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Build build = SearchBuild(buildsSelector.SelectedText);
            foreach (Server server in buildsList.builds[buildsSelector.SelectedIndex].servers)
            {
                serverSelector.Items.Add(server.Name);
            }
            serverSelector.Items.Add("");
        }
        public static void saveSetings()
        {
            SerSetingsClass serSetings = new SerSetingsClass();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            serSetings.accessToken = SetingClass.accessToken;
            serSetings.clientToken = SetingClass.clientToken;
            serSetings.dopArguments = SetingClass.dopArguments;
            serSetings.forge = SetingClass.forge;
            serSetings.javaPath = SetingClass.javaPath;
            serSetings.oldBuild = SetingClass.oldBuild;
            serSetings.oldServer = SetingClass.oldServer;
            serSetings.shaders = SetingClass.shaders;
            serSetings.sram = SetingClass.sram;
            File.WriteAllText(SetingClass.pDir + "\\" + SetingClass.gDir + "\\seting.json", jss.Serialize(serSetings));
        }
    }
    [Serializable]
    public class Builds
    {
        public Build[] builds;
    }
    public class Build
    {
        public string Name;
        public string ID;
        public Server[] servers;
    }
    public class Server
    {
        public string Name;
        public string IP;
        public string Port;
    }
}

