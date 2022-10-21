
using System.IO;



namespace LabelImageSystem
{
    /// <summary>
    /// IconIndexs类 对应ImageList中5张图片的序列
    /// </summary>
    class IconIndexes
    {
        public const int MyComputer = 0;      //我的电脑
        public const int ClosedFolder = 1;    //文件夹关闭
        public const int OpenFolder = 2;      //文件夹打开
        public const int FixedDrive = 3;      //磁盘盘符
        public const int MyDocuments = 4;     //我的文档
        public const int Picture = 5;     //我的文档
    }


    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.directoryIcons = new System.Windows.Forms.ImageList(this.components);
            this.filesIcons = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标签类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形形状ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多边形形状ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_SaveLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StartAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StopAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.数据集转cocoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.训练cocoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出模型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryTree = new System.Windows.Forms.TreeView();
            this.groupBox_objTypes = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblCpoyright = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox_Process = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // directoryIcons
            // 
            this.directoryIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("directoryIcons.ImageStream")));
            this.directoryIcons.TransparentColor = System.Drawing.Color.Gray;
            this.directoryIcons.Images.SetKeyName(0, "Computer.ico");
            this.directoryIcons.Images.SetKeyName(1, "Closed Folder.ico");
            this.directoryIcons.Images.SetKeyName(2, "Open Folder.ico");
            this.directoryIcons.Images.SetKeyName(3, "drive.ico");
            this.directoryIcons.Images.SetKeyName(4, "My Documents.ico");
            this.directoryIcons.Images.SetKeyName(5, "Picture.ico");
            // 
            // filesIcons
            // 
            this.filesIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("filesIcons.ImageStream")));
            this.filesIcons.TransparentColor = System.Drawing.Color.Maroon;
            this.filesIcons.Images.SetKeyName(0, "Closed Folder.ico");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1349, 35);
            this.panel1.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(228)))), ((int)(((byte)(247)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.ToolStripMenuItem_SaveLabel,
            this.ToolStripMenuItem_AutoSave,
            this.数据集转cocoToolStripMenuItem,
            this.训练cocoToolStripMenuItem,
            this.导出模型ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1349, 35);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标签类型ToolStripMenuItem,
            this.矩形形状ToolStripMenuItem,
            this.多边形形状ToolStripMenuItem});
            this.设置ToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.setting;
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(67, 31);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 标签类型ToolStripMenuItem
            // 
            this.标签类型ToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.slot;
            this.标签类型ToolStripMenuItem.Name = "标签类型ToolStripMenuItem";
            this.标签类型ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.标签类型ToolStripMenuItem.Text = "标签管理";
            this.标签类型ToolStripMenuItem.Click += new System.EventHandler(this.标签类型ToolStripMenuItem_Click);
            // 
            // 矩形形状ToolStripMenuItem
            // 
            this.矩形形状ToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.rectangle;
            this.矩形形状ToolStripMenuItem.Name = "矩形形状ToolStripMenuItem";
            this.矩形形状ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.矩形形状ToolStripMenuItem.Text = "Create Rectangle";
            // 
            // 多边形形状ToolStripMenuItem
            // 
            this.多边形形状ToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.polygon2;
            this.多边形形状ToolStripMenuItem.Name = "多边形形状ToolStripMenuItem";
            this.多边形形状ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.多边形形状ToolStripMenuItem.Text = "Create Polygon";
            // 
            // ToolStripMenuItem_SaveLabel
            // 
            this.ToolStripMenuItem_SaveLabel.Image = global::LabelImageSystem.Properties.Resources.save;
            this.ToolStripMenuItem_SaveLabel.Name = "ToolStripMenuItem_SaveLabel";
            this.ToolStripMenuItem_SaveLabel.Size = new System.Drawing.Size(99, 31);
            this.ToolStripMenuItem_SaveLabel.Text = "保存标定";
            this.ToolStripMenuItem_SaveLabel.Click += new System.EventHandler(this.ToolStripMenuItem_SaveLabel_Click);
            // 
            // ToolStripMenuItem_AutoSave
            // 
            this.ToolStripMenuItem_AutoSave.Checked = true;
            this.ToolStripMenuItem_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_AutoSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_StartAuto,
            this.ToolStripMenuItem_StopAuto});
            this.ToolStripMenuItem_AutoSave.Image = global::LabelImageSystem.Properties.Resources.weiwai;
            this.ToolStripMenuItem_AutoSave.Name = "ToolStripMenuItem_AutoSave";
            this.ToolStripMenuItem_AutoSave.Size = new System.Drawing.Size(211, 31);
            this.ToolStripMenuItem_AutoSave.Text = "切换图片自动保存: 启用";
            // 
            // ToolStripMenuItem_StartAuto
            // 
            this.ToolStripMenuItem_StartAuto.Checked = true;
            this.ToolStripMenuItem_StartAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_StartAuto.Name = "ToolStripMenuItem_StartAuto";
            this.ToolStripMenuItem_StartAuto.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItem_StartAuto.Text = "启用自动保存";
            this.ToolStripMenuItem_StartAuto.Click += new System.EventHandler(this.ToolStripMenuItem_StartAuto_Click);
            // 
            // ToolStripMenuItem_StopAuto
            // 
            this.ToolStripMenuItem_StopAuto.Name = "ToolStripMenuItem_StopAuto";
            this.ToolStripMenuItem_StopAuto.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItem_StopAuto.Text = "停用自动保存";
            this.ToolStripMenuItem_StopAuto.Click += new System.EventHandler(this.ToolStripMenuItem_StopAuto_Click);
            // 
            // 数据集转cocoToolStripMenuItem
            // 
            this.数据集转cocoToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.rukudan;
            this.数据集转cocoToolStripMenuItem.Name = "数据集转cocoToolStripMenuItem";
            this.数据集转cocoToolStripMenuItem.Size = new System.Drawing.Size(131, 31);
            this.数据集转cocoToolStripMenuItem.Text = "数据集转coco";
            this.数据集转cocoToolStripMenuItem.Click += new System.EventHandler(this.数据集转cocoToolStripMenuItem_Click);
            // 
            // 训练cocoToolStripMenuItem
            // 
            this.训练cocoToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.routebinding;
            this.训练cocoToolStripMenuItem.Name = "训练cocoToolStripMenuItem";
            this.训练cocoToolStripMenuItem.Size = new System.Drawing.Size(99, 31);
            this.训练cocoToolStripMenuItem.Text = "训练coco";
            this.训练cocoToolStripMenuItem.Click += new System.EventHandler(this.训练cocoToolStripMenuItem_Click);
            // 
            // 导出模型ToolStripMenuItem
            // 
            this.导出模型ToolStripMenuItem.Image = global::LabelImageSystem.Properties.Resources.toline;
            this.导出模型ToolStripMenuItem.Name = "导出模型ToolStripMenuItem";
            this.导出模型ToolStripMenuItem.Size = new System.Drawing.Size(99, 31);
            this.导出模型ToolStripMenuItem.Text = "导出模型";
            this.导出模型ToolStripMenuItem.Click += new System.EventHandler(this.导出模型ToolStripMenuItem_Click);
            // 
            // directoryTree
            // 
            this.directoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryTree.ImageIndex = 0;
            this.directoryTree.ImageList = this.directoryIcons;
            this.directoryTree.Location = new System.Drawing.Point(0, 0);
            this.directoryTree.Name = "directoryTree";
            this.directoryTree.SelectedImageIndex = 0;
            this.directoryTree.Size = new System.Drawing.Size(292, 700);
            this.directoryTree.TabIndex = 2;
            this.directoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.directoryTree_BeforeExpand);
            this.directoryTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.directoryTree_AfterExpand);
            this.directoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.directoryTree_AfterSelect);
            // 
            // groupBox_objTypes
            // 
            this.groupBox_objTypes.BackColor = System.Drawing.Color.White;
            this.groupBox_objTypes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox_objTypes.Location = new System.Drawing.Point(0, 590);
            this.groupBox_objTypes.Name = "groupBox_objTypes";
            this.groupBox_objTypes.Size = new System.Drawing.Size(617, 110);
            this.groupBox_objTypes.TabIndex = 9;
            this.groupBox_objTypes.TabStop = false;
            this.groupBox_objTypes.Text = "标签类型";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.lblCurrentTime);
            this.panel5.Controls.Add(this.lblCpoyright);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 735);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1349, 15);
            this.panel5.TabIndex = 3;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentTime.Location = new System.Drawing.Point(1176, 0);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(173, 12);
            this.lblCurrentTime.TabIndex = 1;
            this.lblCurrentTime.Text = "当前时间:2022-10-21 13:36:47";
            // 
            // lblCpoyright
            // 
            this.lblCpoyright.AutoSize = true;
            this.lblCpoyright.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCpoyright.Location = new System.Drawing.Point(0, 0);
            this.lblCpoyright.Name = "lblCpoyright";
            this.lblCpoyright.Size = new System.Drawing.Size(347, 12);
            this.lblCpoyright.TabIndex = 0;
            this.lblCpoyright.Text = "Cpoyright © 福州国化智能技术有限公司 All Rights Reserved.";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox_Process);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(909, 35);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(440, 700);
            this.panel4.TabIndex = 13;
            // 
            // textBox_Process
            // 
            this.textBox_Process.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(36)))), ((int)(((byte)(86)))));
            this.textBox_Process.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Process.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox_Process.Location = new System.Drawing.Point(0, 0);
            this.textBox_Process.Multiline = true;
            this.textBox_Process.Name = "textBox_Process";
            this.textBox_Process.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_Process.Size = new System.Drawing.Size(440, 700);
            this.textBox_Process.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.directoryTree);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 700);
            this.panel3.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox_objTypes);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(292, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(617, 700);
            this.panel2.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(617, 700);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 750);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "国化智能标注工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion
        private System.Windows.Forms.ImageList directoryIcons;
        private System.Windows.Forms.ImageList filesIcons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标签类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SaveLabel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AutoSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StartAuto;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StopAuto;
        private System.Windows.Forms.ToolStripMenuItem 数据集转cocoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 训练cocoToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TreeView directoryTree;
        private System.Windows.Forms.GroupBox groupBox_objTypes;
        private System.Windows.Forms.ToolStripMenuItem 矩形形状ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多边形形状ToolStripMenuItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox_Process;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCpoyright;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.ToolStripMenuItem 导出模型ToolStripMenuItem;
    }
}

