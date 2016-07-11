namespace wificaar
{
    partial class opencv
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
            this.button_text = new System.Windows.Forms.Button();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.button_face = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_gettext = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_text
            // 
            this.button_text.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_text.Location = new System.Drawing.Point(343, 12);
            this.button_text.Name = "button_text";
            this.button_text.Size = new System.Drawing.Size(108, 36);
            this.button_text.TabIndex = 0;
            this.button_text.Text = "hello world";
            this.button_text.UseVisualStyleBackColor = true;
            this.button_text.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_face
            // 
            this.button_face.Location = new System.Drawing.Point(343, 167);
            this.button_face.Name = "button_face";
            this.button_face.Size = new System.Drawing.Size(108, 39);
            this.button_face.TabIndex = 1;
            this.button_face.Text = "人脸识别";
            this.button_face.UseVisualStyleBackColor = true;
            this.button_face.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 320);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_gettext
            // 
            this.textBox_gettext.Location = new System.Drawing.Point(343, 54);
            this.textBox_gettext.Multiline = true;
            this.textBox_gettext.Name = "textBox_gettext";
            this.textBox_gettext.Size = new System.Drawing.Size(209, 34);
            this.textBox_gettext.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "生成文字图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(343, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 42);
            this.button2.TabIndex = 5;
            this.button2.Text = "加载图片数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // opencv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 368);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_gettext);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_face);
            this.Controls.Add(this.button_text);
            this.Name = "opencv";
            this.Text = "opencv";
            this.Load += new System.EventHandler(this.opencv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_text;
        private System.ServiceProcess.ServiceController serviceController1;
        private System.Windows.Forms.Button button_face;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox_gettext;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}