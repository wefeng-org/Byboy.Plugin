namespace SendSnsTools
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            itemId = new TextBox();
            itemUrl = new TextBox();
            label2 = new Label();
            label3 = new Label();
            sendTime = new DateTimePicker();
            label4 = new Label();
            content = new TextBox();
            commentList = new TextBox();
            label5 = new Label();
            button1 = new Button();
            groupBox1 = new GroupBox();
            pictureBox9 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            openFileDialog1 = new OpenFileDialog();
            label6 = new Label();
            comboBox1 = new ComboBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33,18);
            label1.Name = "label1";
            label1.Size = new Size(43,17);
            label1.TabIndex = 0;
            label1.Text = "活动id";
            // 
            // itemId
            // 
            itemId.Location = new Point(83,13);
            itemId.Name = "itemId";
            itemId.Size = new Size(300,23);
            itemId.TabIndex = 1;
            // 
            // itemUrl
            // 
            itemUrl.Location = new Point(83,42);
            itemUrl.Name = "itemUrl";
            itemUrl.Size = new Size(300,23);
            itemUrl.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18,74);
            label2.Name = "label2";
            label2.Size = new Size(56,17);
            label2.TabIndex = 2;
            label2.Text = "商品类型";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20,106);
            label3.Name = "label3";
            label3.Size = new Size(56,17);
            label3.TabIndex = 4;
            label3.Text = "发送时间";
            // 
            // sendTime
            // 
            sendTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            sendTime.Format = DateTimePickerFormat.Custom;
            sendTime.Location = new Point(82,101);
            sendTime.Name = "sendTime";
            sendTime.Size = new Size(301,23);
            sendTime.TabIndex = 5;
            sendTime.Value = new DateTime(2023,10,27,16,37,10,0);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8,134);
            label4.Name = "label4";
            label4.Size = new Size(68,17);
            label4.TabIndex = 4;
            label4.Text = "朋友圈文案";
            // 
            // content
            // 
            content.Location = new Point(82,131);
            content.Multiline = true;
            content.Name = "content";
            content.ScrollBars = ScrollBars.Vertical;
            content.Size = new Size(301,145);
            content.TabIndex = 6;
            // 
            // commentList
            // 
            commentList.Location = new Point(82,282);
            commentList.Multiline = true;
            commentList.Name = "commentList";
            commentList.ScrollBars = ScrollBars.Vertical;
            commentList.Size = new Size(301,130);
            commentList.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8,285);
            label5.Name = "label5";
            label5.Size = new Size(68,17);
            label5.TabIndex = 7;
            label5.Text = "朋友圈评论";
            // 
            // button1
            // 
            button1.Location = new Point(395,12);
            button1.Name = "button1";
            button1.Size = new Size(75,23);
            button1.TabIndex = 9;
            button1.Text = "上传图片";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox9);
            groupBox1.Controls.Add(pictureBox8);
            groupBox1.Controls.Add(pictureBox7);
            groupBox1.Controls.Add(pictureBox6);
            groupBox1.Controls.Add(pictureBox5);
            groupBox1.Controls.Add(pictureBox4);
            groupBox1.Controls.Add(pictureBox3);
            groupBox1.Controls.Add(pictureBox2);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Location = new Point(394,44);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(390,372);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "已上传的图片";
            // 
            // pictureBox9
            // 
            pictureBox9.Location = new Point(271,254);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(113,110);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 8;
            pictureBox9.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.Location = new Point(141,254);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(113,110);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 7;
            pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Location = new Point(6,254);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(113,110);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 6;
            pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Location = new Point(271,138);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(113,110);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 5;
            pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Location = new Point(141,138);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(113,110);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 4;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(6,138);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(113,110);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(271,22);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(113,110);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(141,22);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(113,110);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(6,22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(113,110);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(288,425);
            button2.Name = "button2";
            button2.Size = new Size(196,36);
            button2.TabIndex = 11;
            button2.Text = "发        送";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21,44);
            label6.Name = "label6";
            label6.Size = new Size(55,17);
            label6.TabIndex = 12;
            label6.Text = "活动URL";
            // 
            // comboBox1
            // 
            comboBox1.DisplayMember = "Text";
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(82,70);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(301,25);
            comboBox1.TabIndex = 13;
            comboBox1.ValueMember = "Value";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F,17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800,473);
            Controls.Add(comboBox1);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(commentList);
            Controls.Add(label5);
            Controls.Add(content);
            Controls.Add(sendTime);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(itemUrl);
            Controls.Add(label2);
            Controls.Add(itemId);
            Controls.Add(label1);
            Name = "Form1";
            Text = "发送朋友圈";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox itemId;
        private TextBox itemUrl;
        private Label label2;
        private Label label3;
        private DateTimePicker sendTime;
        private Label label4;
        private TextBox content;
        private TextBox commentList;
        private Label label5;
        private Button button1;
        private GroupBox groupBox1;
        private Button button2;
        private PictureBox pictureBox9;
        private PictureBox pictureBox8;
        private PictureBox pictureBox7;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private Label label6;
        private ComboBox comboBox1;
    }
}