﻿using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using OneStopModbus.Views;
using OneStopModbus.Helper;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OneStopModbus.Settings;

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

        public string name;

        #endregion Public Properties

        #region Private Properties

        private string aaaa;

        #endregion Private Properties

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            Log.Verbose("verbose logged");
            Log.Information("MainWindow Initialized");
            Log.Debug("MainWindow Initialized");
            Log.Warning("MainWindow Initialized");
            Log.Error("MainWindow Initialized");
            ConnectionSettings connectionSettings = new ConnectionSettings();
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
            Log.Logger.Information("Save As Clicked");
        }
    }
}