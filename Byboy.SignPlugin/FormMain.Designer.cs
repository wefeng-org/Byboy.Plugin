namespace Byboy.SignPlugin
{
    partial class FormMain
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
            if (disposing && (components != null)) {
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
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            btnSave = new Button();
            chkOnlyAdminEdit = new CheckBox();
            tabPage4 = new TabPage();
            lvCluster = new ListView();
            columnHeader14 = new ColumnHeader();
            columnHeader15 = new ColumnHeader();
            columnHeader16 = new ColumnHeader();
            columnHeader17 = new ColumnHeader();
            columnHeader18 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            开ToolStripMenuItem = new ToolStripMenuItem();
            关ToolStripMenuItem = new ToolStripMenuItem();
            设置ToolStripMenuItem = new ToolStripMenuItem();
            删除ToolStripMenuItem = new ToolStripMenuItem();
            tabPage1 = new TabPage();
            button1 = new Button();
            label19 = new Label();
            label18 = new Label();
            cmbPage = new ComboBox();
            lvSign = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            columnHeader13 = new ColumnHeader();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage4.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0,0);
            tabControl1.Margin = new Padding(4,4,4,4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(913,684);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnSave);
            tabPage2.Controls.Add(chkOnlyAdminEdit);
            tabPage2.Location = new Point(4,26);
            tabPage2.Margin = new Padding(4,4,4,4);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(905,654);
            tabPage2.TabIndex = 4;
            tabPage2.Text = "总体设置";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(289,18);
            btnSave.Margin = new Padding(4,4,4,4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88,33);
            btnSave.TabIndex = 1;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // chkOnlyAdminEdit
            // 
            chkOnlyAdminEdit.AutoSize = true;
            chkOnlyAdminEdit.Location = new Point(31,28);
            chkOnlyAdminEdit.Margin = new Padding(4,4,4,4);
            chkOnlyAdminEdit.Name = "chkOnlyAdminEdit";
            chkOnlyAdminEdit.Size = new Size(171,21);
            chkOnlyAdminEdit.TabIndex = 0;
            chkOnlyAdminEdit.Text = "只能管理员修改积分等设置";
            chkOnlyAdminEdit.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(lvCluster);
            tabPage4.Location = new Point(4,26);
            tabPage4.Margin = new Padding(4,4,4,4);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(905,654);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "群设置";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // lvCluster
            // 
            lvCluster.Columns.AddRange(new ColumnHeader[] { columnHeader14,columnHeader15,columnHeader16,columnHeader17,columnHeader18 });
            lvCluster.ContextMenuStrip = contextMenuStrip1;
            lvCluster.Dock = DockStyle.Fill;
            lvCluster.FullRowSelect = true;
            lvCluster.GridLines = true;
            lvCluster.Location = new Point(0,0);
            lvCluster.Margin = new Padding(4,4,4,4);
            lvCluster.Name = "lvCluster";
            lvCluster.Size = new Size(905,654);
            lvCluster.TabIndex = 0;
            lvCluster.UseCompatibleStateImageBehavior = false;
            lvCluster.View = View.Details;
            lvCluster.MouseDoubleClick += lvCluster_MouseDoubleClick;
            // 
            // columnHeader14
            // 
            columnHeader14.Text = "群号";
            columnHeader14.Width = 118;
            // 
            // columnHeader15
            // 
            columnHeader15.Text = "群名";
            columnHeader15.Width = 125;
            // 
            // columnHeader16
            // 
            columnHeader16.Text = "人数";
            // 
            // columnHeader17
            // 
            columnHeader17.Text = "群主";
            columnHeader17.Width = 131;
            // 
            // columnHeader18
            // 
            columnHeader18.Text = "开关";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 开ToolStripMenuItem,关ToolStripMenuItem,设置ToolStripMenuItem,删除ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(101,92);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // 开ToolStripMenuItem
            // 
            开ToolStripMenuItem.Name = "开ToolStripMenuItem";
            开ToolStripMenuItem.Size = new Size(100,22);
            开ToolStripMenuItem.Text = "开";
            开ToolStripMenuItem.Click += 开ToolStripMenuItem_Click;
            // 
            // 关ToolStripMenuItem
            // 
            关ToolStripMenuItem.Name = "关ToolStripMenuItem";
            关ToolStripMenuItem.Size = new Size(100,22);
            关ToolStripMenuItem.Text = "关";
            关ToolStripMenuItem.Click += 关ToolStripMenuItem_Click;
            // 
            // 设置ToolStripMenuItem
            // 
            设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            设置ToolStripMenuItem.Size = new Size(100,22);
            设置ToolStripMenuItem.Text = "设置";
            设置ToolStripMenuItem.Click += 设置ToolStripMenuItem_Click;
            // 
            // 删除ToolStripMenuItem
            // 
            删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            删除ToolStripMenuItem.Size = new Size(100,22);
            删除ToolStripMenuItem.Text = "删除";
            删除ToolStripMenuItem.Click += 删除ToolStripMenuItem_Click;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label19);
            tabPage1.Controls.Add(label18);
            tabPage1.Controls.Add(cmbPage);
            tabPage1.Controls.Add(lvSign);
            tabPage1.Location = new Point(4,26);
            tabPage1.Margin = new Padding(4,4,4,4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4,4,4,4);
            tabPage1.Size = new Size(905,654);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "签到情况";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(174,608);
            button1.Margin = new Padding(4,4,4,4);
            button1.Name = "button1";
            button1.Size = new Size(176,33);
            button1.TabIndex = 4;
            button1.Text = "清理不存在的群签到";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label19.AutoSize = true;
            label19.Location = new Point(9,615);
            label19.Margin = new Padding(4,0,4,0);
            label19.Name = "label19";
            label19.Size = new Size(50,17);
            label19.TabIndex = 3;
            label19.Text = "label19";
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label18.AutoSize = true;
            label18.Location = new Point(678,615);
            label18.Margin = new Padding(4,0,4,0);
            label18.Name = "label18";
            label18.Size = new Size(44,17);
            label18.TabIndex = 2;
            label18.Text = "跳转：";
            // 
            // cmbPage
            // 
            cmbPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmbPage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPage.FormattingEnabled = true;
            cmbPage.Location = new Point(733,611);
            cmbPage.Margin = new Padding(4,4,4,4);
            cmbPage.Name = "cmbPage";
            cmbPage.Size = new Size(140,25);
            cmbPage.TabIndex = 1;
            cmbPage.SelectedIndexChanged += cmbPage_SelectedIndexChanged;
            // 
            // lvSign
            // 
            lvSign.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvSign.Columns.AddRange(new ColumnHeader[] { columnHeader1,columnHeader2,columnHeader3,columnHeader4,columnHeader5,columnHeader6,columnHeader7,columnHeader8,columnHeader9,columnHeader10,columnHeader11,columnHeader12,columnHeader13 });
            lvSign.FullRowSelect = true;
            lvSign.GridLines = true;
            lvSign.Location = new Point(4,4);
            lvSign.Margin = new Padding(4,4,4,4);
            lvSign.Name = "lvSign";
            lvSign.Size = new Size(903,596);
            lvSign.TabIndex = 0;
            lvSign.UseCompatibleStateImageBehavior = false;
            lvSign.View = View.Details;
            lvSign.ColumnClick += lvSign_ColumnClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Id";
            columnHeader1.Width = 34;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "群号";
            columnHeader2.Width = 76;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "微信号";
            columnHeader3.Width = 77;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "昵称";
            columnHeader4.Width = 79;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "总天数";
            columnHeader5.Width = 49;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "月天数";
            columnHeader6.Width = 52;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "连续天数";
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "上次签到";
            columnHeader8.Width = 120;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "上次奖励";
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "首次发言";
            columnHeader10.Width = 120;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "最后发言";
            columnHeader11.Width = 120;
            // 
            // columnHeader12
            // 
            columnHeader12.Text = "总发言";
            // 
            // columnHeader13
            // 
            columnHeader13.Text = "本月发言";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F,17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(913,684);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4,4,4,4);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterParent;
            Text = "群签到系统";
            Load += FormMain_Load;
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage4.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbPage;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ListView lvSign;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lvCluster;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkOnlyAdminEdit;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}