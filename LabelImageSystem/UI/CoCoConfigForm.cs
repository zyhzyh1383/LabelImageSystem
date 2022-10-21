
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
    public partial class CoCoConfigForm : Form
    {

        public delegate void DelegateCoCoConfigHandler(string inputDir, string outputDir, List<ObejctDefineViewModel> obejctDefineViewModelLists);

        public event DelegateCoCoConfigHandler DirEvent;

        /// <summary>
        /// 目标定义列表
        /// </summary>
        List<ObejctDefineViewModel> m_vObjectDefine;
        /// <summary>
        /// 选中的标签类型
        /// </summary>
        List<ObejctDefineViewModel> obejctDefineViewModelLists = new List<ObejctDefineViewModel>();

        public CoCoConfigForm()
        {
            InitializeComponent();
        }

        public CoCoConfigForm(string inputDir) : this()
        {

            txtInputDir.Text = inputDir;
            ManageObjectForm formOjbject = new ManageObjectForm();
            m_vObjectDefine = formOjbject.GetObjectList();
            initObjectShapeButtons();
        }

        private void btnInputDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择输入的图片路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtInputDir.Text = dialog.SelectedPath;
            }
        }

        private void btnOutputDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择CoCo输出的路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtOuputDir.Text = dialog.SelectedPath + "\\coco";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtInputDir.Text.IsEmpty() || txtOuputDir.Text.IsEmpty())
            {
                MessageShow.Show("请选择输入输出路径!");
                return;
            }
            foreach (var ctrl in groupBox_objTypes.Controls)
            {
                if (ctrl is CheckBox)
                {
                    var tempChk = ctrl as CheckBox;
                    if (tempChk.Checked)
                        obejctDefineViewModelLists.Add(tempChk.Tag as ObejctDefineViewModel);
                }
            }

            if (obejctDefineViewModelLists.Count == 0)
            {
                MessageShow.Show("请选择当前数据集使用到的所有标签!");
                return;
            }
            if (DirEvent != null)
            {
                DirEvent.Invoke(txtInputDir.Text, txtOuputDir.Text, obejctDefineViewModelLists);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void initObjectShapeButtons()
        {
            int x = 0;
            int width = 20;
            int hang = 10;

            CheckBox chkBox = null;
            groupBox_objTypes.Controls.Clear();
            for (int i = 0; i < m_vObjectDefine.Count; i++)
            {
                if (width > 650)
                {
                    hang += 10;
                    width = 20;
                }
                chkBox = new CheckBox();
                chkBox.Name = "CheckBox_obj" + i;
                chkBox.Text = m_vObjectDefine[i].ObjScript;
                chkBox.Tag = m_vObjectDefine[i];
                chkBox.Location = new System.Drawing.Point(width, 2 * hang);
                x = chkBox.Text.Length;
                chkBox.Size = new System.Drawing.Size(5 * 20 + 30, 16);
                width = chkBox.Location.X + chkBox.Size.Width + 10;
                groupBox_objTypes.Controls.Add(chkBox);
            }
        }
    }
}
