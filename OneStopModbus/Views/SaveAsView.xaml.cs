using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.DirectX.Common.DXGI;
using DevExpress.Xpf.Core;

namespace OneStopModbus.Views
{
    /// <summary>
    /// Interaction logic for SaveAsView.xaml
    /// </summary>
    public partial class SaveAsView : ThemedWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        private string _FileName = "Project";

        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                _FilePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private string _FilePath;

        public bool SaveBtnEnabled
        {
            get
            {
                return _savebtnEnabled;
            }
            set
            {
                _savebtnEnabled = value;
                OnPropertyChanged(nameof(SaveBtnEnabled));
            }
        }

        private bool _savebtnEnabled;

        public SaveAsView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
            };
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePath = folderBrowserDialog.SelectedPath;
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ValidityCheck(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                SaveBtnEnabled = false;
                return;
            }
            if (string.IsNullOrEmpty(FileName) || FileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                SaveBtnEnabled = false;
                return;
            }
        }
    }
}