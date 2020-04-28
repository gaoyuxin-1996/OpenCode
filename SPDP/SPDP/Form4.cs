using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDP
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.Text = Form1.appconfig.AppSettings.Settings["Height"].Value;
            textBox2.Text = Form1.appconfig.AppSettings.Settings["Width"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                Form1.appconfig.AppSettings.Settings["Height"].Value = textBox1.Text;
                Form1.appconfig.AppSettings.Settings["Width"].Value = textBox2.Text;
                Form1.appconfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
                Form1.width= int.Parse(textBox2.Text);
                Form1.height= int.Parse(textBox1.Text);
            this.Close();

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
