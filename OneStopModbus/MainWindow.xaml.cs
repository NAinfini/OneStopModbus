using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using OneStopModbus.Views;
using OneStopModbus.Helper;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using OneStopModbus.Settings;
using System.Windows.Forms;
using DevExpress.Xpf.Dialogs;
using DevExpress.Utils.MVVM.Services;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System.IO;

namespace OneStopModbus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow, INotifyPropertyChanged
    {
        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged

        #region Public Properties

        public LayoutPanel LWindowPanel { get; set; }
        public LayoutPanel DeviceTreePanel { get; set; }
        public LayoutPanel ConnectionSettingPanel { get; set; }

        #endregion Public Properties

        #region Private Properties

        private string aaaa;

        #endregion Private Properties

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            LWindowPanel = this.LogWindowPanel;
            DeviceTreePanel = this.ItemTreePanel;
            ConnectionSettingPanel = this.CSWindowPanel;
            Log.Information("MainWindow Initialized");
        }

        private void AddFloatingLayoutPanel()
        {
            // Create a new LayoutPanel
            LayoutPanel floatingPanel = new LayoutPanel
            {
                Caption = "Floating Panel", // Set the panel caption
                Content = new LogWindow()
            };
            FloatGroup floatGroup = new FloatGroup();
            floatGroup.Add(floatingPanel);
            MainDockLyoutManager.FloatGroups.Add(floatGroup);
        }

        private void saveAsItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                ProjectSettings.Instance.SaveAsProjectToJson();
                Log.Information($"Project saved:{ProjectSettings.Instance.ProjectName}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error saving project");
            }
        }

        private void openItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Json files (*.json)|*.json|All files (*.*)|*.*",
                    RestoreDirectory = true,
                    Title = "Open Project",
                };
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProjectSettings.Instance.LoadProjectFromJson(openFileDialog.FileName);
                    Log.Information($"Project opened:{ProjectSettings.Instance.ProjectName}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error opening project");
            }
        }

        private void SaveItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                ProjectSettings.Instance.SaveProjectToJson();
                Log.Information($"Project saved:{ProjectSettings.Instance.ProjectName}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error saving project");
            }
        }

        private void newItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                ProjectSettings.Instance = new ProjectSettings();
                Log.Information("New project created");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating new project");
            }
        }
    }
}