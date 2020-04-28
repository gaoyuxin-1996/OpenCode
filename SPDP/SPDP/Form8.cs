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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            string str = Form1.appconfig.AppSettings.Settings["ClassName"].Value;
            char[] charTemp = { ',', '+'};
            string[] arr = str.Split(charTemp);
            Console.WriteLine(arr.Length);
            Form1.classlist.Clear();
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] == "")
                    break;
                Form1.classlist.Add(int.Parse(arr[i]), arr[i + 1]);
                i = i + 1;
            }
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            for (int i = 0; i < Form1.classlist.Count; i++)   //添加10行数据
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = i.ToString();

                lvi.SubItems.Add(Form1.classlist[i]);

                this.listView1.Items.Add(lvi);
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            for (int i = 0; i < Form1.classlist.Count; i++) {
                this.comboBox1.Items.Add(i);
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入信息！");
            }
            else
            {
                Form1.classlist.Add(Form1.classlist.Count(), this.textBox1.Text);
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

                for (int i = 0; i < Form1.classlist.Count; i++)   //添加10行数据
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                    lvi.Text = i.ToString();

                    lvi.SubItems.Add(Form1.classlist[i]);

                    this.listView1.Items.Add(lvi);
                }

                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
                this.comboBox1.Items.Clear();
                for (int i = 0; i < Form1.classlist.Count; i++)
                {
                    this.comboBox1.Items.Add(i);
                }
                string str = "";
                for (int i = 0; i < Form1.classlist.Count; i++)
                {
                    str = str + i + "+" + Form1.classlist[i] + ",";
                }
                Form1.appconfig.AppSettings.Settings["ClassName"].Value = str;
                Form1.appconfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.classlist[int.Parse(comboBox1.Text)] = this.textBox2.Text;
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

                for (int i = 0; i < Form1.classlist.Count; i++)   //添加10行数据
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                    lvi.Text = i.ToString();

                    lvi.SubItems.Add(Form1.classlist[i]);

                    this.listView1.Items.Add(lvi);
                }

                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
                string str = "";
                for (int i = 0; i < Form1.classlist.Count; i++)
                {
                    str = str + i + "+" + Form1.classlist[i] + ",";
                }
                Form1.appconfig.AppSettings.Settings["ClassName"].Value = str;
                Form1.appconfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(Form1.appconfig.AppSettings.SectionInformation.Name);
            }
            catch (Exception) { }
        }



        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = Form1.classlist[int.Parse(comboBox1.Text)];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
