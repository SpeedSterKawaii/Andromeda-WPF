using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Andromeda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LuaEditor.Options.EnableHyperlinks = false;
            LuaEditor.Options.EnableEmailHyperlinks = false;
            LuaEditor.Options.AllowScrollBelowDocument = false;

            using (var streamReader = new StreamReader("lua.xshd"))
            {
                using (var reader = new XmlTextReader(streamReader))
                {
                    LuaEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }

        private void Top_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void ExitBtn_Click(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);

        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void AttachBtn_Click(object sender, RoutedEventArgs e)
        {
            ea.Attach();

        }
        private void SrgsBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings lol = new Settings();
            lol.Show();
        }

        private void ExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            ea.Execute(LuaEditor.Text, LuaEditor.Text, LuaEditor.Text);
        }

        andromeda ea = new andromeda();

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opendialogfile = new OpenFileDialog();
            opendialogfile.Filter = "Lua File (*.lua)|*.lua|Text File (*.txt)|*.txt";
            opendialogfile.FilterIndex = 2;
            opendialogfile.RestoreDirectory = true;
            if (opendialogfile.ShowDialog() != DialogResult)
                return;
            try
            {
                LuaEditor.Text = "";
                System.IO.Stream stream;
                if ((stream = opendialogfile.OpenFile()) == null)
                    return;
                using (stream)
                    this.LuaEditor.Text = System.IO.File.ReadAllText(opendialogfile.FileName);
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult)
            {
                Stream s = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(s);
                sw.Write(LuaEditor.Text);
                sw.Close();
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            LuaEditor.Text = "";
        }

        private void CloseRBLXBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "RobloxPlayerBeta")
                {
                    process.Kill();
                }
            }
        }

        private void LuaEditor_TextChanged(object sender, EventArgs e)
        {
            int count = LuaEditor.Text.Length;
        }
    }
}

















































