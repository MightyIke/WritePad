﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WritePad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string Version = "0.1 Prerelease";
        public string FileName { get; set; } = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateStatusBarCharacterLength()
        {
            StatusBarRight.Content = $"Characters: {Editor.Text.Length}";
        }

        private void Editor_KeyUp(object sender, KeyEventArgs e)
        {
            this.UpdateStatusBarCharacterLength();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateStatusBarCharacterLength();
            StatusBarLeft.Content = $"Version: {this.Version}";
            this.SetTitle("Untitled");
        }
        private void SetTitle(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                this.Title = $"WritePad {Version}";
            }
            else
            {
                this.Title = $"WritePad {Version} - {text}";
            }
        }

        private void MenuItemOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //This Code Opens a File
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "Open File";
            dialog.CheckFileExists = true;
            dialog.Filter = "*.txt|*.txt|*.*|*.*";
            dialog.Multiselect = false;
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            Editor.Text = System.IO.File.ReadAllText(dialog.FileName);
            this.FileName = dialog.FileName;
            this.SetTitle(Argus.IO.FileSystemUtilities.ExtractFileName(dialog.FileName));
            this.StatusBarLeft.Content = dialog.FileName;
        }

        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            //This Code Saves a File
            var dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Title = "Save As";
            dialog.OverwritePrompt = true;
            dialog.Filter = ".txt|.txt|.|.";

            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            System.IO.File.WriteAllText(dialog.FileName, Editor.Text);
        }
    }
}
