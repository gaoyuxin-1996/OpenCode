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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = Form1.appconfig.AppSettings.Settings["PYPath"].Value;
            textBox2.Text = Form1.appconfig.AppSettings.Settings["PYimgpath"].Value;
            textBox3.Text = Form1.appconfig.AppSettings.Settings["PYimgname"].Value;
            textBox4.Text = Form1.appconfig.AppSettings.Settings["PYName"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1.appconfig.AppSettings.Settings["PYPath"].Value = textBox1.Text;
            Form1.appconfig.AppSettings.Settings["PYimgpath"].Value = textBox2.Text;
            Form1.appconfig.AppSettings.Settings["PYimgname"].Value = textBox3.Text;
            Form1.appconfig.AppSettings.Settings["PYName"].Value = textBox4.Text;
            Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            char[] charTemp = { '\\'};
            string[] pypath = textBox1.Text.Split(charTemp);
            string[] pypath1 = textBox2.Text.Split(charTemp);
            string str = "python ";
            foreach (string i in pypath) {
                str = str + i + "/";
            }
            Form1.Cmdstr = str + textBox4.Text + " ";
            str = "";
            foreach (string i in pypath1)
            {
                str = str + i + "/";
            }
            Form1.Cmdstr1 = str + " " + textBox3.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox4.Text = openFileDialog1.SafeFileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox2.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
