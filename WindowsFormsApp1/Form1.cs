using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WebClient webClient = new WebClient();
            try
            {
                string currentVersion = CurrentVersion;
                var response = webClient.DownloadString("https://localhost:44394/Home/checkupdate?version=" + currentVersion + "&systemID=1");

                Response<Installer> resp = JsonConvert.DeserializeObject<Response<Installer>>(response);

                if (resp.Code == 200)
                {
                    if (MessageBox.Show("Would you like to update?", resp.message, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Process.Start("updaterDemo.exe");
                            
                            this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cur_version = CurrentVersion;
            label2.Text = "Version" + cur_version;
        }

        public string CurrentVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }

    public class Installer
    {
        public int ID { get; set; }
        public string System { get; set; }
        public string cur_version { get; set; }
        public string location { get; set; }
        public DateTime date { get; set; }
    }

    public class Response<T>
    {
        public int Code { get; set; }
        public string message { get; set; }
        public T Data { get; set; }
    }
}
