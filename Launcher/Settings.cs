using System;
using System.IO;
using System.Web.Script.Serialization;

namespace Launcher {
    public class Settings {
        // Конфигурации Java машины
        public string jRAM = "1G";
        public string jArg = "-XX:+UseConcMarkSweepGC -XX:+CMSIncrementalMode -XX:-UseAdaptiveSizePolicy -Xmn128M";
        public string jPath = "";
        public bool jShowClonsole = false;
        // Конфигурации запуска Minecraft
        public string mMainClass = "net.minecraft.launchwrapper.Launch";
        public bool mForge = true;
        // Конфигурации лаунчера
        public bool lBuildAutoSync = true;
        public bool lAutoClose = false;
        public bool lAutoLogin = false;
        public bool lNET = true;

        public const string site = "https://nm101.tk/_syabro_/";
        public const string lDir = "\\CraftUniverse\\";
        public string Dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public void Save()
        {
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            File.WriteAllText(Dir + lDir + "config.json", (new JavaScriptSerializer()).Serialize(this));
        }

        public void Load()
        {
            if (File.Exists(Dir + lDir + "Config.json")) {
                Settings _settings = (new JavaScriptSerializer()).Deserialize<Settings>(File.ReadAllText(Dir + lDir + "config.json"));
                this.jRAM = _settings.jRAM;
                this.jArg = _settings.jArg;
                this.jPath = _settings.jPath;
                this.jShowClonsole = _settings.jShowClonsole;
                this.mMainClass = _settings.mMainClass;
                this.mForge = _settings.mForge;
                this.lBuildAutoSync = _settings.lBuildAutoSync;
                this.lAutoClose = _settings.lAutoClose;
                this.lAutoLogin = _settings.lAutoLogin;
            }
        }

        public string GetBuildDir(string bName)
        {
            if (!Directory.Exists(Dir + lDir + $"builds\\{bName}\\"))
                Directory.CreateDirectory(Dir + lDir + $"builds\\{bName}\\");
            return Dir + lDir + $"builds\\{bName}\\";
        }

        public string GetBuildDir(Build b)
        {
            if (!Directory.Exists(Dir + lDir + $"builds\\{b.dir}\\"))
                Directory.CreateDirectory(Dir + lDir + $"builds\\{b.dir}\\");
            return Dir + lDir + $"builds\\{b.dir}\\";
        }

        public string GetGameEXEFile(string ver) { return Dir + lDir + $"versions\\{ver}\\minecraft.jar"; }

        public string GetGameDLLFiles(string ver) { return Dir + lDir + $"versions\\{ver}\\natives\\"; }

        public string GetGameEXEFile(Build b) { return Dir + lDir + $"versions\\{b.dir}\\minecraft.jar"; }

        public string GetGameDLLFiles(Build b) { return Dir + lDir + $"versions\\{b.dir}\\natives\\"; }

        public string GetLPath()
        {
            if (!Directory.Exists(Dir + lDir))
                Directory.CreateDirectory(Dir + lDir);
            return Dir + lDir;
        }
    }
}
