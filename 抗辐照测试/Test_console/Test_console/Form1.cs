using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;

namespace Test_console
{
    public partial class FormConsole : Form
    {
        //****constant defintion******//
        const int CmdLen = 8;
        const int RspLen = 2;
        const int ReqLen = 7;
        const int DatLen = 256;

        const Byte UpReq = 0x80;
        const Byte UpRsp = 0xC0;
        const Byte UpData = 0xA0;
        const Byte DownCmd = 0x20;
        const Byte DownRsp = 0x40;

        const Byte OK = 0x33;
        const Byte Retry = 0x11;
        const Byte Restart = 0x22;

        const Byte SelfTest = 0xFF;
        const Byte Reset = 0x01;
        const Byte ManualTest = 0x02;
        const Byte AutoTest = 0x03;
        const Byte TEST_QUIT = 0x1f;
        const Byte TEST_ON = 0x0;
        //PeripheralID//
        const Byte WAIT = 0x00;
        const Byte MEM = 0x01;
        const Byte INSTRUC_SET = 0x02;
        const Byte LINK_PORT = 0x03;
        const Byte SDRAM = 0x04;
        const Byte DMA = 0x05;

        const int SUCCESS = 0;
        const int FAILURE = -1;

        const Byte TEST_SUCCESS = 1;
        const Byte TEST_FAILURE = 0;
        const Byte AutoTestNum = 100;
        const int ErrBitTH = 100;

        const Byte StopTest = 1;
        const Byte NormalTest = 0;
        //fpga parameter//
        //const int MaxRecvByteNum = (CmdLen+3)*2+2;
        //const int MaxRecvDataNum = CmdLen;
        const int MaxSendDataNum = DatLen + 6 + 2;
        //const int MaxSendByteNum = (MaxSendDataNum)*2+2;  

        //console parameter//
        const int CMaxSendDataNum = CmdLen + 3;    //CmdLen +1//
        //const int CMaxSendByteNum = (CMaxSendDataNum)*2+2;   //(CMaxSendDataNum+2)*2+2 used in PPP mode//
        //const int CMaxRecvByteNum = (MaxSendDataNum)*2+2;
        const int CMaxRecvDataNum = MaxSendDataNum;

        //PPP status//
        const Byte IDLE = 0x0;
        const Byte ByteRx = 0x10;
        const Byte SOH = 0x7E;
        const Byte EOT = 0x7E;
        const Byte ESC = 0x7D;
        const Byte CRC = 0xFF;
        //for memory test////
        const Byte MEMREQ = 0x11;
        const Byte MEMQUIT = 0x12;
        ///const Byte MEMLen = 0x01;
        //////
        UInt16[] crc_tab = new UInt16[256]{
        0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7,
        0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef,
        0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6,
        0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de,
        0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485,
        0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d,
        0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4,
        0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc,
        0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823,
        0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b,
        0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12,
        0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a,
        0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41,
        0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49,
        0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70,
        0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78,
        0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f,
        0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067,
        0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e,
        0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256,
        0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d,
        0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
        0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c,
        0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634,
        0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab,
        0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3,
        0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a,
        0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92,
        0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9,
        0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1,
        0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8,
        0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0
        };

        enum TestID : byte
        {
            Mem = 1,
            InstSet = 2,
            Link = 3,
            Sdram = 4,
            Dma = 5
        }
        //****** globle variable definition*****//
        public SerialPort sp = new SerialPort();
        public delegate void RecvEventEventHandler(Byte[] InBuffer);
        public static RecvEventEventHandler RecvEvent = null;

        public Boolean Dma_Link_Flag = false;
        public Boolean mem_test_flag = false;
        public UInt64 MemTest_count = 0;
        private bool isSetProperty = false;
        private bool IsAutoTest = false;
        private bool PeripheryIDCheck = false;
        private bool MemDataModeCheck = false;
        private string CmdName;

        private Byte PeripheralID = 0;
        private Byte MemDataMode = 0x10;
        private UInt16 TestNum = 0;
        private Byte RetryNum = 0;
        private UInt16 TestTimes = 0;
        private int TolErrBitNum = 0;
        static int RecvByteNum = 0;
        static int RetryFlag = 0;
        private int MemErrBitNum = 0;
        private Byte MemQuitFlag = 0;
        private Byte MemTestOn = 0;
        //private Byte MemTestTimes = 0;

        string MyTime;
        //string StartTestTime;
        System.DateTime STime;
        System.DateTime ETime;
        System.TimeSpan TSpan;
        string TolDoseRate;
        //DateTime MyDT;
        Byte MyYear1;
        Byte MyYear2;
        Byte MyMonth;
        Byte MyDay;
        Byte MyHour;
        Byte MyMin;
        Byte MySec;
        Byte MyMilliSec1;
        Byte MyMilliSec2;

        //string FilePath = @"E:\wangfy\Task6_fpga\Test_ErrDat\TestFile.txt";
        //string DirPath = @"E:\wangfy\Task6_fpga\Test_ErrDat\";
        static string dirpath = System.IO.Directory.GetCurrentDirectory();
        string DirPath = System.IO.Directory.GetCurrentDirectory();//错误数据存放位置

        string FilePath;
        string FileName;

        string LogFilePath;
        string LogName = "0901_TestLog.log";

        UInt32 MyRecvData;
        ////
        Byte[] SelfTestDat = new Byte[DatLen];
        //*********************************//
        public FormConsole()
        {
            InitializeComponent();
        }


        private void FormConsole_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;   //固定调整窗口大小
            this.MinimumSize = this.Size;
            this.MaximizeBox = true;
            int Index;

            Control.CheckForIllegalCrossThreadCalls = false;
            //添加端口名称
            ComNum.Items.AddRange(SerialPort.GetPortNames());
            ComNum.SelectedIndex = ComNum.Items.Count > 0 ? 0 : -1;
            //列出常用的波特率
            ComBaud.Items.Add("300");   ///index 0//
            ComBaud.Items.Add("600");
            ComBaud.Items.Add("1200");
            ComBaud.Items.Add("2400");
            ComBaud.Items.Add("4800");
            ComBaud.Items.Add("9600");
            ComBaud.Items.Add("19200");
            ComBaud.Items.Add("38400");
            ComBaud.Items.Add("43000");
            ComBaud.Items.Add("56000");
            ComBaud.Items.Add("57600");
            ComBaud.Items.Add("115200");
            ComBaud.SelectedIndex = 5;
            //列出奇偶校验位
            ComParity.Items.Add("无");
            ComParity.Items.Add("奇校验");
            ComParity.Items.Add("偶校验");
            ComParity.SelectedIndex = 0;
            //列出数据位
            ComData.Items.Add("8");
            ComData.Items.Add("7");
            ComData.Items.Add("6");
            ComData.Items.Add("5");
            ComData.SelectedIndex = 0;
            //列出停止位
            ComStop.Items.Add("0");
            ComStop.Items.Add("1");
            ComStop.Items.Add("1.5");
            ComStop.Items.Add("2");
            ComStop.SelectedIndex = 1;
            //////////////////////////
            //tbinfo.Text = "提示：该测试界面用于控制FPGA对test chip芯片的测试，";
            tbErrBitNumTH.Text = ErrBitTH.ToString();
            //***create directory and file to restore error data of L2 in the test************//

            //Console.WriteLine(dirpath);
            Console.WriteLine(DirPath);
            //--------------------------create directory-------------------------------
            try
            {
                if (!Directory.Exists(DirPath))
                {
                    Directory.CreateDirectory(DirPath);
                }
            }
            catch (DirectoryNotFoundException ExcepInfo)
            {
                Console.WriteLine(ExcepInfo.Message);
                MessageBox.Show("DirectoryNotFoundException", "Error!");
            }
            //--------------------------create subdirectory---------------------------
            DirectoryInfo dir = new DirectoryInfo(DirPath);
            dir.CreateSubdirectory(Enum.GetName(typeof(TestID), 1));
            dir.CreateSubdirectory(Enum.GetName(typeof(TestID), 2));
            dir.CreateSubdirectory(Enum.GetName(typeof(TestID), 3));
            dir.CreateSubdirectory(Enum.GetName(typeof(TestID), 4));
            dir.CreateSubdirectory(Enum.GetName(typeof(TestID), 5));
            //-------------------------logfile----------------------------------------
            LogFilePath = DirPath + "\\" + LogName;
            if (!File.Exists(LogFilePath))
            {
                FileStream fs = File.Create(LogFilePath);
                fs.Close();
                fs.Dispose();

            }

            //StartTestTime = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //using (File.Create(FilePath)) ;
            WriteLineFile(LogFilePath, "---------------------- " + System.DateTime.Now + "----------------------");
            WriteLineFile(LogFilePath, "PeripheryID = 01:MEM, 02:InstructSet, 03:Link Port, 04:SDRAM, 05:DMA");

            ///////
            for (Index = 0; Index < DatLen; Index++)
            {
                SelfTestDat[Index] = (Byte)Index;
            }
            //**********************//
            //register in serial receive event
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);

            //register in receive data process method//
            RecvEvent += new RecvEventEventHandler(RecvProc);


        }

        /// <summary>
        /// receive event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = sp.BytesToRead;
            //int Index = 0;

            //Console.WriteLine(n);
            if (n == RecvByteNum)
            {
                if (timer.Enabled)
                {
                    this.Invoke(new MethodInvoker(() => { timer.Stop(); }));
                    Console.WriteLine("stop timer!");
                }

                Byte[] Inbuffer = new Byte[n];
                sp.Read(Inbuffer, 0, n);
                sp.DiscardInBuffer();

                if (RecvEvent != null)
                {
                    //Console.WriteLine("RecvByteNum = {0}", n);
                    //Console.WriteLine("CmdName = {0}", CmdName);
                    RecvEvent(Inbuffer);

                }
                //for(Index=0;Index<n;Index++)
                //Console.WriteLine(Inbuffer[Index]);

                n = 0;

            }

        }

        public void RecvProc(Byte[] InBuffer)
        {
            Byte FrmType = 0, TestSel = 0;
            UInt16 FrameNum = 0, FrameSeq = 0;
            int Status = 0, Index = 0, SelfTestErr = 0, FrameLen = 0;
            Byte[] Recv_data = new Byte[MaxSendDataNum - 3]; //3=1 frmtype,2 crc//
                                                             // Byte[] CmdData = new Byte[CmdLen];
            Byte[] RspData = new Byte[RspLen];
            //Byte[] SelfTestDat = new Byte[DatLen]; 
            //初始化命令数组的值//
            /*for (Index = 0; Index < CmdLen; Index++)
            {
               CmdData[Index] = 0;
            } */
            ////////////////////////////////////////////////////////////////////////            
            switch (CmdName)
            {
                case "SelfTest":
                    Status = RecvPack(ref FrmType, ref TestSel, Recv_data, InBuffer);
                    //加入对dsp和FPGA通讯状态的诊断信息//
                    if (TestSel == TEST_QUIT)
                    {
                        tbResult.AppendText("SelfTest: DSP is unready, FPGA quit!\r\n");
                        WriteLineFile(LogFilePath, "DSP is unready, FPGA quit!");
                        MessageBox.Show("FPGA quit, please restart your test!");
                        ConsoleInit();
                        break;
                    }
                    else if (Status != SUCCESS)
                    {
                        tbResult.AppendText("SelfTest:Receive data CRC error!\r\n");
                    }
                    else
                    {
                        for (Index = 0; Index < DatLen; Index++)
                        {
                            if (Recv_data[Index] != SelfTestDat[Index])
                            {
                                SelfTestErr = 1;
                                break;
                            }
                        }
                    }

                    if (SelfTestErr == 1)
                    {
                        tbResult.AppendText("SelfTest failed!\r\n");
                        SelfTestErr = 0;
                        bnOverFail.BackColor = Color.Red;
                        WriteLineFile(LogFilePath, "Failed!");  //add//
                    }
                    else
                    {
                        tbResult.AppendText("SelfTest succeed!\r\n");
                        bnOverSuc.BackColor = Color.Green;
                        WriteLineFile(LogFilePath, "Succeed!");//add//
                    }

                    break;

                case "Reset":
                    Status = RecvPack(ref FrmType, ref TestSel, Recv_data, InBuffer);
                    if (TestSel == TEST_QUIT)
                    {
                        tbResult.AppendText("Reset: DSP is unready, FPGA quit!\r\n");
                        WriteLineFile(LogFilePath, "DSP is unready, FPGA quit!");
                        MessageBox.Show("FPGA quit, please restart your test!");
                        ConsoleInit();
                        break;
                    }
                    switch (Recv_data[0])
                    {
                        case OK:
                            tbResult.AppendText("Reset succeed!\r\n");
                            bnOverSuc.BackColor = Color.Green;
                            WriteLineFile(LogFilePath, "Succeed!");
                            break;
                        case Retry:
                            tbResult.AppendText("PC retry!!!\r\n");
                            bnRetry.BackColor = Color.Red;
                            //Delay(100000000);
                            Thread.Sleep(1000);
                            RetryFlag = 1;
                            bnCommand_Click(bnReset, null);
                            WriteLineFile(LogFilePath, "PC retry!!!");
                            break;
                        case Restart:
                            tbResult.AppendText("PC retry failed. Please restart!!!\r\n");
                            bnRetry.BackColor = Color.Red;
                            bnOverFail.BackColor = Color.Red;
                            WriteLineFile(LogFilePath, "PC Retry failed. Please restart!!!");
                            break;
                        default: break;

                    }
                    break;

                case "ManualTest":
                    Status = RecvPack(ref FrmType, ref TestSel, Recv_data, InBuffer);

                    //Console.WriteLine("ManualTest:Recv_data[0]={0}", Recv_data[0]);
                    if (TestSel == TEST_QUIT)
                    {
                        tbResult.AppendText("ManualTest: DSP is unready, FPGA quit!\r\n");
                        WriteLineFile(LogFilePath, "DSP is unready, FPGA quit!");
                        MessageBox.Show("FPGA quit, please restart your test!");
                        ConsoleInit();
                        break;
                    }
                    switch (Recv_data[0])
                    {
                        case OK:
                            tbResult.AppendText("Begin to test!\r\n");
                            //bnOverSuc.BackColor = Color.Green;
                            RecvByteNum = ReqLen + 3;
                            CmdName = "Test";
                            WriteLineFile(LogFilePath, "Begin to test!");
                            if (PeripheralID != MEM)
                            {
                                Console.WriteLine("start timer!!!");
                                //System.Timers.Timer;
                                this.Invoke(new MethodInvoker(() => { timer.Start(); }));  ////add 2015/12/12/////
                            }
                            else
                            {
                                //MemQuitFlag = 0;
                                txtTestnum.Text = "0";
                                bnMEMQuit.Text = "测试中...";
                                MemTestOn = 1;
                                bnMEMQuit.BackColor = Color.HotPink;
                            }
                            break;
                        case Retry:
                            tbResult.AppendText("Retry!!!\r\n");
                            bnRetry.BackColor = Color.Red;
                            //Delay(100000000);
                            Thread.Sleep(1000);
                            RetryFlag = 1;
                            bnCommand_Click(bnManualTest, null);
                            WriteLineFile(LogFilePath, "PC retry!!!");
                            break;
                        case Restart:
                            tbResult.AppendText("PC retry failed. Please restart!!!\r\n");
                            bnRetry.BackColor = Color.Red;
                            bnOverFail.BackColor = Color.Red;
                            WriteLineFile(LogFilePath, "PC Retry failed. Please restart!!!");
                            break;
                        default: break;

                    }
                    break;

                case "AutoTest":
                    Status = RecvPack(ref FrmType, ref TestSel, Recv_data, InBuffer);

                    //Console.WriteLine("AutoTest:Recv_data[0]={0}", Recv_data[0]);
                    if (TestSel == TEST_QUIT)
                    {
                        tbResult.AppendText("AutoTest: DSP is unready, FPGA quit!\r\n");
                        WriteLineFile(LogFilePath, "DSP is unready, FPGA quit!");
                        MessageBox.Show("FPGA quit, please restart your test!");
                        ConsoleInit();
                        break;
                    }
                    switch (Recv_data[0])
                    {
                        case OK:
                            tbResult.AppendText("Begin to test!\r\n");
                            //bnOverSuc.BackColor = Color.Green;
                            RecvByteNum = ReqLen + 3;
                            CmdName = "Test";
                            WriteLineFile(LogFilePath, "Begin to test!");
                            if (PeripheralID != MEM)
                            {
                                Console.WriteLine("start timer!");
                                this.Invoke(new MethodInvoker(() => { timer.Start(); }));  ////add 2015/12/12/////
                            }
                            else
                            {
                                //MemQuitFlag = 0;
                                //txtTestnum.Text = "Null";
                                bnMEMQuit.Text = "测试中...";
                                MemTestOn = 1;
                                bnMEMTestResult();
                                bnMEMQuit.BackColor = Color.HotPink;
                            }
                            break;
                        case Retry:
                            tbResult.AppendText("Retry!!!\r\n");
                            bnRetry.BackColor = Color.Red;
                            //Delay(100000000);
                            Thread.Sleep(1000);
                            RetryFlag = 1;
                            bnCommand_Click(bnAutoTest, null);
                            WriteLineFile(LogFilePath, "PC retry!!!");
                            break;
                        case Restart:
                            tbResult.AppendText("PC retry failed. Please restart!!!\r\n");
                            bnRetry.BackColor = Color.Red;
                            bnOverFail.BackColor = Color.Red;
                            WriteLineFile(LogFilePath, "PC Retry failed. Please restart!!!");
                            break;
                        default: break;

                    }
                    break;

                case "Test":  //Console.WriteLine(CmdName);
                    Status = RecvPack(ref FrmType, ref TestSel, Recv_data, InBuffer);   ///接收上传的FPGA测试结果请求/////
                    //Console.Write("FrmType="); 
                    //Console.WriteLine(FrmType&0xE0);         ///包括上传请求和上传数据////
                    if (Status != SUCCESS)
                    {

                        if (RetryNum == 3)
                        {
                            bnOverFail.BackColor = Color.Red; /////
                            RetryNum = 0;
                            RspData[0] = Restart;
                            tbResult.AppendText("FPGA retry failed, please check and restart!\r\n");
                            WriteLineFile(LogFilePath, PeripheralID.ToString() + ":FPGA retry failed, please check and restart!");
                            break;
                        }
                        else
                        {
                            RetryNum++;
                            bnRetry.BackColor = Color.Red;
                            tbResult.AppendText("Retry " + RetryNum.ToString() + "\r\n");
                            RspData[0] = Retry;
                            WriteLineFile(LogFilePath, PeripheralID.ToString() + ":FPGA retry !!!");
                        }
                        SendPack(DownRsp, RspData, RspLen);
                    }
                    else
                    {
                        bnRetry.BackColor = SystemColors.Control;
                        bnOverFail.BackColor = SystemColors.Control;

                        if (TestSel == TEST_QUIT)
                        {
                            //Thread.Sleep(5000); //delay used for waitting for FPGA quit//
                            tbResult.AppendText("Test: DSP is unready, FPGA quit!\r\n");
                            WriteLineFile(LogFilePath, "DSP is unready, FPGA quit!");
                            MessageBox.Show("FPGA quit, please restart your test!");
                            //MemTestOn = 0;
                            //bnMEMQuit.BackColor = SystemColors.Control;
                            //bnMEMQuit.Text = "等待测试";
                            ConsoleInit();
                            tbTolErrNum.Text = TolErrBitNum.ToString();  //display accumulate total errors//
                            break;
                        }

                        switch (FrmType)
                        {
                            case UpReq:

                                TestTimes = System.BitConverter.ToUInt16(Recv_data, 1);
                                tbtestproc.Text = TestTimes.ToString();


                                //memory测试时PC界面的累积错误数量只代表memory单个模块的测试结果，在退出memory测试时才会将memory测试结果累积起来
                                if ((PeripheralID == MEM) && (MemQuitFlag == 0))
                                {
                                    MemErrBitNum = System.BitConverter.ToInt32(Recv_data, 3);

                                    WriteLineFile(LogFilePath, "Memory test error bit number is " + MemErrBitNum.ToString());
                                    tbTolErrNum.Text = MemErrBitNum.ToString();
                                    bnMEMResult.BackColor = SystemColors.Control;
                                }
                                else
                                {
                                    TolErrBitNum = TolErrBitNum + System.BitConverter.ToInt32(Recv_data, 3); //计算累计出错次数///

                                    tbTolErrNum.Text = TolErrBitNum.ToString(); //demonstrate the total error bit number///
                                }


                                //所有的不需要产生错误数据的情况，测试正确以及某些测试选项///
                                if ((Recv_data[0] == TEST_SUCCESS) || (PeripheralID == INSTRUC_SET))  //只需要判断是否正确，不需要产生错误数据信息///
                                {
                                    bnRetry.BackColor = SystemColors.Control;

                                    RspData[0] = OK;
                                    //Console.Write("Recv_data[0]=");
                                    //Console.WriteLine(Recv_data[0]);

                                    //如果测试正确///???不明所以
                                    if (Recv_data[0] == TEST_SUCCESS)
                                    {     //明白了 别的外设的是否测试正确根据TolErrBitNum，mem的正确与否根据memerrbitnum
                                        if ((PeripheralID == MEM) & (MemErrBitNum != 0)) //add 2016_2_23 after test//
                                        {
                                            bnMEMResult.BackColor = SystemColors.Control;
                                            tbResult.AppendText("Test " + TestTimes.ToString() + ": Failed!\r\n");
                                            bnOverFail.BackColor = Color.Red;
                                            WriteLineFile(LogFilePath, "Test " + TestTimes.ToString() + ": Failed!");
                                        }
                                        else
                                        {
                                            if ((PeripheralID == MEM)) bnMEMResult.BackColor = SystemColors.Control;
                                            tbResult.AppendText("Test " + TestTimes.ToString() + ": Succeed!\r\n");
                                            mem_test_flag = true;
                                            bnOverSuc.BackColor = Color.Green;
                                            WriteLineFile(LogFilePath, "Test " + TestTimes.ToString() + ": Succeed!");
                                        }
                                    }


                                    //测试失败
                                    else if (Recv_data[0] == TEST_FAILURE)
                                    {
                                        tbResult.AppendText("Test " + TestTimes.ToString() + ": Failed!\r\n");
                                        bnOverFail.BackColor = Color.Red;
                                        WriteLineFile(LogFilePath, "Test " + TestTimes.ToString() + ": Failed!");
                                    }

                                    //测试错误bit数目超过某个阈值，结束测试
                                    if (TolErrBitNum >= Convert.ToUInt32(tbErrBitNumTH.Text, 10))    ///通知FPGA停止进行测试/////
                                    {
                                        RspData[1] = StopTest;
                                    }
                                    else
                                    {
                                        RspData[1] = NormalTest;
                                    }

                                    SendPack(DownRsp, RspData, RspLen);
                                    /////////////////////////////////////////////////
                                    if (TolErrBitNum >= Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                                    {
                                        bnQuit.BackColor = Color.Red;
                                        if ((TestTimes == TestNum) || (PeripheralID == MEM))
                                        {
                                            tbResult.AppendText("TestID " + PeripheralID.ToString() + ": More errors occur, quit!\r\n");
                                            WriteLineFile(LogFilePath, "More errors occur, quit!!!");
                                        }
                                        else
                                        {
                                            tbResult.AppendText("TestID " + PeripheralID.ToString() + ": More errors occur, wait for fpga quit......\r\n");
                                            WriteLineFile(LogFilePath, "More errors occur, quit!!!");
                                            this.Invoke(new MethodInvoker(() => { timer1.Start(); }));
                                        }
                                        FuncQuit();
                                        break;
                                    }
                                }
                                else
                                {
                                    bnOverFail.BackColor = Color.Red;
                                    RspData[0] = OK;
                                    if (TolErrBitNum >= Convert.ToUInt32(tbErrBitNumTH.Text, 10))    ///通知FPGA停止进行测试/////
                                    {
                                        RspData[1] = StopTest;
                                    }
                                    SendPack(DownRsp, RspData, RspLen);
                                    //将接收数据写进文件中,一次测试结果放一个文件//
                                    MyTime = System.DateTime.Now.ToString("MMddHHmmssffff");

                                    FileName = MyTime;
                                    FilePath = DirPath + "\\" + Enum.GetName(typeof(TestID), PeripheralID) + "\\" + FileName + ".dat";
                                    if (!File.Exists(FilePath))
                                    {
                                        FileStream fs = File.Create(FilePath);
                                        fs.Close();
                                        fs.Dispose();

                                    }
                                    RecvByteNum = CMaxRecvDataNum;  //准备接收错误数据//
                                                                    ///打开计时器，开始计时下次测试完成的时间///
                                    Console.WriteLine("start timer!");
                                    this.Invoke(new MethodInvoker(() => { timer.Start(); }));       ///add 2015/12/12/////
                                    tbResult.AppendText("Test " + TestTimes.ToString() + ": Failure! wait for receiving error data\r\n");
                                    WriteLineFile(LogFilePath, "Test " + TestTimes.ToString() + ": Failed! wait for receivng error data");
                                    break;

                                }
                                //////////////////////////////////////////////
                                if (((Recv_data[0] == TEST_SUCCESS) || (PeripheralID == INSTRUC_SET)) && (((PeripheralID != MEM) & (TestTimes == TestNum)) || (MemQuitFlag == 1))) //不需要上传错误数据的外设测试完成///
                                {

                                    //link id 保持一致
                                    if (Dma_Link_Flag)
                                    {
                                        Dma_Link_Flag = false;
                                        tbResult.AppendText("TestID " + "3" + ":Test over!!!!!!!!!!!!\r\n");
                                    }
                                    else tbResult.AppendText("TestID " + PeripheralID.ToString() + ":Test over!!!!!!!!!!!!\r\n");


                                    bnAutoTest.Enabled = true;
                                    bnManualTest.BackColor = SystemColors.Control;

                                    ETime = System.DateTime.Now;
                                    TSpan = ETime - STime;

                                    if (IsAutoTest & (PeripheralID != DMA))    ///自动测试还没有测试完成///
                                    {
                                        CmdName = "AutoTest";
                                        Thread.Sleep(1000);
                                        PeripheralID = (Byte)(PeripheralID + 0x01);
                                        bnCommand_Click(bnAutoTest, null);
                                    }
                                    else if (IsAutoTest & (PeripheralID == DMA))
                                    {
                                        IsAutoTest = false;
                                        bnManualTest.Enabled = true;
                                        bnAutoTest.BackColor = SystemColors.Control;
                                        tbResult.AppendText("AutoTest over!!!!!!!!!!!!\r\n");
                                        WriteLineFile(LogFilePath, "AutoTest over!!!!!!!!!!!!");
                                        rbZero.Checked = true;
                                        rbMEM.Checked = true;

                                    }

                                    ///////////////////////////////////////////
                                    if (MemQuitFlag == 1)
                                    {
                                        bnMEMQuit.BackColor = SystemColors.Control;
                                        bnMEMQuit.Text = "等待测试";
                                        MemQuitFlag = 0;
                                        MemErrBitNum = 0;
                                        if (!IsAutoTest) txtTestnum.Clear();

                                        tbResult.AppendText("Quit Memory test.....\r\n");
                                        WriteLineFile(LogFilePath, "Quit Memory test.....");
                                    }

                                    WriteLineFile(LogFilePath, "ID " + PeripheralID.ToString() + ":Test over!");
                                    WriteLineFile(LogFilePath, "TolErrBitNum = " + TolErrBitNum.ToString());
                                    WriteLineFile(LogFilePath, ETime.ToString());
                                    WriteLineFile(LogFilePath, "TimeSpan = " + TSpan.TotalSeconds.ToString() + "s");

                                }
                                //添加mem测试退出和自动测试循环
                                if (MemTest_count==TestNum)
                                {
                                    Click1();
                                    if (rbZero.Checked)
                                    {
                                        rbZero.Checked = false;
                                        rbOne.Checked = true;
                                    }
                                    else if (rbOne.Checked)
                                    {
                                        rbOne.Checked = false;
                                        rbChess.Checked = true;
                                    }
                                    else {
                                        PeripheralID = (Byte)(PeripheralID + 0x01);
                                    }
                                    CmdName = "AutoTest";
                                    Thread.Sleep(1000);
                                    bnCommand_Click(bnAutoTest, null);
                                    MemTest_count = 0;
                                }
                                //自动上传
                                if (PeripheralID == MEM & MemTestOn == 1)
                                {
                                    bnMEMTestResult();
                                }
                                break;

                            case UpData:
                                RspData[0] = OK;
                                //FrameNum = (Byte)(FrmType & 0x1F); 
                                FrameNum = System.BitConverter.ToUInt16(Recv_data, 0);
                                FrameSeq = System.BitConverter.ToUInt16(Recv_data, 2);
                                //FrameSeq = Recv_data[0];
                                if (Recv_data[4] == 0)
                                {
                                    FrameLen = DatLen;
                                }
                                else
                                {
                                    FrameLen = Recv_data[4];
                                }

                                tbFrmNum.Text = FrameNum.ToString();
                                tbFrmSeq.Text = FrameSeq.ToString();

                                Console.WriteLine("FrameNum={0}", FrameNum);
                                Console.WriteLine("FrameSeq={0}", FrameSeq);
                                Console.WriteLine("FrameLen={0}", FrameLen);

                                for (Index = 5; Index < FrameLen + 5; Index = Index + 4)
                                {
                                    MyRecvData = System.BitConverter.ToUInt32(Recv_data, Index);
                                    //Console.WriteLine("{0}:{1:x2}",Index, MyRecvData);
                                    WriteLineFile(FilePath, MyRecvData);
                                }

                                SendPack(DownRsp, RspData, RspLen);
                                ///////////////////////////////////////  
                                if (FrameSeq == FrameNum)
                                {
                                    tbResult.AppendText("Done!\r\n");
                                    if (TolErrBitNum >= Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                                    {
                                        bnQuit.BackColor = Color.Red;
                                        if ((TestTimes == TestNum) || (PeripheralID == MEM))
                                        {
                                            tbResult.AppendText("TestID " + PeripheralID.ToString() + ": More errors occur, quit!\r\n");
                                            WriteLineFile(LogFilePath, "More errors occur, quit!!!");
                                        }
                                        else
                                        {
                                            tbResult.AppendText("TestID " + PeripheralID.ToString() + ": More errors occur, wait for fpga quit......\r\n");
                                            WriteLineFile(LogFilePath, "More errors occur, quit!!!");
                                            this.Invoke(new MethodInvoker(() => { timer1.Start(); }));
                                        }
                                        FuncQuit();
                                        break;
                                    }
                                    else if (TolErrBitNum < Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                                    {
                                        if (TestTimes < TestNum)
                                        {
                                            RecvByteNum = ReqLen + 3;   //本次数据接收完成后开始接收下一次测试的请求//
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("start timer!");
                                    this.Invoke(new MethodInvoker(() => { timer.Start(); }));   //add 2015/12/12///打开数据传输间的计时器///// 
                                }
                                ///////////////////////////////////////////////////////////              
                                if ((((PeripheralID != MEM) & (TestTimes == TestNum)) || (MemQuitFlag == 1)) && (FrameSeq == FrameNum))
                                {
                                    tbResult.AppendText(PeripheralID.ToString() + ": Test over!!!!!!!!!!!!\r\n");
                                    bnAutoTest.Enabled = true;
                                    bnManualTest.BackColor = SystemColors.Control;

                                    ETime = System.DateTime.Now;
                                    TSpan = ETime - STime;

                                    if (PeripheralID == MEM) bnMEMResult.BackColor = SystemColors.Control;

                                    if (IsAutoTest & (PeripheralID != DMA))
                                    {
                                        CmdName = "AutoTest";
                                        Thread.Sleep(1000);
                                        PeripheralID = (Byte)(PeripheralID + 0x01);
                                        //Console.WriteLine("PeripheralID = {0}", PeripheralID);
                                        bnCommand_Click(bnAutoTest, null);
                                    }
                                    else if (IsAutoTest & (PeripheralID == DMA))
                                    {
                                        IsAutoTest = false;
                                        bnManualTest.Enabled = true;
                                        bnAutoTest.BackColor = SystemColors.Control;
                                        tbResult.AppendText("AutoTest over!!!!!!!!!!!!\r\n");
                                        WriteLineFile(LogFilePath, "AutoTest over!!!!!!!!!!!!");
                                    }
                                    ///////////////////////////////////////////
                                    if (MemQuitFlag == 1)
                                    {
                                        bnMEMQuit.BackColor = SystemColors.Control;
                                        bnMEMQuit.Text = "等待测试";
                                        MemQuitFlag = 0;
                                        MemErrBitNum = 0;
                                        if (!IsAutoTest) txtTestnum.Clear();

                                        tbResult.AppendText("Quit Memory test.....\r\n");
                                        WriteLineFile(LogFilePath, "Quit Memory test.....");
                                    }

                                    WriteLineFile(LogFilePath, "ID " + PeripheralID.ToString() + ":Test over!");
                                    WriteLineFile(LogFilePath, "TolErrBitNum = " + TolErrBitNum.ToString());
                                    WriteLineFile(LogFilePath, ETime.ToString());
                                    WriteLineFile(LogFilePath, "TimeSpan = " + TSpan.TotalSeconds.ToString() + "s");
                                }

                                break;

                            default: break;

                        }
                    }
                    ////////////////////////////////////////
                    break;

                default: break;

            }

            //Console.WriteLine("Exit receive process......");

        }

        /// <summary>
        /// 检查串口设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private bool CheckPortSetting()
        {
            //检查串口是否设置
            if (ComNum.Text.Trim() == "")
                return false;
            if (ComBaud.Text.Trim() == "")
                return false;
            if (ComData.Text.Trim() == "")
                return false;
            if (ComParity.Text.Trim() == "")
                return false;
            if (ComStop.Text.Trim() == "")
                return false;
            return true;
        }

        private void SetPortProperty()
        {
            //设置串口的属性
            sp.PortName = ComNum.Text.Trim();
            //设置串口波特率
            sp.BaudRate = Convert.ToInt32(ComBaud.Text.Trim());
            //设置停止位
            float f = Convert.ToSingle(ComStop.Text.Trim());
            if (f == 0)
            {
                sp.StopBits = StopBits.None;
            }
            else if (f == 1)
            {
                sp.StopBits = StopBits.One;
            }
            else if (f == 1.5)
            {
                sp.StopBits = StopBits.OnePointFive;
            }
            else if (f == 2)
            {
                sp.StopBits = StopBits.Two;
            }
            else
            {
                sp.StopBits = StopBits.One;
            }
            //设置数据位
            sp.DataBits = Convert.ToInt16(ComData.Text.Trim());
            //设置奇偶校验位
            string s = ComParity.Text.Trim();
            if (s.CompareTo("无") == 0)
            {
                sp.Parity = Parity.None;
            }
            else if (s.CompareTo("奇校验") == 0)
            {
                sp.Parity = Parity.Odd;
            }
            else if (s.CompareTo("偶校验") == 0)
            {
                sp.Parity = Parity.Even;
            }
            else
            {
                sp.Parity = Parity.None;
            }

        }

        //open/close com

        private void btnCom_Click(object sender, EventArgs e)
        {
            ////
            if (MemTestOn == 1)
            {
                MessageBox.Show("请先退出memory测试！", "提示");
                goto Quit;
            }
            else if (sp.IsOpen)
            {

                // normal positon
                this.btnCom.BackColor = Color.Empty;
                ConsoleInit();
                TestOptionInit();

                tbResult.Text = "";
                txtTestnum.Text = "";
                txtRefreshFreq.Text = "6";
                txtTolDoseRate.Text = "";
                tbtestproc.Text = "";
                tbTolErrNum.Clear();
                tbFrmNum.Clear();
                tbFrmSeq.Clear();
                isSetProperty = false;
                //PeripheralID = 0;
                IsAutoTest = false;
                timer.Enabled = false;
                sp.Close();
                MemTest_count = 0;
            }
            else
            {
                //检查串口设置
                if (!CheckPortSetting())
                {
                    MessageBox.Show("串口未设置！", "错误提示");
                }

                if (!isSetProperty)
                {
                    SetPortProperty();
                    isSetProperty = true;
                }

                try
                {
                    sp.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("打开串口时发生错误", "错误提示");
                }
                this.btnCom.BackColor = Color.Lime;
                MemTest_count = 0;
            }
            //设置按钮的状态
            btnCom.Text = sp.IsOpen ? "关闭串口" : "打开串口";

            Quit:;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnCommand_Click(object sender, EventArgs e)
        {
            int Index = 0;
            Byte[] CmdData = new Byte[CmdLen];
            Byte[] TestNumByte = new Byte[2];
            Byte[] RefreshFreqByte = new Byte[2];
            UInt16 RefreshFreq = 0;
            //-------------------------------------//
            if (sp.IsOpen)
            {
                ////
                if (MemTestOn == 1)
                {
                    MessageBox.Show("请先退出memory测试！", "提示");
                    goto Quit;
                }

                //初始化命令数组的值//
                for (Index = 0; Index < CmdLen; Index++)
                {
                    CmdData[Index] = 0;
                }

                if (sender != bnAutoTest)
                    IsAutoTest = false;
                //相关控件初始化//
                if (!IsAutoTest && (RetryFlag == 0))
                {
                    ConsoleInit();
                    tbResult.Text = "";
                    tbtestproc.Text = "";
                    //tbTolErrNum.Clear();
                    tbFrmNum.Clear();
                    tbFrmSeq.Clear();
                }

                bnMEMResult.BackColor = SystemColors.Control;
                bnMEMQuit.BackColor = SystemColors.Control;
                bnMEMQuit.Text = "等待测试";
                ////
                if (sender == bnSelfTest)
                {
                    CmdName = "SelfTest";
                    bnSelfTest.BackColor = Color.Gold;
                    if (RetryFlag == 0)
                        tbResult.AppendText("SelfTest:Begin selftest....\r\n");
                    //txtTestnum.Clear();

                }
                else if (sender == bnReset)
                {
                    CmdName = "Reset";
                    bnReset.BackColor = Color.Gold;
                    if (RetryFlag == 0)
                        tbResult.AppendText("Reset:reset test chip....\r\n");
                    //txtTestnum.Clear();
                }
                else if (sender == bnManualTest)
                {
                    CmdName = "ManualTest";
                    bnManualTest.BackColor = Color.Gold;
                    bnAutoTest.Enabled = false;
                    if (RetryFlag == 0)
                        tbResult.AppendText("ManualTest:begin to test....\r\n");
                }
                else if (!IsAutoTest & (sender == bnAutoTest))
                {
                    CmdName = "AutoTest";
                    bnAutoTest.BackColor = Color.Gold;
                    bnManualTest.Enabled = false;
                    if (RetryFlag == 0)
                        tbResult.AppendText("AutoTest:begin to test....\r\n");

                }
                //////add write file operation///////
                if (RetryFlag == 0)
                {
                    WriteLineFile(LogFilePath, "");
                    WriteLineFile(LogFilePath, "------ " + CmdName + " ------");
                }
                ////////////////////////////////////
                switch (CmdName)
                {
                    case "SelfTest":
                        CmdData[0] = SelfTest;
                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = DatLen + 3;
                        break;

                    case "Reset":
                        CmdData[0] = Reset;
                        if (txtRefreshFreq.Text == "")
                        {
                            tbResult.Clear();
                            MessageBox.Show("请先设置刷新率！", "错误提示");
                            bnReset.BackColor = SystemColors.Control;
                            break;
                        }
                        try
                        {
                            RefreshFreq = Convert.ToUInt16(txtRefreshFreq.Text, 10);
                            RefreshFreqByte = System.BitConverter.GetBytes(RefreshFreq);  //byte array//
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("字节越界，请重新输入范围内的值！", "错误提示");
                            break;
                        }
                        CmdData[1] = WAIT;                   //PeripheralID=0,DSP wait for test command//
                        CmdData[2] = RefreshFreqByte[0];
                        CmdData[3] = RefreshFreqByte[1];
                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = RspLen + 3;
                        break;

                    case "ManualTest":
                        if (TolErrBitNum > Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                        {
                            MessageBox.Show("错误总数超过阈值！", "退出测试");
                            FuncQuit();
                            tbResult.Clear();
                            break;
                        }

                        if (PeripheralID == MEM)
                        {
                            txtTestnum.Text = "0";
                            if (!MemDataModeCheck) rbZero.Checked = true;
                        }

                        if (!PeripheryIDCheck)
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("请先选择测试接口！", "错误提示");
                            break;
                        }
                        else if (txtTestnum.Text == "")
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("请先设置测试次数！", "错误提示");
                            break;
                        }
                        else if ((PeripheralID != MEM) && (txtTestnum.Text == "0"))
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("测试次数应大于0，请重新设置！", "提示");
                            break;
                        }
                        else if (txtRefreshFreq.Text == "")
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("请设置刷新率！", "错误提示");
                            break;
                        }
                        else
                        {
                            try
                            {
                                TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
                                TestNumByte = System.BitConverter.GetBytes(TestNum);

                                RefreshFreq = Convert.ToUInt16(txtRefreshFreq.Text, 10);
                                RefreshFreqByte = System.BitConverter.GetBytes(RefreshFreq);  //刷新率//
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("字节越界，请重新输入范围内的值！", "错误提示");
                                break;
                            }

                            tbResult.AppendText("The total test number is " + txtTestnum.Text + "\r\n");

                            STime = System.DateTime.Now;
                            WriteLineFile(LogFilePath, STime.ToString());         //开始测试的时间///
                            TolDoseRate = txtTolDoseRate.Text.ToString();         //注量率///
                            WriteLineFile(LogFilePath, "The refresh frequency is  " + txtRefreshFreq.Text.ToString());
                            WriteLineFile(LogFilePath, "The total dose rate is " + TolDoseRate);
                            WriteLineFile(LogFilePath, "The total test number is " + TestNum.ToString()); //add//
                        }

                        //TestErrNum = 0;

                        CmdData[0] = ManualTest;
                        CmdData[1] = (Byte)(PeripheralID | MemDataMode);
                        CmdData[2] = RefreshFreqByte[0];
                        CmdData[3] = RefreshFreqByte[1];
                        CmdData[4] = TestNumByte[0];
                        CmdData[5] = TestNumByte[1];

                        SendPack(DownCmd, CmdData, CmdLen);//
                        RecvByteNum = RspLen + 3;
                        break;

                    case "AutoTest":
                        if (TolErrBitNum > Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                        {
                            MessageBox.Show("错误总数超过阈值！", "退出测试");
                            FuncQuit();
                            tbResult.Clear();
                            break;
                        }

                        IsAutoTest = true;
                        //TestErrNum = 0;
                        //txtTestnum.Text = AutoTestNum.ToString();  //default test number///
                        //TestNum = AutoTestNum;  
                        if (!PeripheryIDCheck)       //如果自动测试中没有选择测试接口的话默认从PeripheralID=1开始///
                        {
                            PeripheralID = MEM;
                            rbMEM.Checked = true;
                            if (!MemDataModeCheck) rbZero.Checked = true;
                        }
                        //per id
                        SelectPeripheralID(PeripheralID);
                        //if (PeripheralID == MEM)
                        //    txtTestnum.Text = "0";

                        if (txtTestnum.Text == "")
                        {
                            tbResult.Clear();
                            bnAutoTest.BackColor = SystemColors.Control;
                            bnManualTest.Enabled = true;
                            MessageBox.Show("请先设置测试次数！", "错误提示");
                            break;
                        }
                        else if (txtTestnum.Text == "0")
                        {
                            tbResult.Clear();
                            bnAutoTest.BackColor = SystemColors.Control;

                            bnManualTest.Enabled = true;
                            MessageBox.Show("测试次数应大于0，请重新设置！", "提示");
                            break;
                        }
                        else if (txtRefreshFreq.Text == "")
                        {
                            tbResult.Clear();
                            bnAutoTest.BackColor = SystemColors.Control;
                            bnManualTest.Enabled = true;
                            MessageBox.Show("请设置刷新率！", "错误提示");
                            break;
                        }
                        else
                        {
                            try
                            {
                                TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
                                TestNumByte = System.BitConverter.GetBytes(TestNum);
                                RefreshFreq = Convert.ToUInt16(txtRefreshFreq.Text, 10);
                                RefreshFreqByte = System.BitConverter.GetBytes(RefreshFreq);  //刷新率//
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("字节越界，请重新输入范围内的值！", "错误提示");
                                break;
                            }
                        }
                        CmdData[0] = AutoTest;
                        CmdData[1] = (Byte)(PeripheralID | MemDataMode);
                        CmdData[2] = RefreshFreqByte[0];
                        CmdData[3] = RefreshFreqByte[1];
                        //CmdData[2] = AutoTestNum;  
                        CmdData[4] = TestNumByte[0];
                        CmdData[5] = TestNumByte[1];

                        tbResult.AppendText("PeripheralID = " + PeripheralID.ToString() + "\r\n");

                        STime = System.DateTime.Now;
                        WriteLineFile(LogFilePath, STime.ToString());  //add time value//
                        TolDoseRate = txtTolDoseRate.Text.ToString();
                        WriteLineFile(LogFilePath, "The refresh frequency is " + txtRefreshFreq.Text.ToString());
                        WriteLineFile(LogFilePath, "The total dose rate is " + TolDoseRate);

                        WriteLineFile(LogFilePath, "PeripheralID = " + PeripheralID.ToString());  //add//
                        WriteLineFile(LogFilePath, "TestNumber = " + TestNum.ToString());   //add//

                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = RspLen + 3;
                        break;
                    default: break;

                }
            }
            else
            {
                MessageBox.Show("串口未打开！", "错误提示");
            }

            if (RetryFlag == 1)
                RetryFlag = 0;
            /////
            Quit:;

        }

        public void handle_test()
        {
            int Index = 0;
            Byte[] CmdData = new Byte[CmdLen];
            Byte[] TestNumByte = new Byte[2];
            Byte[] RefreshFreqByte = new Byte[2];
            UInt16 RefreshFreq = 0;
            //-------------------------------------//
            if (sp.IsOpen)
            {
                ////
                if (MemTestOn == 1)
                {
                    MessageBox.Show("请先退出memory测试！", "提示");
                    goto Quit;
                }

                //初始化命令数组的值//
                for (Index = 0; Index < CmdLen; Index++)
                {
                    CmdData[Index] = 0;
                }

                /*if (sender != bnAutoTest)
                    IsAutoTest = false;
                //相关控件初始化//
                if (!IsAutoTest && (RetryFlag == 0))
                {
                    ConsoleInit();
                    tbResult.Text = "";
                    tbtestproc.Text = "";
                    //tbTolErrNum.Clear();
                    tbFrmNum.Clear();
                    tbFrmSeq.Clear();
                }

                bnMEMResult.BackColor = SystemColors.Control;
                bnMEMQuit.BackColor = SystemColors.Control;
                bnMEMQuit.Text = "等待测试";
                ////
                if (sender == bnSelfTest)
                {
                    CmdName = "SelfTest";
                    bnSelfTest.BackColor = Color.Gold;
                    if (RetryFlag == 0)
                        tbResult.AppendText("SelfTest:Begin selftest....\r\n");
                    //txtTestnum.Clear();

                }
                else if (sender == bnReset)
                {
                    CmdName = "Reset";
                    bnReset.BackColor = Color.Gold;
                    if (RetryFlag == 0)
                        tbResult.AppendText("Reset:reset test chip....\r\n");
                    //txtTestnum.Clear();
                }
                else if (sender == bnManualTest)
                {
                    CmdName = "ManualTest";
                    bnManualTest.BackColor = Color.Gold;
                    bnAutoTest.Enabled = false;
                    if (RetryFlag == 0)
                        tbResult.AppendText("ManualTest:begin to test....\r\n");
                }
                else if (!IsAutoTest & (sender == bnAutoTest))
                {
                    CmdName = "AutoTest";
                    bnAutoTest.BackColor = Color.Gold;
                    bnManualTest.Enabled = false;
                    if (RetryFlag == 0)
                        tbResult.AppendText("AutoTest:begin to test....\r\n");

                }
                //////add write file operation///////
                if (RetryFlag == 0)
                {
                    WriteLineFile(LogFilePath, "");
                    WriteLineFile(LogFilePath, "------ " + CmdName + " ------");
                }*/
                ////////////////////////////////////
                //CmdName = "ManualTest";
                //bnManualTest.BackColor = Color.Gold;
                //bnAutoTest.Enabled = false;
                //if (RetryFlag == 0)
                //tbResult.AppendText("ManualTest:begin to test....\r\n");
                switch (CmdName)
                {
                    case "SelfTest":
                        CmdData[0] = SelfTest;
                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = DatLen + 3;
                        break;

                    case "Reset":
                        CmdData[0] = Reset;
                        if (txtRefreshFreq.Text == "")
                        {
                            tbResult.Clear();
                            MessageBox.Show("请先设置刷新率！", "错误提示");
                            bnReset.BackColor = SystemColors.Control;
                            break;
                        }
                        try
                        {
                            RefreshFreq = Convert.ToUInt16(txtRefreshFreq.Text, 10);
                            RefreshFreqByte = System.BitConverter.GetBytes(RefreshFreq);  //byte array//
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("字节越界，请重新输入范围内的值！", "错误提示");
                            break;
                        }
                        CmdData[1] = WAIT;                   //PeripheralID=0,DSP wait for test command//
                        CmdData[2] = RefreshFreqByte[0];
                        CmdData[3] = RefreshFreqByte[1];
                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = RspLen + 3;
                        break;

                    case "ManualTest":
                        if (TolErrBitNum > Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                        {
                            MessageBox.Show("错误总数超过阈值！", "退出测试");
                            FuncQuit();
                            tbResult.Clear();
                            break;
                        }

                        if (PeripheralID == MEM)
                        {
                            txtTestnum.Text = "0";
                            if (!MemDataModeCheck) rbZero.Checked = true;
                        }

                        if (!PeripheryIDCheck)
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("请先选择测试接口！", "错误提示");
                            break;
                        }
                        else if (txtTestnum.Text == "")
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("请先设置测试次数！", "错误提示");
                            break;
                        }
                        else if ((PeripheralID != MEM) && (txtTestnum.Text == "0"))
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("测试次数应大于0，请重新设置！", "提示");
                            break;
                        }
                        else if (txtRefreshFreq.Text == "")
                        {
                            tbResult.Clear();
                            bnManualTest.BackColor = SystemColors.Control;
                            bnAutoTest.Enabled = true;
                            MessageBox.Show("请设置刷新率！", "错误提示");
                            break;
                        }
                        else
                        {
                            try
                            {
                                TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
                                TestNumByte = System.BitConverter.GetBytes(TestNum);
                                RefreshFreq = Convert.ToUInt16(txtRefreshFreq.Text, 10);
                                RefreshFreqByte = System.BitConverter.GetBytes(RefreshFreq);  //刷新率//
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("字节越界，请重新输入范围内的值！", "错误提示");
                                break;
                            }

                            tbResult.AppendText("The total test number is " + txtTestnum.Text + "\r\n");

                            STime = System.DateTime.Now;
                            WriteLineFile(LogFilePath, STime.ToString());         //开始测试的时间///
                            TolDoseRate = txtTolDoseRate.Text.ToString();         //注量率///
                            WriteLineFile(LogFilePath, "The refresh frequency is  " + txtRefreshFreq.Text.ToString());
                            WriteLineFile(LogFilePath, "The total dose rate is " + TolDoseRate);
                            WriteLineFile(LogFilePath, "The total test number is " + TestNum.ToString()); //add//
                        }

                        //TestErrNum = 0;

                        CmdData[0] = ManualTest;
                        CmdData[1] = (Byte)(PeripheralID | MemDataMode);
                        CmdData[2] = RefreshFreqByte[0];
                        CmdData[3] = RefreshFreqByte[1];
                        CmdData[4] = TestNumByte[0];
                        CmdData[5] = TestNumByte[1];

                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = RspLen + 3;
                        break;

                    case "AutoTest":
                        if (TolErrBitNum > Convert.ToUInt32(tbErrBitNumTH.Text, 10))
                        {
                            MessageBox.Show("错误总数超过阈值！", "退出测试");
                            FuncQuit();
                            tbResult.Clear();
                            break;
                        }

                        IsAutoTest = true;
                        //TestErrNum = 0;
                        //txtTestnum.Text = AutoTestNum.ToString();  //default test number///
                        //TestNum = AutoTestNum;  
                        if (!PeripheryIDCheck)       //如果自动测试中没有选择测试接口的话默认从PeripheralID=1开始///
                        {
                            PeripheralID = MEM;
                            rbMEM.Checked = true;
                            if (!MemDataModeCheck) rbZero.Checked = true;
                        }

                        SelectPeripheralID(PeripheralID);
                        //if (PeripheralID == MEM)
                        //    txtTestnum.Text = "0";

                        if (txtTestnum.Text == "")
                        {
                            tbResult.Clear();
                            bnAutoTest.BackColor = SystemColors.Control;
                            bnManualTest.Enabled = true;
                            MessageBox.Show("请先设置测试次数！", "错误提示");
                            break;
                        }
                        else if (txtTestnum.Text == "0")
                        {
                            tbResult.Clear();
                            bnAutoTest.BackColor = SystemColors.Control;
                            bnManualTest.Enabled = true;
                            MessageBox.Show("测试次数应大于0，请重新设置！", "提示");
                            break;
                        }
                        else if (txtRefreshFreq.Text == "")
                        {
                            tbResult.Clear();
                            bnAutoTest.BackColor = SystemColors.Control;
                            bnManualTest.Enabled = true;
                            MessageBox.Show("请设置刷新率！", "错误提示");
                            break;
                        }
                        else
                        {
                            try
                            {
                                TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
                                TestNumByte = System.BitConverter.GetBytes(TestNum);
                                RefreshFreq = Convert.ToUInt16(txtRefreshFreq.Text, 10);
                                RefreshFreqByte = System.BitConverter.GetBytes(RefreshFreq);  //刷新率//
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("字节越界，请重新输入范围内的值！", "错误提示");
                                break;
                            }
                        }
                        CmdData[0] = AutoTest;
                        CmdData[1] = (Byte)(PeripheralID | MemDataMode);
                        CmdData[2] = RefreshFreqByte[0];
                        CmdData[3] = RefreshFreqByte[1];
                        //CmdData[2] = AutoTestNum;  
                        CmdData[4] = TestNumByte[0];
                        CmdData[5] = TestNumByte[1];

                        tbResult.AppendText("PeripheralID = " + PeripheralID.ToString() + "\r\n");

                        STime = System.DateTime.Now;
                        WriteLineFile(LogFilePath, STime.ToString());  //add time value//
                        TolDoseRate = txtTolDoseRate.Text.ToString();
                        WriteLineFile(LogFilePath, "The refresh frequency is " + txtRefreshFreq.Text.ToString());
                        WriteLineFile(LogFilePath, "The total dose rate is " + TolDoseRate);

                        WriteLineFile(LogFilePath, "PeripheralID = " + PeripheralID.ToString());  //add//
                        WriteLineFile(LogFilePath, "TestNumber = " + TestNum.ToString());   //add//

                        SendPack(DownCmd, CmdData, CmdLen);
                        RecvByteNum = RspLen + 3;
                        break;
                    default: break;

                }
            }
            else
            {
                MessageBox.Show("串口未打开！", "错误提示");
            }

            if (RetryFlag == 1)
                RetryFlag = 0;
            /////
            Quit:;

        }


        private void bnMEMTestResult_Click(object sender, EventArgs e)
        {
            //Byte[] TestNumByte = new Byte[2];
            TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
            //TestNumByte = System.BitConverter.GetBytes(TestNum);
            //while (MemTest_count != TestNum)
            {

                MemTest_count = MemTest_count + 1;


                Byte[] MemCmdDat = new Byte[CmdLen];

                if ((PeripheralID == MEM) && (CmdName == "Test"))
                {
                    if (sender == bnMEMResult)
                    {
                        MemCmdDat[0] = MEMREQ;
                        bnMEMResult.BackColor = Color.Blue;
                        tbResult.AppendText("Return Memory test result.....\r\n");
                        WriteLineFile(LogFilePath, "Return Memory test result.....");
                        Console.WriteLine("MEMREQ");

                        RecvByteNum = ReqLen + 3; //wait for receive up request///

                    }
                    else if (sender == bnMEMQuit)
                    {
                        MemCmdDat[0] = MEMQUIT;
                        MemQuitFlag = 1;
                        MemTestOn = 0;
                        rbChess.Enabled = true;
                        rbOne.Enabled = true;
                        rbZero.Enabled = true;
                        //txtTestnum.Clear();
                        //tbtestproc.Clear();
                        //bnMEMQuit.Text = "停止测试";
                        bnMEMQuit.BackColor = Color.Green;
                        //tbResult.AppendText("Quit Memory test.....\r\n");
                        //WriteLineFile(LogFilePath, "Quit Memory test.....");
                        RecvByteNum = ReqLen + 3; //wait for receive up request///
                    }

                    SendPack(DownCmd, MemCmdDat, CmdLen);

                    //timer.Start(); //Start the timer//
                }
                else  //there is no effect if test other peripheral/// 
                {
                    MessageBox.Show("请选择'存储器'然后开始测试", "无效点击");
                }
                //
                tbResult.AppendText("memtest\r\n");
                //while (!mem_test_flag) ;
                //mem_test_flag = false;
            }
            MemTest_count = 0;
        }
        //--------------------- PPP function definition-------------------///
        public UInt32 SendPack(Byte FrmType, Byte[] Send_data, int SendByteNum)
        {
            int ToalSendByteNum, Index;
            //Byte[] UART_Sendbuffer = new Byte[CMaxSendByteNum]; //used in PPP mode//
            Byte[] PPP_Buffer = new Byte[CMaxSendDataNum];

            ToalSendByteNum = SendByteNum + 3;  //2 :CRC bytes,1:FrmType//

            //Receiver receive data according to the same principle///
            //UartSendByteNum is only used in PPP mode //
            //ToalSendByteNum is the real send bytes number//
            /* switch (FrmType)
             {
                 case DownCmd:
                     PPP_Buffer[0] = FrmType;
                     ToalSendByteNum++;
                     //UartSendByteNum = (SendByteNum + 3) * 2 + 2;
                     break;
                 case DownRsp:
                     PPP_Buffer[0] = FrmType;
                     ToalSendByteNum++;
                     //UartSendByteNum = (SendByteNum + 3) * 2 + 2;
                     break;
                 default: break;
             }*/
            //Console.WriteLine(Send_data.Length);
            //Console.WriteLine(ToalSendByteNum);
            PPP_Buffer[0] = FrmType;
            for (Index = 0; Index < Send_data.Length; Index++)
            {
                PPP_Buffer[Index + 1] = Send_data[Index];
                //Console.WriteLine(Send_data[Index]);
            }
            //UART_Sendbuffer is useless if not in PPP mode//
            ProcPPPSend(PPP_Buffer, ToalSendByteNum);

            //sp.Write(UART_Sendbuffer, 0, UartSendByteNum);
            sp.Write(PPP_Buffer, 0, ToalSendByteNum);

            //Console.WriteLine(UartSendByteNum);

            if (FrmType == DownCmd)
            {
                Console.WriteLine("start timer!!");
                this.Invoke(new MethodInvoker(() => { timer.Start(); }));
            }

            return SUCCESS;
        }

        public int RecvPack(ref Byte FrmType, ref Byte TestSel, Byte[] Recv_data, Byte[] InBuffer)
        {
            Int16 CRCValue;
            int Index = 0;
            int TotalRecvByteNum = InBuffer.Length;


            CRCValue = ProcPPPReceive(InBuffer);

            if (CRCValue != 0)
                return FAILURE;
            else
            {
                FrmType = (Byte)(InBuffer[0] & 0xE0);
                TestSel = (Byte)(InBuffer[0] & 0x1F);

                for (Index = 0; Index < (TotalRecvByteNum - 3); Index++)  //1:FrmType,2:crcvalue//
                {
                    Recv_data[Index] = InBuffer[Index + 1];
                    //Console.Write(Index);
                    //Console.Write(":");
                    //Console.WriteLine(Recv_data[Index]);
                }
            }

            return SUCCESS;
        }

        public Int16 ProcPPPReceive(Byte[] Inbuffer)
        {

            Int16 CRC_Value = 0;
            CRC_Value = PPPGetChecksum(Inbuffer, Inbuffer.Length);
            return CRC_Value;

        }

        public int ProcPPPSend(Byte[] In_buffer, int len)
        {
            Int16 Checksum = 0;

            Checksum = PPPGetChecksum(In_buffer, len - 2);   /*获得2个字节的CRC校验结果,有效数据个数为len-2*/
            In_buffer[len - 1] = (Byte)(Checksum & 0xFF);
            In_buffer[len - 2] = (Byte)((Checksum >> 8) & 0xFF);
            return len;
        }

        public Int16 PPPGetChecksum(Byte[] cp, int len)
        {
            return PPPfcs16(0x0000, cp, len);
        }

        public Int16 PPPfcs16(Int16 fcs, Byte[] cp, int len)
        {
            int k = 0;
            while (len != 0)
            {
                //Console.WriteLine("len={0},k={1},cp={2}",len ,k,cp[k]);
                fcs = (Int16)((fcs << 8) ^ crc_tab[((fcs >> 8) ^ cp[k++]) & 0xff]);
                len--;
            }

            return fcs;
        }
        //************************************************/
        private void rbPeripheryID_CheckedChanged(object sender, EventArgs e)
        {
            PeripheryIDCheck = true;
            if (sender == rbMEM) PeripheralID = MEM;
            else if (sender == rbISet) { PeripheralID = INSTRUC_SET; Dma_Link_Flag = false; }
            //暂时用dma来替代link
            //else if (sender == rbLink)   PeripheralID = LINK_PORT;
            else if (sender == rbLink)
            {
                PeripheralID = LINK_PORT; Dma_Link_Flag = true;
            }

            else if (sender == rbSDRAM)
            {
                PeripheralID = SDRAM; Dma_Link_Flag = false;
            }
            else if (sender == rbDMA)
            {

                PeripheralID = DMA; Dma_Link_Flag = false;
            }

        }
        private void SelectPeripheralID(byte PeripheralID)
        {
            switch (PeripheralID)
            {
                case MEM:
                    {
                        rbMEM.Checked = true;
                        if (!MemDataModeCheck) rbZero.Checked = true;
                        break;
                    }
                case INSTRUC_SET:
                    {
                        rbISet.Checked = true;
                        break;
                    }
                case LINK_PORT:
                    {
                        rbLink.Checked = true;
                        break;
                    }
                case SDRAM:
                    {
                        rbSDRAM.Checked = true;
                        break;
                    }
                case DMA:
                    {
                        rbDMA.Checked = true;
                        break;
                    }
                default:
                    {
                        rbMEM.Checked = true;
                        break;
                    }
            }
        }
        //open/close com
        private void ConsoleInit()
        {
            //清除状态内容//
            //tbResult.Text = "";
            //txtTestnum.Clear();
            tbResult.Multiline = true;
            //状态显示按钮颜色赋初值//
            bnOverSuc.BackColor = SystemColors.Control;
            bnOverFail.BackColor = SystemColors.Control;
            bnTimeOut.BackColor = SystemColors.Control;
            bnRetry.BackColor = SystemColors.Control;
            bnQuit.BackColor = SystemColors.Control;

            bnSelfTest.BackColor = SystemColors.Control;
            bnManualTest.BackColor = SystemColors.Control;
            bnReset.BackColor = SystemColors.Control;
            bnAutoTest.BackColor = SystemColors.Control;
            bnMEMResult.BackColor = SystemColors.Control;
            bnMEMQuit.BackColor = SystemColors.Control;
            bnMEMQuit.Text = "等待测试";
            MemQuitFlag = 0;
            MemTestOn = 0;
            //bnChgChip.BackColor = SystemColors.Control;
            bnAutoTest.Enabled = true;
            bnManualTest.Enabled = true;
            IsAutoTest = false;
            //rbZero.Checked = true;
            //
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
        }
        //测试选项
        private void TestOptionInit()
        {
            rbMEM.Checked = false;
            rbSDRAM.Checked = false;
            rbDMA.Checked = false;
            rbLink.Checked = false;
            rbISet.Checked = false;
            PeripheryIDCheck = false;
            MemDataModeCheck = false;
        }
        ////
        //read file//
        public static string ReadFile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                MessageBox.Show("所读文件不存在", "错误提示");
                return null;
            }
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return new StreamReader(fs).ReadLine();
            }
        }

        //write file//
        public static bool WriteLineFile(string filepath, Object content)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                lock (fs)
                {
                    if (!fs.CanWrite)
                    {
                        throw new System.Security.SecurityException("文件filepath=" + filepath + "是只读文件不能写入");
                    }
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("{0:x8}", content);
                    sw.Dispose();
                    sw.Close();
                    return true;
                }
            }
        }

        public static bool WriteFile(string filepath, Object content)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                lock (fs)
                {
                    if (!fs.CanWrite)
                    {
                        throw new System.Security.SecurityException("文件filepath=" + filepath + "是只读文件不能写入");
                    }
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write("{0}", content);
                    sw.Dispose();
                    sw.Close();
                    return true;
                }
            }
        }
        //超时提示
        private void timer_Tick(object sender, EventArgs e)
        {
            //Yellow shows FPGA does not back response in the request time//
            //Red shows test chip does not finish test in request time //
            bnTimeOut.BackColor = Color.Yellow;
            tbResult.AppendText("Time out!\r\n");
            timer.Stop();
            //取消弹框
            //MessageBox.Show("Time out!");

            ConsoleInit();
            //清理串口缓存数据///
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            //ResetCmd_Click();
            //add
            ReTest();
            handle_test();


        }

        private void bnClearFile_Click(object sender, EventArgs e)
        {

            DialogResult Response = MessageBox.Show("您确定要清除文件夹中的所有文件吗？", "请确认", MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (Response == System.Windows.Forms.DialogResult.Yes)
            {
                if (Directory.Exists(DirPath))
                {
                    DirectoryInfo DirInfo = new DirectoryInfo(DirPath);
                    DirectoryInfo[] SubDirInfo = DirInfo.GetDirectories();
                    FileInfo[] files = DirInfo.GetFiles();
                    try
                    {
                        //DirInfo.Delete(true);  //该操作会同时也把该文件夹和该文件夹下的子文件夹和文件都删除了//
                        foreach (DirectoryInfo di in SubDirInfo)  //删除子文件//
                        {
                            di.Delete(true);
                        }
                        foreach (FileInfo file in files)      //删除文件下的所有文件内容//
                        {
                            file.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("there occur error when delete directory", "Error info");
                    }
                    // FileInfo[] files = DirInfo.GetFiles();
                    // foreach (FileInfo file in files)
                    // {
                    //     file.Delete();
                    // }
                    DirInfo.CreateSubdirectory(Enum.GetName(typeof(TestID), 1));
                    DirInfo.CreateSubdirectory(Enum.GetName(typeof(TestID), 2));
                    DirInfo.CreateSubdirectory(Enum.GetName(typeof(TestID), 3));
                    DirInfo.CreateSubdirectory(Enum.GetName(typeof(TestID), 4));
                    DirInfo.CreateSubdirectory(Enum.GetName(typeof(TestID), 5));
                }
            }
        }


        private void bnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", DirPath);
        }

        private void bnClearErr_Click(object sender, EventArgs e)
        {
            TolErrBitNum = 0;
            tbTolErrNum.Text = "0";
        }

        private void FuncQuit()
        {
            ETime = System.DateTime.Now;
            TSpan = ETime - STime;
            WriteLineFile(LogFilePath, "ID " + PeripheralID.ToString() + ":Test over!");
            WriteLineFile(LogFilePath, "TolErrBitNum = " + TolErrBitNum.ToString());
            WriteLineFile(LogFilePath, ETime.ToString());
            WriteLineFile(LogFilePath, "TimeSpan = " + TSpan.TotalSeconds.ToString() + "s");
            //TolErrBitNum = 0;
            IsAutoTest = false;
            timer.Enabled = false;
            bnManualTest.Enabled = true;
            bnAutoTest.Enabled = true;
            bnMEMQuit.BackColor = SystemColors.Control;
            bnManualTest.BackColor = SystemColors.Control;
            bnAutoTest.BackColor = SystemColors.Control;
            bnMEMQuit.Text = "等待测试";
            MemQuitFlag = 0;
            MemTestOn = 0;
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
        }
        //重新测试按钮事件
        private void ResetCmd_Click(object sender, EventArgs e)
        {
            if (!sp.IsOpen)
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
            else if (MemTestOn == 1)
            {
                MessageBox.Show("请先退出memory测试！", "提示");
            }
            else
            {
                ConsoleInit();
                TestOptionInit();

                tbResult.Text = "";
                //刷新频率以及测试次数
                /*txtTestnum.Text = "";
                txtRefreshFreq.Text = "";*/
                //
                txtTolDoseRate.Text = "";
                tbtestproc.Text = "";
                //tbTolErrNum.Clear();
                tbFrmNum.Clear();
                tbFrmSeq.Clear();
                isSetProperty = false;
                //PeripheralID = 0;
                IsAutoTest = false;
                timer.Enabled = false;
                if (sp.IsOpen)
                {
                    sp.DiscardInBuffer();
                    sp.DiscardOutBuffer();
                    sp.Close();
                }
                sp.Open();
            }

        }

        public void ReTest()
        {

            Byte[] cmdData_in = new Byte[] { 0x01, 0x00, 0x0a, 0x00, 0x00, 0x00, 0x00, 0x00 };

            if (!sp.IsOpen)
            {
                MessageBox.Show("请先打开串口！", "提示");
            }
            else if (MemTestOn == 1)
            {
                MessageBox.Show("请先退出memory测试！", "提示");
            }
            else
            {
                ConsoleInit();
                //TestOptionInit();
                tbResult.Text = "";
                //刷新频率以及测试次数
                /*txtTestnum.Text = "";
                txtRefreshFreq.Text = "";*/
                //
                txtTolDoseRate.Text = "";
                tbtestproc.Text = "";
                //tbTolErrNum.Clear();
                tbFrmNum.Clear();
                tbFrmSeq.Clear();
                isSetProperty = false;
                //PeripheralID = 0;
                IsAutoTest = false;
                timer.Enabled = false;
                if (sp.IsOpen)
                {
                    sp.DiscardInBuffer();
                    sp.DiscardOutBuffer();
                    sp.Close();
                }
                sp.Open();
            }

            //发送复位指令
            //SendPack(0x20, cmdData_in, 8);



        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            tbResult.AppendText("TestID " + PeripheralID.ToString() + ": fpga quit, please restart!\r\n");
            MessageBox.Show("错误数量超过阈值，fpga退出！", "提示");
        }

        private void rbZero_CheckedChanged(object sender, EventArgs e)
        {
            MemDataModeCheck = true;
            if (MemTestOn == 1)
            {
                MessageBox.Show("请先退出memory测试！", "提示");
            }
            else if (sender == rbZero)
            {
                MemDataMode = 0x10;  //represent all zeros 0x0//
                
            }
            else if (sender == rbOne)
            {
                MemDataMode = 0x20;  //represent all ones 0xffffffff//
                
            }
            else if (sender == rbChess)
            {
                MemDataMode = 0x30;  //represent 0x55555555 and 0xaaaaaaaa//
                
            }
        }
        private void bnMEMTestResult()
        {
            //Byte[] TestNumByte = new Byte[2];
            TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
                                                                            //TestNumByte = System.BitConverter.GetBytes(TestNum);
            //for (int i = 0; i <= TestNum; i++)
            {

                MemTest_count = MemTest_count + 1;


                Byte[] MemCmdDat = new Byte[CmdLen];

                if ((PeripheralID == MEM) && (CmdName == "Test"))
                {

                    MemCmdDat[0] = MEMREQ;
                    bnMEMResult.BackColor = Color.Blue;
                    tbResult.AppendText("Return Memory test result.....\r\n");
                    WriteLineFile(LogFilePath, "Return Memory test result.....");
                    Console.WriteLine("MEMREQ");

                    RecvByteNum = ReqLen + 3; //wait for receive up request///



                    SendPack(DownCmd, MemCmdDat, CmdLen);

                    //timer.Start(); //Start the timer//
                }
                else  //there is no effect if test other peripheral/// 
                {
                    MessageBox.Show("请选择'存储器'然后开始测试", "无效点击");
                }
                //
                tbResult.AppendText("memtest\r\n");
                //while (!mem_test_flag) ;
                //mem_test_flag = false;
            }
            
        }

        private void Click1()
        {
            //Byte[] TestNumByte = new Byte[2];
            TestNum = Convert.ToUInt16(txtTestnum.Text, 10);                //测试次数//
            //TestNumByte = System.BitConverter.GetBytes(TestNum);
            //while (MemTest_count != TestNum)
            {

                MemTest_count = MemTest_count + 1;


                Byte[] MemCmdDat = new Byte[CmdLen];

                if ((PeripheralID == MEM) && (CmdName == "Test"))
                {
                    
                        MemCmdDat[0] = MEMQUIT;
                        MemQuitFlag = 1;
                        MemTestOn = 0;                    
                        RecvByteNum = ReqLen + 3; //wait for receive up request///
                    

                    SendPack(DownCmd, MemCmdDat, CmdLen);

                    //timer.Start(); //Start the timer//
                }
                else  //there is no effect if test other peripheral/// 
                {
                    MessageBox.Show("请选择'存储器'然后开始测试", "无效点击");
                }
                //
                tbResult.AppendText("memtest\r\n");
                //while (!mem_test_flag) ;
                //mem_test_flag = false;
            }
            MemTest_count = 0;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (sp.IsOpen)
            {
                Byte[] RspData = new Byte[RspLen];
                RspData[0] = OK;//上对下响应，看诊结构，上下行响应针定义不一致的
                                ///通知FPGA停止进行测试/////
                RspData[1] = StopTest;
                SendPack(DownRsp, RspData, RspLen);
                FuncQuit();
                rbOne.Enabled = true;
                rbZero.Enabled = true;
                rbChess.Enabled = true;
                tbResult.AppendText("退出测试！\r\n");
                MemTest_count = 0;
            }
            else {
                MessageBox.Show("未连接串口", "无效点击");
            }
        }



        ////////


    }
}
