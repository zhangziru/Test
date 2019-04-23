using Cyrus.CodeGenerator.BLL;
using Cyrus.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyrus.CodeGenerator.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        //窗体加载
        private void Main_Load(object sender, EventArgs e)
        {
            cbxGender.SelectedIndex = 0;
            //加载所有人员信息
            LoadPersonData();
        }
        //获取总人数
        private void btnTest_Click(object sender, EventArgs e)
        {
            TblPersonBll bll = new TblPersonBll();
            MessageBox.Show(string.Format("当前的人数为：{0}人", bll.GetPersonCount()));
        }
        //增加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //1、采集数据
            TblPerson person = new TblPerson();
            person.Name = tbxName.Text.Trim();
            person.Age = Convert.ToInt32(tbxAge.Text.Trim());
            person.Height = Convert.ToInt32(tbxHeight.Text.Trim());
            person.Gender = cbxGender.SelectedIndex == 0 ? null : (bool?)(cbxGender.SelectedIndex == 1 ? true : false);
            //2、实例化业务逻辑层，调用方法
            TblPersonBll bll = new TblPersonBll();
            int r = bll.AddPerson(person);
            if (r > 0)
            {
                MessageBox.Show("增加成功！");
                LoadPersonData();
            }
        }

        //加载所有人员信息
        void LoadPersonData()
        {
            TblPersonBll bll = new TblPersonBll();
            List<TblPerson> list = bll.GetAllPerson();
            //指定数据源
            //1、我们可以自定义列的名称，不用模型绑定的字段名称
            //2、对DataGridView中的，性别 内容进行格式化显示
            //3、设置单行被选中，属性(MultiSelect)设置为 false
            //4、给 行 添加 进入事件处理方式
            this.dgvDataList.DataSource = list;
        }
        //单元格格式化
        private void dgvDataList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //只能根据 列索引来进行处理
            if (e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    e.Value = (bool)e.Value ? "男" : "女";
                }
            }
        }

        //当前行获取焦点事件
        private void dgvDataList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //1、获取当前选中行【e.RowIndex获取当前事件触发，所在的行索引】
            DataGridViewRow row = this.dgvDataList.Rows[e.RowIndex];
            //2、获取当前行 绑定的数据对象，将其转换为 TblPerson 类型对象
            TblPerson person = row.DataBoundItem as TblPerson;
            //3、把数据 显示到“编辑”框中
            if (person != null)
            {
                tbxName.Text = person.Name;
                tbxAge.Text = person.Age.ToString();
                tbxHeight.Text = person.Height.ToString();
                if (person.Gender == null)
                {
                    cbxGender.SelectedIndex = 0;
                }
                else if (person.Gender.Value)
                {

                    cbxGender.SelectedIndex = 1;
                }
                else
                {
                    cbxGender.SelectedIndex = 2;
                }
                lblId.Text = person.Id.ToString();
            }

        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //删除操作，提示
            DialogResult result = MessageBox.Show("确定要删除吗?", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                //有选中行
                if (this.dgvDataList.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(lblId.Text);
                    TblPersonBll bll = new TblPersonBll();
                    if (bll.DeletePersonById(id))
                    {
                        //删除成功后，重新加载数据
                        LoadPersonData();
                        MessageBox.Show("删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("操作异常！");
                    }
                }
                else
                {
                    MessageBox.Show("请选中行！");
                }
            }            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //1、采集数据
            TblPerson person = new TblPerson();
            person.Id = Convert.ToInt32(lblId.Text);
            person.Name = tbxName.Text.Trim();
            person.Age = Convert.ToInt32(tbxAge.Text.Trim());
            person.Height = Convert.ToInt32(tbxHeight.Text.Trim());
            person.Gender = cbxGender.SelectedIndex == 0 ? null : (bool?)(cbxGender.SelectedIndex == 1 ? true : false);
            //2、实例化业务逻辑层，调用方法
            TblPersonBll bll = new TblPersonBll();
            if (bll.UpdatePerson(person))
            {
                //重新加载数据
                LoadPersonData();
                MessageBox.Show("修改成功！");
            }
        }
    }
}
