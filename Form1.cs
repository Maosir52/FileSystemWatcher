using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FolderWatcher1
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher watcher;
        int a = 0;
        public Form1()
        {
            InitializeComponent();
            InitializeWatcher();
        }

        private void InitializeWatcher()
        {
            string folderpath = @"F:\watcherFolder\test"; 
            textBox2.Text = folderpath;
            watcher = new FileSystemWatcher();
            watcher.Path = folderpath;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            string selectedFolderPath = fbd.SelectedPath;
            textBox2.Text = selectedFolderPath;
            watcher.Path = selectedFolderPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (a == 0)
            {
                watcher.EnableRaisingEvents = true;
                a++;
                label1.Text = "当前监听状态：启用";
            }
            else
            {
                watcher.EnableRaisingEvents = false;
                a--;
                label1.Text = "当前监听状态：禁用";
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText($"Changed: {e.FullPath}\n");
            });
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText($"添加文件: {e.FullPath}\n");
                string content = File.ReadAllText(e.FullPath);
                richTextBox1.AppendText($"文件内容:\n{content}\n");
            });
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText($"Deleted: {e.FullPath}\n");
            });
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText($"Renamed: {e.OldFullPath} to {e.FullPath}\n");
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
