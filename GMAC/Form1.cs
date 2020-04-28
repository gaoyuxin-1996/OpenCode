using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.Items.Add("SOCKET");
            this.comboBox1.Items.Add("UDP");
            this.comboBox1.Items.Add("TCP Server");
            this.comboBox1.Items.Add("TCP Client");
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            this.comboBox2.Items.AddRange(ipadrlist);
            CheckForIllegalCrossThreadCalls = false;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.Visible = false;
            this.textBox4.Visible = false;
            this.label4.Visible = false;
            this.label5.Visible = false;
            this.button1.BackColor = Color.Red;
        }

        //定义回调
        private delegate void SetTextCallBack(string strValue);
        //声明
        private SetTextCallBack setCallBack;

        //定义接收服务端发送消息的回调
        private delegate void ReceiveMsgCallBack(string strMsg);
        //声明
        private ReceiveMsgCallBack receiveCallBack;

        //创建连接的Socket
        Socket socketSend;
        //创建接收客户端发送消息的线程
        Thread threadReceive;

        IPEndPoint m_LocalIPEndPoint;
        UdpClient m_UdpClientSend;
        UdpClient m_UdpClientReceive;
        private Thread m_ReceThread;//接收线程

        private const int bufferSize = 8000;//缓存空间
        private TcpClient client;
        private TcpListener server;
        NetworkStream sendStream;

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "SOCKET")
            {
                if (this.textBox1.Text != "" && this.textBox3.Text != "")
                {
                    try
                    {
                        socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPAddress ip = IPAddress.Parse(this.textBox1.Text.Trim());
                        socketSend.Connect(ip, Convert.ToInt32(this.textBox3.Text.Trim()));
                        //实例化回调
                        setCallBack = new SetTextCallBack(SetValue);
                        receiveCallBack = new ReceiveMsgCallBack(SetValue);
                        Invoke(setCallBack, "连接成功");
                        this.button1.BackColor = Color.Blue;
                        //开启一个新的线程不停的接收服务器发送消息的线程
                        threadReceive = new Thread(new ThreadStart(Receive));
                        //设置为后台线程
                        threadReceive.IsBackground = true;
                        threadReceive.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接服务端出错:" + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("请输入远程主机IP和端口");
                }
            }
            else if (this.comboBox1.Text == "UDP")
            {
                if (this.textBox1.Text != "" && this.textBox3.Text != "" && this.comboBox2.Text != "" && this.textBox4.Text != "")
                {
                    try
                    {
                        IPAddress LocalIP = IPAddress.Parse(this.comboBox2.Text);//本地IP
                        int LocalPort = Convert.ToInt32(this.textBox4.Text);//本地Port
                        m_LocalIPEndPoint = new IPEndPoint(LocalIP, LocalPort);//本地IP和Port
                                                                               //Bind
                        m_UdpClientSend = new UdpClient(LocalPort);//Bind Send UDP = Local some IP&Port
                        m_UdpClientReceive = new UdpClient(m_LocalIPEndPoint);//Bind Receive UDP = Local IP&Port
                        this.button1.BackColor = Color.Blue;
                        /*
                        发送的UdpClient对象是m_UdpClientSend，绑定的地址是 0.0.0.0:8010
                        接收的UdpClient对象是m_UdpClientReceive，绑定的地址是 10.13.68.220:8010
                        */
                        m_ReceThread = new Thread(new ThreadStart(ReceProcess));//线程处理程序为 ReceProcess
                        m_ReceThread.IsBackground = true;//后台线程，前台线程GG，它也GG
                        m_ReceThread.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接服务端出错:" + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("请输入远程主机与本地主机IP和端口");
                }
            }
            else if (this.comboBox1.Text == "TCP Server")
            {
                if (this.textBox1.Text != "" && this.textBox3.Text != "" && this.comboBox2.Text != "" && this.textBox4.Text != "")
                {
                    try
                    {
                        Thread thread = new Thread(reciveAndListener);
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接服务端出错:" + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("请输入远程主机与本地主机IP和端口");
                }
            }
            else if (this.comboBox1.Text == "TCP Client")
            {
                if (this.textBox1.Text != "" && this.textBox3.Text != "" && this.comboBox2.Text != "" && this.textBox4.Text != "")
                {
                    try
                    {
                        IPAddress ip = IPAddress.Parse(this.textBox1.Text);
                        client = new TcpClient();
                        client.Connect(ip, int.Parse(this.textBox3.Text));
                        this.listBox1.Items.Add("已经连接服务端");
                        //获取用于发送数据的传输流
                        this.button1.BackColor = Color.Blue;
                        sendStream = client.GetStream();
                        Thread thread = new Thread(ListenerServer);
                        thread.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接服务端出错:" + ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("请输入远程主机与本地主机IP和端口");
                }
            }
        }

        private void ListenerServer()
        {
            do
            {
                try
                {
                    int readSize;
                    byte[] buffer = new byte[bufferSize];
                    lock (sendStream)
                    {
                        readSize = sendStream.Read(buffer, 0, bufferSize);
                    }
                    if (readSize == 0)
                        return;
                    this.listBox1.Items.Add(Encoding.Default.GetString(buffer, 0, readSize));
                    

                }
                catch
                {
                    
                }
                //将缓存中的数据写入传输流
            } while (true);
        }

        /// <summary>
        /// 接口服务器发送的消息
        /// </summary>
        private void Receive()
        {

            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    //实际接收到的字节数
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    else
                    {
                        //判断发送的数据的类型 
                        string str = Encoding.Default.GetString(buffer, 0, r);
                        Invoke(receiveCallBack, "接收远程服务器:" + socketSend.RemoteEndPoint + "发送的消息:" + str);

                    }
                }
            }
            catch (ThreadAbortException)
            {
                MessageBox.Show("断开连接");
                this.button1.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收服务端发送的消息出错:" + ex.ToString());
            }
        }


        private void SetValue(string strValue)
        {
            this.listBox1.Items.Add(strValue + "\r \n");
            this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text != "")
            {
                try
                {
                    if (this.comboBox1.Text == "SOCKET")
                    {

                        string strMsg = this.textBox2.Text;
                        byte[] buffer = new byte[2048];
                        buffer = Encoding.Default.GetBytes(strMsg);
                        int receive = socketSend.Send(buffer);

                    }
                    else if (this.comboBox1.Text == "UDP")
                    {
                        IPAddress RemoteIP;   //远端 IP                
                        int RemotePort;      //远端 Port
                        IPEndPoint RemoteIPEndPoint; //远端 IP&Port

                        if (IPAddress.TryParse(this.textBox1.Text, out RemoteIP) == false)//远端 IP
                        {
                            MessageBox.Show("Remote IP is Wrong!", "Wrong");
                            return;
                        }
                        RemotePort = Convert.ToInt32(this.textBox3.Text);//远端 Port
                        RemoteIPEndPoint = new IPEndPoint(RemoteIP, RemotePort);//远端 IP和Port


                        //Get Data
                        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(this.textBox2.Text);
                        int cnt = sendBytes.Length;

                        if (0 == cnt)
                        {
                            return;
                        }

                        //Send
                        m_UdpClientSend.Send(sendBytes, cnt, RemoteIPEndPoint);

                    }
                    else if (this.comboBox1.Text == "TCP Server")
                    {
                        NetworkStream sendStream = client.GetStream();//获得用于数据传输的流
                        byte[] buffer = Encoding.Default.GetBytes(this.textBox2.Text);//将数据存进缓存中
                        sendStream.Write(buffer, 0, buffer.Length);//最终写入流中
                    }
                    else if (this.comboBox1.Text == "TCP Client")
                    {
                        //要发送的信息
                        if (this.textBox2.Text == string.Empty)
                            return;
                        string msg = this.textBox2.Text;
                        //将信息存入缓存中
                        byte[] buffer = Encoding.Default.GetBytes(msg);
                        //lock (sendStream)
                        //{
                        sendStream.Write(buffer, 0, buffer.Length);
                        //}
                        //rtbtxtShowData.AppendText("发送给服务端的数据:" + msg + "\n");
                        //txtSendMsg.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("发送数据错误或未成功连接" + ex.ToString());
                }
            }
            else {
                MessageBox.Show("请输入发送数据");
            }
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                socketSend.Close();
                m_UdpClientSend.Close();
                m_UdpClientReceive.Close();
                m_ReceThread.Abort();//接收线程
                client.Close();
                server.Stop();
                threadReceive.Abort();
                this.button1.BackColor = Color.Red;
            }
            catch (Exception)
            {
                this.button1.BackColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }




        /// <summary>
        /// TCP侦听客户端的连接并接收客户端发送的信息
        /// </summary>
        /// <param name="ipAndPort">服务端Ip、侦听端口</param>
        private void reciveAndListener()
        {
            IPAddress LocalIP = IPAddress.Parse(this.comboBox2.Text);//本地IP
            int LocalPort = Convert.ToInt32(this.textBox4.Text);//本地Port
            server = new TcpListener(LocalIP, LocalPort);
            server.Start();//启动监听
            this.button1.BackColor = Color.Blue;
            //rtbtxtShowData.Dispatcher.Invoke(new showData(rtbtxtShowData.AppendText), "服务端开启侦听....\n");
            //  btnStart.IsEnabled = false;

            //获取连接的客户端对象
            client = server.AcceptTcpClient();
            //rtbtxtShowData.Dispatcher.Invoke(new showData(rtbtxtShowData.AppendText), "有客户端请求连接，连接已建立！");//AcceptTcpClient 是同步方法，会阻塞进程，得到连接对象后才会执行这一步  

            //获得流
            NetworkStream reciveStream = client.GetStream();

            #region 循环监听客户端发来的信息

            do
            {
                byte[] buffer = new byte[bufferSize];
                int msgSize;
                try
                {
                    lock (reciveStream)
                    {
                        msgSize = reciveStream.Read(buffer, 0, bufferSize);
                    }
                    if (msgSize == 0)
                        return;
                    string msg = Encoding.Default.GetString(buffer, 0, bufferSize);
                    //rtbtxtShowData.Dispatcher.Invoke(new showData(rtbtxtShowData.AppendText), "\n客户端曰：" + Encoding.Default.GetString(buffer, 0, msgSize));
                    this.listBox1.Items.Add(msg);
                }
                catch
                {
                    //rtbtxtShowData.Dispatcher.Invoke(new showData(rtbtxtShowData.AppendText), "\n 出现异常：连接被迫关闭");
                    break;
                }
            } while (true);

            #endregion
        }


        /// <summary>
        /// 在后台运行的接收线程
        /// </summary>
        private void ReceProcess()
        {
            int cnt = 0;
            string receiveFromOld = "";
            string receiveFromNew = "";

            //定义IPENDPOINT，装载远程IP地址和端口 
            IPEndPoint remoteIpAndPort = new IPEndPoint(IPAddress.Any, int.Parse(this.textBox4.Text));
            while (true)
            {
                byte[] ReceiveBytes = m_UdpClientReceive.Receive(ref remoteIpAndPort);

                cnt = ReceiveBytes.Length;
                receiveFromNew = remoteIpAndPort.ToString();
                if (!receiveFromNew.Equals(receiveFromOld))
                {
                    receiveFromOld = receiveFromNew;
                    string str_From = String.Format("\r\n【Receive from {0}】\r\n", receiveFromNew);

                    this.listBox1.Items.Add(str_From);
                    Console.WriteLine(str_From);
                }

                string str = System.Text.Encoding.Default.GetString(ReceiveBytes, 0, cnt);
                this.listBox1.Items.Add(str);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Socket")
            {
                this.comboBox2.Visible = false;
                this.textBox4.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
            }
            else
            {
                this.comboBox2.Visible = true;
                this.textBox4.Visible = true;
                this.label4.Visible = true;
                this.label5.Visible = true;
            }
        }
    }
}
