
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zach.Util;
using Zach.Util.WinForm;

namespace LabelImageSystem
{
    public partial class TrainConfigForm : Form
    {

        public delegate void DelegateTrainConfigHandler(int epoch, int numClasses, bool useGpu, string datasetDir, string saveDir);

        public event DelegateTrainConfigHandler TrainConfigEvent;

        public TrainConfigForm()
        {
            InitializeComponent();
        }
        public TrainConfigForm(string datasetDir) : this()
        {

            txtDatasetDir.Text = datasetDir;
        }

        private void btnDatasetDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择CoCo所在的路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDatasetDir.Text = dialog.SelectedPath;
            }
        }

        private void btnSaveDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择模型输出的路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtSaveDir.Text = dialog.SelectedPath + "\\output";
            }
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            var epoch = nudEpoch.Value.ToInt();
            var numClasses = nudClasses.Value.ToInt();
            if (epoch == 0 || numClasses == 0)
            {
                MessageShow.Show("训练轮数,标签数量需为不等于的正整数!");
                return;
            }
            if (txtDatasetDir.Text.IsEmpty())
            {
                MessageShow.Show("CoCo路径不能为空");
                return;
            }
            if (txtSaveDir.Text.IsEmpty())
            {
                MessageShow.Show("模型输出路径不能为空");
                return;
            }
            if (TrainConfigEvent != null)
            {
                TrainConfigEvent.Invoke(epoch, numClasses, chkUseGpu.Checked, txtDatasetDir.Text.Trim(), txtSaveDir.Text.Trim());
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
