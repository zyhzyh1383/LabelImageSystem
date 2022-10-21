namespace LabelImageSystem
{
    partial class CoCoConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtInputDir = new System.Windows.Forms.TextBox();
            this.btnInputDir = new System.Windows.Forms.Button();
            this.btnOutputDir = new System.Windows.Forms.Button();
            this.txtOuputDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox_objTypes = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图片输入路径";
            // 
            // txtInputDir
            // 
            this.txtInputDir.Location = new System.Drawing.Point(108, 32);
            this.txtInputDir.Name = "txtInputDir";
            this.txtInputDir.ReadOnly = true;
            this.txtInputDir.Size = new System.Drawing.Size(512, 21);
            this.txtInputDir.TabIndex = 1;
            // 
            // btnInputDir
            // 
            this.btnInputDir.Location = new System.Drawing.Point(618, 31);
            this.btnInputDir.Name = "btnInputDir";
            this.btnInputDir.Size = new System.Drawing.Size(75, 23);
            this.btnInputDir.TabIndex = 2;
            this.btnInputDir.Text = "...";
            this.btnInputDir.UseVisualStyleBackColor = true;
            this.btnInputDir.Click += new System.EventHandler(this.btnInputDir_Click);
            // 
            // btnOutputDir
            // 
            this.btnOutputDir.Location = new System.Drawing.Point(618, 69);
            this.btnOutputDir.Name = "btnOutputDir";
            this.btnOutputDir.Size = new System.Drawing.Size(75, 23);
            this.btnOutputDir.TabIndex = 5;
            this.btnOutputDir.Text = "...";
            this.btnOutputDir.UseVisualStyleBackColor = true;
            this.btnOutputDir.Click += new System.EventHandler(this.btnOutputDir_Click);
            // 
            // txtOuputDir
            // 
            this.txtOuputDir.Location = new System.Drawing.Point(108, 70);
            this.txtOuputDir.Name = "txtOuputDir";
            this.txtOuputDir.ReadOnly = true;
            this.txtOuputDir.Size = new System.Drawing.Size(512, 21);
            this.txtOuputDir.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "coco输出路径";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(215, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(351, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox_objTypes
            // 
            this.groupBox_objTypes.BackColor = System.Drawing.Color.White;
            this.groupBox_objTypes.Location = new System.Drawing.Point(34, 109);
            this.groupBox_objTypes.Name = "groupBox_objTypes";
            this.groupBox_objTypes.Size = new System.Drawing.Size(659, 139);
            this.groupBox_objTypes.TabIndex = 10;
            this.groupBox_objTypes.TabStop = false;
            this.groupBox_objTypes.Text = "本次使用到的标签类型";
            // 
            // CoCoConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 308);
            this.Controls.Add(this.groupBox_objTypes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOutputDir);
            this.Controls.Add(this.txtOuputDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnInputDir);
            this.Controls.Add(this.txtInputDir);
            this.Controls.Add(this.label1);
            this.Name = "CoCoConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "coco配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInputDir;
        private System.Windows.Forms.Button btnInputDir;
        private System.Windows.Forms.Button btnOutputDir;
        private System.Windows.Forms.TextBox txtOuputDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox_objTypes;
    }
}