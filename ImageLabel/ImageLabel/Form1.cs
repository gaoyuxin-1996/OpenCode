using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ImageLabel
{
    public partial class Form1 : Form
    {
        //系统配置
        public static Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static string classname = "";
        //图像名称
        ArrayList list = new ArrayList();
        //文件内容
        ArrayList config = new ArrayList();
        //类别名称
        public static ArrayList classnamelist = new ArrayList();
        //方框类别
        public static ArrayList classlist = new ArrayList();
        //方框
        public static ArrayList Pointlist = new ArrayList();
        bool bDrawStart = false;
        Point pointStart = Point.Empty;
        Point pointContinue = Point.Empty;
        public static string path = "";
        public static string subPath = "";
        public static string name = "";
        //定义获取图片像素
        double width;
        double height;
        //获取图像本来的像素
        int TrueWidth;
        int TrueHeight;
        //EXE程序地址
        string ExrPath;
        bool mouse;
        //平移
        double moveX;
        double moveY;
        //小方框
        int id = 0;
        int LOR = 0;
        public static bool smallbox = false;
        //FORM2
        public static ArrayList list2 = new ArrayList();
        //大方框
        public static bool maxbox = false;
        //编辑
        bool edit = false;



        public Form1()
        {
            InitializeComponent();
            Load();
        }


        //初始化加载
        public new void Load()
        {
            button5.BackColor = Color.Green;
            button6.BackColor = Color.Red;
            width = this.pictureBox1.Width;
            height = this.pictureBox1.Height;
            ExrPath = System.Windows.Forms.Application.StartupPath;
            classnamelist.Clear();
            path = appconfig.AppSettings.Settings["SystemSettingPath"].Value;
            if (!System.IO.Directory.Exists(@path))
            {
                path = "";
            }
            name = appconfig.AppSettings.Settings["SystemSettingPicture"].Value;
            string[] sArray = Regex.Split(appconfig.AppSettings.Settings["SystemSettingClass"].Value, "oi,oi", RegexOptions.IgnoreCase);
            foreach (string i in sArray)
            {
                if (i != "")
                {
                    classnamelist.Add(i);
                    classname = classname + i + "oi,oi";
                }
            }
            if (path == "")
            {
                path = ExrPath;
                try
                {
                    appconfig.AppSettings.Settings["SystemSettingPath"].Value = path;
                    appconfig.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                }
                catch (Exception) { }
            }
            else
            {
                openfile(1);
                openimg(1);
            }


        }

        //打开文件
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openfile(0);

        }
        //打开文件方法
        public void openfile(int e)
        {
            this.listBox1.Items.Clear();
            if (e == 0)
            {
                list.Clear();
                this.folderBrowserDialog1.ShowDialog();
                Console.WriteLine(this.folderBrowserDialog1.SelectedPath);
                if (this.folderBrowserDialog1.SelectedPath != "")
                {
                    path = this.folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    return;
                }
            }
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (var f in Directory.GetFiles(path, ".", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".gif")))
            {
                this.listBox1.Items.Add(System.IO.Path.GetFileName(f));
                list.Add(System.IO.Path.GetFileName(f));
            }
            subPath = path + "/NewPicture";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }
            if (e == 1)
            {
                listBox1.SelectedIndex = list.IndexOf(name);
                this.pictureBox1.ImageLocation = path + "\\" + name;
                if (list.Count > 0)
                {
                    Bitmap pic = new Bitmap(path + "\\" + name);
                    TrueWidth = pic.Size.Width;
                    TrueHeight = pic.Size.Height;
                    this.pictureBox1.Size = new System.Drawing.Size((int)(TrueWidth), (int)(TrueHeight));
                    width = this.pictureBox1.Width;
                    height = this.pictureBox1.Height;
                }
            }
            else
            {
                if (list.Count > 0)
                {
                    name = list[0].ToString();
                    listBox1.SelectedIndex = 0;
                    this.pictureBox1.ImageLocation = path + "\\" + name;
                    Bitmap pic = new Bitmap(path + "\\" + name);
                    TrueWidth = pic.Size.Width;
                    TrueHeight = pic.Size.Height;
                    this.pictureBox1.Size = new System.Drawing.Size((int)(TrueWidth), (int)(TrueHeight));
                    width = this.pictureBox1.Width;
                    height = this.pictureBox1.Height;
                }
                else
                {
                    name = "";
                }
            }
            if (!File.Exists(subPath + "\\Class.txt"))
            {
                FileStream fs = new FileStream(subPath + "\\Class.txt", FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
                MessageBox.Show("注意！添加方框的图像及坐标类别文件Class.txt将存放于图像文件夹中的NewPicture文件夹中");
            }
            Imgid();

        }

        //动态化控件
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            this.panel1.Size = new System.Drawing.Size((int)(this.Width * 0.67), (int)(this.Height - 100));
            //this.pictureBox1.Size = new System.Drawing.Size((int)(this.Width * 0.67), (int)(this.Height * 0.67));
            this.groupBox2.Size = new System.Drawing.Size((int)(this.Width * 0.27), (int)(this.Height - 100));
            this.groupBox2.Location = new System.Drawing.Point((int)(this.Width * 0.70), 42);
            this.listBox1.Size = new System.Drawing.Size((int)(this.groupBox2.Width * 0.9), (int)(this.groupBox2.Height * 0.3));
            this.button1.Location = new System.Drawing.Point(this.button1.Location.X, (int)(this.listBox1.Location.Y + this.listBox1.Height + 6));
            this.button2.Location = new System.Drawing.Point((int)(this.listBox1.Location.X + this.listBox1.Width - this.button2.Width), (int)(this.listBox1.Location.Y + this.listBox1.Height + 6));
            this.button3.Location = new System.Drawing.Point(this.button3.Location.X, (int)(this.button1.Location.Y + this.button1.Height + 6));
            this.button4.Location = new System.Drawing.Point((int)(this.listBox1.Location.X + this.listBox1.Width - this.button2.Width), (int)(this.button1.Location.Y + this.button1.Height + 6));
            this.button5.Location = new System.Drawing.Point(this.button5.Location.X, (int)(this.button3.Location.Y + this.button3.Height + 6));
            this.button6.Location = new System.Drawing.Point((int)(this.listBox1.Location.X + this.listBox1.Width - this.button2.Width), (int)(this.button3.Location.Y + this.button3.Height + 6));
            this.button7.Location = new System.Drawing.Point(this.button7.Location.X, (int)(this.button5.Location.Y + this.button5.Height + 6));
            this.button7.Size = new System.Drawing.Size(this.listBox1.Width, this.button7.Height);
            this.listBox2.Location = new System.Drawing.Point(this.button7.Location.X, (int)(this.button7.Location.Y + this.button7.Height + 10));
            this.listBox2.Size = new System.Drawing.Size((int)(this.groupBox2.Width * 0.9), (int)(this.groupBox2.Height * 0.3));

        }
        //图片扩大与缩小
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            int a = 0;
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            if (e.Delta > 0 && ((int)(width / 1.2) < 10 || (int)(height / 1.2) < 10))
            {
                toolStripStatusLabel1.Text = "无法更加缩小";
                return;
            }
            else
            {
                if (e.Delta > 0)
                {
                    //计算缩放大小
                    width = width / 1.2;
                    height = height / 1.2;
                    this.pictureBox1.Size = new System.Drawing.Size((int)(width), (int)(height));
                    for (int i = 0; i < Pointlist.Count; i++)
                    {
                        dicPoints = (Dictionary<double, double>)Pointlist[i];
                        Dictionary<double, double> dicPoints1 = new Dictionary<double, double>();
                        foreach (var item in dicPoints)
                        {
                            Console.WriteLine(item.Key + "........" + item.Value);
                            dicPoints1.Add(item.Key / 1.2000, item.Value / 1.2000);
                        }
                        Pointlist[i] = dicPoints1;
                    }

                }
                else
                {
                    width = width * 1.2;
                    height = height * 1.2;
                    this.pictureBox1.Size = new System.Drawing.Size((int)(width), (int)(height));
                    for (int i = 0; i < Pointlist.Count; i++)
                    {
                        dicPoints = (Dictionary<double, double>)Pointlist[i];
                        Dictionary<double, double> dicPoints1 = new Dictionary<double, double>();
                        foreach (var item in dicPoints)
                        {
                            Console.WriteLine(item.Key + "........" + item.Value);
                            dicPoints1.Add(item.Key * 1.2000, item.Value * 1.2000);
                        }
                        Pointlist[i] = dicPoints1;
                    }
                }
            }
        }
        //焦点处理
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
            Refresh();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            int c = 0;
            double x = 0;
            double y = 0;
            int o = 0;
            if (e.Button == MouseButtons.Left)
            {
                if (edit)
                {
                    for (int i = 0; i < Pointlist.Count; i++)
                    {
                        if (o == 1)
                        {
                            break;
                        }
                        dicPoints = (Dictionary<double, double>)Pointlist[i];
                        foreach (var a in dicPoints)
                        {
                            if (c == 0)
                            {
                                x = a.Key;
                                y = a.Value;
                                c = 1;
                            }
                            else
                            {
                                if (e.Location.X > x && e.Location.X < a.Key && e.Location.Y > y && e.Location.Y < a.Value)
                                {
                                    Console.WriteLine("this");
                                    id = i;
                                    smallbox = true;
                                    Form2info(dicPoints);
                                    o = 1;
                                    smallbox = false;
                                    maxbox = true;
                                    Form2 form2 = new Form2();
                                    form2.ShowDialog();
                                    break;
                                }
                                c = 0;
                            }
                        }
                    }
                }
                if (o == 0 && edit == false)
                {
                    mouse = true;
                }
                else
                {
                    mouse = false;
                }
                o = 0;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouse = false;
                moveX = pointStart.X;
                moveY = pointStart.Y;
            }
            //拖拉小方框
            chang(e);

        }

        //拖拉小方框
        public void chang(MouseEventArgs e)
        {
            double curx = 0;
            double cury = 0;
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            if (this.Cursor == Cursors.SizeWE && mouse)
            {
                smallbox = true;
                int i = 0;
                dicPoints = (Dictionary<double, double>)Pointlist[id];
                foreach (var item in dicPoints)
                {
                    if (LOR == 1)
                    {
                        if (i == 0)
                        {
                            curx = item.Key;
                            cury = item.Value;
                            i = 1;
                        }
                        else if (i == 1)
                        {
                            bDrawStart = true;
                            pointStart.X = (int)item.Key;
                            pointStart.Y = (int)item.Value;
                            i = 0;

                        }
                    }
                    else if (LOR == 2)
                    {
                        if (i == 0)
                        {
                            curx = item.Key;
                            cury = item.Value;
                            i = 1;
                        }
                        else if (i == 1)
                        {
                            bDrawStart = true;
                            pointStart.X = (int)item.Key;
                            pointStart.Y = (int)cury;
                            i = 0;

                        }
                    }
                    else if (LOR == 3)
                    {
                        if (i == 0)
                        {
                            curx = item.Key;
                            cury = item.Value;
                            i = 1;
                        }
                        else if (i == 1)
                        {
                            bDrawStart = true;
                            pointStart.X = (int)curx;
                            pointStart.Y = (int)item.Value;
                            i = 0;

                        }
                    }
                    else if (LOR == 4)
                    {
                        if (i == 0)
                        {
                            curx = item.Key;
                            cury = item.Value;
                            i = 1;
                        }
                        else if (i == 1)
                        {
                            bDrawStart = true;
                            pointStart.X = (int)curx;
                            pointStart.Y = (int)cury;
                            i = 0;

                        }
                    }
                }
                Pointlist.RemoveAt(id);
            }
            else
            {
                if (bDrawStart)
                {
                    bDrawStart = false;
                }
                else
                {
                    bDrawStart = true;
                    pointStart = e.Location;
                }
            }
        }

        //松开鼠标
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            pointContinue = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                if (bDrawStart)
                {
                    //方向判断与坐标转换
                    if (pointStart.X != pointContinue.Y && pointStart.X != pointContinue.X)
                    {
                        if (pointStart.X < pointContinue.X && pointStart.Y < pointContinue.Y)
                        {
                            dicPoints.Add(pointStart.X, pointStart.Y);
                            dicPoints.Add(pointContinue.X, pointContinue.Y);
                        }
                        else if (pointStart.X < pointContinue.X && pointStart.Y > pointContinue.Y)
                        {
                            dicPoints.Add(pointStart.X, pointContinue.Y);
                            dicPoints.Add(pointContinue.X, pointStart.Y);
                        }
                        else if (pointStart.X > pointContinue.X && pointStart.Y < pointContinue.Y)
                        {
                            dicPoints.Add(pointContinue.X, pointStart.Y);
                            dicPoints.Add(pointStart.X, pointContinue.Y);
                        }
                        else if (pointStart.X > pointContinue.X && pointStart.Y > pointContinue.Y)
                        {
                            dicPoints.Add(pointContinue.X, pointContinue.Y);
                            dicPoints.Add(pointStart.X, pointStart.Y);
                        }
                        if (edit == false)
                        {
                            Pointlist.Add(dicPoints);
                            Form2info(dicPoints);
                            Form2 form2 = new Form2();
                            form2.ShowDialog();
                            smallbox = false;
                            if (Pointlist.Count == 0)
                            {
                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                }
            }
            bDrawStart = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int c = 0;
            LOR = 0;
            int key = 0;
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            if (bDrawStart && mouse)
            {
                this.Cursor = Cursors.SizeWE;
                pointContinue = e.Location;
                Refresh();
            }
            else if (maxbox == false)
            {
                double curx = 0;
                double cury = 0;
                //判断光标是否位于方框内
                for (int t = 0; t < Pointlist.Count; t++)
                {
                    if (key == 1)
                    {
                        break;
                    }
                    dicPoints = (Dictionary<double, double>)Pointlist[t];
                    foreach (var item in dicPoints)
                    {
                        if (c == 0)
                        {
                            curx = item.Key;
                            cury = item.Value;
                            c++;
                        }
                        else
                        {
                            //左上
                            if (curx - 2 <= e.Location.X && e.Location.X <= curx + 2 && cury - 2 <= e.Location.Y && e.Location.Y <= cury + 2)
                            {
                                this.Cursor = Cursors.SizeWE;
                                id = t;
                                LOR = 1;
                                key = 1;
                                break;
                            }
                            //左下
                            else if (curx - 2 <= e.Location.X && e.Location.X <= curx + 2 && item.Value - 2 <= e.Location.Y && e.Location.Y <= item.Value + 2)
                            {
                                this.Cursor = Cursors.SizeWE;
                                id = t;
                                LOR = 2;
                                key = 1;
                                break;
                            }
                            //右上
                            else if (item.Key - 2 <= e.Location.X && e.Location.X <= item.Key + 2 && cury - 2 <= e.Location.Y && e.Location.Y <= cury + 2)
                            {
                                this.Cursor = Cursors.SizeWE;
                                id = t;
                                LOR = 3;
                                key = 1;
                                break;
                            }
                            //右下
                            else if (item.Key - 2 <= e.Location.X && e.Location.X <= item.Key + 2 && item.Value - 2 <= e.Location.Y && e.Location.Y <= item.Value + 2)
                            {
                                this.Cursor = Cursors.SizeWE;
                                id = t;
                                LOR = 4;
                                key = 1;
                                break;
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                            }
                            c = 0;
                        }

                    }
                }

            }

            if (e.Button == MouseButtons.Right)
            {

                panel1.AutoScrollPosition = new Point(panel1.HorizontalScroll.Value - (int)((e.Location.X - (int)moveX) / 1.4), panel1.VerticalScroll.Value - (int)((e.Location.Y - (int)moveY) / 1.4));//滚动
                moveX = e.Location.X;
                moveY = e.Location.Y;
                Refresh();
            }

        }

        //画图
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int a = 0;
            double startx = 0;
            double starty = 0;
            System.Drawing.Pen pen = new System.Drawing.Pen(Color.LimeGreen);
            pen.Width = 2;
            if (mouse)
            {
                if (bDrawStart)
                {
                    //实时的画矩形
                    Graphics g = e.Graphics;
                    //g.DrawRectangle(pen, pointStart.X, pointStart.Y, pointContinue.X - pointStart.X, pointContinue.Y - pointStart.Y);
                    if (pointStart.X < pointContinue.X && pointStart.Y < pointContinue.Y)
                    {
                        g.DrawRectangle(pen, (float)pointStart.X, (float)pointStart.Y, (float)(pointContinue.X - pointStart.X), (float)(pointContinue.Y - pointStart.Y));
                    }
                    else if (pointStart.X < pointContinue.X && pointStart.Y > pointContinue.Y)
                    {
                        g.DrawRectangle(pen, (float)pointStart.X, (float)pointContinue.Y, (float)(pointContinue.X - pointStart.X), (float)(pointStart.Y - pointContinue.Y));
                    }
                    else if (pointStart.X > pointContinue.X && pointStart.Y < pointContinue.Y)
                    {
                        g.DrawRectangle(pen, (float)pointContinue.X, (float)pointStart.Y, (float)(pointStart.X - pointContinue.X), (float)(pointContinue.Y - pointStart.Y));
                    }
                    else
                    {
                        g.DrawRectangle(pen, (float)pointContinue.X, (float)pointContinue.Y, (float)(pointStart.X - pointContinue.X), (float)(pointStart.Y - pointContinue.Y));
                    }
                    g.DrawRectangle(pen, (float)pointStart.X - 2, (float)pointStart.Y - 2, 5, 5);
                    g.DrawRectangle(pen, (float)pointStart.X - 2, (float)pointContinue.Y - 2, 5, 5);
                    g.DrawRectangle(pen, (float)pointContinue.X - 2, (float)pointStart.Y - 2, 5, 5);
                    g.DrawRectangle(pen, (float)pointContinue.X - 2, (float)pointContinue.Y - 2, 5, 5);
                }
            }
            //实时的画之前已经画好的矩形
            for (int t = 0; t < Pointlist.Count; t++)
            {
                dicPoints = (Dictionary<double, double>)Pointlist[t];
                foreach (var item in dicPoints)
                {
                    if (a == 0)
                    {
                        startx = item.Key;
                        starty = item.Value;
                        a = 1;
                    }
                    else
                    {
                        if (startx < item.Key && starty < item.Value)
                        {
                            e.Graphics.DrawRectangle(pen, (float)startx, (float)starty, (float)(item.Key - startx), (float)(item.Value - starty));
                        }
                        else if (startx < item.Key && starty > item.Value)
                        {
                            e.Graphics.DrawRectangle(pen, (float)startx, (float)item.Value, (float)(item.Key - startx), (float)(starty - item.Value));
                        }
                        else if (startx > item.Key && starty < item.Value)
                        {
                            e.Graphics.DrawRectangle(pen, (float)item.Key, (float)starty, (float)(startx - item.Key), (float)(item.Value - starty));
                        }
                        else
                        {
                            e.Graphics.DrawRectangle(pen, (float)item.Key, (float)item.Value, (float)(startx - item.Key), (float)(starty - item.Value));
                        }
                        e.Graphics.DrawRectangle(pen, (float)startx - 2, (float)starty - 2, 5, 5);
                        e.Graphics.DrawRectangle(pen, (float)startx - 2, (float)item.Value - 2, 5, 5);
                        e.Graphics.DrawRectangle(pen, (float)item.Key - 2, (float)starty - 2, 5, 5);
                        e.Graphics.DrawRectangle(pen, (float)item.Key - 2, (float)item.Value - 2, 5, 5);
                        a = 0;
                    }
                }
            }
            pen.Dispose();
        }

        //保存图片
        private void button7_Click(object sender, EventArgs e)
        {
            Dictionary<double, double> dicPoints = new Dictionary<double, double>();
            Rectangle rect = new Rectangle(Point.Empty, this.pictureBox1.Size);
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                this.pictureBox1.DrawToBitmap(bmp, rect);  // 画pictureBox1显示的图，假定它没有边框
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    pictureBox1_Paint(this, new PaintEventArgs(g, rect)); // 画自定义标记
                }
                bmp.Save(@subPath + "\\" + name, ImageFormat.Jpeg);
            }

            FileStream fs = new FileStream(subPath + "\\Class.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            string classxy = name + " 0,0," + TrueWidth + "," + TrueHeight + ",";
            for (int i = 0; i < Pointlist.Count; i++)
            {
                dicPoints = (Dictionary<double, double>)Pointlist[i];
                classxy = classxy + classlist[i] + " ";
                foreach (var item in dicPoints)
                {
                    classxy = classxy + (int)(item.Key * TrueWidth / pictureBox1.Width) + "," + (int)(item.Value * TrueHeight / pictureBox1.Height) + ",";
                }

            }
            sw.WriteLine(classxy);
            //记得要关闭！不然里面没有字！
            sw.Close();
            fs.Close();

            MessageBox.Show("ok");
        }

        //打开图片
        private void button1_Click(object sender, EventArgs e)
        {
            openimg(0);
            Imgid();
            Pointlist.Clear();
            classlist.Clear();
        }

        public void openimg(int e)
        {
            if (e == 1)
            {
                int i = list.LastIndexOf(name);
                if (i >= 0)
                {
                    this.pictureBox1.ImageLocation = path + "\\" + list[i];
                    this.listBox1.SelectedIndex = i;
                }
            }
            else if (this.listBox1.SelectedIndex >= 0)
            {
                this.pictureBox1.ImageLocation = path + "\\" + this.listBox1.SelectedItem.ToString();
                name = this.listBox1.SelectedItem.ToString();
                Console.WriteLine(name);
            }
            if (list.Count > 0)
            {
                Bitmap pic = new Bitmap(path + "\\" + name);
                width = this.pictureBox1.Width;
                height = this.pictureBox1.Height;
                TrueWidth = pic.Size.Width;
                TrueHeight = pic.Size.Height;
            }
            this.pictureBox1.Size = new System.Drawing.Size((int)(TrueWidth), (int)(TrueHeight));
            width = this.pictureBox1.Width;
            height = this.pictureBox1.Height;
        }

        //打开文件夹
        private void button2_Click(object sender, EventArgs e)
        {
            openfile(0);
        }
        //撤销上一步
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Pointlist.Count > 0)
            {
                Pointlist.RemoveAt(Pointlist.Count - 1);
            }
            this.pictureBox1.Refresh();
            if (classlist.Count > 0)
            {
                classlist.RemoveAt(classlist.Count - 1);
            }
        }
        //上一张图片
        private void button3_Click(object sender, EventArgs e)
        {
            int i = list.LastIndexOf(name);
            if (i > 0)
            {
                this.pictureBox1.ImageLocation = path + "\\" + list[i - 1];
                name = list[i - 1].ToString();
                this.listBox1.SelectedIndex = i - 1;
            }
            else
            {
                MessageBox.Show("当前图片为第一张！");
            }
            Bitmap pic = new Bitmap(path + "\\" + name);
            TrueWidth = pic.Size.Width;
            TrueHeight = pic.Size.Height;
            this.pictureBox1.Size = new System.Drawing.Size((int)(TrueWidth), (int)(TrueHeight));
            width = this.pictureBox1.Width;
            height = this.pictureBox1.Height;
            Imgid();
            Pointlist.Clear();
            classlist.Clear();

        }
        //下一张图片
        private void button4_Click(object sender, EventArgs e)
        {
            int i = list.LastIndexOf(name);
            if (list.Count > i + 1)
            {
                this.pictureBox1.ImageLocation = path + "\\" + list[i + 1];
                name = list[i + 1].ToString();
                Console.WriteLine(name);
                this.listBox1.SelectedIndex = i + 1;
            }
            else
            {
                MessageBox.Show("当前图片为最后一张！");
            }
            Bitmap pic = new Bitmap(path + "\\" + name);
            TrueWidth = pic.Size.Width;
            TrueHeight = pic.Size.Height;
            this.pictureBox1.Size = new System.Drawing.Size((int)(TrueWidth), (int)(TrueHeight));
            width = this.pictureBox1.Width;
            height = this.pictureBox1.Height;
            Imgid();
            Pointlist.Clear();
            classlist.Clear();
        }

        //记录目前打开的图片
        public void Imgid()
        {
            appconfig.AppSettings.Settings["SystemSettingPath"].Value = path;
            appconfig.AppSettings.Settings["SystemSettingPicture"].Value = name;
            appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);

        }

        //Form2传参
        public void Form2info(Dictionary<double, double> dicPoints)
        {
            try
            {
                list2.Clear();
                list2.Add(dicPoints);
                list2.Add(pictureBox1.Width);
                list2.Add(pictureBox1.Height);
                list2.Add(smallbox);
                list2.Add(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //定时更新标签栏
        private void Timer1_Tick(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            foreach (var a in classlist)
            {
                listBox2.Items.Add(a);
            }
        }
        //编辑标签
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();
        }
        //编辑
        private void button6_Click(object sender, EventArgs e)
        {
            edit = true;
            button6.BackColor = Color.Green;
            button5.BackColor = Color.Red;
        }
        //创建
        private void button5_Click(object sender, EventArgs e)
        {
            edit = false;
            button6.BackColor = Color.Red;
            button5.BackColor = Color.Green;
        }
        //帮助
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
        }
        //关于
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }
    }
}
