using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneStopModbus.Settings
{
    public class ProjectSettings
    {
        public static ProjectSettings Instance { get; set; } = new ProjectSettings();
        public string ProjectName { get; set; }
        public ConnectionSettings ConnectionSettings { get; set; } = new ConnectionSettings();
        public DateTime LastModified { get; set; }
        public string ProjectFilePath { get; set; }

        public static void LoadProjectFromJson(string JsonFilePath)
        {
            Instance = JsonConvert.DeserializeObject<ProjectSettings>(File.ReadAllText(JsonFilePath));
        }

        public static void SaveProjectToJson(string JsonFilePath)
        {
            File.WriteAllText(JsonFilePath, JsonConvert.SerializeObject(Instance, Formatting.Indented));
        }
    }
}