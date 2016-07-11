using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wificaar
{
    public partial class calculator : Form
    {
        string str="",str_temp="";
        int cal_flag=0,flag=0;
        double cal_b=0.0;
        public calculator()
        {
            InitializeComponent();
            textBox1.Text = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "1";
            textBox1.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "2";
            textBox1.Text = str;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "3";
            textBox1.Text = str;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "4";
            textBox1.Text = str;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "5";
            textBox1.Text = str;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "6";
            textBox1.Text = str;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "7";
            textBox1.Text = str;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "8";
            textBox1.Text = str;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "9";
            textBox1.Text = str;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "0";
            textBox1.Text = str;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            check_str();
            if (textBox1.Text == "")
            {
                str = "0";
            }
            else if (textBox1.Text != "")
            {
                if (str[str.Length-1] == '+') str += "0";
            }
            str = str + ".";
            textBox1.Text = str;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "+";
            textBox1.Text = str;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "-";
            textBox1.Text = str;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "*";
            textBox1.Text = str;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            check_str();
            str = str + "/";
            textBox1.Text = str;
        }
        private void check_str()
        {
            if (cal_flag == 1)
            {
                cal_flag = 0;
                str = "";
            }
        }
        private void button16_Click(object sender, EventArgs e)//=号
        {
            str = str + "+0+0+0";
            flag = 0;
            if (cal_flag == 1)
            {
                textBox1.Text = "";
                str = "";
                str_temp = "";
                return;
            }
            foreach (char str_key in str)
            {
                if (str_key == '+')
                {
                    cal_b=get_answer( str_temp, cal_b,flag);
                    flag = 1;
                    str_temp = "";
                }
                else if (str_key == '-')
                {
                    cal_b = get_answer(str_temp, cal_b, flag);
                    flag = 2;
                    str_temp = "";
                }
                else if (str_key == '*')
                {
                    cal_b = get_answer(str_temp, cal_b, flag);
                    flag = 3;
                    str_temp = "";
                }
                else if (str_key == '/')
                {
                    cal_b = get_answer(str_temp, cal_b, flag);
                    flag = 4;
                    str_temp = "";
                }
                else
                {
                    str_temp = str_temp + Convert.ToString(str_key);
                }
            }
            textBox1.Text = Convert.ToString(cal_b);
            cal_b = 0.0;
            flag = 0;
            cal_flag = 1;//运算结束，下次再运算先清空str
        }
        private double get_answer(string astr,double b,int flag)
        {
            double a = Convert.ToDouble(astr);
            switch (flag)
            {
                case 1: return (a + b);
                case 2: return (b - a);
                case 3: return (a * b);
                case 4: return (b / a);
                default: return (a);
            }
        }
    }
}
