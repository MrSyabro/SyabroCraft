using Launcher.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public static User userData;
        WebClient webClient = new WebClient();
        MainWindow mainWindow = new MainWindow();
        public static Settings settings = new Settings();

        public AuthWindow()
        {
            InitializeComponent();
            settings.Load();
            AECheckBox.IsChecked = settings.lAutoLogin;
            if (settings.lAutoLogin)
            {
                userData = new User(true);
                mainWindow.Show();
                Close();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            settings.lAutoLogin = AECheckBox.IsChecked.Value;
            userData = new User(loginBox.Text, passwordBox.Password);
            settings.Save();
            mainWindow.Show();
            Close();
        }

        private void loginBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (loginBox.Text == "Login")
                loginBox.Text = "";
        }
        private void passwordBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (passwordBox.Password == "Password")
                passwordBox.Password = "";
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            settings.lAutoLogin = AECheckBox.IsChecked.Value;
            userData = new User(loginBox.Text, passwordBox.Password, true);
            mainWindow.Show();
            Close();
        }
    }
}
