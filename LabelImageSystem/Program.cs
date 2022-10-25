using System;
using System.Windows.Forms;
using Zach.Util;

namespace LabelImageSystem
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitConfig();
            Application.Run(new MainForm());
        }

        public static void InitConfig()
        {
            var configPath = AppDomain.CurrentDomain.BaseDirectory + "Config.json";
            var jsonStr = DirFileHelper.ReadAllText(configPath);
            var Config = jsonStr.ToObject<Config>();
            ConfigContext.shapeType = Config.shapeType;
            ConfigContext.LabelmeVersion = Config.LabelmeVersion;
            ConfigContext.file_attributes = Config.file_attributes;
            ConfigContext.coco = Config.coco.Replace("@debug/", AppDomain.CurrentDomain.BaseDirectory);
            ConfigContext.train = Config.train.Replace("@debug/", AppDomain.CurrentDomain.BaseDirectory);
            ConfigContext.export = Config.export.Replace("@debug/", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
