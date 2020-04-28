using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulation
{
    public partial class Form1 : Form
    {
        string path;
        string subPath;
        public static Dictionary<string, string> classlist = new Dictionary<string, string>();
        public static string form2class1;
        public static string form2class2;
        public static string form2classname;
        private Process _CMD;//cmd进程
        private Encoding _OutEncoding;//输出字符编码
        private Stream _OutStream;//基础输出流
        public event Action<string> Output;//输出事件
        private Queue<string> _Results;//输出结果队列
        private bool _Run;//循环控制
        private byte[] _TempBuffer;//临时缓冲


        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            _CMD = new Process();
            _CMD.StartInfo.FileName = "cmd.exe";
            _CMD.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            _CMD.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            _CMD.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            _CMD.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            _CMD.StartInfo.CreateNoWindow = true;//不显示程序窗口
            _Results = new Queue<string>();


            ReStart();
        }
        //打开文件夹并检索class文件
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            if (this.folderBrowserDialog1.SelectedPath != "")
            {
                path = this.folderBrowserDialog1.SelectedPath;
                foreach (var f in Directory.GetFiles(path, ".", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".gif")))
                {
                    this.listBox1.Items.Add(System.IO.Path.GetFileName(f));
                }
                subPath = path + "/NewPicture";
                if (false == System.IO.Directory.Exists(subPath))
                {
                    //该文件夹不存在
                    MessageBox.Show("警告，图像文件夹内不存在NewPicture文件夹，请手动选择！");
                    this.folderBrowserDialog1.ShowDialog();
                    if (this.folderBrowserDialog1.SelectedPath != "")
                    {
                        subPath = this.folderBrowserDialog1.SelectedPath;
                        if (!File.Exists(subPath + "\\Class.txt"))
                        {
                            MessageBox.Show("警告，文件夹内不存在Class.txt文件！请确保Class文件存在，并重新选择图像文件夹！");
                        }
                        else {
                            OpenClass();
                        }
                    }
                }
                else {
                    if (!File.Exists(subPath + "\\Class.txt"))
                    {
                        MessageBox.Show("警告，文件夹内不存在Class.txt文件！请确保Class文件存在，并重新选择图像文件夹！");
                    }
                    else {
                        OpenClass();
                    }
                }
            }
        }

        //打开Class文件
        public void OpenClass() {
            StreamReader sr = new StreamReader(subPath + "\\Class.txt", Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                string classstring = null;
                line=line.Replace(" ", ",");
                string[] sArray = Regex.Split(line.ToString(), ",", RegexOptions.IgnoreCase);
                for (int i=1;i<sArray.Count();i++) {
                    classstring = classstring + sArray[i]+",";
                }
                try
                {
                    classlist.Add(sArray[0], classstring);
                }
                catch (Exception) {
                    //MessageBox.Show("警告！Class文件内图片："+sArray[0]+"标签重复，请手动删除其中一条后重新点击打开文件按钮。");
                    form2classname = sArray[0];
                    form2class1 = classlist[form2classname];
                    form2class2 = classstring;
                    Form2 form = new Form2();
                    form.ShowDialog();
                }
            }
        }
        //选择图片，展示信息
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            int c = 0;
            try
            {
                if (classlist.ContainsKey(listBox1.SelectedItem.ToString()))
                {
                    string classstring = classlist[listBox1.SelectedItem.ToString()];
                    string[] sArray = Regex.Split(classstring, ",", RegexOptions.IgnoreCase);
                    string imgname = "像素：" + sArray[2] + " X " + sArray[3];
                    listBox2.Items.Add(imgname);
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
                            listBox2.Items.Add(imgname);
                            c = 0;
                        }
                        else if (c > 0)
                        {
                            imgname = imgname + sArray[i] + "，";
                            c = c + 1;
                        }
                    }
                }
                else
                {
                    listBox2.Items.Add("该图片没有添加标签");
                }
            }
            catch(Exception) {
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str = textBox4.Text;
            if (!_Run)
            {
                return;
            }
            _CMD.StandardInput.WriteLine(str);
        

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.button12_Click(sender, e);//触发button事件
                textBox4.Focus();
            }
        }
        /// <summary>
        /// 停止使用，关闭进程和循环线程
        /// </summary>
        public void Stop()
        {
            _Run = false;
            _Results.Clear();
            _CMD.Close();
        }

        /// <summary>
        /// 重新启用
        /// </summary>
        public void ReStart()
        {
            //Stop();

            _CMD.Start();
            _OutEncoding = _CMD.StandardOutput.CurrentEncoding;
            _OutStream = _CMD.StandardOutput.BaseStream;
            _Run = true;
            Thread thread = new Thread(Update)
            {
                IsBackground = true
            };
            thread.Start();
        }

        //循环读取输出结果
        private void Update()
        {
            while (_Run)
            {
                ReadResult();
                Console.WriteLine("1");
                if (_Results.Count > 0)
                {
                    Console.WriteLine("2");
                    richTextBox1.AppendText(_Results.Dequeue());
                    this.richTextBox1.Focus();//获取焦点
                    this.richTextBox1.Select(this.richTextBox1.TextLength, 0);//光标定位到文本最后
                    this.richTextBox1.ScrollToCaret();//滚动到光标处
                    Console.WriteLine("3");
                }
            }
        }



        //异步读取输出结果
        private void ReadResult()
        {
            try
            {
                Console.WriteLine("11");
                byte[] readBuffer = new byte[1024];
                _CMD.StandardInput.AutoFlush = true;
                _OutStream.BeginRead(readBuffer, 0, 1024, ReadEnd, readBuffer);
            }
            catch (Exception) {

            }
        }

        //一次异步读取结束
        private void ReadEnd(IAsyncResult ar)
        {
            int count = _OutStream.EndRead(ar);
            byte[] readBuffer = ar.AsyncState as byte[];

            if (count < 1)
            {
                return;
            }

            if (_TempBuffer == null)
            {
                _TempBuffer = new byte[count];
                Buffer.BlockCopy(readBuffer, 0, _TempBuffer, 0, count);
            }
            else
            {
                byte[] buff = _TempBuffer;
                _TempBuffer = new byte[buff.Length + count];
                Buffer.BlockCopy(buff, 0, _TempBuffer, 0, buff.Length);
                Buffer.BlockCopy(_TempBuffer, 0, _TempBuffer, buff.Length, count);
            }

            if (count < readBuffer.Length)
            {
                string str = _OutEncoding.GetString(_TempBuffer);
                _Results.Enqueue(str);
                _TempBuffer = null;
            }
        }

        ~Form1()
        {
            _Run = false;
            _CMD?.Close();
            _CMD?.Dispose();
        }
 


    }
}
