using System;
using System.Collections;
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
    public partial class Form2 : Form
    {
        Dictionary<double, double> dicPoints = new Dictionary<double, double>();
        ArrayList list = new ArrayList();
        public Form2()
        {
            InitializeComponent();
            this.ControlBox = false;
            list = Form1.list2;
            Load();
        }

        public new void Load()
        {
            Console.WriteLine(list[4]);
            listBox1.Items.Clear();
            int a = 0;
            double x = 0;
            double y = 0;
            label2.Text = list[1] + " X " + list[2];
            dicPoints = (Dictionary<double, double>)list[0];
            foreach (var item in dicPoints)
            {
                if (a == 0)
                {
                    x = item.Key;
                    y = item.Value;
                    a = 1;
                }
                else
                {
                    label5.Text = (int)x + " X " + (int)y;
                    label7.Text = (int)item.Key + " X " + (int)item.Value;
                }
            }
            for (int i = 0; i < Form1.classnamelist.Count; i++)
            {
                listBox1.Items.Add(Form1.classnamelist[i]);
            }
            if ((bool)list[3])
            {
                listBox1.SelectedIndex = Form1.classnamelist.IndexOf(Form1.classlist[(int)list[4]]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((bool)list[3])
            {
                if (Form1.maxbox)
                {
                    Form1.classlist[(int)list[4]] = listBox1.SelectedItem.ToString();
                    Form1.maxbox = false;
                }
                else
                {
                    Form1.classlist.RemoveAt((int)list[4]);
                    Form1.classlist.Add(listBox1.SelectedItem.ToString());
                    list[4] = Form1.classlist.Count - 1;
                }
                this.Close();
            }
            else
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    Form1.classlist.Add(listBox1.SelectedItem.ToString());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("未选择类别");
                }
            }
        }
        //添加类别
        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否确定删除该方框？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                if (Form1.maxbox)
                {
                    Form1.Pointlist.RemoveAt((int)list[4]);
                    Form1.classlist.RemoveAt((int)list[4]);
                    Form1.maxbox = false;
                }
                else
                {
                    if (Form1.Pointlist.Count == Form1.classlist.Count)
                    {
                        if (Form1.classname.Count() > 0) { 
                        Form1.classlist.RemoveAt((int)list[4]);
                            }
                    }
                    Form1.maxbox = false;
                    Form1.Pointlist.RemoveAt(Form1.Pointlist.Count - 1);
                }
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                if ((bool)list[3])
                {
                    if (Form1.maxbox)
                    {
                        Form1.maxbox = false;
                    }
                    else
                    {                       
                        Form1.classlist.Add(Form1.classlist[(int)list[4]]);
                        Form1.classlist.RemoveAt((int)list[4]);
                        list[4] = Form1.classlist.Count - 1;
                    }
                    this.Close();
                }
                else
                {
                    if (listBox1.SelectedIndex >= 0)
                    {
                        Form1.Pointlist.RemoveAt(Form1.Pointlist.Count-1);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择类别");
                    }
                }
            }
            else {
                if (Form1.maxbox)
                {
                    Form1.Pointlist.RemoveAt((int)list[4]);
                    Form1.classlist.RemoveAt((int)list[4]);
                    Form1.maxbox = false;
                }
                else
                {
                    if (Form1.Pointlist.Count == Form1.classlist.Count)
                    {
                        if (Form1.classname.Count() > 0)
                        {
                            Form1.classlist.RemoveAt((int)list[4]);
                        }
                    }
                    Form1.maxbox = false;
                    Form1.Pointlist.RemoveAt(Form1.Pointlist.Count - 1);
                }
                this.Close();
            }
        }
    }
}
