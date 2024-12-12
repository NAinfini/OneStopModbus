using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Helper
{
    public static class FileHelpers
    {
        public static string MainFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\OneStopModbus";
        public static string LogFolderPath = $"{MainFolderPath}\\Logs";
    }
}