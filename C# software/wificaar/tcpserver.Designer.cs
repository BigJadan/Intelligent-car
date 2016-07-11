namespace wificaar
{
    partial class tcpserver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tcpserver));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxServerIP = new System.Windows.Forms.ComboBox();
            this.nmServerPort = new System.Windows.Forms.NumericUpDown();
            this.btnListen = new System.Windows.Forms.Button();
            this.picComStatus = new System.Windows.Forms.PictureBox();
            this.lstConn = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.断开连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textData = new System.Windows.Forms.TextBox();
            this.button_text = new System.Windows.Forms.Button();
            this.button_up = new System.Windows.Forms.Button();
            this.button_down = new System.Windows.Forms.Button();
            this.button_left = new System.Windows.Forms.Button();
            this.button_right = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.textBox_speed = new System.Windows.Forms.TextBox();
            this.button_setspeed = new System.Windows.Forms.Button();
            this.button_getspeed = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_watch = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_takephoto = new System.Windows.Forms.Button();
            this.button_photo = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nmServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picComStatus)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "本地:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口:";
            // 
            // cbxServerIP
            // 
            this.cbxServerIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxServerIP.FormattingEnabled = true;
            this.cbxServerIP.Location = new System.Drawing.Point(43, 6);
            this.cbxServerIP.Name = "cbxServerIP";
            this.cbxServerIP.Size = new System.Drawing.Size(104, 20);
            this.cbxServerIP.TabIndex = 2;
            // 
            // nmServerPort
            // 
            this.nmServerPort.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nmServerPort.Location = new System.Drawing.Point(43, 29);
            this.nmServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmServerPort.Name = "nmServerPort";
            this.nmServerPort.Size = new System.Drawing.Size(104, 21);
            this.nmServerPort.TabIndex = 3;
            this.nmServerPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(53, 56);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(94, 39);
            this.btnListen.TabIndex = 4;
            this.btnListen.Text = "监听";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // picComStatus
            // 
            this.picComStatus.BackgroundImage = global::wificaar.Properties.Resources.redlight;
            this.picComStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picComStatus.Location = new System.Drawing.Point(14, 56);
            this.picComStatus.Name = "picComStatus";
            this.picComStatus.Size = new System.Drawing.Size(33, 39);
            this.picComStatus.TabIndex = 5;
            this.picComStatus.TabStop = false;
            // 
            // lstConn
            // 
            this.lstConn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConn.ContextMenuStrip = this.contextMenuStrip1;
            this.lstConn.FormattingEnabled = true;
            this.lstConn.ItemHeight = 12;
            this.lstConn.Location = new System.Drawing.Point(14, 101);
            this.lstConn.Name = "lstConn";
            this.lstConn.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstConn.Size = new System.Drawing.Size(133, 124);
            this.lstConn.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.断开连接ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // 断开连接ToolStripMenuItem
            // 
            this.断开连接ToolStripMenuItem.Name = "断开连接ToolStripMenuItem";
            this.断开连接ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.断开连接ToolStripMenuItem.Text = "断开连接";
            this.断开连接ToolStripMenuItem.Click += new System.EventHandler(this.断开连接ToolStripMenuItem_Click);
            // 
            // textData
            // 
            this.textData.BackColor = System.Drawing.SystemColors.Info;
            this.textData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textData.Location = new System.Drawing.Point(14, 231);
            this.textData.Multiline = true;
            this.textData.Name = "textData";
            this.textData.ReadOnly = true;
            this.textData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textData.Size = new System.Drawing.Size(617, 190);
            this.textData.TabIndex = 8;
            // 
            // button_text
            // 
            this.button_text.Location = new System.Drawing.Point(83, 61);
            this.button_text.Name = "button_text";
            this.button_text.Size = new System.Drawing.Size(75, 38);
            this.button_text.TabIndex = 9;
            this.button_text.Text = "STOP";
            this.button_text.UseVisualStyleBackColor = true;
            this.button_text.Click += new System.EventHandler(this.button_text_Click);
            // 
            // button_up
            // 
            this.button_up.Location = new System.Drawing.Point(164, 62);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(75, 36);
            this.button_up.TabIndex = 10;
            this.button_up.Text = "右转";
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_down
            // 
            this.button_down.Location = new System.Drawing.Point(2, 61);
            this.button_down.Name = "button_down";
            this.button_down.Size = new System.Drawing.Size(75, 38);
            this.button_down.TabIndex = 11;
            this.button_down.Text = "左转";
            this.button_down.UseVisualStyleBackColor = true;
            this.button_down.Click += new System.EventHandler(this.button_down_Click);
            // 
            // button_left
            // 
            this.button_left.Location = new System.Drawing.Point(83, 105);
            this.button_left.Name = "button_left";
            this.button_left.Size = new System.Drawing.Size(75, 38);
            this.button_left.TabIndex = 12;
            this.button_left.Text = "后退";
            this.button_left.UseVisualStyleBackColor = true;
            this.button_left.Click += new System.EventHandler(this.button_left_Click);
            // 
            // button_right
            // 
            this.button_right.Location = new System.Drawing.Point(83, 17);
            this.button_right.Name = "button_right";
            this.button_right.Size = new System.Drawing.Size(75, 38);
            this.button_right.TabIndex = 13;
            this.button_right.Text = "向前";
            this.button_right.UseVisualStyleBackColor = true;
            this.button_right.Click += new System.EventHandler(this.button_right_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hScrollBar1.LargeChange = 100;
            this.hScrollBar1.Location = new System.Drawing.Point(13, 17);
            this.hScrollBar1.Maximum = 999;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(371, 24);
            this.hScrollBar1.SmallChange = 10;
            this.hScrollBar1.TabIndex = 14;
            this.hScrollBar1.Value = 899;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // textBox_speed
            // 
            this.textBox_speed.Location = new System.Drawing.Point(387, 20);
            this.textBox_speed.MaxLength = 900;
            this.textBox_speed.Name = "textBox_speed";
            this.textBox_speed.ReadOnly = true;
            this.textBox_speed.Size = new System.Drawing.Size(27, 21);
            this.textBox_speed.TabIndex = 18;
            this.textBox_speed.Text = "900";
            // 
            // button_setspeed
            // 
            this.button_setspeed.Location = new System.Drawing.Point(420, 17);
            this.button_setspeed.Name = "button_setspeed";
            this.button_setspeed.Size = new System.Drawing.Size(52, 24);
            this.button_setspeed.TabIndex = 19;
            this.button_setspeed.Text = "设置";
            this.button_setspeed.UseVisualStyleBackColor = true;
            this.button_setspeed.Click += new System.EventHandler(this.button_setspeed_Click);
            // 
            // button_getspeed
            // 
            this.button_getspeed.Location = new System.Drawing.Point(6, 18);
            this.button_getspeed.Name = "button_getspeed";
            this.button_getspeed.Size = new System.Drawing.Size(75, 31);
            this.button_getspeed.TabIndex = 20;
            this.button_getspeed.Text = "测速";
            this.button_getspeed.UseVisualStyleBackColor = true;
            this.button_getspeed.Click += new System.EventHandler(this.button_getspeed_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "左轮:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "右轮:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button_getspeed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(431, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 58);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测速功能";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_setspeed);
            this.groupBox2.Controls.Add(this.hScrollBar1);
            this.groupBox2.Controls.Add(this.textBox_speed);
            this.groupBox2.Location = new System.Drawing.Point(153, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 50);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "调速功能";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_up);
            this.groupBox3.Controls.Add(this.button_text);
            this.groupBox3.Controls.Add(this.button_right);
            this.groupBox3.Controls.Add(this.button_left);
            this.groupBox3.Controls.Add(this.button_down);
            this.groupBox3.Location = new System.Drawing.Point(153, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 160);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "控制面板";
            // 
            // button_watch
            // 
            this.button_watch.Location = new System.Drawing.Point(6, 16);
            this.button_watch.Name = "button_watch";
            this.button_watch.Size = new System.Drawing.Size(75, 43);
            this.button_watch.TabIndex = 27;
            this.button_watch.Text = "监控开始";
            this.button_watch.UseVisualStyleBackColor = true;
            this.button_watch.Click += new System.EventHandler(this.button_watch_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.button_watch);
            this.groupBox4.Location = new System.Drawing.Point(431, 73);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 65);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数监控";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(170, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 35;
            this.label14.Text = "？";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(140, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 34;
            this.label13.Text = "烟雾:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(123, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "？";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "红外:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(123, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(87, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "湿度:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "温度:";
            // 
            // button_takephoto
            // 
            this.button_takephoto.Location = new System.Drawing.Point(6, 15);
            this.button_takephoto.Name = "button_takephoto";
            this.button_takephoto.Size = new System.Drawing.Size(88, 23);
            this.button_takephoto.TabIndex = 29;
            this.button_takephoto.Text = "拍摄图像";
            this.button_takephoto.UseVisualStyleBackColor = true;
            this.button_takephoto.Click += new System.EventHandler(this.button_takephoto_Click);
            // 
            // button_photo
            // 
            this.button_photo.Location = new System.Drawing.Point(89, 15);
            this.button_photo.Name = "button_photo";
            this.button_photo.Size = new System.Drawing.Size(88, 23);
            this.button_photo.TabIndex = 30;
            this.button_photo.Text = "查看图像";
            this.button_photo.UseVisualStyleBackColor = true;
            this.button_photo.Click += new System.EventHandler(this.button_photo_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button_takephoto);
            this.groupBox5.Controls.Add(this.button_photo);
            this.groupBox5.Location = new System.Drawing.Point(431, 137);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(194, 52);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "拍照功能";
            // 
            // timer1
            // 
            this.timer1.Interval = 6000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tcpserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(643, 433);
            this.Controls.Add(this.textData);
            this.Controls.Add(this.lstConn);
            this.Controls.Add(this.picComStatus);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.nmServerPort);
            this.Controls.Add(this.cbxServerIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "tcpserver";
            this.Text = "控制小车操作界面";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcpserver_KeyDown_1);
            ((System.ComponentModel.ISupportInitialize)(this.nmServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picComStatus)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxServerIP;
        private System.Windows.Forms.NumericUpDown nmServerPort;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.PictureBox picComStatus;
        private System.Windows.Forms.ListBox lstConn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 断开连接ToolStripMenuItem;
        private System.Windows.Forms.TextBox textData;
        private System.Windows.Forms.Button button_text;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.Button button_down;
        private System.Windows.Forms.Button button_left;
        private System.Windows.Forms.Button button_right;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TextBox textBox_speed;
        private System.Windows.Forms.Button button_setspeed;
        private System.Windows.Forms.Button button_getspeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_watch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_takephoto;
        private System.Windows.Forms.Button button_photo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer timer1;
    }
}