﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLabel
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine(SystemInformation.MouseWheelPresent.ToString());
            Console.WriteLine(SystemInformation.MouseWheelScrollLines.ToString());
            Console.WriteLine(SystemInformation.NativeMouseWheelSupport.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }
    }
}
