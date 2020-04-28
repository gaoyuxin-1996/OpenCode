using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulation
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Load();
        }

        private new void Load(){
            label3.Text = Form1.form2classname;
            int c = 0;
            string[] sArray = Regex.Split(Form1.form2class1, ",", RegexOptions.IgnoreCase);
            string imgname = "像素：" + sArray[2] + " X " + sArray[3];
            listBox1.Items.Add(imgname);
            for (int i = 4; i < sArray.Count(); i++)
            {
                if (c == 0)
                {
                    imgname = null;
                    imgname = imgname + sArray[i] + ":  ";
                    c = 1;
                }
                else if (c == 4)
                {
                    imgname = imgname + sArray[i];
                    listBox1.Items.Add(imgname);
                    c = 0;
                }
                else if (c > 0)
                {
                    imgname = imgname + sArray[i] + "，";
                    c = c + 1;
                }
            }
            c = 0;
            string[] sArray1 = Regex.Split(Form1.form2class2, ",", RegexOptions.IgnoreCase);
            string imgname1 = "像素：" + sArray1[2] + " X " + sArray1[3];
            listBox2.Items.Add(imgname1);
            for (int i = 4; i < sArray1.Count(); i++)
            {
                if (c == 0)
                {
                    imgname1 = null;
                    imgname1 = imgname1 + sArray1[i] + ":  ";
                    c = 1;
                }
                else if (c == 4)
                {
                    imgname1 = imgname1 + sArray1[i];
                    listBox2.Items.Add(imgname1);
                    c = 0;
                }
                else if (c > 0)
                {
                    imgname1 = imgname1 + sArray1[i] + "，";
                    c = c + 1;
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1.classlist.Remove(Form1.form2classname);
            Form1.classlist.Add(Form1.form2classname, Form1.form2class2);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
