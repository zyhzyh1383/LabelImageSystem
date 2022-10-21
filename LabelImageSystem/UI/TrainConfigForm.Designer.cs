namespace LabelImageSystem
{
    partial class TrainConfigForm
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
            this.chkUseGpu = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudEpoch = new System.Windows.Forms.NumericUpDown();
            this.btnDatasetDir = new System.Windows.Forms.Button();
            this.txtDatasetDir = new System.Windows.Forms.TextBox();
            this.lbl_dataset_dir = new System.Windows.Forms.Label();
            this.nudClasses = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSaveDir = new System.Windows.Forms.Button();
            this.txtSaveDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudEpoch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // chkUseGpu
            // 
            this.chkUseGpu.AutoSize = true;
            this.chkUseGpu.Location = new System.Drawing.Point(512, 33);
            this.chkUseGpu.Name = "chkUseGpu";
            this.chkUseGpu.Size = new System.Drawing.Size(66, 16);
            this.chkUseGpu.TabIndex = 1;
            this.chkUseGpu.Text = "使用gpu";
            this.chkUseGpu.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "训练轮数";
            // 
            // nudEpoch
            // 
            this.nudEpoch.Location = new System.Drawing.Point(116, 31);
            this.nudEpoch.Name = "nudEpoch";
            this.nudEpoch.Size = new System.Drawing.Size(68, 21);
            this.nudEpoch.TabIndex = 3;
            this.nudEpoch.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnDatasetDir
            // 
            this.btnDatasetDir.Location = new System.Drawing.Point(626, 73);
            this.btnDatasetDir.Name = "btnDatasetDir";
            this.btnDatasetDir.Size = new System.Drawing.Size(75, 23);
            this.btnDatasetDir.TabIndex = 8;
            this.btnDatasetDir.Text = "...";
            this.btnDatasetDir.UseVisualStyleBackColor = true;
            this.btnDatasetDir.Click += new System.EventHandler(this.btnDatasetDir_Click);
            // 
            // txtDatasetDir
            // 
            this.txtDatasetDir.Location = new System.Drawing.Point(116, 74);
            this.txtDatasetDir.Name = "txtDatasetDir";
            this.txtDatasetDir.ReadOnly = true;
            this.txtDatasetDir.Size = new System.Drawing.Size(512, 21);
            this.txtDatasetDir.TabIndex = 7;
            // 
            // lbl_dataset_dir
            // 
            this.lbl_dataset_dir.AutoSize = true;
            this.lbl_dataset_dir.Location = new System.Drawing.Point(43, 78);
            this.lbl_dataset_dir.Name = "lbl_dataset_dir";
            this.lbl_dataset_dir.Size = new System.Drawing.Size(53, 12);
            this.lbl_dataset_dir.TabIndex = 6;
            this.lbl_dataset_dir.Text = "CoCo路径";
            // 
            // nudClasses
            // 
            this.nudClasses.Location = new System.Drawing.Point(359, 31);
            this.nudClasses.Name = "nudClasses";
            this.nudClasses.Size = new System.Drawing.Size(68, 21);
            this.nudClasses.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "标签数量";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(359, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(223, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSaveDir
            // 
            this.btnSaveDir.Location = new System.Drawing.Point(626, 118);
            this.btnSaveDir.Name = "btnSaveDir";
            this.btnSaveDir.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDir.TabIndex = 15;
            this.btnSaveDir.Text = "...";
            this.btnSaveDir.UseVisualStyleBackColor = true;
            this.btnSaveDir.Click += new System.EventHandler(this.btnSaveDir_Click);
            // 
            // txtSaveDir
            // 
            this.txtSaveDir.Location = new System.Drawing.Point(116, 119);
            this.txtSaveDir.Name = "txtSaveDir";
            this.txtSaveDir.ReadOnly = true;
            this.txtSaveDir.Size = new System.Drawing.Size(512, 21);
            this.txtSaveDir.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "模型输出路径";
            // 
            // TrainConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 226);
            this.Controls.Add(this.btnSaveDir);
            this.Controls.Add(this.txtSaveDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudClasses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDatasetDir);
            this.Controls.Add(this.txtDatasetDir);
            this.Controls.Add(this.lbl_dataset_dir);
            this.Controls.Add(this.nudEpoch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkUseGpu);
            this.Name = "TrainConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "训练配置";
            ((System.ComponentModel.ISupportInitialize)(this.nudEpoch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudClasses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkUseGpu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudEpoch;
        private System.Windows.Forms.Button btnDatasetDir;
        private System.Windows.Forms.TextBox txtDatasetDir;
        private System.Windows.Forms.Label lbl_dataset_dir;
        private System.Windows.Forms.NumericUpDown nudClasses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSaveDir;
        private System.Windows.Forms.TextBox txtSaveDir;
        private System.Windows.Forms.Label label3;
    }
}