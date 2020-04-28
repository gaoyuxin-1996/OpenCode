using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using System.Runtime.InteropServices;
using OpenCvSharp;
using System.Net.Sockets;
using System.Net;

namespace SPDP
{
    public partial class Form1 : Form
    {
        //摄像头配置界面
        Form2 form2 = new Form2();
        //图像文件夹配置
        Form3 form3 = new Form3();
        //识别结果展示
        Form9 form9 = new Form9();
        Socket server;
        //APP配置文件
        public static Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //图像名称
        ArrayList list = new ArrayList();
        Bitmap bmpInitial = (Bitmap)Image.FromFile(Environment.CurrentDirectory + "\\load.jpg");
        Image imgshow;
        [DllImport("DllTest.dll")]
        static unsafe extern void pre_process_image_for_csharp(byte[] in_img, ref int in_img_size, int width, int height, int channel, ref int mean, ref IntPtr out_img, ref int out_img_size);
        //后处理方法
        [DllImport("DllTest.dll")]
        static unsafe extern void post_process_for_csharp(byte[] fpga_out, ref int fpga_out_size, ref IntPtr intPtr, ref int preds_size);
        //UDP返回的结果字节长度
        public static int returnnum;
        VideoCapture cap;
        //开启线程数
        public static int threadnum = 1;
        //锁
        private static object ojb = new object();
        private static object ojb1 = new object();
        private static object ojb2 = new object();
        //前处理数量
        public static int endnum = 0;
        //当前模式
        int Model;
        //摄像头ID
        public static int CapID = 10;
        //摄像头帧
        Mat image = new Mat();
        //图像大小
        public static int width;
        public static int height;
        //图像通道
        public static int channel = 4;
        //当前通道分离数目
        public static int ABGRK = 0;
        public static int ABGRK1 = 1;
        public static int ABGRK2 = 2;
        public static int ABGRKEND = 0;
        //类别目录
        public static Dictionary<int, string> classlist = new Dictionary<int, string>();
        //抽帧队列
        Dictionary<int, Mat> listimage = new Dictionary<int, Mat>();
        //记录队列>>>>>>>>>>>>原视频输出
        List<Mat> listimag = new List<Mat>();
        //记录队列>>>>>>>>>>>>原视频输出
        public static List<Mat> listimag1 = new List<Mat>();
        //检测框添加队列>>>>>>>检测结果视频输出
        public static List<Mat> listimage2 = new List<Mat>();
        //UDP结果队列
        List<byte[]> listudpout=new List<byte[]>();
        //结果队列
        List<prediction> listout1 =new List<prediction>();
        //分离记录
        Dictionary<int, byte[]> ABGRList = new Dictionary<int, byte[]>();
        //预处理返回
        Dictionary<int, byte[]> ABGRList1 = new Dictionary<int, byte[]>();
        List<List<byte[]>> ABGRList3 = new List<List<byte[]>>();//数据
        List<byte[]> ABGRList4 = new List<byte[]>();//数据
        //后处理结果
        Dictionary<int, List<prediction>> listall = new Dictionary<int, List<prediction>>();
        //UDP接收数据
        List<byte[]> udplist = new List<byte[]>();//数据
        List<int> udplist1 = new List<int>();//长度
        List<int> udplist2 = new List<int>();//图像ID
        //视频图像地址
        string path;
        //视频图像名称
        string name;
        //当前发送的次数
        int ABGRLiatNum = 0;
        int ABGRLiatNum1 = 0;
        //批次数量
        public static int time = 10;
        //包大小
        public static int udpnum = 20000;
        //需要发送的次数
        int udptime = 0;
        //FPS
        public static int FPS = 0;
        //DLL接受结果数
        public static int Dllnum = 0;
        //准确率
        public static float accuracy = 0;
        //检测框字体大小
        float fontSize = 10.0f;
        Boolean beforebool = false;
        Boolean endbool = false;
        //CMD
        public static String Cmdstr = "";
        public static String Cmdstr1 = "";
     
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Load();
        }
        //初始化
        void Load()
        {
            comboBox1.Items.Add("摄像头");
            comboBox1.Items.Add("本地视频");
            comboBox1.Items.Add("单图像");
            comboBox1.Items.Add("连续图像");
            comboBox1.Items.Add("雷达信号");
            comboBox2.Items.Add("FFT");
            comboBox2.Items.Add("SPWVD");
            Thread video = new Thread(videoimage);
            video.Start();
            appconfig.AppSettings.Settings["ModelID"].Value = "";
            appconfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
            height = int.Parse(appconfig.AppSettings.Settings["Height"].Value);
            width = int.Parse(appconfig.AppSettings.Settings["Width"].Value);
            time = int.Parse(appconfig.AppSettings.Settings["ImgTime"].Value);
            udpnum = int.Parse(appconfig.AppSettings.Settings["UDPNum"].Value);
            returnnum = int.Parse(appconfig.AppSettings.Settings["UDPReturn"].Value);
            FPS = int.Parse(appconfig.AppSettings.Settings["FPS"].Value);
            Dllnum = int.Parse(appconfig.AppSettings.Settings["DLLReturnNum"].Value);
            accuracy = float.Parse(appconfig.AppSettings.Settings["ShowNum"].Value);
            string str = appconfig.AppSettings.Settings["ClassName"].Value;
            char[] charTemp = { ',', '+' };
            string[] arr = str.Split(charTemp);
            classlist.Clear();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == "")
                    break;
                classlist.Add(int.Parse(arr[i]), arr[i + 1]);
                i = i + 1;
            }
            imgshow = bmpInitial;
            char[] charTemp1 = { '\\' };
            string[] pypath = appconfig.AppSettings.Settings["PYPath"].Value.Split(charTemp1);
            string[] pypath1 = appconfig.AppSettings.Settings["PYimgpath"].Value.Split(charTemp1);
            str = "python ";
            foreach (string i in pypath)
            {
                str = str + i + "/";
            }
            Form1.Cmdstr = str + appconfig.AppSettings.Settings["PYName"].Value + " ";
            str = "";
            foreach (string i in pypath1)
            {
                str = str + i + "/";
            }
            Form1.Cmdstr1 = str + " " + appconfig.AppSettings.Settings["PYimgname"].Value;

        }
        
        //重置
        void Load1()
        {

            beforebool = false;
            endbool = false;
            //抽帧队列
            listimage.Clear();
            //记录队列>>>>>>>>>>>>原视频输出
            listimag.Clear(); 
            //记录队列>>>>>>>>>>>>原视频输出
            listimag1.Clear();
            //检测框添加队列>>>>>>>检测结果视频输出
            listimage2.Clear(); 
            //UDP结果队列
            listudpout.Clear(); 
            //结果队列
            listout1.Clear(); 
            //分离记录
            ABGRK = 0;
            //ABGRList.Clear();
            //预处理返回
            //ABGRList1.Clear();
            //当前发送的次数
            ABGRLiatNum = 0;
            endnum = 0;
            //当前通道分离数目
            ABGRKEND = 0;
            Cv2.WaitKey(1000);
        }
        //模式
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Thread threadA = new Thread(run_cap);
                
                switch (comboBox1.Text)
                {
                    case "摄像头":
                        appconfig.AppSettings.Settings["ModelID"].Value = "0";
                        appconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                        form2.ShowDialog();
                        if (CapID == 10)
                        {
                            appconfig.AppSettings.Settings["ModelID"].Value = "";
                            appconfig.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                        }
                        break;
                    case "本地视频":
                        appconfig.AppSettings.Settings["ModelID"].Value = "1";
                        appconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                        form3.ShowDialog();
                        break;
                    case "单图像":
                        appconfig.AppSettings.Settings["ModelID"].Value = "2";
                        appconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                        form3.ShowDialog();
                        break;
                    case "连续图像":
                        appconfig.AppSettings.Settings["ModelID"].Value = "3";
                        appconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                        form3.ShowDialog();
                        break;
                    case "雷达信号":
                        appconfig.AppSettings.Settings["ModelID"].Value = "4";
                        appconfig.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection(appconfig.AppSettings.SectionInformation.Name);
                        form3.ShowDialog();
                        break;
                    default:
                        break;

                }
                listBox1.Items.Clear();
                list.Clear();
                Load1();
                switch (appconfig.AppSettings.Settings["ModelID"].Value)
                {
                    case "0":
                        Model = 0;
                        threadA.Start();
                        this.Text = "信号处理演示平台——摄像头模式";
                        time = int.Parse(appconfig.AppSettings.Settings["ImgTime"].Value);
                        break;
                    case "1":
                        Model = 1;
                        this.Text = "信号处理演示平台——本地视频模式";
                        foreach (var f in Directory.GetFiles(appconfig.AppSettings.Settings["SystemImagePath"].Value))
                        {
                            this.listBox1.Items.Add(System.IO.Path.GetFileName(f));
                            list.Add(System.IO.Path.GetFileName(f));
                        }
                        time = int.Parse(appconfig.AppSettings.Settings["ImgTime"].Value);
                        break;
                    case "2":
                        Model = 2;
                        foreach (var f in Directory.GetFiles(appconfig.AppSettings.Settings["SystemImagePath"].Value, ".", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".gif")))
                        {
                            this.listBox1.Items.Add(System.IO.Path.GetFileName(f));
                            list.Add(System.IO.Path.GetFileName(f));
                        }
                        this.Text = "信号处理演示平台——单图像模式";
                        break;
                    case "3":
                        Model = 3;
                        foreach (var f in Directory.GetFiles(appconfig.AppSettings.Settings["SystemImagePath"].Value, ".", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".gif")))
                        {
                            this.listBox1.Items.Add(System.IO.Path.GetFileName(f));
                            list.Add(System.IO.Path.GetFileName(f));
                        }
                        this.Text = "信号处理演示平台——连续图像模式";
                        break;
                    case "4":
                        Model = 4;
                        foreach (var f in Directory.GetFiles(appconfig.AppSettings.Settings["SystemImagePath"].Value, ".", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mat") ))
                        {
                            this.listBox1.Items.Add(System.IO.Path.GetFileName(f));
                            list.Add(System.IO.Path.GetFileName(f));
                        }
                        this.Text = "信号处理演示平台——雷达信号模式";
                        break;
                    default:
                        //Console.WriteLine("ds");
                        break;
                }

            }
            catch (Exception)
            {

            }
        }
        //播放视频图像帧数
        void videoimage() {
            while (true) {
                Cv2.WaitKey(500);
                label2.Text = "当前处理完成：" + ABGRLiatNum.ToString() + "————剩余可播放帧数" + listimag.Count;
                pictureBox1.Focus();
                pictureBox1.Invalidate();
            }
        }
        //读取摄像头
        void run_cap()
        {
            try
            {
                Thread threadA = new Thread(CharA);//创建线程
                Thread thread = new Thread(Udp);//创建线程
                Thread threadb = new Thread(Before);//创建线程
                Thread thr = new Thread(End);
                Thread thr1 = new Thread(UDPDO);
                thr1.Start();
                endbool = true;
                Thread hi = new Thread(ImageWrite);
                hi.Start();
                thr.Start();
                beforebool = true;
                threadb.Start();
                threadA.Start();
                thread.Start();
                Mat src = new Mat();
                VideoCapture cap = new VideoCapture();
                OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                cap.Open(CapID);
                int u = 0;
                while (Model == 0)
                {
                    Mat image = new Mat();
                    cap.Read(src);
                    //image = src.Reshape(channel, 0).Clone();
                    image = src.Clone();
                    Cv2.Resize(image, image, size, 0, 0);
                    listimage.Add(u, image);
                    u = u + 1;
                    //listimage1.Add(src1);
                    listimag.Add(image.Clone());
                    listimag1.Add(image.Clone());
                    Cv2.WaitKey(FPS);
                    if (Model != 0)
                    {
                        threadA.Abort();
                        break;
                    }

                }
            }
            catch (Exception)
            {
            }

        }
        //读取本地视频
        void run_cap1()
        {
            try
            {
                Thread threadA = new Thread(CharA);//创建线程
                Thread thread = new Thread(Udp);//创建线程
                Thread threadb = new Thread(Before);//创建线程
                Thread thr = new Thread(End);
                Thread thr1 = new Thread(UDPDO);
                thr1.Start();
                endbool = true;
                Thread hi = new Thread(ImageWrite);
                hi.Start();
                thr.Start();
                beforebool = true;
                threadA.Start();
                threadb.Start();
                thread.Start();
                Mat src = new Mat();
                cap = new VideoCapture(path + "\\" + name);
                OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                Boolean on = true;
                //cap.Open(0);
                int u = 0;
                while (on&&Model==1)
                {
                    try
                    {
                        Mat image = new Mat();
                        cap.Read(src);
                        image = src.Clone();
                        Cv2.Resize(image, image, size, 0, 0);
                        listimage.Add(u, image);
                        u = u + 1;
                        //listimage1.Add(src1);
                        listimag.Add(image.Clone());
                        listimag1.Add(image.Clone());
                        Cv2.WaitKey(FPS);
                        if (Model != 1)
                        {
                            threadA.Abort();
                            on = false;
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e);
                        on = false;

                    }
                }

            }
            catch (Exception)
            {
            }

        }
        //读取本地图像
        void run_image()
        {
            try
            {
                Thread threadA = new Thread(CharA);//创建线程
                Thread thread = new Thread(Udp);//创建线程
                Thread threadb = new Thread(Before);//创建线程
                Thread thr = new Thread(End);
                Thread thr1 = new Thread(UDPDO);
                thr1.Start();
                endbool = true;
                Thread hi = new Thread(ImageWrite);
                hi.Start();
                thr.Start();
                beforebool = true;
                threadA.Start();
                threadb.Start();
                thread.Start();
                Mat src = new Mat(path + "\\" + name);
                OpenCvSharp.Size size = new OpenCvSharp.Size(width, height);
                //cap.Open(0);
                int u = 0;
                try
                {
                    Mat image = new Mat();
                    //image = src.Reshape(channel, 0).Clone();
                    image = src.Clone();
                    Cv2.Resize(image, image, size, 0, 0);
                    for (int i = 0; i < time; i++)
                    {
                        listimage.Add(u, image);
                        u = u + 1;
                        //listimage1.Add(src1);
                        listimag.Add(image.Clone());
                        listimag1.Add(image.Clone());
                        Cv2.WaitKey(FPS);
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);

                }


            }
            catch (Exception)
            {
            }

        }
        //4线程图像通道分离
        void CharA()
        {
            int num1 = 0;
            Mat image = new Mat();
            byte[] A = new byte[height * width * channel];
            while (threadnum >= 1)
            {
                try
                {
                    lock (ojb)
                    {
                        num1 = ABGRK;
                        ABGRK = ABGRK + 1;
                    }
                    while (!listimage.ContainsKey(num1))
                    {

                    }
                    image = listimage[num1];
                    byte[] PixelValues = new byte[height * width * 3];
                    int start = 0;
                    for (int i = 0; i < image.Rows; i++)
                    {
                        IntPtr str1 = image.Ptr(i);
                        byte[] PixelValues1 = new byte[image.Cols * 3];
                        Marshal.Copy(str1, PixelValues1, 0, image.Cols * 3);
                        Array.Copy(PixelValues1, 0, PixelValues, start, image.Cols * 3);
                        start = start + image.Cols * 3;
                    }
                    int c = 0;
                    for (int i = 0; i < image.Rows * image.Cols * 3; i++)
                    {
                        A[c] = 128;
                        A[c + 1] = PixelValues[i];
                        A[c + 2] = PixelValues[i + 1];
                        A[c + 3] = PixelValues[i + 2];
                        i = i + 2;
                        c = c + 4;
                    }
                    //ABGRList.Add(num1, Top(A));
                    if (ABGRList.ContainsKey(num1))
                    {
                        ABGRList[num1] = A;
                    }
                    else
                    {
                        ABGRList.Add(num1, A);
                    }


                }
                catch (Exception e) { Console.WriteLine(e+num1.ToString()); }

            }
        }
        void CharB()
        {
            int num1 = 0;
            Mat image = new Mat();
            byte[] A = new byte[height * width * channel];
            while (threadnum >= 1)
            {
                try
                {
                    lock (ojb)
                    {
                        num1 = ABGRK;
                        ABGRK = ABGRK + 1;
                    }
                    while (!listimage.ContainsKey(num1))
                    {

                    }
                    image = listimage[num1];
                    byte[] PixelValues = new byte[height * width * 3];
                    int start = 0;
                    for (int i = 0; i < image.Rows; i++)
                    {
                        IntPtr str1 = image.Ptr(i);
                        byte[] PixelValues1 = new byte[image.Cols * 3];
                        Marshal.Copy(str1, PixelValues1, 0, image.Cols * 3);
                        Array.Copy(PixelValues1, 0, PixelValues, start, image.Cols * 3);
                        start = start + image.Cols * 3;
                    }
                    int c = 0;
                    for (int i = 0; i < image.Rows * image.Cols * 3; i++)
                    {
                        A[c] = 128;
                        A[c + 1] = PixelValues[i];
                        A[c + 2] = PixelValues[i + 1];
                        A[c + 3] = PixelValues[i + 2];
                        i = i + 2;
                        c = c + 4;
                    }
                    //ABGRList.Add(num1, Top(A));
                    if (ABGRList.ContainsKey(num1))
                    {
                        ABGRList[num1] = A;
                    }
                    else
                    {
                        ABGRList.Add(num1, A);
                    }


                }
                catch (Exception e) { Console.WriteLine(e + num1.ToString()); }

            }
        }
        void CharG()
        {
            int num1 = 0;
            Mat image = new Mat();
            byte[] A = new byte[height * width * channel];
            while (threadnum >= 1)
            {
                try
                {
                    lock (ojb)
                    {
                        num1 = ABGRK;
                        ABGRK = ABGRK + 1;
                    }
                    while (!listimage.ContainsKey(num1))
                    {

                    }
                    image = listimage[num1];
                    byte[] PixelValues = new byte[height * width * 3];
                    int start = 0;
                    for (int i = 0; i < image.Rows; i++)
                    {
                        IntPtr str1 = image.Ptr(i);
                        byte[] PixelValues1 = new byte[image.Cols * 3];
                        Marshal.Copy(str1, PixelValues1, 0, image.Cols * 3);
                        Array.Copy(PixelValues1, 0, PixelValues, start, image.Cols * 3);
                        start = start + image.Cols * 3;
                    }
                    int c = 0;
                    for (int i = 0; i < image.Rows * image.Cols * 3; i++)
                    {
                        A[c] = 128;
                        A[c + 1] = PixelValues[i];
                        A[c + 2] = PixelValues[i + 1];
                        A[c + 3] = PixelValues[i + 2];
                        i = i + 2;
                        c = c + 4;
                    }
                    //ABGRList.Add(num1, Top(A));
                    if (ABGRList.ContainsKey(num1))
                    {
                        ABGRList[num1] = A;
                    }
                    else
                    {
                        ABGRList.Add(num1, A);
                    }


                }
                catch (Exception e) { Console.WriteLine(e + num1.ToString()); }

            }
        }
        void CharR()
        {
            int num1 = 0;
            Mat image = new Mat();
            byte[] A = new byte[height * width * channel];
            while (threadnum >= 1)
            {
                try
                {
                    lock (ojb)
                    {
                        num1 = ABGRK;
                        ABGRK = ABGRK + 1;
                    }
                    while (!listimage.ContainsKey(num1))
                    {

                    }
                    image = listimage[num1];
                    byte[] PixelValues = new byte[height * width * 3];
                    int start = 0;
                    for (int i = 0; i < image.Rows; i++)
                    {
                        IntPtr str1 = image.Ptr(i);
                        byte[] PixelValues1 = new byte[image.Cols * 3];
                        Marshal.Copy(str1, PixelValues1, 0, image.Cols * 3);
                        Array.Copy(PixelValues1, 0, PixelValues, start, image.Cols * 3);
                        start = start + image.Cols * 3;
                    }
                    int c = 0;
                    for (int i = 0; i < image.Rows * image.Cols * 3; i++)
                    {
                        A[c] = 128;
                        A[c + 1] = PixelValues[i];
                        A[c + 2] = PixelValues[i + 1];
                        A[c + 3] = PixelValues[i + 2];
                        i = i + 2;
                        c = c + 4;
                    }
                    //ABGRList.Add(num1, Top(A));
                    if (ABGRList.ContainsKey(num1))
                    {
                        ABGRList[num1] = A;
                    }
                    else
                    {
                        ABGRList.Add(num1, A);
                    }


                }
                catch (Exception e) { Console.WriteLine(e + num1.ToString()); }

            }
        }
        //图片大小配置
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }
        //关闭程序
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (System.Exception) { }
        }
        //线程控制
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int num = threadnum;
            Form5 form = new Form5();
            form.ShowDialog();
            try
            {
                if (threadnum > num)
                {
                    Thread threadR = new Thread(CharR);//创建线程
                    Thread threadA = new Thread(CharA);//创建线程
                    Thread threadB = new Thread(CharB);//创建线程
                    Thread threadG = new Thread(CharG);//创建线程

                    if (Model == 0)
                    {

                        if (threadnum == 1)
                        {
                            threadnum = 0;
                            Cv2.WaitKey(100);
                            threadnum = 1;
                            threadA.Start();
                        }
                        else if (threadnum == 2)
                        {
                            threadnum = 0;
                            Cv2.WaitKey(100);
                            threadnum = 2;
                            threadA.Start();
                            threadB.Start();
                        }
                        else if (threadnum == 3)
                        {
                            threadnum = 0;
                            Cv2.WaitKey(100);
                            threadnum = 3;
                            threadA.Start();
                            threadB.Start();
                            threadG.Start();
                        }
                        else
                        {
                            threadnum = 0;
                            Cv2.WaitKey(100);
                            threadnum = 4;
                            threadA.Start();
                            threadB.Start();
                            threadG.Start();
                            threadR.Start();
                        }

                    }
                }
            }
            catch (Exception) { }
        }
        //本地视频读取开始
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Load1();
                if (Model == 1)
                {
                    Model = 10;
                    Cv2.WaitKey(1000);
                    Model = 1;
                    Thread threadA = new Thread(run_cap1);
                    path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                    name = listBox1.SelectedItem.ToString();
                    threadA.Start();
                }
                else if (Model == 2) {
                    Thread threadb = new Thread(run_image);
                    path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                    name = listBox1.SelectedItem.ToString();
                    threadb.Start();
                }
                else if (Model == 3)
                {
                    Thread threadb = new Thread(run_image);
                    path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                    name = listBox1.SelectedItem.ToString();
                    threadb.Start();
                }
                else if (Model == 4)
                {
                    if(comboBox2.Text == "SPWVD")
                    {
                        path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                        name = listBox1.SelectedItem.ToString();
                        char[] charTemp = { '\\' };
                        string[] pypath = path.Split(charTemp);
                        string str = "";
                        foreach (string i in pypath)
                        {
                            str = str + i + "/";
                        }
                        str = str + name+" ";
                        Console.WriteLine(Cmdstr+str+Cmdstr1);
                        CMD(Cmdstr + str + Cmdstr1);
                    }
                    else
                    {
                        MessageBox.Show("请选择雷达信号处理模式");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请选择文件");
            }
        }
        //UDP配置界面
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.ShowDialog();
        }
        //UDP      
        public void Udp()
        {
            try { server.Close(); } catch (Exception) { }
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(appconfig.AppSettings.Settings["IP"].Value), int.Parse(appconfig.AppSettings.Settings["Port"].Value));
            //定义网络类型，数据连接类型和网络协议UDP
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse(appconfig.AppSettings.Settings["IP1"].Value), int.Parse(appconfig.AppSettings.Settings["Port1"].Value)));
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;
            byte[] data;
            string input= appconfig.AppSettings.Settings["FPGAStart"].Value;
            string input1 = "";
            string endsend = appconfig.AppSettings.Settings["ImgEnd"].Value;
            while (true)
            {
                
                try
                {
                    if (ABGRList3.Count>0)
                    {
                        byte[] UDPOUT = new byte[returnnum]; 
                        string welcome = appconfig.AppSettings.Settings["ImgStart"].Value+ ABGRLiatNum1.ToString().PadLeft(6, '0');
                        data = Encoding.ASCII.GetBytes(welcome);
                        byte[] data1 = new byte[data.Length + 1];
                        byte[] ALL = new byte[width * height * channel * time];
                        Array.Copy(data, 0, data1, 0, data.Length);
                        server.SendTo(data1, data1.Length, SocketFlags.None, ip);
                        ABGRLiatNum1 = ABGRLiatNum1 + time;
                        input1 = input + (ABGRLiatNum1-1).ToString().PadLeft(6, '0');
                        label3.Text = "开始发送";
                        ABGRList4 = ABGRList3[0];
                        ABGRList3.RemoveAt(0);
                        foreach (byte[] send in ABGRList4) {
                            server.SendTo(send, send.Length, SocketFlags.None, ip);
                        }
                        label3.Text = "开始接收";
                        data = new byte[endsend.Length + 1];
                        data = Encoding.ASCII.GetBytes(endsend);
                        data1 = new byte[data.Length + 1];
                        Array.Copy(data, 0, data1, 0, data.Length);
                        Cv2.WaitKey(1);
                        data = new byte[udpnum];
                        server.SendTo(data1, data1.Length, SocketFlags.None, ip);             
                        while (true)
                        { 
                            int recv = server.ReceiveFrom(data, ref Remote);
                            udplist.Add(data);
                            udplist1.Add(recv);
                            if (recv == 16)
                            {
                                if (Encoding.ASCII.GetString(data, 0, recv - 1) == input1)
                                {
                                    break;
                                }
                            }
                            data = new byte[udpnum];
                        }                       
                        label3.Text = "接受完毕";
                    }
                }
                catch (SocketException) {
                    break;
                    
                }
                catch (Exception e)
                {
                    richTextBox1.AppendText(e.ToString());

                }
            }
        }
        //处理UDP接收的数据
        void UDPDO()
        {
            string input = appconfig.AppSettings.Settings["FPGAStart"].Value;
            byte[] data = new byte[udpnum];
            int num = 0;
            int udpnum1 = 0;
            byte[] UDPOUT = new byte[returnnum];
            while (true)
            {
                try
                {
                    if (udplist1.Count > 0)
                    {
                        data = udplist[0];
                        udpnum1 = udplist1[0];
                        udplist.RemoveAt(0);
                        udplist1.RemoveAt(0);
                        if (input == Encoding.ASCII.GetString(data, 0, 9))
                        {
                            int a = int.Parse(Encoding.ASCII.GetString(data, 9, 6));
                            if (num > 0)
                            {
                                listudpout.Add(UDPOUT);
                                udplist2.Add(a);
                                UDPOUT = new byte[returnnum];
                            }
                            num = 0;
                            continue;

                        }
                        if (num < returnnum)
                        {
                            if (num + udpnum1 > returnnum)
                            {
                                udpnum1 = returnnum - num;
                            }
                            Array.Copy(data, 0, UDPOUT, num, udpnum1);
                            num = num + udpnum1;
                            if (num >= returnnum)
                                continue;
                        }
                    }
                }
                catch (Exception e) {
                }
            }
        }
        //预处理线程
        public void Before()
        {
            int i = 0;
            if (width * height * channel * time % udpnum == 0)
            {
                udptime = width * height * channel * time / udpnum;
            }
            else
            {
                udptime = (width * height * channel * time / udpnum) + 1;
            }
            while (beforebool)
            {
                try
                {
                    if (ABGRList.ContainsKey(i))
                    {
                        if (ABGRList1.ContainsKey(i))
                        {
                            ABGRList1[i] = Top(ABGRList[i]);
                        }
                        else
                        {
                            ABGRList1.Add(i, Top(ABGRList[i]));
                        }
                        listimage.Remove(i);
                        ABGRList.Remove(i);
                        i = i + 1;
                    }
                    //数据整合与分离
                    if (i % time == 0) {
                        List<byte[]> ABGRList2 = new List<byte[]>();//数据
                        byte[] ALL = new byte[width * height * channel * time];
                        for (int b = 0; b < time; b++)
                        {
                            Array.Copy(ABGRList1[ABGRLiatNum], 0, ALL, width * height * channel*b, width * height * channel);
                            ABGRLiatNum = ABGRLiatNum + 1;
                        }
                        
                        for (int h = 0; h < udptime; h++)
                        {
                            byte[] send = new byte[udpnum];
                            if (h == udptime - 1)
                            {
                                send = new byte[width * height * channel * time - (udpnum * h)];
                                Array.Copy(ALL, h * udpnum, send, 0, width * height * channel * time - (udpnum * h));
                            }
                            else
                            {
                                Array.Copy(ALL, h * udpnum, send, 0, udpnum);
                            }

                            ABGRList2.Add(send);
                        }
                        ABGRList3.Add(ABGRList2);
                    }
                }
                catch (Exception e) {
                }
            }
        }
        //预处理方法
        public byte[] Top(byte[] all)
        {
            byte[] outstr = new byte[width * height * channel];
            try
            {
                byte[] outimg = new byte[all.Length];
                int allsize = all.Length;
                int mean = channel;
                int size = 0;
                IntPtr ssss = Marshal.AllocHGlobal(width * height * channel);
                pre_process_image_for_csharp(all, ref allsize, width, height, channel, ref mean, ref ssss, ref size);              
                Marshal.Copy(ssss, outstr, 0, width * height * channel);
                //最后释放掉
                Marshal.FreeHGlobal(ssss);
            }
            catch (Exception e) {
                
                MessageBox.Show(e.ToString());
            }
            return outstr;
        }
        //后处理结构体
        public struct prediction
        {
            public int class_label;
            public float confidence;
            public float left;
            public float top;
            public float right;
            public float bot;
        }
        //后处理线程及方法
        public void End()
        {
           
            byte[] outstr = new byte[returnnum];
            int workStationCount = 63375;
            int id = 0;
            int length = returnnum;
            int size = Marshal.SizeOf(typeof(prediction));
            prediction[] infos = new prediction[Dllnum];
            while (endbool)
            {
                if (listudpout.Count > 0)
                {
                    try
                    {
                        //结果队列
                        listout1 = new List<prediction>();
                        outstr = listudpout[0];
                        id = udplist2[0];
                        IntPtr infosIntptr = Marshal.AllocHGlobal(workStationCount);
                        richTextBox1.AppendText("后处理");
                        Cv2.WaitKey(100);
                        post_process_for_csharp(outstr, ref length, ref infosIntptr, ref workStationCount);
                        for (int inkIndex = 0; inkIndex < Dllnum; inkIndex++)
                        {
                            IntPtr ptr = (IntPtr)((long)infosIntptr + inkIndex * size);
                            infos[inkIndex] = (prediction)Marshal.PtrToStructure(ptr, typeof(prediction));
                        }
                        Marshal.FreeHGlobal(infosIntptr);
                        listout1 = new List<prediction>();
                        foreach (prediction info in infos)
                        {
                            listout1.Add(info);
                        }
                        listudpout.RemoveAt(0);
                        udplist2.RemoveAt(0);
                        listall.Add(id,listout1);

                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        //FPGA配置界面
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form7 form = new Form7();
            form.ShowDialog();
        }
        //打印
        private void button6_Click(object sender, EventArgs e)
        {
            string strall = "";
            byte[] UDPOUT = new byte[returnnum * time];
            UDPOUT = listudpout[2];
            for (int u = 0; u < listudpout[2].Length; u++)
            {
                strall = strall + UDPOUT[u].ToString("X2") + "\r\n";
            }
            FileStream fileStream = new FileStream(Environment.CurrentDirectory + "\\" + "out.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
            streamWriter.Write(strall);
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
            StreamReader sr = new StreamReader("F:\\git\\source-code\\SPDP\\SPDP\\bin\\Debug\\新建文件夹\\1.txt", Encoding.Default);
            String line;
            int q = 0;
            byte[] d = new byte[81120];
            while ((line = sr.ReadLine()) != "1")
            {
                if (line == null)
                {
                    break;
                }
                d[q] = (byte)int.Parse(line, System.Globalization.NumberStyles.HexNumber);
                q = q + 1;
            }

        }
        //类别配置
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.ShowDialog();
        }
        //图像画检测框
        public void ImageWrite()
        {
            OpenCvSharp.Point leftTopP = new OpenCvSharp.Point();
            OpenCvSharp.Point rightbotP = new OpenCvSharp.Point();
            List<prediction> listoutimg = new List<prediction>();
            Font font = new Font("微软雅黑", fontSize, FontStyle.Regular);   //定义字体
            Brush whiteBrush = new SolidBrush(Color.Red);  //白笔刷，画文字用
            prediction prediction = new prediction();
            Mat src = new Mat();
            float rectX = 0;
            float rectY = 0;
            while (true)
            {
                if (listall.ContainsKey(ABGRKEND))
                {
                    try
                    {
                        for (int c = 0; c < time; c++)
                        {
                            listoutimg = listall[ABGRKEND];
                            src = listimag1[0].Clone();
                            listall.Remove(ABGRKEND);
                            listimag1.RemoveAt(0);                          
                            if (true)
                            {
                                Bitmap bitmap = null;
                                for (int i = 0; i < listoutimg.Count; i++)
                                {
                                    prediction = listoutimg[i];
                                    if (prediction.class_label >= 0 && prediction.confidence > accuracy&& prediction.class_label <= classlist.Count+1)
                                    {
                                        //实时的画矩形
                                        if ((float)prediction.left < (float)prediction.right)
                                        {
                                            leftTopP.X = (int)((float)prediction.left * width);
                                            leftTopP.Y = (int)((float)prediction.top * height);
                                            rightbotP.X = (int)((float)prediction.right * width);
                                            rightbotP.Y = (int)((float)prediction.bot * height);
                                            rectX = (int)((float)prediction.left * width);
                                            rectY = (int)((float)prediction.top * height - 16);
                                        }
                                        else {
                                            leftTopP.X = (int)((float)prediction.right * width);
                                            leftTopP.Y = (int)((float)prediction.bot * height);
                                            rightbotP.X = (int)((float)prediction.left * width);
                                            rightbotP.Y = (int)((float)prediction.top * height);
                                            rectX = (int)((float)prediction.right * width);
                                            rectY = (int)((float)prediction.bot * height - 16);
                                        }
                                        try
                                        {
                                            Cv2.Rectangle(src, leftTopP, rightbotP, Scalar.Red, 1, LineTypes.Link4, 0);
                                        }
                                        catch (Exception) { }
                                        bitmap = BitmapConverter.ToBitmap(src);
                                        Graphics g = Graphics.FromImage(bitmap);
                                        string str = classlist[prediction.class_label] + " " + (int)(prediction.confidence * 100) + "%";
                                        float textWidth = str.Length * fontSize; //文本的长度
                                        float rectWidth = str.Length * (fontSize + 40);
                                        float rectHeight = fontSize + 40;
                                        //声明矩形域
                                        RectangleF textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);
                                        g.DrawString(str, font, whiteBrush, textArea);
                                        src = BitmapConverter.ToMat(bitmap);

                                    }
                                }

                            }
                            listimage2.Add(src.Clone());
                            ABGRKEND = ABGRKEND + 1;
                            richTextBox1.AppendText(ABGRKEND.ToString()+"__"+ABGRLiatNum1.ToString()+"\r\n");
                        }
                    }
                    catch (Exception)
                    {
                        listimag1.RemoveAt(0);
                        ABGRKEND = ABGRKEND + 1;
                        //MessageBox.Show(e.ToString());
                    }
                }
            }
            //实时的画之前已经画好的矩形


        }
        //识别展示
        private void button7_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
        }
        //原视频展示
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Mat src3 = new Mat();
                if (Model != 4)
                {
                    if (ABGRKEND > endnum && listimag.Count > endnum)
                    {
                        src3 = listimag[0];
                        listimag.RemoveAt(0);
                        //listimag.TryDequeue(out src3);
                        Bitmap bitmap = BitmapConverter.ToBitmap(src3);
                        imgshow = bitmap;
                        endnum = endnum + 1;
                    }
                }
                else
                {
                    src3 = listimag[0];
                    listimag.RemoveAt(0);
                    Bitmap bitmap = BitmapConverter.ToBitmap(src3);
                    imgshow = bitmap;
                }
            }
            catch (Exception) { }
            try
            {
                e.Graphics.DrawImage(imgshow, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            }
            catch (Exception) { }
        }
        //视频配置
        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Form10 form = new Form10();
            form.Show();
        }
        //雷达信号处理模式更改
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {

        }//unsafe
        //CMD
        void CMD(string str) {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序
                          //向cmd窗口发送输入信息
                p.StandardInput.WriteLine(str + "&exit");
                p.StandardInput.AutoFlush = true;
                p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                Mat mat = new Mat(appconfig.AppSettings.Settings["PYimgpath"].Value + "\\"
                                + name.Substring(0, name.Length - 4) + "-" + appconfig.AppSettings.Settings["PYimgname"].Value + ".png");
                listimag.Add(mat);
                pictureBox1.Invalidate();
            }
            catch (Exception) {
                MessageBox.Show("请检查数据文件名称是否存在空格等关键字节");
            }

        }
        //识别处理
        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Form11 form = new Form11();
            form.ShowDialog();
        }
        //雷达信号配置
        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Form12 form = new Form12();
            form.ShowDialog();
        }
        //上一张
        private void button11_Click(object sender, EventArgs e)
        {
            if (Model == 4 && comboBox2.Text == "SPWVD")
            {
                int i = list.LastIndexOf(name);
                if (i - 1 >= 0)
                {
                    name = list[i - 1].ToString();
                    this.listBox1.SelectedIndex = i - 1;
                }
                path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                char[] charTemp = { '\\' };
                string[] pypath = path.Split(charTemp);
                string str = "";
                foreach (string u in pypath)
                {
                    str = str + u + "/";
                }
                str = str + name + " ";
                Console.WriteLine(Cmdstr + str + Cmdstr1);
                CMD(Cmdstr + str + Cmdstr1);
            }
            else
            {
                MessageBox.Show("请选择资源及处理模式！");
            }
        }
        //下一张
        private void button13_Click(object sender, EventArgs e)
        {
            if (Model == 4 && comboBox2.Text == "SPWVD")
            {
                int i = list.LastIndexOf(name);
                if (list.Count > i + 1)
                {
                    name = list[i + 1].ToString();
                    this.listBox1.SelectedIndex = i + 1;
                }
                path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                char[] charTemp = { '\\' };
                string[] pypath = path.Split(charTemp);
                string str = "";
                foreach (string u in pypath)
                {
                    str = str + u + "/";
                }
                str = str + name + " ";
                Console.WriteLine(Cmdstr + str + Cmdstr1);
                CMD(Cmdstr + str + Cmdstr1);
            }
            else {
                MessageBox.Show("请选择资源及处理模式！");
            }
        }
        //确定
        private void button12_Click(object sender, EventArgs e)
        {
            if (Model == 4 && comboBox2.Text == "SPWVD")
            {
                path = appconfig.AppSettings.Settings["SystemImagePath"].Value;
                name = listBox1.SelectedItem.ToString();
                char[] charTemp = { '\\' };
                string[] pypath = path.Split(charTemp);
                string str = "";
                foreach (string u in pypath)
                {
                    str = str + u + "/";
                }
                str = str + name + " ";
                Console.WriteLine(Cmdstr + str + Cmdstr1);
                CMD(Cmdstr + str + Cmdstr1);
            }
            else
            {
                MessageBox.Show("请选择资源及处理模式！");
            }
        }
    }
}
