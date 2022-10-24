namespace LabelImageSystem
{
    partial class AutoLabelImageForm
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
            this.btnImageDir = new System.Windows.Forms.Button();
            this.txtImageDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnModelDir = new System.Windows.Forms.Button();
            this.txtModelDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImageDir
            // 
            this.btnImageDir.Location = new System.Drawing.Point(612, 59);
            this.btnImageDir.Name = "btnImageDir";
            this.btnImageDir.Size = new System.Drawing.Size(75, 23);
            this.btnImageDir.TabIndex = 11;
            this.btnImageDir.Text = "...";
            this.btnImageDir.UseVisualStyleBackColor = true;
            this.btnImageDir.Click += new System.EventHandler(this.btnImageDir_Click);
            // 
            // txtImageDir
            // 
            this.txtImageDir.Location = new System.Drawing.Point(102, 60);
            this.txtImageDir.Name = "txtImageDir";
            this.txtImageDir.ReadOnly = true;
            this.txtImageDir.Size = new System.Drawing.Size(512, 21);
            this.txtImageDir.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "数据集路径";
            // 
            // btnModelDir
            // 
            this.btnModelDir.Location = new System.Drawing.Point(612, 21);
            this.btnModelDir.Name = "btnModelDir";
            this.btnModelDir.Size = new System.Drawing.Size(75, 23);
            this.btnModelDir.TabIndex = 8;
            this.btnModelDir.Text = "...";
            this.btnModelDir.UseVisualStyleBackColor = true;
            this.btnModelDir.Click += new System.EventHandler(this.btnModelDir_Click);
            // 
            // txtModelDir
            // 
            this.txtModelDir.Location = new System.Drawing.Point(102, 22);
            this.txtModelDir.Name = "txtModelDir";
            this.txtModelDir.ReadOnly = true;
            this.txtModelDir.Size = new System.Drawing.Size(512, 21);
            this.txtModelDir.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "模型路径";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(127, 168);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 22);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(276, 168);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(121, 22);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // AutoLabelImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 211);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnImageDir);
            this.Controls.Add(this.txtImageDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnModelDir);
            this.Controls.Add(this.txtModelDir);
            this.Controls.Add(this.label1);
            this.Name = "AutoLabelImageForm";
            this.Text = "自动标注数据集";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImageDir;
        private System.Windows.Forms.TextBox txtImageDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnModelDir;
        private System.Windows.Forms.TextBox txtModelDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
    }
}