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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = Form1.appconfig.AppSettings.Settings["ImgTime"].Value;
            textBox2.Text = Form1.appconfig.AppSettings.Settings["ImgStart"].Value;
            textBox3.Text = Form1.appconfig.AppSettings.Settings["ImgEnd"].Value;
            textBox4.Text = Form1.appconfig.AppSettings.Settings["FPGAStart"].Value;
            textBox5.Text = Form1.appconfig.AppSettings.Settings["UDPReturn"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.appconfig.AppSettings.Settings["ImgTime"].Value = textBox1.Text;
            Form1.appconfig.AppSettings.Settings["ImgStart"].Value = textBox2.Text;
            Form1.appconfig.AppSettings.Settings["ImgEnd"].Value = textBox3.Text;
            Form1.appconfig.AppSettings.Settings["FPGAStart"].Value = textBox4.Text;
            Form1.appconfig.AppSettings.Settings["UDPReturn"].Value = textBox5.Text;
            Form1.returnnum = int.Parse(textBox5.Text);
            Form1.time = int.Parse(textBox1.Text);
            Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }
    }
}
