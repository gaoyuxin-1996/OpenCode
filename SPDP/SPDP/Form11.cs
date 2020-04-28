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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = Form1.appconfig.AppSettings.Settings["DLLReturnNum"].Value;
            textBox2.Text = Form1.appconfig.AppSettings.Settings["ShowNum"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.appconfig.AppSettings.Settings["DLLReturnNum"].Value = textBox1.Text;
            Form1.appconfig.AppSettings.Settings["ShowNum"].Value=textBox2.Text;
            Form1.Dllnum = int.Parse(textBox1.Text);
            Form1.accuracy = float.Parse(textBox2.Text);
            Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }
    }
}
