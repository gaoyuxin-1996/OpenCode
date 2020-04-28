namespace Test_console
{
    partial class FormConsole
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ComSet = new System.Windows.Forms.GroupBox();
            this.btnCom = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComParity = new System.Windows.Forms.ComboBox();
            this.ComStop = new System.Windows.Forms.ComboBox();
            this.ComData = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComBaud = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ComNum = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.bnMEMQuit = new System.Windows.Forms.Button();
            this.bnMEMResult = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRefreshFreq = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTolDoseRate = new System.Windows.Forms.TextBox();
            this.bnAutoTest = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbtestproc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTestnum = new System.Windows.Forms.TextBox();
            this.bnManualTest = new System.Windows.Forms.Button();
            this.bnReset = new System.Windows.Forms.Button();
            this.bnSelfTest = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbChess = new System.Windows.Forms.RadioButton();
            this.rbOne = new System.Windows.Forms.RadioButton();
            this.rbZero = new System.Windows.Forms.RadioButton();
            this.bnOpenFile = new System.Windows.Forms.Button();
            this.bnClearFile = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbFrmSeq = new System.Windows.Forms.TextBox();
            this.tbFrmNum = new System.Windows.Forms.TextBox();
            this.rbDMA = new System.Windows.Forms.RadioButton();
            this.rbMEM = new System.Windows.Forms.RadioButton();
            this.rbSDRAM = new System.Windows.Forms.RadioButton();
            this.rbLink = new System.Windows.Forms.RadioButton();
            this.rbISet = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.bnQuit = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bnRetry = new System.Windows.Forms.Button();
            this.bnTimeOut = new System.Windows.Forms.Button();
            this.bnOverFail = new System.Windows.Forms.Button();
            this.bnOverSuc = new System.Windows.Forms.Button();
            this.tbTolErrNum = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bnClearErr = new System.Windows.Forms.Button();
            this.tbErrBitNumTH = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ComSet.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComSet
            // 
            this.ComSet.BackColor = System.Drawing.SystemColors.Control;
            this.ComSet.Controls.Add(this.btnCom);
            this.ComSet.Controls.Add(this.label5);
            this.ComSet.Controls.Add(this.label4);
            this.ComSet.Controls.Add(this.label3);
            this.ComSet.Controls.Add(this.ComParity);
            this.ComSet.Controls.Add(this.ComStop);
            this.ComSet.Controls.Add(this.ComData);
            this.ComSet.Controls.Add(this.label2);
            this.ComSet.Controls.Add(this.ComBaud);
            this.ComSet.Controls.Add(this.label1);
            this.ComSet.Controls.Add(this.ComNum);
            this.ComSet.Location = new System.Drawing.Point(6, 3);
            this.ComSet.Name = "ComSet";
            this.ComSet.Size = new System.Drawing.Size(220, 284);
            this.ComSet.TabIndex = 0;
            this.ComSet.TabStop = false;
            this.ComSet.Text = "ComSet";
            // 
            // btnCom
            // 
            this.btnCom.Location = new System.Drawing.Point(71, 232);
            this.btnCom.Name = "btnCom";
            this.btnCom.Size = new System.Drawing.Size(95, 43);
            this.btnCom.TabIndex = 10;
            this.btnCom.Text = "打开串口";
            this.btnCom.UseVisualStyleBackColor = true;
            this.btnCom.Click += new System.EventHandler(this.btnCom_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "校验位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "停止位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "数据位";
            // 
            // ComParity
            // 
            this.ComParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComParity.FormattingEnabled = true;
            this.ComParity.Location = new System.Drawing.Point(71, 186);
            this.ComParity.Name = "ComParity";
            this.ComParity.Size = new System.Drawing.Size(98, 22);
            this.ComParity.TabIndex = 6;
            // 
            // ComStop
            // 
            this.ComStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComStop.FormattingEnabled = true;
            this.ComStop.Location = new System.Drawing.Point(71, 145);
            this.ComStop.Name = "ComStop";
            this.ComStop.Size = new System.Drawing.Size(98, 22);
            this.ComStop.TabIndex = 5;
            // 
            // ComData
            // 
            this.ComData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComData.FormattingEnabled = true;
            this.ComData.Location = new System.Drawing.Point(71, 106);
            this.ComData.Name = "ComData";
            this.ComData.Size = new System.Drawing.Size(98, 22);
            this.ComData.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率";
            // 
            // ComBaud
            // 
            this.ComBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBaud.FormattingEnabled = true;
            this.ComBaud.Location = new System.Drawing.Point(71, 72);
            this.ComBaud.Name = "ComBaud";
            this.ComBaud.Size = new System.Drawing.Size(98, 22);
            this.ComBaud.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口号";
            // 
            // ComNum
            // 
            this.ComNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComNum.FormattingEnabled = true;
            this.ComNum.Location = new System.Drawing.Point(71, 35);
            this.ComNum.Name = "ComNum";
            this.ComNum.Size = new System.Drawing.Size(98, 22);
            this.ComNum.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtRefreshFreq);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtTolDoseRate);
            this.groupBox1.Controls.Add(this.bnAutoTest);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbtestproc);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTestnum);
            this.groupBox1.Controls.Add(this.bnManualTest);
            this.groupBox1.Controls.Add(this.bnReset);
            this.groupBox1.Controls.Add(this.bnSelfTest);
            this.groupBox1.Location = new System.Drawing.Point(253, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 151);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Command";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button1.Location = new System.Drawing.Point(287, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 19;
            this.button1.Text = "重新测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ResetCmd_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.bnMEMQuit);
            this.groupBox5.Controls.Add(this.bnMEMResult);
            this.groupBox5.Location = new System.Drawing.Point(9, 75);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(254, 70);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Memory Test Only";
            // 
            // bnMEMQuit
            // 
            this.bnMEMQuit.Location = new System.Drawing.Point(146, 21);
            this.bnMEMQuit.Name = "bnMEMQuit";
            this.bnMEMQuit.Size = new System.Drawing.Size(92, 40);
            this.bnMEMQuit.TabIndex = 19;
            this.bnMEMQuit.Text = "等待测试";
            this.bnMEMQuit.UseVisualStyleBackColor = true;
            this.bnMEMQuit.Click += new System.EventHandler(this.bnMEMTestResult_Click);
            // 
            // bnMEMResult
            // 
            this.bnMEMResult.Location = new System.Drawing.Point(14, 21);
            this.bnMEMResult.Name = "bnMEMResult";
            this.bnMEMResult.Size = new System.Drawing.Size(94, 40);
            this.bnMEMResult.TabIndex = 18;
            this.bnMEMResult.Text = "上传结果";
            this.bnMEMResult.UseVisualStyleBackColor = true;
            this.bnMEMResult.Click += new System.EventHandler(this.bnMEMTestResult_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(408, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 14);
            this.label18.TabIndex = 17;
            this.label18.Text = "刷新频率：";
            // 
            // txtRefreshFreq
            // 
            this.txtRefreshFreq.Location = new System.Drawing.Point(407, 45);
            this.txtRefreshFreq.Name = "txtRefreshFreq";
            this.txtRefreshFreq.Size = new System.Drawing.Size(70, 23);
            this.txtRefreshFreq.TabIndex = 16;
            this.txtRefreshFreq.Text = "6";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(405, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 20);
            this.label17.TabIndex = 15;
            this.label17.Text = "注量率：";
            // 
            // txtTolDoseRate
            // 
            this.txtTolDoseRate.Location = new System.Drawing.Point(407, 98);
            this.txtTolDoseRate.Name = "txtTolDoseRate";
            this.txtTolDoseRate.Size = new System.Drawing.Size(72, 23);
            this.txtTolDoseRate.TabIndex = 14;
            // 
            // bnAutoTest
            // 
            this.bnAutoTest.Location = new System.Drawing.Point(307, 26);
            this.bnAutoTest.Name = "bnAutoTest";
            this.bnAutoTest.Size = new System.Drawing.Size(80, 40);
            this.bnAutoTest.TabIndex = 10;
            this.bnAutoTest.Text = "自动测试";
            this.bnAutoTest.UseVisualStyleBackColor = true;
            this.bnAutoTest.Click += new System.EventHandler(this.bnCommand_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(499, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "测试进度：";
            // 
            // tbtestproc
            // 
            this.tbtestproc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbtestproc.Location = new System.Drawing.Point(498, 98);
            this.tbtestproc.Name = "tbtestproc";
            this.tbtestproc.ReadOnly = true;
            this.tbtestproc.Size = new System.Drawing.Size(70, 23);
            this.tbtestproc.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(494, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "测试次数：";
            // 
            // txtTestnum
            // 
            this.txtTestnum.Location = new System.Drawing.Point(498, 45);
            this.txtTestnum.MaxLength = 6;
            this.txtTestnum.Name = "txtTestnum";
            this.txtTestnum.Size = new System.Drawing.Size(70, 23);
            this.txtTestnum.TabIndex = 6;
            // 
            // bnManualTest
            // 
            this.bnManualTest.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bnManualTest.Location = new System.Drawing.Point(211, 25);
            this.bnManualTest.Name = "bnManualTest";
            this.bnManualTest.Size = new System.Drawing.Size(80, 40);
            this.bnManualTest.TabIndex = 5;
            this.bnManualTest.Text = "手动测试";
            this.bnManualTest.UseVisualStyleBackColor = true;
            this.bnManualTest.Click += new System.EventHandler(this.bnCommand_Click);
            // 
            // bnReset
            // 
            this.bnReset.Location = new System.Drawing.Point(117, 25);
            this.bnReset.Name = "bnReset";
            this.bnReset.Size = new System.Drawing.Size(80, 40);
            this.bnReset.TabIndex = 2;
            this.bnReset.Text = "复位";
            this.bnReset.UseVisualStyleBackColor = true;
            this.bnReset.Click += new System.EventHandler(this.bnCommand_Click);
            // 
            // bnSelfTest
            // 
            this.bnSelfTest.BackColor = System.Drawing.SystemColors.Control;
            this.bnSelfTest.Location = new System.Drawing.Point(20, 25);
            this.bnSelfTest.Name = "bnSelfTest";
            this.bnSelfTest.Size = new System.Drawing.Size(80, 40);
            this.bnSelfTest.TabIndex = 0;
            this.bnSelfTest.Text = "自检";
            this.bnSelfTest.UseVisualStyleBackColor = true;
            this.bnSelfTest.Click += new System.EventHandler(this.bnCommand_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.bnOpenFile);
            this.groupBox2.Controls.Add(this.bnClearFile);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tbFrmSeq);
            this.groupBox2.Controls.Add(this.tbFrmNum);
            this.groupBox2.Controls.Add(this.rbDMA);
            this.groupBox2.Controls.Add(this.rbMEM);
            this.groupBox2.Controls.Add(this.rbSDRAM);
            this.groupBox2.Controls.Add(this.rbLink);
            this.groupBox2.Controls.Add(this.rbISet);
            this.groupBox2.Location = new System.Drawing.Point(253, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(593, 127);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TestOption";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbChess);
            this.groupBox6.Controls.Add(this.rbOne);
            this.groupBox6.Controls.Add(this.rbZero);
            this.groupBox6.Location = new System.Drawing.Point(19, 66);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(199, 52);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "MemDataMode";
            // 
            // rbChess
            // 
            this.rbChess.AutoSize = true;
            this.rbChess.Location = new System.Drawing.Point(121, 19);
            this.rbChess.Name = "rbChess";
            this.rbChess.Size = new System.Drawing.Size(67, 18);
            this.rbChess.TabIndex = 2;
            this.rbChess.TabStop = true;
            this.rbChess.Text = "棋盘格";
            this.rbChess.UseVisualStyleBackColor = true;
            this.rbChess.CheckedChanged += new System.EventHandler(this.rbZero_CheckedChanged);
            // 
            // rbOne
            // 
            this.rbOne.AutoSize = true;
            this.rbOne.Location = new System.Drawing.Point(69, 20);
            this.rbOne.Name = "rbOne";
            this.rbOne.Size = new System.Drawing.Size(46, 18);
            this.rbOne.TabIndex = 1;
            this.rbOne.TabStop = true;
            this.rbOne.Text = "全1";
            this.rbOne.UseVisualStyleBackColor = true;
            this.rbOne.CheckedChanged += new System.EventHandler(this.rbZero_CheckedChanged);
            // 
            // rbZero
            // 
            this.rbZero.AutoSize = true;
            this.rbZero.Location = new System.Drawing.Point(10, 20);
            this.rbZero.Name = "rbZero";
            this.rbZero.Size = new System.Drawing.Size(46, 18);
            this.rbZero.TabIndex = 0;
            this.rbZero.TabStop = true;
            this.rbZero.Text = "全0";
            this.rbZero.UseVisualStyleBackColor = true;
            this.rbZero.CheckedChanged += new System.EventHandler(this.rbZero_CheckedChanged);
            // 
            // bnOpenFile
            // 
            this.bnOpenFile.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bnOpenFile.Location = new System.Drawing.Point(493, 22);
            this.bnOpenFile.Name = "bnOpenFile";
            this.bnOpenFile.Size = new System.Drawing.Size(94, 43);
            this.bnOpenFile.TabIndex = 17;
            this.bnOpenFile.Text = "打开文件";
            this.bnOpenFile.UseVisualStyleBackColor = true;
            this.bnOpenFile.Click += new System.EventHandler(this.bnOpenFile_Click);
            // 
            // bnClearFile
            // 
            this.bnClearFile.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bnClearFile.Location = new System.Drawing.Point(493, 75);
            this.bnClearFile.Name = "bnClearFile";
            this.bnClearFile.Size = new System.Drawing.Size(94, 43);
            this.bnClearFile.TabIndex = 5;
            this.bnClearFile.Text = "清空文件";
            this.bnClearFile.UseVisualStyleBackColor = true;
            this.bnClearFile.Click += new System.EventHandler(this.bnClearFile_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(393, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 14);
            this.label14.TabIndex = 16;
            this.label14.Text = "数据帧序号:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(393, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 14);
            this.label13.TabIndex = 15;
            this.label13.Text = "数据帧总数:";
            // 
            // tbFrmSeq
            // 
            this.tbFrmSeq.Location = new System.Drawing.Point(400, 95);
            this.tbFrmSeq.Name = "tbFrmSeq";
            this.tbFrmSeq.ReadOnly = true;
            this.tbFrmSeq.Size = new System.Drawing.Size(70, 23);
            this.tbFrmSeq.TabIndex = 14;
            // 
            // tbFrmNum
            // 
            this.tbFrmNum.Location = new System.Drawing.Point(399, 42);
            this.tbFrmNum.Name = "tbFrmNum";
            this.tbFrmNum.ReadOnly = true;
            this.tbFrmNum.Size = new System.Drawing.Size(70, 23);
            this.tbFrmNum.TabIndex = 13;
            // 
            // rbDMA
            // 
            this.rbDMA.AutoSize = true;
            this.rbDMA.Location = new System.Drawing.Point(326, 86);
            this.rbDMA.Name = "rbDMA";
            this.rbDMA.Size = new System.Drawing.Size(46, 18);
            this.rbDMA.TabIndex = 12;
            this.rbDMA.TabStop = true;
            this.rbDMA.Text = "DMA";
            this.rbDMA.UseVisualStyleBackColor = true;
            this.rbDMA.CheckedChanged += new System.EventHandler(this.rbPeripheryID_CheckedChanged);
            // 
            // rbMEM
            // 
            this.rbMEM.AutoSize = true;
            this.rbMEM.Location = new System.Drawing.Point(27, 28);
            this.rbMEM.Name = "rbMEM";
            this.rbMEM.Size = new System.Drawing.Size(67, 18);
            this.rbMEM.TabIndex = 0;
            this.rbMEM.TabStop = true;
            this.rbMEM.Text = "存储器";
            this.rbMEM.UseVisualStyleBackColor = true;
            this.rbMEM.CheckedChanged += new System.EventHandler(this.rbPeripheryID_CheckedChanged);
            // 
            // rbSDRAM
            // 
            this.rbSDRAM.AutoSize = true;
            this.rbSDRAM.Location = new System.Drawing.Point(326, 32);
            this.rbSDRAM.Name = "rbSDRAM";
            this.rbSDRAM.Size = new System.Drawing.Size(60, 18);
            this.rbSDRAM.TabIndex = 4;
            this.rbSDRAM.TabStop = true;
            this.rbSDRAM.Text = "SDRAM";
            this.rbSDRAM.UseVisualStyleBackColor = true;
            this.rbSDRAM.CheckedChanged += new System.EventHandler(this.rbPeripheryID_CheckedChanged);
            // 
            // rbLink
            // 
            this.rbLink.AutoSize = true;
            this.rbLink.Location = new System.Drawing.Point(237, 86);
            this.rbLink.Name = "rbLink";
            this.rbLink.Size = new System.Drawing.Size(67, 18);
            this.rbLink.TabIndex = 3;
            this.rbLink.TabStop = true;
            this.rbLink.Text = "链路口";
            this.rbLink.UseVisualStyleBackColor = true;
            this.rbLink.CheckedChanged += new System.EventHandler(this.rbPeripheryID_CheckedChanged);
            // 
            // rbISet
            // 
            this.rbISet.AutoSize = true;
            this.rbISet.Location = new System.Drawing.Point(237, 32);
            this.rbISet.Name = "rbISet";
            this.rbISet.Size = new System.Drawing.Size(67, 18);
            this.rbISet.TabIndex = 1;
            this.rbISet.TabStop = true;
            this.rbISet.Text = "指令集";
            this.rbISet.UseVisualStyleBackColor = true;
            this.rbISet.CheckedChanged += new System.EventHandler(this.rbPeripheryID_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.bnQuit);
            this.groupBox3.Controls.Add(this.tbResult);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.bnRetry);
            this.groupBox3.Controls.Add(this.bnTimeOut);
            this.groupBox3.Controls.Add(this.bnOverFail);
            this.groupBox3.Controls.Add(this.bnOverSuc);
            this.groupBox3.Location = new System.Drawing.Point(253, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(593, 159);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status and Results";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(432, 119);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 14);
            this.label16.TabIndex = 16;
            this.label16.Text = "退出测试：";
            // 
            // bnQuit
            // 
            this.bnQuit.Location = new System.Drawing.Point(515, 111);
            this.bnQuit.Name = "bnQuit";
            this.bnQuit.Size = new System.Drawing.Size(28, 28);
            this.bnQuit.TabIndex = 15;
            this.bnQuit.UseVisualStyleBackColor = true;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(20, 23);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(533, 82);
            this.tbResult.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(323, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 11;
            this.label12.Text = "重试：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(216, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 10;
            this.label11.Text = "超时：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(117, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "错误：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(16, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "正确：";
            // 
            // bnRetry
            // 
            this.bnRetry.Location = new System.Drawing.Point(375, 112);
            this.bnRetry.Name = "bnRetry";
            this.bnRetry.Size = new System.Drawing.Size(28, 28);
            this.bnRetry.TabIndex = 6;
            this.bnRetry.UseVisualStyleBackColor = true;
            // 
            // bnTimeOut
            // 
            this.bnTimeOut.Location = new System.Drawing.Point(267, 112);
            this.bnTimeOut.Name = "bnTimeOut";
            this.bnTimeOut.Size = new System.Drawing.Size(28, 28);
            this.bnTimeOut.TabIndex = 5;
            this.bnTimeOut.UseVisualStyleBackColor = true;
            // 
            // bnOverFail
            // 
            this.bnOverFail.Location = new System.Drawing.Point(168, 112);
            this.bnOverFail.Name = "bnOverFail";
            this.bnOverFail.Size = new System.Drawing.Size(28, 28);
            this.bnOverFail.TabIndex = 3;
            this.bnOverFail.UseVisualStyleBackColor = true;
            // 
            // bnOverSuc
            // 
            this.bnOverSuc.Location = new System.Drawing.Point(66, 112);
            this.bnOverSuc.Name = "bnOverSuc";
            this.bnOverSuc.Size = new System.Drawing.Size(28, 28);
            this.bnOverSuc.TabIndex = 2;
            this.bnOverSuc.UseVisualStyleBackColor = true;
            // 
            // tbTolErrNum
            // 
            this.tbTolErrNum.BackColor = System.Drawing.SystemColors.Window;
            this.tbTolErrNum.Location = new System.Drawing.Point(14, 59);
            this.tbTolErrNum.Name = "tbTolErrNum";
            this.tbTolErrNum.ReadOnly = true;
            this.tbTolErrNum.Size = new System.Drawing.Size(80, 23);
            this.tbTolErrNum.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(11, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 14);
            this.label15.TabIndex = 14;
            this.label15.Text = "累积错误数量：";
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.bnClearErr);
            this.groupBox4.Controls.Add(this.tbErrBitNumTH);
            this.groupBox4.Controls.Add(this.tbTolErrNum);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Location = new System.Drawing.Point(13, 293);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(213, 158);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Results";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(125, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 30);
            this.button3.TabIndex = 19;
            this.button3.Text = "退出测试";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(151, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(42, 14);
            this.label19.TabIndex = 18;
            this.label19.Text = "阈值:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(103, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 14);
            this.label10.TabIndex = 17;
            this.label10.Text = "<=";
            // 
            // bnClearErr
            // 
            this.bnClearErr.Location = new System.Drawing.Point(14, 102);
            this.bnClearErr.Name = "bnClearErr";
            this.bnClearErr.Size = new System.Drawing.Size(90, 30);
            this.bnClearErr.TabIndex = 15;
            this.bnClearErr.Text = "Reset";
            this.bnClearErr.UseVisualStyleBackColor = true;
            this.bnClearErr.Click += new System.EventHandler(this.bnClearErr_Click);
            // 
            // tbErrBitNumTH
            // 
            this.tbErrBitNumTH.BackColor = System.Drawing.SystemColors.Window;
            this.tbErrBitNumTH.Location = new System.Drawing.Point(137, 59);
            this.tbErrBitNumTH.Name = "tbErrBitNumTH";
            this.tbErrBitNumTH.Size = new System.Drawing.Size(70, 23);
            this.tbErrBitNumTH.TabIndex = 16;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 467);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ComSet);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormConsole";
            this.Text = "抗辐照实验测试平台";
            this.Load += new System.EventHandler(this.FormConsole_Load);
            this.ComSet.ResumeLayout(false);
            this.ComSet.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ComSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComBaud;
        private System.Windows.Forms.Button btnCom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComParity;
        private System.Windows.Forms.ComboBox ComStop;
        private System.Windows.Forms.ComboBox ComData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMEM;
        private System.Windows.Forms.Button bnReset;
        private System.Windows.Forms.Button bnSelfTest;
        private System.Windows.Forms.Button bnManualTest;
        private System.Windows.Forms.TextBox txtTestnum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbtestproc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbSDRAM;
        private System.Windows.Forms.RadioButton rbLink;
        private System.Windows.Forms.RadioButton rbISet;
        private System.Windows.Forms.RadioButton rbDMA;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bnRetry;
        private System.Windows.Forms.Button bnTimeOut;
        private System.Windows.Forms.Button bnOverFail;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.TextBox tbFrmSeq;
        private System.Windows.Forms.TextBox tbFrmNum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button bnClearFile;
        private System.Windows.Forms.Button bnAutoTest;
        private System.Windows.Forms.Button bnOpenFile;
        private System.Windows.Forms.TextBox tbTolErrNum;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button bnQuit;
        private System.Windows.Forms.TextBox txtTolDoseRate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtRefreshFreq;
        private System.Windows.Forms.Button bnOverSuc;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button bnClearErr;
        private System.Windows.Forms.TextBox tbErrBitNumTH;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button bnMEMQuit;
        private System.Windows.Forms.Button bnMEMResult;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbZero;
        private System.Windows.Forms.RadioButton rbChess;
        private System.Windows.Forms.RadioButton rbOne;
        private System.Windows.Forms.Button button3;

        public System.EventHandler tbResult_TextChanged { get; set; }
    }
}

