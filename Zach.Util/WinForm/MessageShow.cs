using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Zach.Util.WinForm
{
    public class MessageShow
    {
        public static bool Confirm(string text, string caption = "确认框")
        {
            var dialogResult = MessageBox.Show(text, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            return dialogResult == DialogResult.OK ? true : false;
        }

        public static void Show(string test, string caption = "消息框")
        {
            MessageBox.Show(test, caption);
        }
    }
}
