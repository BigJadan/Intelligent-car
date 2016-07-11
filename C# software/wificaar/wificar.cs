using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.Diagnostics;
using System.Net.Sockets;

namespace wificaar
{
    public partial class wificar : Form
    {
        public wificar()
        {
            InitializeComponent();
            this.serialPort1.Encoding = System.Text.Encoding.Unicode;//保证串口能够收发汉字
        }

        Sunisoft.IrisSkin.SkinEngine se = new Sunisoft.IrisSkin.SkinEngine();   //定义一个皮肤~se

        private void wificar_Load(object sender, EventArgs e)
        {
           // System.Diagnostics.Process.Start("IEXPLORE.EXE", "weibo.com/dnjbd ");
            string[] ports = System.IO.Ports.SerialPort.GetPortNames(); //字符串数组存储可用端口
            foreach (string port in ports)
            {
                this.comboBox1.Items.Add(port);   //添加可用端口
            }
            comboBox1.Text = "COM5";
            comboBox2.Text = "115200";
            comboBox3.Text = "无";
            comboBox4.Text = "1";
            comboBox5.Text = "8";
            label_led.BackColor = Color.Red;
            label_receive.Text = "0";
            label_send.Text = "0";
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceive);

            se.SkinAllForm = true;
            se.SkinFile = "skin\\DeepyCyan.ssk";
            DirectoryInfo di = new DirectoryInfo(@"skin\");
            FileInfo[] ff = di.GetFiles("*.ssk");
            foreach (FileInfo temp in ff)
            {
                comboBox_skin.Items.Add(temp.Name);          //添加所有皮肤
            }
            comboBox_skin.SelectedItem = "DeepCyan.ssk";          //默认silver.ssk皮肤
                                                                //防止线程不一致
            wificar.CheckForIllegalCrossThreadCalls = false;

        }
      

        private void port_DataReceive(object sender, SerialDataReceivedEventArgs e)//串口接收
        {
            /* if (radioButton1.Checked)//数值模式
             {
                 byte dataone;
                 dataone = (byte)serialPort1.ReadByte();
                 string str = Convert.ToString(dataone, 16).ToUpper();
                 this.textBox1.AppendText("0x"+(str.Length==1?"0"+ str :str)+" ");
             }
             else                    //字符模式
             {
                 string str = serialPort1.ReadExisting();
                 this.textBox1.AppendText(str);
             }*/
            if (!radioButton3.Checked)//字符串接受模式
            { 
                    UTF8Encoding unicode = new UTF8Encoding();
                Byte[] readBytes = new Byte[serialPort1.BytesToRead];
              

                this.textBox1.Invoke(
                    new MethodInvoker(
                        delegate
                        {
                            int SDataTemp = this.serialPort1.Read(readBytes, 0, readBytes.Length);
                            //   this.textBox1.AppendText(unicode.GetString(readBytes));
                            this.textBox1.AppendText(Encoding.GetEncoding("gb2312").GetString(readBytes));
                                textBox1.SelectionStart = textBox1.TextLength;
                            int length = readBytes.Length / 2;
                            label_receive.Text = (Convert.ToInt32(label_receive.Text) + length).ToString();
                        }

                )
                );
              }
            else//16进制接受模式
            {
                byte dataone;
                dataone = (byte)serialPort1.ReadByte();
                string str = Convert.ToString(dataone, 16).ToUpper();
                this.textBox1.Invoke(
                   new MethodInvoker(
                       delegate
                       {
                           int length = str.Length / 2;
                           this.textBox1.AppendText("0x" + (length == 1 ? "0" + str : str) + " ");
                          
                           label_receive.Text = (Convert.ToInt32(label_receive.Text) + length).ToString();
                       }
                      )
                );
            }
        }


        private void button5_Click(object sender, EventArgs e)//清除屏幕
        {
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)//打开串口
        {
            if(button1.Text=="打开端口")
            { 
                try
                {
                    serialPort1.PortName = comboBox1.Text;                           //串口号
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text,10);          //波特率
                    serialPort1.DataBits = Convert.ToInt32(comboBox5.Text);             //数据位
                    switch (comboBox3.Text)
                    {
                        case "奇校验":
                            serialPort1.Parity = Parity.Odd;                           //校验位
                            break;
                        case "偶校验":
                            serialPort1.Parity = Parity.Even;
                            break;
                        case "无":
                            serialPort1.Parity = Parity.None;                           //不发生校验
                            break;
                    }
                    switch (comboBox4.Text)                                             //设置停止位
                    {
                        case"None":
                            serialPort1.StopBits = StopBits.None;
                            break;
                        case "1":
                            serialPort1.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            serialPort1.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            serialPort1.StopBits = StopBits.Two;
                            break;
                    }
                    serialPort1.ReadBufferSize = 1024;//设置串口读缓冲区大小数值
                    serialPort1.WriteBufferSize = 1024;//设置串口写缓冲区大小数值
                    serialPort1.ReceivedBytesThreshold = 2;//引发DataReceived事件时输入缓冲区字节数
                    serialPort1.Open();

                    button1.Text = "关闭端口";
                    label_led.BackColor = Color.Green;
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.ToString());
            
                }
            }
            else
            {
                try
                {
                    serialPort1.Close();
                    button1.Text = "打开端口";
                    label_led.BackColor = Color.Red;
                    button3.Enabled = true;
                    button6.Text = "定时发送";
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }

 

        private void button3_Click(object sender, EventArgs e)//发送
        {
            senddata();
        }

        private void button4_Click(object sender, EventArgs e)// 
        {
            textBox2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)// 
        {
            if (button6.Text=="定时发送")
            {
                if (serialPort1.IsOpen == false)
                {
                    MessageBox.Show("请先打开串口");
                    this.button6.Text = "定时发送";
                    return;
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("请输入要发送的数据");
                    this.button6.Text = "定时发送";
                    return;
                }
                else if(textBox_period.Text=="")
                {
                    MessageBox.Show("请输入定时发送的周期");
                    return;
                }
                else
                {
                    timer1.Interval = Convert.ToInt32(textBox_period.Text);
                    this.timer1.Enabled = true;
                    this.button6.Text = "停止发送";
                    button3.Enabled = false;
                    label_led.BackColor = Color.Black;

                }

            }
            else if(button6.Text == "停止发送")
            {
                this.timer1.Enabled = false;
                this.button6.Text = "定时发送";
                button3.Enabled = true;
                label_led.BackColor = Color.Green;
                return;
            }
        }

        private void 高级计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process pr = new Process();//声明一个进程类对象
            pr.StartInfo.FileName = "calc.exe";
            pr.Start();//运行
        }

        private void 简单计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calculator calculator = new calculator();
            if (calculator.IsDisposed == true)
            {
                calculator = new calculator();
                calculator.TopMost = true;

            }
            calculator.Show();
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardInBuffer();
                serialPort1.DiscardOutBuffer();
                serialPort1.Close();

            }
            this.Close();
        }

        private void textBox_period_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
               //MessageBox.Show("请输入数字");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label_receive.Text = "0";
            label_send.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                senddata();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        public void senddata()
        {
            byte[] Data = new byte[1];
            if (serialPort1.IsOpen)
            {
                if (textBox2.Text != "")
                {
                    if (radioButton1.Checked)       //发送数值模式
                    {
                        try//16进制
                        {
                            for (int i = 0; i < (textBox2.Text.Length - textBox2.Text.Length % 2) / 2; i++)
                            {
                                Data[0] = Convert.ToByte(textBox2.Text.Substring(i * 2, 2), 16);
                                serialPort1.Write(Data, 0, 2);
                                label_send.Text = (Convert.ToInt32(label_send.Text) + 2).ToString();
                            }
                            if (textBox2.Text.Length % 2 != 0)
                            {
                                Data[0] = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length - textBox2.Text.Length % 2, textBox2.Text.Length % 2), 16);
                                serialPort1.Write(Data, 0, 1);
                                label_send.Text = (Convert.ToInt32(label_send.Text) + 1).ToString();
                            }

                        }
                        catch (Exception err)
                        {
                            serialPort1.Close();
                            button1.Enabled = true;
                            MessageBox.Show(err.ToString());

                        }
                    }
                    else       //发送字符模式
                    {
                        try
                        {
                            //"\r\n"
                            //string str = textBox2.Text + "\\r"+"\\n";
                            string str = textBox2.Text + ((char)0x0d).ToString()  + ((char)0x0a).ToString();
                            serialPort1.WriteLine( str);
                          /*  serialPort1.WriteLine(textBox2.Text);
                            Byte[] finish_byte = new Byte[2];
                            finish_byte[0] = 0x0d;
                            finish_byte[1] = 0x0a;
                            serialPort1.Write(finish_byte, 0, 2);*/

                            label_send.Text = (Convert.ToInt32(label_send.Text) + textBox2.Text.Length).ToString();
                        }
                        catch (Exception err)
                        {
                          //  serialPort1.Close();
                            button1.Enabled = true;
                            MessageBox.Show(err.ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter something.");
                }
            }
            else
            {
                MessageBox.Show("请先打开串口");
            }
        }

        private void comboBox_skin_SelectedIndexChanged(object sender, EventArgs e)
        {
            se.SkinFile = "skin\\" + comboBox_skin.SelectedItem.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {

                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("接收区为空，没有可以保存的数据源！");
                    return;
                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFile.FileName;
                    File.WriteAllText(fileName, this.textBox1.Text);
                }
        }

        private void timer_showtime_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString();
        }
        string fileData = string.Empty;
        private void 导入文件数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("请先打开串口");

                return;

            }
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFile.FileName;
                using (StreamReader sr = new StreamReader(fileName, Encoding.Unicode))//这里改成了unicode格式，为什么接收到数据还是乱码？？
                {
                    while (!sr.EndOfStream)
                    {
                        fileData = sr.ReadLine();
                    }
                }
                textBox2.Text = "文件 " + fileName + " 已经被导入";
                textBox2.Enabled = false;
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutme aboutme = new aboutme();
            if (aboutme.IsDisposed == true)
            {
                aboutme = new aboutme();
                aboutme.TopMost = true;

            }
            aboutme.Show();
        }

        private void 网络调试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           tcpserver tcpserver = new tcpserver();
            tcpserver.Show();
            //tcpserver.ShowDialog();
        }
    }
}
