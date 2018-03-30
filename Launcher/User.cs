using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows;

namespace Launcher
{
    public class User : ServerError
    {
        Settings settings = AuthWindow.settings;
        WebClient webClient = new WebClient();
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public string username;
        public string password;
        public Guid clientToken;
        public Guid accessToken;
        public Guid uuid;

        public User(string uName, string uPassword, bool nUser = false)
        {
            if (File.Exists(userFile) && settings.lAutoLogin && !nUser)
            {
                User _user = jss.Deserialize<User>(File.ReadAllText(userFile));
                username = _user.username;
                password = _user.password;
                clientToken = _user.clientToken;
                accessToken = _user.accessToken;
                uuid = _user.uuid;
                request(nUser);
            } else if (uName != "" && uPassword != "") {
                username = uName;
                password = uPassword.ToString();
                clientToken = Guid.NewGuid();
                accessToken = Guid.NewGuid();
                uuid = Guid.NewGuid();
                request(nUser);
            } else MessageBox.Show("Пожалуйста, введите логин и пароль.");
        }

        void request(bool nUser)
        {
            try
            {
                string script;
                if (nUser) { script = Settings.site + "register.php"; }
                else { script = Settings.site + "auth.php"; }
                User _user = jss.Deserialize<User>(webClient.UploadString(script, jss.Serialize(this)));
                if (_user.error == null)
                {
                    username = _user.username;
                    password = _user.password;
                    clientToken = _user.clientToken;
                    accessToken = _user.accessToken;
                    uuid = _user.uuid;

                    if (settings.lAutoLogin)
                        File.WriteAllText(userFile, jss.Serialize(this));
                }
                else
                {
                    MessageBox.Show(_user.errorMessage, _user.error);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout) settings.lNET = false;
                else MessageBox.Show(ex.Message, "Не ебу, че за ошибка..");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Не ебу, че за ошибка.."); }
        }

        public User(bool check)
        {
            if (File.Exists(userFile))
            {
                User _user = jss.Deserialize<User>(File.ReadAllText(userFile));
                username = _user.username;
                password = _user.password;
                clientToken = _user.clientToken;
                accessToken = _user.accessToken;
                uuid = _user.uuid;
                request(false);
            }
        }

        public User() { }

        static string userFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +  Settings.lDir + "UserData.json";
    }
}
