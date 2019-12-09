using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Variety;

namespace VarletUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindowLoaded;
        }

        private static void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(References.AppConfigFile)) return;
            var config = new Config
            {
                SelectedPhpVersion = "php-7.3-ts",
                LastUpdateCheck = DateTime.Now,
                CloseMinimizeToTray = false,
                Services = new string[] {"http"}
            };
            File.WriteAllText(
                References.AppConfigFile,
                JsonConvert.SerializeObject(config, Formatting.Indented)
            );
        }
    }
}
