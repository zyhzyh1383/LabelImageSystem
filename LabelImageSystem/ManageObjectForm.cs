using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Zach.Service;

namespace LabelImageSystem
{


    public partial class ManageObjectForm : Form
    {
        List<ObejctDefine> m_vObjects;
        ObjectdefineService objectdefineService = new ObjectdefineService();

        public ManageObjectForm()
        {
            InitializeComponent();
            GetOjectDefines(false);
        }

        public void GetOjectDefines(bool bInsertToDgv = false)
        {
            m_vObjects = null;
            m_vObjects = new List<ObejctDefine>();
            var entityList = objectdefineService.GetList();
            entityList.ToList().ForEach(o =>
            {
                ObejctDefine obj = new ObejctDefine();
                obj.id = o.ID;
                obj.objName = o.Name;
                obj.objScript = o.script;
                m_vObjects.Add(obj);
                if (bInsertToDgv)
                {
                    int index = this.dataGridView_object.Rows.Add();
                    this.dataGridView_object.Rows[index].Cells[0].Value = obj.id;
                    this.dataGridView_object.Rows[index].Cells[1].Value = obj.objName;
                    this.dataGridView_object.Rows[index].Cells[2].Value = obj.objScript;
                }
            });
        }


        private void Form_ManageObject_Load(object sender, EventArgs e)
        {
            GetOjectDefines(true);
        }

        public List<ObejctDefine> GetObjectList()
        {
            return m_vObjects;
        }
    }
}
