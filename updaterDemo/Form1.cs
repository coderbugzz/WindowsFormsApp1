using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace updaterDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(@".\demoDeploy.exe")) //If file exists delete it
                {
                    File.Delete(@".\demoDeploy.exe");
                    File.Delete(@".\demoDeploy.exe.config");
                }

                if (File.Exists(@".\demoDeploy.zip"))
                {
                    File.Delete(@".\demoDeploy.zip");
                }



                var client = new WebClient();
                client.DownloadFile(new Uri("https://localhost:44394/Home/GetUpdateFile?systemID=1"), @".\demoDeploy.zip");

                
                string zipPath = @".\demoDeploy.zip";
                string extract_path = @".\";


                ZipFile.ExtractToDirectory(zipPath, extract_path);
                File.Delete(@".\demoDeploy.zip");
                Process.Start(@".\demoDeploy.exe");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("main catch " + ex.Message);

            }
        }
    }
}
