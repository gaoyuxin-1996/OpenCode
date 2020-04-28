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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = Form1.appconfig.AppSettings.Settings["IP"].Value;
            textBox2.Text = Form1.appconfig.AppSettings.Settings["Port"].Value;
            textBox3.Text = Form1.appconfig.AppSettings.Settings["UDPNum"].Value;
            textBox4.Text = Form1.appconfig.AppSettings.Settings["IP1"].Value;
            textBox5.Text = Form1.appconfig.AppSettings.Settings["Port1"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.appconfig.AppSettings.Settings["IP"].Value = textBox1.Text;
            Form1.appconfig.AppSettings.Settings["Port"].Value = textBox2.Text;
            Form1.appconfig.AppSettings.Settings["UDPNum"].Value = textBox3.Text;
            Form1.appconfig.AppSettings.Settings["IP1"].Value = textBox4.Text;
            Form1.appconfig.AppSettings.Settings["Port1"].Value = textBox5.Text;
            Form1.udpnum = int.Parse(textBox3.Text);
            Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }
    }
}
