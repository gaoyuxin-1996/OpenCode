using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLabel
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Load();
        }

        public new void Load()
        {
            listBox1.Items.Clear();
            foreach (var item in Form1.classnamelist)
            {
                listBox1.Items.Add(item);
            }
        }
        //添加类别
        private void button1_Click(object sender, EventArgs e)
        {
            string ExrPath = System.Windows.Forms.Application.StartupPath;
            if (textBox1.Text != "")
            {
                if (Form1.classnamelist.IndexOf(textBox1.Text) >= 0)
                {
                    MessageBox.Show("该类别重复！");
                }
                else
                {
                    try
                    {
                        Form1.classname = Form1.classname + textBox1.Text + "oi,oi";
                        Form1.appconfig.AppSettings.Settings["SystemSettingClass"].Value = Form1.classname;
                        Form1.appconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
                        Form1.classnamelist.Add(textBox1.Text);
                    }
                    catch (Exception) { }
                }
                Load();

            }
            else
            {
                MessageBox.Show("请输入类别");
            }
        }

        //删除
        private void button2_Click(object sender, EventArgs e)
        {
            string ExrPath = System.Windows.Forms.Application.StartupPath;
            Form1.classname = "SystemSettingClass";
            if (listBox1.SelectedIndex >= 0)
            {
                Form1.classnamelist.Remove(listBox1.SelectedItem.ToString());
                try
                {
                    string str = "";
                    for (int i=0;i<Form1.classnamelist.Count;i++) {
                        str=str+Form1.classnamelist[i]+ "oi,oi";
                    }
                    Form1.appconfig.AppSettings.Settings["SystemSettingClass"].Value = str;
                    Form1.appconfig.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
                    Form1.classname = str;
                }
                catch (Exception)
                {
                }
                Load();

            }
            else
            {
                MessageBox.Show("请选择类别！");
            }
        }
    }
}
