
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
            this.directoryTree = new System.Windows.Forms.TreeView();
            this.directoryIcons = new System.Windows.Forms.ImageList(this.components);
            this.filesIcons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.目标类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_SaveLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StartAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StopAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.数据集转cocoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.训练cocoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_shape2 = new System.Windows.Forms.RadioButton();
            this.radioButton_shape1 = new System.Windows.Forms.RadioButton();
            this.groupBox_objTypes = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_Process = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // directoryTree
            // 
            this.directoryTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.directoryTree.ImageIndex = 0;
            this.directoryTree.ImageList = this.directoryIcons;
            this.directoryTree.Location = new System.Drawing.Point(12, 69);
            this.directoryTree.Name = "directoryTree";
            this.directoryTree.SelectedImageIndex = 0;
            this.directoryTree.Size = new System.Drawing.Size(323, 676);
            this.directoryTree.TabIndex = 0;
            this.directoryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.directoryTree_BeforeExpand);
            this.directoryTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.directoryTree_AfterExpand);
            this.directoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.directoryTree_AfterSelect);
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
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.ToolStripMenuItem_SaveLabel,
            this.ToolStripMenuItem_AutoSave,
            this.数据集转cocoToolStripMenuItem,
            this.训练cocoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1648, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.目标类型ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 目标类型ToolStripMenuItem
            // 
            this.目标类型ToolStripMenuItem.Name = "目标类型ToolStripMenuItem";
            this.目标类型ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.目标类型ToolStripMenuItem.Text = "目标管理";
            this.目标类型ToolStripMenuItem.Click += new System.EventHandler(this.目标管理ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_SaveLabel
            // 
            this.ToolStripMenuItem_SaveLabel.Name = "ToolStripMenuItem_SaveLabel";
            this.ToolStripMenuItem_SaveLabel.Size = new System.Drawing.Size(68, 21);
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
            this.ToolStripMenuItem_AutoSave.Name = "ToolStripMenuItem_AutoSave";
            this.ToolStripMenuItem_AutoSave.Size = new System.Drawing.Size(147, 21);
            this.ToolStripMenuItem_AutoSave.Text = "切换图片自动保存: 启用";
            // 
            // ToolStripMenuItem_StartAuto
            // 
            this.ToolStripMenuItem_StartAuto.Checked = true;
            this.ToolStripMenuItem_StartAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_StartAuto.Name = "ToolStripMenuItem_StartAuto";
            this.ToolStripMenuItem_StartAuto.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_StartAuto.Text = "启用自动保存";
            this.ToolStripMenuItem_StartAuto.Click += new System.EventHandler(this.ToolStripMenuItem_StartAuto_Click);
            // 
            // ToolStripMenuItem_StopAuto
            // 
            this.ToolStripMenuItem_StopAuto.Name = "ToolStripMenuItem_StopAuto";
            this.ToolStripMenuItem_StopAuto.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_StopAuto.Text = "停用自动保存";
            this.ToolStripMenuItem_StopAuto.Click += new System.EventHandler(this.ToolStripMenuItem_StopAuto_Click);
            // 
            // 数据集转cocoToolStripMenuItem
            // 
            this.数据集转cocoToolStripMenuItem.Name = "数据集转cocoToolStripMenuItem";
            this.数据集转cocoToolStripMenuItem.Size = new System.Drawing.Size(96, 21);
            this.数据集转cocoToolStripMenuItem.Text = "数据集转coco";
            this.数据集转cocoToolStripMenuItem.Click += new System.EventHandler(this.数据集转cocoToolStripMenuItem_Click);
            // 
            // 训练cocoToolStripMenuItem
            // 
            this.训练cocoToolStripMenuItem.Name = "训练cocoToolStripMenuItem";
            this.训练cocoToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.训练cocoToolStripMenuItem.Text = "训练coco";
            this.训练cocoToolStripMenuItem.Click += new System.EventHandler(this.训练cocoToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_shape2);
            this.groupBox1.Controls.Add(this.radioButton_shape1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 36);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标记形状选择：";
            // 
            // radioButton_shape2
            // 
            this.radioButton_shape2.AutoSize = true;
            this.radioButton_shape2.Location = new System.Drawing.Point(107, 14);
            this.radioButton_shape2.Name = "radioButton_shape2";
            this.radioButton_shape2.Size = new System.Drawing.Size(83, 16);
            this.radioButton_shape2.TabIndex = 4;
            this.radioButton_shape2.Text = "多边形区域";
            this.radioButton_shape2.UseVisualStyleBackColor = true;
            this.radioButton_shape2.Click += new System.EventHandler(this.radioButton_shape2_Click);
            // 
            // radioButton_shape1
            // 
            this.radioButton_shape1.AutoSize = true;
            this.radioButton_shape1.Checked = true;
            this.radioButton_shape1.Location = new System.Drawing.Point(13, 14);
            this.radioButton_shape1.Name = "radioButton_shape1";
            this.radioButton_shape1.Size = new System.Drawing.Size(71, 16);
            this.radioButton_shape1.TabIndex = 3;
            this.radioButton_shape1.TabStop = true;
            this.radioButton_shape1.Text = "矩形区域";
            this.radioButton_shape1.UseVisualStyleBackColor = true;
            this.radioButton_shape1.Click += new System.EventHandler(this.radioButton_shape1_Click);
            // 
            // groupBox_objTypes
            // 
            this.groupBox_objTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_objTypes.Location = new System.Drawing.Point(360, 639);
            this.groupBox_objTypes.Name = "groupBox_objTypes";
            this.groupBox_objTypes.Size = new System.Drawing.Size(927, 106);
            this.groupBox_objTypes.TabIndex = 6;
            this.groupBox_objTypes.TabStop = false;
            this.groupBox_objTypes.Text = "目标类型";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(360, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(927, 606);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // textBox_Process
            // 
            this.textBox_Process.Location = new System.Drawing.Point(1293, 27);
            this.textBox_Process.Multiline = true;
            this.textBox_Process.Name = "textBox_Process";
            this.textBox_Process.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_Process.Size = new System.Drawing.Size(343, 718);
            this.textBox_Process.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1648, 757);
            this.Controls.Add(this.textBox_Process);
            this.Controls.Add(this.groupBox_objTypes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.directoryTree);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "LabelImageSystem";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.TreeView directoryTree;
        private System.Windows.Forms.ImageList directoryIcons;
        private System.Windows.Forms.ImageList filesIcons;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 目标类型ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_shape2;
        private System.Windows.Forms.RadioButton radioButton_shape1;
        private System.Windows.Forms.GroupBox groupBox_objTypes;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SaveLabel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AutoSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StartAuto;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StopAuto;
        private System.Windows.Forms.ToolStripMenuItem 数据集转cocoToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox_Process;
        private System.Windows.Forms.ToolStripMenuItem 训练cocoToolStripMenuItem;
    }
}

