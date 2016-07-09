using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServer
{
    
    public partial class Form1 : Form
    {
        TCPServer server;
        public delegate void InvokeDelegate(string text);

        public Form1()
        {
            InitializeComponent();
            gui_dir.Text = Directory.GetCurrentDirectory() + "/www";
            gui_dir.Text = Regex.Replace(gui_dir.Text , @"\\", @"/");
        }

        private void InitializeWebServer()
        {                                                          
            int port = Convert.ToInt32(gui_port.Value);
            server = new TCPServer(port, this);            
        }

        private void StopWebServer()
        {
            server.TryToStop();
        }
        
        public void UpdateLog(string text)
        {
            gui_log.AppendText(text + System.Environment.NewLine);
        }

        /* Button handlers: */

        private void gui_run_Click(object sender, EventArgs e)
        {
            InitializeWebServer();

            gui_run.Visible = false;
            gui_stop.Visible = true;
            gui_port.Enabled = false;
            gui_dir.Enabled = false;
            gui_choosedir.Enabled = false;
        }

        private void gui_stop_Click(object sender, EventArgs e)
        {
            if (server != null)
                StopWebServer();

            gui_run.Visible = true;
            gui_stop.Visible = false;
            gui_port.Enabled = true;
            gui_dir.Enabled = true;
            gui_choosedir.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
                StopWebServer();
        }

        private void gui_choosedir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                //string[] files = Directory.GetFiles(fbd.SelectedPath);
                //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                gui_dir.Text = fbd.SelectedPath;
                gui_dir.Text = Regex.Replace(gui_dir.Text, @"\\", @"/");

            }
        }

        private void gui_dir_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
