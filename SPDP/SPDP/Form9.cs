using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDP
{
    public partial class Form9 : Form
    {
        int endnum = 0;
        Image imgshow;
        Bitmap bmpInitial = (Bitmap)System.Drawing.Image.FromFile(System.Environment.CurrentDirectory + "\\load.jpg");
        public Form9()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Thread threadA = new Thread(read);//创建线程
            threadA.Start();
            imgshow = bmpInitial;

        }

        public void read() {
            while (true) {
                Cv2.WaitKey(500);
                pictureBox1.Focus();
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            OpenCvSharp.Size size = new OpenCvSharp.Size(pictureBox1.Width, pictureBox1.Height);
            if (true)
            {
                try
                {
                    Bitmap src3;
                    Mat mat = new Mat();
                    if (Form1.ABGRKEND > endnum)
                    {
                        Console.WriteLine(endnum+"a");
                        mat=Form1.listimage2[endnum];
                        src3 = BitmapConverter.ToBitmap(mat);
                        label1.Text = endnum.ToString();
                        imgshow = src3;
                        endnum =endnum+1;
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                try
                {
                    e.Graphics.DrawImage(imgshow, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                }
                catch (Exception) { }
            }
        }
    }
}
