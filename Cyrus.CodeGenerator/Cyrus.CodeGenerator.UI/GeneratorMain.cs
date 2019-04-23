using Cyrus.CodeGenerator.BLL;
using Cyrus.CodeGenerator.Model;
using Cyrus.CodeGenerator.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyrus.CodeGenerator.UI
{
    public partial class GeneratorMain : Form
    {
        public GeneratorMain()
        {
            InitializeComponent();
        }

        private void GeneratorMain_Load(object sender, EventArgs e)
        {
            cbxTable.SelectedIndex = 0;
        }

        //单表生成->缓存
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string tableName = tbxTableName.Text.Trim();
            string nameSpace = tbxNameSpace.Text.Trim();
            ColumnInfoBll bll = new ColumnInfoBll();
            tbxContent.Text = bll.DynamicGenerateModel(tableName, nameSpace);
        }

        //单表生成->本地（持久化操作）
        private void btnSaveLocal_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = tbxTableName.Text.Trim();
                string nameSpace = tbxNameSpace.Text.Trim();//命名空间
                ColumnInfoBll columnInfoBll = new ColumnInfoBll();
                //获取生成的内容
                string generateContent = columnInfoBll.DynamicGenerateModel(tableName, nameSpace);
                //缓存内容持久化（保存）到本地
                string pathModel = string.Format("{0}/Model", AppDomain.CurrentDomain.BaseDirectory);//模型管理目录
                string pathModelFile = string.Format("{0}/Model/{1}.cs", AppDomain.CurrentDomain.BaseDirectory, tableName);//模型文件
                if (!Directory.Exists(pathModel))
                {
                    //不存在
                    Directory.CreateDirectory(pathModel);
                }
                File.WriteAllText(pathModelFile, generateContent, Encoding.UTF8);//写入操作
                MessageBox.Show("数据持久化成功，请前往应用程序所在的目录下查看！");
            }
            catch (Exception)
            {

                MessageBox.Show("数据持久化失败！");
            }
        }

        //批量生成->本地（持久化操作）
        private void btnBatchSaveLocal_Click(object sender, EventArgs e)
        {
            try
            {
                string nameSpace = tbxNameSpace.Text.Trim();//命名空间                 
                enumTableType tableType = (enumTableType)Enum.Parse(typeof(enumTableType), cbxTable.SelectedItem.ToString());//当前选中的类型值

                //获取当前库中的所有表(当前只做基础表，稍后将该选项抛给前端选择。)
                TableInfoBll tableInfoBll = new TableInfoBll();
                ColumnInfoBll columnInfoBll = new ColumnInfoBll();
                List<TableInfo> listTables = tableInfoBll.FindTableInfoByTableType(tableType);
                foreach (var item in listTables)
                {
                    //Model持久化
                    //获取生成的内容
                    string generateContent = columnInfoBll.DynamicGenerateModel(item.TableName, nameSpace);
                    //缓存内容持久化（保存）到本地
                    string pathModel = string.Format("{0}/Model", AppDomain.CurrentDomain.BaseDirectory);//模型管理目录
                    string pathModelFile = string.Format("{0}/Model/{1}.cs", AppDomain.CurrentDomain.BaseDirectory, item.TableName);//模型文件
                    if (!Directory.Exists(pathModel))
                    {
                        //不存在
                        Directory.CreateDirectory(pathModel);
                    }
                    File.WriteAllText(pathModelFile, generateContent, Encoding.UTF8);//写入操作
                }
                MessageBox.Show("数据持久化成功，请前往应用程序所在的目录下查看！");
            }
            catch (Exception)
            {

                MessageBox.Show("数据持久化失败！");
            }
        }
        //表类型 更改事件
        private void cbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //当前选中的值
            enumTableType tableType = (enumTableType)Enum.Parse(typeof(enumTableType), cbxTable.SelectedItem.ToString());
            //获取当前类型的表 信息列表
            TableInfoBll tableInfoBll = new TableInfoBll();
            List<TableInfo> listTableInfo = tableInfoBll.FindTableInfoByTableType(tableType);
            //要操作的类型下的表 列表
            string contentTableInfo = string.Join(Environment.NewLine, listTableInfo.Select(p => p.TableName).ToArray());
            tbxContent.Text = contentTableInfo;
        }

    }
}
