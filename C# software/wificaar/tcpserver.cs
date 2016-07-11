using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using wificaar.Lib;
using System.IO;

namespace wificaar
{
    public partial class tcpserver : Form
    {
        /// TCP服务端监听
        /// </summary>
        TcpListener tcpsever = null;
        /// 监听状态
        /// </summary>
        bool isListen = false;
        /// <summary>
        /// 当前已连接客户端集合
        /// </summary>
        public BindingList<LeafTCPClient> lstClient = new BindingList<LeafTCPClient>();
        /// <summary>
        /// 接受数据处理方式
        /// </summary>
        bool textspeed = false;
        bool watchdata = false;
        bool photodata = false;
        int uc=0;
        public tcpserver()
        {
            InitializeComponent();
            cbxServerIP.Items.Add("192.168.191.1");
            cbxServerIP.SelectedIndex = 0;
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in ipHostEntry.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {//筛选IPV4
                    cbxServerIP.Items.Add(ip.ToString());
                }
            }
           
        }
        /// <summary>
        /// 绑定客户端列表
        /// </summary>
        private void BindLstClient()
        {
            lstConn.Invoke(new MethodInvoker(delegate {
                lstConn.DataSource = null;
                lstConn.DataSource = lstClient;
                lstConn.DisplayMember = "Name";
            }));
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (isListen == false)
            {//监听已停止
                StartTCPServer();
                picComStatus.BackgroundImage = Properties.Resources.greenlight;
            }
            else
            {//监听已开启
                StopTCPServer();
                picComStatus.BackgroundImage = Properties.Resources.redlight;
                buttonfalse(true);
            }
            cbxServerIP.Enabled = !isListen;
            nmServerPort.Enabled = !isListen;
            if (isListen)
            {
                btnListen.Text = "停止";
            }
            else
            {
                btnListen.Text = "监听";
            }


        }
        /// <summary>
        /// 开启TCP监听
        /// </summary>
        /// <returns></returns>
        private void StartTCPServer()
        {
            try
            {
                if (cbxServerIP.SelectedIndex == 0)
                {
                    tcpsever = new TcpListener(IPAddress.Any, (int)nmServerPort.Value);
                }
                else
                {
                    tcpsever = new TcpListener(IPAddress.Parse(cbxServerIP.SelectedItem.ToString()), (int)nmServerPort.Value);
                }
                tcpsever.Start();
                tcpsever.BeginAcceptTcpClient(new AsyncCallback(Acceptor), tcpsever);
                isListen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 停止TCP监听
        /// </summary>
        /// <returns></returns>
        private void StopTCPServer()
        {
            tcpsever.Stop();
            isListen = false;
        }


        /// <summary>
        /// 客户端连接初始化
        /// </summary>
        /// <param name="o"></param>
        private void Acceptor(IAsyncResult o)
        {
            TcpListener server = o.AsyncState as TcpListener;
            try
            {
                //初始化连接的客户端
                LeafTCPClient newClient = new LeafTCPClient();
                newClient.NetWork = server.EndAcceptTcpClient(o);
                lstClient.Add(newClient);
                BindLstClient();
                newClient.NetWork.GetStream().BeginRead(newClient.buffer, 0, newClient.buffer.Length, new AsyncCallback(TCPCallBack), newClient);
                server.BeginAcceptTcpClient(new AsyncCallback(Acceptor), server);//继续监听客户端连接
            }
            catch (ObjectDisposedException ex)
            { //监听被关闭
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 对当前选中的客户端发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public bool SendData(byte[] data)
        {
            if (lstConn.SelectedItems.Count > 0)
            {
                for (int i = 0; i < lstConn.SelectedItems.Count; i++)
                {
                    LeafTCPClient selClient = (LeafTCPClient)lstConn.SelectedItems[i];
                    try
                    {
                        selClient.NetWork.GetStream().Write(data, 0, data.Length);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(selClient.Name + ":" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("无可用客户端", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 客户端通讯回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void TCPCallBack(IAsyncResult ar)
        {
            LeafTCPClient client = (LeafTCPClient)ar.AsyncState;
            if (client.NetWork.Connected)
            {
                NetworkStream ns = client.NetWork.GetStream();
                byte[] recdata = new byte[ns.EndRead(ar)];
                if (recdata.Length > 0)
                {
                    Array.Copy(client.buffer, recdata, recdata.Length);
                    AddContent(new ASCIIEncoding().GetString(recdata));
                    ns.BeginRead(client.buffer, 0, client.buffer.Length, new AsyncCallback(TCPCallBack), client);
                }
                else
                {
                    client.DisConnect();
                    lstClient.Remove(client);
                    BindLstClient();
                }
            }
        }
        /// <summary>
        /// 添加文本内容
        /// </summary>
        /// <param name="content"></param>
        private void AddContent(string content)
        {
            textData.BeginInvoke(new MethodInvoker(delegate
            {
                if (textData.Text!="")
                {
                    textData.AppendText("\r\n");
                }
                
                dealdata( content);//处理数据
                if (textData.Text.Length > 60000)
                {
                    textData.Text.Remove(0, 10000);
                }
                textData.SelectionStart = textData.Text.Length;
                textData.ScrollToCaret();
            }));
        }
        /// <summary>
        /// 收集数据，显示储存
        /// </summary>
        /// <param name="content"></param>
        private void dealdata(string content)
        {
            content = content.Substring(0, content.Length-1);

            if (textspeed == true)//显示速度
            {
                textData.AppendText(content);//添加数据到界面
                string[] sArray1 = content.Split(new char[4] { ',', 'A', 'T','+' });
                double speed = Convert.ToDouble(sArray1[0]) * 65 * 3.14 / (500 * 270);
                label3.Text = speed.ToString("0.000") + "m/s";
                label4.Text = speed.ToString("0.000") + "m/s";
                textspeed = false;
            }
            if (watchdata == true)//显示温度湿度红外烟雾
            {
                textData.AppendText(content);//添加数据到界面
                string[] sArray1 = content.Split(new char[4] { ',', 'A', 'T', '+' });
                label9.Text = sArray1[0];
                label10.Text = sArray1[1];
                label12.Text= sArray1[2];
                label14.Text= sArray1[3];
                watchdata = false;
                if (sArray1[2] == "1"){
                    label12.Text = "有人";
                    MessageBox.Show("发现有人！发现有人！发现有人！");
                }
                else{
                    label12.Text = "没人";
                }

                if (sArray1[3] == "1"){
                    label14.Text = "有毒";
                    MessageBox.Show("侦查到有毒气体");
                }
                else{
                    label14.Text = "安全";
                }
            }
            if (photodata == true)
            {
                uc++;
                if (uc == 1) 
                {
                    buttonfalse(false);
                }

                textData.AppendText(content);//添加数据到textData

                if (uc == 40)//接受图片完毕，一波创建图片
                {
                    try
                    {
                        photodata = false;
                        uc = 0;
                        buttonfalse(true);
                        string myfile = "abc.bmp";
                        FileStream myFs = new FileStream(myfile, FileMode.Create);//创建文件
                        BinaryWriter bw = new BinaryWriter(myFs);

                        CarOrder.BITMAPFILEHEADER bmfHeader = new CarOrder.BITMAPFILEHEADER();
                        CarOrder.BITMAPINFOHEADER bmiHeader = new CarOrder.BITMAPINFOHEADER();
                        bw.Write(bmfHeader.bfType = ((ushort)'M' << 8) + 'B');
                        bw.Write(bmfHeader.bfSize = 19266);
                        bw.Write(bmfHeader.bfReserved1 = 0);
                        bw.Write(bmfHeader.bfReserved2 = 0);
                        bw.Write(bmfHeader.bfOffBits = 66);

                        bw.Write(bmiHeader.biSize = 40);
                        bw.Write(bmiHeader.biWidth = 80);
                        bw.Write(bmiHeader.biHeight = 120);
                        bw.Write(bmiHeader.biPlanes = 1);
                        bw.Write(bmiHeader.biBitCount = 16);
                        bw.Write(bmiHeader.biCompression = 3);
                        bw.Write(bmiHeader.biSizeImage = 15366);
                        bw.Write(bmiHeader.biXPelsPerMeter = 0);
                        bw.Write(bmiHeader.biClrUsed = 0);
                        bw.Write(bmiHeader.biClrUsed = 0);
                        bw.Write(bmiHeader.biClrImportant = 0);
                        bw.Write(bmiHeader.RGB_MASK[0] = 0X00F800);
                        bw.Write(bmiHeader.RGB_MASK[1] = 0X0007E0);
                        bw.Write(bmiHeader.RGB_MASK[2] = 0X00001F);

                        //bw.Write((textData.Text));//从textData控件拿回图像数据，并二进制写入

                        StringTurnOther SaveString = new StringTurnOther();//每8个字节，转换成整形uint，然后写入
                        byte temp = 0;
                        //textData.Text = textData.Text.Replace((char)10, (char)0);
                        // string tempStr = textData.Text.Replace((char)13, (char)0);

                        for (int i = 0; i < 38400; i = i + 2) //80*120*4字节的图像。
                        {
                            if (i < 34000)//每波960字节，40波，共80*120*4 =38400字节，理论每4个字节写入9600
                            {
                                //每4个字节，比如“7f60”转成整形32608，在二进制写入文件，就是7F60，剩余用0补充。
                                temp = SaveString.StringTurnUint(textData.Text.Substring(i, 2));
                                bw.Write(temp);
                            }
                            else
                            {
                                temp = 0;
                                bw.Write(temp);
                            }
                        }
                        bw.Flush();
                        bw.Close();
                        myFs.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    buttonfalse(true);
                }
            }
        }
       
        /// <summary>
        /// 停用所有button控件或者启用
        /// </summary>
        private void buttonfalse(bool ok)
        {
            if (ok)
            {
                button_text.Enabled = true;
                button_up.Enabled = true;
                button_left.Enabled = true;
                button_right.Enabled = true;
                button_down.Enabled = true;
                button_getspeed.Enabled = true;
                button_watch.Enabled = true;
                button_takephoto.Enabled = true;
                button_setspeed.Enabled = true;
                button_photo.Enabled = true;
            }
            else
            {
                button_up.Enabled = false;
                button_left.Enabled = false;
                button_text.Enabled = false;
                button_right.Enabled = false;
                button_down.Enabled = false;
                button_getspeed.Enabled = false;
                button_watch.Enabled = false;
                button_takephoto.Enabled = false;
                button_setspeed.Enabled = false;
                button_photo.Enabled = false;
            }
            
        }
        /// <summary>
        /// 断开与客户端的链接
        /// </summary>
        /// <param name="content"></param>
        private void 断开连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstConn.SelectedItems.Count > 0)
            {
                if (lstConn.SelectedItems.Count > 0)
                {
                    List<LeafTCPClient> WaitRemove = new List<LeafTCPClient>();
                    for (int i = 0; i < lstConn.SelectedItems.Count; i++)
                    {
                        WaitRemove.Add((LeafTCPClient)lstConn.SelectedItems[i]);
                    }
                    foreach (LeafTCPClient client in WaitRemove)
                    {
                        client.DisConnect();
                        lstClient.Remove(client);
                    }
                }
            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        public void ClearSelf()
        {
            foreach (LeafTCPClient client in lstClient)
            {
                client.DisConnect();
            }
            lstClient.Clear();
            if (tcpsever != null)
            {
                tcpsever.Stop();
            }
        }

        #region 调速功能函数
        /// <summary>
        /// 调节小车速度进度条
        /// </summary>
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            textBox_speed.Text = Convert.ToString( hScrollBar1.Value);
        }
        #endregion

        #region 参数监控
        /// <summary>
        /// 监控温度湿度红外传感器
        /// </summary>
        private void button_watch_Click(object sender, EventArgs e)
        {
            try
            {
                if (button_watch.Text == "监控开始")
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_GetData)).ToString()); ;
                    SendData(bytes);
                    button_watch.Image = Properties.Resources.greenlight;
                    button_watch.Text = "监控停止";
                    buttonfalse(false);
                    button_text.Enabled = true;
                    button_up.Enabled = true;
                    button_left.Enabled = true;
                    button_right.Enabled = true;
                    button_down.Enabled = true;
                    button_watch.Enabled = true;
                    watchdata = true;
                    this.timer1.Enabled = true;
                }
                else
                {
                    button_watch.Image = Properties.Resources.redlight;
                    button_watch.Text = "监控开始";
                    buttonfalse(true);
                    this.timer1.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        #endregion

        #region 小车控制面板函数,上下左右停止
        /// <summary>
        /// 小车停止
        /// </summary>
        private void button_text_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_GetStop)).ToString());
                SendData(bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// 小车向前
        /// </summary>
        private void button_up_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_MoveUp)).ToString());
                SendData(bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 小车左转
        /// </summary>
        private void button_left_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_MoveLeft)).ToString());
                SendData(bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 小车右转
        /// </summary>
        private void button_right_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_MoveRight)).ToString());
                SendData(bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 小车后退
        /// </summary>
        private void button_down_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_MoveBack)).ToString());
                SendData(bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 测速功能函数
        /// <summary>
        /// 小车精准测速
        /// </summary>
        private void button_getspeed_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_GetSpeed)).ToString());
                SendData(bytes);
                textspeed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 图像相关函数
        /// <summary>
        /// 发送拍摄图像信号
        /// </summary>
        private void button_takephoto_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_GetPhoto)).ToString());
                SendData(bytes);
                photodata = true;
                textData.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 打开图像处理界面
        /// </summary>
        private void button_photo_Click(object sender, EventArgs e)
        {
            opencv opencv = new opencv();
            opencv.Show();

        }
        #endregion


        #region 设置小车速度
        /// <summary>
        ///设置小车速度
        /// </summary>
        private void button_setspeed_Click(object sender, EventArgs e)
        {
            try
            {   //确保传输的是3位数字，防止单片机方面读取错误
                string carspeed = ((int)(CarOrder.OrderType.Car_TurnSpeed)).ToString() + Convert.ToInt16(textBox_speed.Text).ToString("000");
                byte[] bytes = Encoding.UTF8.GetBytes(carspeed);
                textData.Text = carspeed;
                if (SendData(bytes) == true)
                {
                    MessageBox.Show("配置速度成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(((int)(CarOrder.OrderType.Car_GetData)).ToString()); ;
            SendData(bytes);
            watchdata = true;
        }

        private void tcpserver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                MessageBox.Show("fuck");
            }
        }

        private void tcpserver_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)//小车前进
            {
                button_right_Click( sender,  e);
            }
            else if (e.KeyCode == Keys.A)//小车左转
            {
                button_down_Click(sender, e);
            }
            else if (e.KeyCode == Keys.D)//小车右转
            {
                button_up_Click(sender, e);
            }
            else if (e.KeyCode == Keys.S)//小车后退
            {
                button_left_Click(sender, e);
            }
            else if (e.KeyCode == Keys.E)//小车停止
            {
                button_text_Click( sender,  e);
            }
        }
    }
}
