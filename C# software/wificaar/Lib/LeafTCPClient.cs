using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;

namespace wificaar.Lib
{
    public class LeafTCPClient
    {
        /// <summary>
        /// 当前客户端名称
        /// </summary>
        private string _Name = "未定义";
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        public void SetName()
        {
            if (_NetWork.Connected)
            {
                IPEndPoint iepR = (IPEndPoint)_NetWork.Client.RemoteEndPoint;
                IPEndPoint iepL = (IPEndPoint)_NetWork.Client.LocalEndPoint;
                _Name = iepL.Port + "->" + iepR.ToString();
            }
        }

        /// <summary>
        /// TCP客户端
        /// </summary>
        private TcpClient _NetWork = null;
        public TcpClient NetWork
        {
            get
            {
                return _NetWork;
            }
            set
            {
                _NetWork = value;
                SetName();
            }
        }


        /// <summary>
        /// 数据接收缓存区
        /// </summary>
        public byte[] buffer = new byte[1024];

        /// <summary>
        /// 断开客户端连接
        /// </summary>
        public void DisConnect()
        {
            try
            {
                if (_NetWork != null && _NetWork.Connected)
                {
                    NetworkStream ns = _NetWork.GetStream();
                    ns.Close();
                    _NetWork.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class CarOrder
    {
        /// <summary>
        /// 小车行动命令1~9
        /// </summary>
        public enum OrderType
        {
            Car_GetSpeed=1,
            Car_GetPhoto,
            Car_GetData,
            Car_MoveUp,
            Car_MoveRight,
            Car_MoveLeft,
            Car_MoveBack,
            Car_GetStop,
            Car_TurnSpeed
        }
        public class BITMAPFILEHEADER
        {
            /// <summary>
            ///  位图头文件
            /// </summary>
            public ushort bfType;     //文件标识，对“BM”，用来识别BMP位图类型
            public uint bfSize;   //文件大小，占4字节
            public ushort bfReserved1;//保留空
            public ushort bfReserved2;//保留空
            public uint bfOffBits;  //头文件到位图信息的偏移量

        }
        public class BITMAPINFOHEADER
        {
            /// <summary>
            ///  位图头信息
            /// </summary>
            public uint biSize;         //说明头结构的字节数
            public uint biWidth;           //图像宽度，像素为单位
            public uint biHeight;      //图像高度，像素为单位
            public ushort biPlanes;           //位面数，为1
            public ushort biBitCount;     //比特数/像素，值为1/4/8/16/31
            public uint biCompression;      //压缩类型,0，没有压缩;1，8比特压缩编码;2，4比特压缩编码;3，由掩码决定
            public uint biSizeImage;        //图像的大小，字节为单位
            public uint biXPelsPerMeter;   //水平分辨率
            public uint biYPelsPerMeter;   //垂直分辨率
            public uint biClrUsed;          //颜色索引数
            public uint biClrImportant;     //颜色索引数的数目
            public uint[] RGB_MASK = new uint[3];           //调色板，存放RGB掩码
            
        }

    } 
}
