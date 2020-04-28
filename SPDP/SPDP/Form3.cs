using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDP
{
    public partial class Form3 : Form
    {
        string path;
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            path = Form1.appconfig.AppSettings.Settings["SystemImagePath"].Value;
            textBox1.Text = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            path = folderBrowserDialog1.SelectedPath.ToString();
            textBox1.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.appconfig.AppSettings.Settings["SystemImagePath"].Value = path;
            Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
