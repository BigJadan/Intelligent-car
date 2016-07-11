using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using wificaar.Lib;
using Emgu.CV.Cuda;
using System.Drawing.Drawing2D;

namespace wificaar
{
    public partial class opencv : Form
    {
        private Pen pen1 = new Pen(Color.Black, 1);
        private Graphics g;

        public opencv()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String win1 = "Test Window"; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
            Mat img = new Mat(200, 400, DepthType.Cv8U, 3); //Create a 3 channel image of 400x200
            img.SetTo(new Bgr(255, 0, 0).MCvScalar); // set it to Blue color
            //Draw "Hello, world." on the image using the specific font
            CvInvoke.PutText(
               img,
               "Hello, world",
               new System.Drawing.Point(10, 80),
               FontFace.HersheyComplex,
               1.0,
               new Bgr(0, 255, 0).MCvScalar);
            CvInvoke.Imshow(win1, img); //Show the image
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(win1); //Destroy the window if key is pressed
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("只能处理三通道图像");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BMP文件|*.bmp|JPG文件|*.jpg|JPEG文件|*.jpeg|所有文件|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Mat image = new Mat(openFileDialog.FileName, LoadImageType.Color); //Read the files as an 8-bit Bgr image  
                long detectionTime;
                List<Rectangle> faces = new List<Rectangle>();
                List<Rectangle> eyes = new List<Rectangle>();

                //The cuda cascade classifier doesn't seem to be able to load "haarcascade_frontalface_default.xml" file in this release
                //disabling CUDA module for now
                bool tryUseCuda = false;
                bool tryUseOpenCL = true;

                DetectFace.Detect(
                  image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
                  faces, eyes,
                  tryUseCuda,
                  tryUseOpenCL,
                  out detectionTime);

                foreach (Rectangle face in faces)
                    CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
                foreach (Rectangle eye in eyes)
                    CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);

                String win1 = "Test Window"; //The name of the window
                CvInvoke.NamedWindow(win1); //Create the window using the specific name
                CvInvoke.Imshow(win1, image); //Show the image
                CvInvoke.WaitKey(0);  //Wait for the key pressing event
                CvInvoke.DestroyWindow(win1); //Destroy the window if key is pressed
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            String text = textBox_gettext.Text;
            String win1 = text; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
            Mat img = new Mat(200, 400, DepthType.Cv8U, 3); //Create a 3 channel image of 400x200
            img.SetTo(new Bgr(255, 0, 0).MCvScalar); // set it to Blue color
            //Draw "Hello, world." on the image using the specific font
            CvInvoke.PutText(
               img,
               textBox_gettext.Text,
               new System.Drawing.Point(10, 80),
               FontFace.HersheyComplex,
               1.0,
               new Bgr(0, 255, 0).MCvScalar);
            CvInvoke.Imshow(win1, img); //Show the image
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(win1); //Destroy the window if key is pressed
        }

        private void opencv_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               // pictureBox1.Image = Image.FromFile("picture.bmp", false);
                pictureBox1.Image = Image.FromFile("textpic.bmp", false);
            }
            catch
            {
            }
            /*
           // pictureBox1.BackColor = Color.White;
            g = pictureBox1.CreateGraphics();
            int x, y;
            g.DrawRectangle(pen1, 70, 100, 100, 80);
            g.DrawRectangle(pen1, 80, 110, 80, 60);
            g.DrawLine(pen1, 80, 80, 120, 100);
            g.DrawLine(pen1, 160, 60, 130, 100);
            g.DrawLine(pen1, 110, 125, 90, 133);
            g.DrawLine(pen1, 130, 125, 150, 133);
            for ( x = 100; x < 120; x++)             //»­×ì
            {
                 y = (16000 - 15 * (x - 110) * (x - 110)) / 100;
                g.DrawEllipse(pen1, x, y, 1, 1);
            }
            for (x = 120; x < 140; x++)
            {
                y = (16000 - 15 * (x - 130) * (x - 130)) / 100;
                g.DrawEllipse(pen1, x, y, 1, 1);
            }
            for (x = 0; x < 20; x++)                //»­½Å
            {
                y = Convert.ToInt16(Math.Sqrt(100 - (10 - x) * (10 - x)) + 180);
                g.DrawLine(pen1, x + 95 - 10, y, x + 95 - 10+1, y+1);
            }
            for (x = 0; x < 20; x++)
            {
                y = Convert.ToInt16(Math.Sqrt(100 - (10 - x) * (10 - x)) + 180);
                g.DrawEllipse(pen1, x+145-10, y, 1, 1);
            }
            g.Dispose();
            */
        }
    }
}
