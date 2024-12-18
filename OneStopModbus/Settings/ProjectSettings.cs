using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using OneStopModbus.Views;
using Serilog;

namespace OneStopModbus.Settings
{
    public class ProjectSettings
    {
        public static ProjectSettings Instance { get; set; } = new ProjectSettings();
        public string ProjectName { get; set; }
        public ConnectionSettings ConnectionSettings { get; set; }
        public DateTime LastModified { get; set; }
        public string ProjectFilePath { get; set; }
        public delegate void ProjectUpdateHandler(object sender, EventArgs e);

        
        public event ProjectUpdateHandler ProjectSettingUpdated;

        protected virtual void OnSettingUpdate()
        {
            ProjectSettingUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void LoadProjectFromJson(string JsonFilePath)
        {
            Instance = JsonConvert.DeserializeObject<ProjectSettings>(File.ReadAllText(JsonFilePath));
            OnSettingUpdate();
        }

        public void SaveAsProjectToJson()
        {
            SaveAsView saveAsView = new SaveAsView();
            saveAsView.ShowDialog();
            if (saveAsView.DialogResult == true)
            {
                ProjectFilePath = saveAsView.FilePath;
                ProjectName = saveAsView.FileName;
            }
            string filePath = ProjectFilePath + "\\" + ProjectName + ".json";
            if (Path.Exists(filePath))
            {
                MessageBoxResult res = MessageBox.Show("File already exists. Do you wish to overwrite the existing file?", "Overwrite", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    LastModified = DateTime.Now;
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(Instance, Formatting.Indented));
                }
            }
            else
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(Instance, Formatting.Indented));
            }

        }

        public void SaveProjectToJson()
        {
            if (string.IsNullOrEmpty(ProjectFilePath) || string.IsNullOrEmpty(ProjectName))
            {
                SaveAsProjectToJson();
                return;
            }
            string filePath = ProjectFilePath + "\\" + ProjectName + ".json";
            LastModified = DateTime.Now;
            File.WriteAllText(filePath, JsonConvert.SerializeObject(Instance, Formatting.Indented));
        }
    }
}