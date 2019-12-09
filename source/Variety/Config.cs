using System;

namespace Variety
{
    public class Config
    {
        public string SelectedPhpVersion { get; set; }
        public DateTime LastUpdateCheck { get; set; }
        public bool CloseMinimizeToTray { get; set; }
        public string[] Services { get; set; }
    }
}
