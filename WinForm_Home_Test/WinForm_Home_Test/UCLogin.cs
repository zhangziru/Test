using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Home_Test
{
    public partial class UCLogin : UserControl
    {
        public UCLogin()
        {
            InitializeComponent();
        }
        //定义一个用户校验事件
        public event Action<object,UserLoginEventArgs> UserLoginValidation;
        private void btnOK_Click(object sender, EventArgs e)
        {
            //点击登录按钮，在这里执行校验
            //这里的校验方式不能写死，得把校验方式交给用户来写。
            //所以在这里要定义一个委托，定义一个事件。（我们可以使用系统的委托）
            if (UserLoginValidation!=null)
            {
                UserLoginEventArgs evtObj = new UserLoginEventArgs();
                evtObj.LoginId = tbxUserName.Text;
                evtObj.LoginPassword = tbxPassword.Text;
                UserLoginValidation(this, evtObj);
                //用户控件，这里我们定义一个事件，模仿系统的事件，也需要两个参数：事件对象+事件参数（里面包含了我们需要的信息）
                //这个用户控件的事件对象：就是用户控件本身。传个this就可以了。
                //事件参数，用户控件中的事件参数，对于调用的人来说，我们就想知道里面的文本框中的输入值，所以我们就自己定义一个事件参数，里面加上我们需要的信息，定义成属性供调用者访问
                if (evtObj.IsOK)
                {
                    this.BackColor = Color.Green;
                }
                else
                {
                    this.BackColor = Color.Red;
                }
                //此时这个验证控件就写好了。也有了事件对象了。
                //自己写控件，又模仿了系统的事件
            }
        }
    }

    public class UserLoginEventArgs : EventArgs
    {
        public string LoginId { get; set; }

        public string LoginPassword { get; set; }

        public bool IsOK { get; set; }
    }
}
