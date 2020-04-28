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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");
            comboBox1.Text = Form1.threadnum.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form1.appconfig.AppSettings.Settings["ThreadNum"].Value = comboBox1.Text;
            //Form1.appconfig.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            Form1.threadnum = int.Parse(comboBox1.Text);
            this.Close();
        }
    }
}
