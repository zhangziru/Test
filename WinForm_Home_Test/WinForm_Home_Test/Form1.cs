using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Home_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //1)在这个Form中添加了一个用户控件,我们不能访问用户控件中的“按钮”和“文本框”
            //2)在用户控件里面，里面的“按钮”和“文本框”都是私有的,在用户控件外部根本访问不到。

            //3)所以只能让当前的窗体加载完毕以后，让这个用户控件有一个事件。给这个事件注册一个处理程序。
            ucLogin1.UserLoginValidation += UcLogin1_UserLoginValidation1;
            
        }

        private void UcLogin1_UserLoginValidation1(object sender, UserLoginEventArgs e)
        {
            if (e.LoginId == "admin" && e.LoginPassword == "88888")
            {
                e.IsOK = true;
            }
            else
            {
                e.IsOK = false;
            }
        }

        //4)当点用户控件上"登录"按钮的时候，就会调用指定的方法。
        //这里是点击登录，事件的校验方法。
        private void UcLogin1_UserLoginValidation()
        {
            throw new NotImplementedException();
            //事件校验，要获取用户输入的内容，然后校验。成功了怎么样，失败了怎么样。
            //这里不能直接获取用户控件中的“文本框”
            //5)如何拿到用户控件中“文本框”中的数据
            //5.1)遍历用户控件中的所有控件，然后拿到我们想要的值。[不科学]
            //5.2)事件中，经常遇到两个参数object sender, EventArgs e.在事件里，如果你特别想要什么，都可以从参数e里面去取。

            //6)此时发现我们 自己写的事件，没有任何参数，跟系统的事件还是不一样。我们的事件里，没有这两个参数(object sender, EventArgs e)，有点山寨。接下来，就把我们的事件装成类似系统事件的样子。
            //7)因为这个事件参数里，我们要自定义一些值，所以我们可以继承EventArgs类并添加自己的属性。比如：我们要拿到用户名，密码，那么我们就可以把这两个属性封装到继承自EventArgs的类里面
        }
    }
}
