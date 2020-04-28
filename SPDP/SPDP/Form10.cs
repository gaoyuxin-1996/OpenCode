using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDP
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = Form1.appconfig.AppSettings.Settings["FPS"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.appconfig.AppSettings.Settings["FPS"].Value = textBox1.Text;
            Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            Form1.FPS = int.Parse(textBox1.Text);
            this.Close();
        }
    }
}
