using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Launcher
{
    public partial class AuthWindow : Form
    {
        INI ini = new INI();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        MainWindow mainWindow = new MainWindow();

        public AuthWindow()
        {
            InitializeComponent();
            ini.path = SetingClass.pDir + "\\" + SetingClass.gDir + "\\seting.ini";
        }

        private void authBotton_Click(object sender, EventArgs e)
        {
            if (loginbox.Text == "" || passbox.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                try
                {
                    loginbox.Enabled = false;
                    passbox.Enabled = false;
                    authBotton.Enabled = false;
                    regBotton.Enabled = false;
                    progressBar1.Show();

                    User loginInfo = new User();
                    loginInfo.username = loginbox.Text;
                    loginInfo.password = passbox.Text;
                    loginInfo.clientToken = SetingClass.clientToken;
                    //"application/json"
                    string responseFromServer = POST(SetingClass.authLink + "authenticate.php", jss.Serialize(loginInfo), @"application/json");
                    if (responseFromServer == "Bad login")
                    {
                        var result = MessageBox.Show("Неправильный логин/пароль! Хотите зарегестрировать аккаунт?", "Неправильный логин/пароль", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                            Process.Start("http://syabrocraft.xyz/");
                        loginbox.Enabled = true;
                        passbox.Enabled = true;
                        authBotton.Enabled = true;
                        regBotton.Enabled = true;
                        progressBar1.Hide();
                    }
                    else
                    {
                        ResponeServer responseServer = jss.Deserialize<ResponeServer>(responseFromServer);
                        SetingClass.accessToken = responseServer.accessToken;
                        SetingClass.login = responseServer.selectedProfile.name;
                        SetingClass.uuid = responseServer.selectedProfile.id;
                        saveSetings();
                        Hide();
                        mainWindow.ShowDialog();
                        Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при попытке авторизироваться"); this.DialogResult = DialogResult.OK; }
            }
        }

        private void regBotton_Click(object sender, EventArgs e)
        {
            Process.Start("http://syabrocraft.esy.es/register/");
        }

        private void loginbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (loginbox.Text == "Login")
            {
                loginbox.Text = "";
            }
        }
        private void passbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (passbox.Text == "Password")
            {
                passbox.Text = "";
            }
        }
        private void loginbox_MouseLeave(object sender, EventArgs e)
        {
            if (loginbox.Text == "")
            {
                loginbox.Text = "Login";
            }
        }
        private void passbox_MouseLeave(object sender, EventArgs e)
        {
            if (passbox.Text == "")
            {
                passbox.Text = "Password";
            }
        }
        private void AuthWindow_Show(object sender, EventArgs e)
        {
            progressBar1.Hide();
            if (File.Exists(SetingClass.pDir + "\\" + SetingClass.gDir + "\\seting.json"))
            {
                SerSetingsClass serSetings = jss.Deserialize<SerSetingsClass>(File.ReadAllText(SetingClass.pDir + "\\" + SetingClass.gDir + "\\seting.json"));
                SetingClass.accessToken = serSetings.accessToken;
                SetingClass.clientToken = serSetings.clientToken;
                SetingClass.dopArguments = serSetings.dopArguments;
                SetingClass.forge = serSetings.forge;
                SetingClass.javaPath = serSetings.javaPath;
                SetingClass.oldBuild = serSetings.oldBuild;
                SetingClass.oldServer = serSetings.oldServer;
                SetingClass.shaders = serSetings.shaders;
                SetingClass.sram = serSetings.sram;
            }
            if (SetingClass.accessToken != "")
            {
                loginbox.Enabled = false;
                passbox.Enabled = false;
                authBotton.Enabled = false;
                regBotton.Enabled = false;
                progressBar1.Show();
                Refresh validateData = new Refresh();
                validateData.clientToken = SetingClass.clientToken;
                validateData.accessToken = SetingClass.accessToken;

                string validateSerData = jss.Serialize(validateData);
                string responseValidServer = POST(SetingClass.authLink + "refresh.php", validateSerData, "application/json");
                ResponeServer responseValidServerObj = jss.Deserialize<ResponeServer>(responseValidServer);
                if (responseValidServerObj.error != null)
                {
                    MessageBox.Show(responseValidServerObj.errorMessage, responseValidServerObj.error);
                    loginbox.Enabled = true;
                    passbox.Enabled = true;
                    authBotton.Enabled = true;
                    regBotton.Enabled = true;
                    progressBar1.Hide();
                    passbox.PasswordChar = '*';
                } else
                {
                    SetingClass.clientToken = responseValidServerObj.clientToken;
                    SetingClass.accessToken = responseValidServerObj.accessToken;
                    SetingClass.login = responseValidServerObj.selectedProfile.name;
                    SetingClass.uuid = responseValidServerObj.selectedProfile.id;
                    saveSetings();
                    Hide();
                    mainWindow.ShowDialog();
                    Close();
                }
                
            }
            else
            {
                passbox.PasswordChar = '*';
            }
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
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Point location = new Point(Cursor.Position.X, Cursor.Position.Y);
                this.Location = location;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

    public class INI
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public void IniWriteValue(string Section, string Key, string Value)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            if (!File.Exists(path))
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
    public static class SetingClass
    {
        public static string sram = "1G";
        public static string dopArguments = "";
        public static string javaPath;
        public static string clientToken;
        public static string accessToken;
        public static string login;
        public static string uuid;
        public static Build selectedBuild;
        public static Server selectedServer;
        public static string pDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string gDir = "SyabroCraft";
        public static Uri repoLink = new Uri("http://syabrocraft.xyz/wp-content/minecraft/");
        public static Uri authLink = new Uri("http://syabrocraft.xyz/wp-content/plugins/syabrocraft/");
        public static bool forge = true;
        public static string oldBuild;
        public static string oldServer;
        public static bool shaders = false;
    }
    [Serializable]
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
    }
    [Serializable]
    public class ResponeServer
    {
        public string accessToken;
        public string clientToken;
        public SelectedProfile selectedProfile;
        public string error;
        public string errorMessage;
    }
    [Serializable]
    public class SelectedProfile
    {
        public string id;
        public string name;
        public bool legacy;
    }
    [Serializable]
    public class User
    {
        public string username;
        public string password;
        public string clientToken;
    }
    [Serializable]
    public class Refresh
    {
        public string clientToken;
        public string accessToken;
    }
}
