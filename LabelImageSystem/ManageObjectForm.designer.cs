namespace LabelImageSystem
{


    partial class ManageObjectForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_object = new System.Windows.Forms.DataGridView();
            this.objNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.object_label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.object_script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_object)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_object
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_object.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_object.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_object.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_object.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_object.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.objNum,
            this.object_label,
            this.object_script});
            this.dataGridView_object.Location = new System.Drawing.Point(12, 29);
            this.dataGridView_object.Name = "dataGridView_object";
            this.dataGridView_object.RowTemplate.Height = 23;
            this.dataGridView_object.Size = new System.Drawing.Size(529, 402);
            this.dataGridView_object.TabIndex = 1;
            // 
            // objNum
            // 
            this.objNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.objNum.HeaderText = "编号";
            this.objNum.Name = "objNum";
            this.objNum.ReadOnly = true;
            this.objNum.Width = 162;
            // 
            // object_label
            // 
            this.object_label.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.object_label.HeaderText = "目标标识";
            this.object_label.Name = "object_label";
            this.object_label.ReadOnly = true;
            this.object_label.Width = 162;
            // 
            // object_script
            // 
            this.object_script.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.object_script.HeaderText = "目标描述";
            this.object_script.Name = "object_script";
            this.object_script.ReadOnly = true;
            // 
            // Form_ManageObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 458);
            this.Controls.Add(this.dataGridView_object);
            this.Name = "Form_ManageObject";
            this.Text = "Form_ManageObject";
            this.Load += new System.EventHandler(this.Form_ManageObject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_object)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_object;
        private System.Windows.Forms.DataGridViewTextBoxColumn objNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn object_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn object_script;
    }
}