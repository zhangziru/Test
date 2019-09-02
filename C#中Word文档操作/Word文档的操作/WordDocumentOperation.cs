using Aspose.Words;
using Aspose.Words.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp测试项目20181203
{
    #region
    //Word的写入操作，参考的链接：https://www.cnblogs.com/wuhuacong/archive/2012/08/30/2662961.html
    //Word的读取操作，参考的链接：https://www.cnblogs.com/z-huifei/p/6594336.html
    #endregion

    public class WordDocumentOperation
    {
        public WordDocumentOperation()
        {
            //WriteToWordDemo("WordFielDemo1.docx", "NewWordFielDemo1.docx");//向Word文档中写入内容
            //GainContentFormWordFile("物资装箱清单-047合同.8.22.docx");
            GainContentFormWordFileRealDemo("物资装箱清单-047合同.8.22.docx");
        }
        /// <summary>
        /// 在Word中 根据模板来创建二维表格
        /// </summary>
        /// <param name="templateFile">模板路径</param>
        /// <param name="saveDocFile">生成的文件保存路径</param>
        public void WriteToWordDemo(string templateFile, string saveDocFile)
        {
            try
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
                Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
                //DataTable nameList = DataTableHelper.CreateTable("编号,姓名,时间");
                DataTable nameList = new DataTable();
                #region 表头的定义
                nameList.Columns.Add("编号", typeof(string));
                nameList.Columns.Add("姓名", typeof(string));
                nameList.Columns.Add("时间", typeof(string)); 
                #endregion

                DataRow row = null;
                for (int i = 0; i < 50; i++)
                {
                    row = nameList.NewRow();
                    row["编号"] = i.ToString().PadLeft(4, '0');
                    row["姓名"] = "伍华聪 " + i.ToString();
                    row["时间"] = DateTime.Now.ToString();
                    nameList.Rows.Add(row);
                }

                List<double> widthList = new List<double>();
                for (int i = 0; i < nameList.Columns.Count; i++)
                {
                    builder.MoveToCell(0, 0, i, 0); //移动单元格                    
                    double width = builder.CellFormat.Width;//获取单元格宽度
                    widthList.Add(width);
                }

                builder.MoveToBookmark("table");        //开始添加值
                for (var i = 0; i < nameList.Rows.Count; i++)
                {
                    for (var j = 0; j < nameList.Columns.Count; j++)
                    {
                        builder.InsertCell();// 添加一个单元格                    
                        builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                        builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                        builder.CellFormat.Width = widthList[j];
                        builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                        builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;//垂直居中对齐
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                        builder.Write(nameList.Rows[i][j].ToString());
                    }
                    builder.EndRow();
                }
                doc.Range.Bookmarks["table"].Text = "";    // 清掉标示  

                doc.Save(saveDocFile);

                #region 打开文档的操作
                //if (MessageUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                //{
                //    System.Diagnostics.Process.Start(saveDocFile);
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// Word文档中 段落的提取
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ParagraphCollection WordParagraphs(string fileName)
        {
            Document doc = new Document(fileName);
            if (doc.FirstSection.Body.Paragraphs.Count > 0)
            {
                return doc.FirstSection.Body.Paragraphs;//word中的所有段落
            }
            return null;
        }

        public void GainContentFormWordFile(string fileName)
        {
            Model1 model1 = new Model1();
            List<Model2> listModel2 = new List<Model2>();
            Model3 model3 = new Model3();

            Document doc = new Document(fileName);

            var temp = doc.Sections[0];
            var body = temp.Body;
            var childNodes = body.ChildNodes;
            foreach (Node item in childNodes)
            {
                var itemType = item.NodeType.ToString();
                //根据节点的类型来判断内容的类型
                if (itemType == "Table")
                {
                    Table table = item as Table;
                    var tableChilds = table.ChildNodes;
                    foreach (Row row in tableChilds)
                    {
                        var rowChilds = row.ChildNodes;
                        var rowIndex = tableChilds.IndexOf(row);
                        foreach (Cell col in rowChilds)
                        {
                            //获取段落中运行的第一个文本
                            Node node = col.FirstChild;
                            string content = node == null ? "" : node.Range.Text;
                            //string content1 = node == null ? "" : col.FirstParagraph.Runs[0].Text;
                            string content2 = col.ToTxt();
                            Console.Write("{0}\t", content);
                        }
                        Console.WriteLine("");
                    }
                    Console.WriteLine("读取到了Table");
                }
            }
        }

        /// <summary>
        /// 获取 物资装箱清单Word 的数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>返回多个值：
        /// <para>Item1:清单编号（DB：运输单编号）</para>
        /// <para>Item2:物料运输数据</para>
        /// <para>Item3:物资运输清单分项 列表</para>
        /// </returns>
        public Tuple<string, MMS_MMT, List<MMS_MMT_ITEM>> GainContentFormWordFileRealDemo(string fileName)
        {
            //物料运输清单
            MMS_MMT mmt = new MMS_MMT();
            //物料运输清单列表
            List<MMS_MMT_ITEM> listMmtItem = new List<MMS_MMT_ITEM>();

            Document doc = new Document(fileName);

            var temp =  doc.Sections[0];
            var body = temp.Body;
            var childNodes = body.ChildNodes;
            //清单编号（DB：运输单编号）
            string listOrderNumber = string.Empty;
               
            foreach (Node item in childNodes)
            {
                var itemType = item.NodeType.ToString();
                if (itemType == "Paragraph" && GetStringFromNode(item).StartsWith("清单编号"))
                {
                    listOrderNumber = GetStringFromNode(item).Substring("清单编号：".Length);//清单编号
                }

                //根据节点的类型来判断内容的类型
                if (itemType == "Table")
                {
                    Table table = item as Table;
                    //表上
                    mmt.CONTRACT_NO = GetStringFromNode(GetCellNodeOfTableByPosition(table, 0, 1));
                    mmt.SUPPLIER_NAME = GetStringFromNode(GetCellNodeOfTableByPosition(table, 0, 3));
                    mmt.SUPPLIER_INFO = GetStringFromNode(GetCellNodeOfTableByPosition(table, 1, 1));
                    mmt.DELIVERY_DATE = GetStringFromNode(GetCellNodeOfTableByPosition(table, 1, 3));
                    mmt.DELIVERY_PLACE = GetStringFromNode(GetCellNodeOfTableByPosition(table, 2, 1));
                    mmt.TRANSPORT_MODE = GetStringFromNode(GetCellNodeOfTableByPosition(table, 2, 3));
                    mmt.PURCHASE_INFO = GetStringFromNode(GetCellNodeOfTableByPosition(table, 3, 1));
                    mmt.RECEIVING_UNIT = GetStringFromNode(GetCellNodeOfTableByPosition(table, 3, 3));
                    mmt.WAREHOUSE_INFO = GetStringFromNode(GetCellNodeOfTableByPosition(table, 4, 1));
                    mmt.ESTIMATED_ARRIVAL_DATE = GetStringFromNode(GetCellNodeOfTableByPosition(table, 4, 3));
                    mmt.ARRIVAL_PLACE = GetStringFromNode(GetCellNodeOfTableByPosition(table, 5, 1));
                    mmt.TRANSPORT_UNIT = GetStringFromNode(GetCellNodeOfTableByPosition(table, 5, 3));

                    //详细信息的读取                    
                    int startIndex = 6 + 1;//表头索引 + 1
                    int endIndex = table.ChildNodes.IndexOf(table.LastRow);
                    for (int i = startIndex + 1; i <= endIndex; i++)
                    {
                        int colIndex = 0;
                        string MaterialCode = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, 1));
                        if (string.IsNullOrEmpty(MaterialCode))
                        {
                            break;
                        }
                        MMS_MMT_ITEM mmsItem = new MMS_MMT_ITEM();
                        mmsItem.SORT = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.NUMBER = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.NAME = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.SPECIFICATION_TYPE = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.PRODUCTION_STANDARD = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.UNIT = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.QUANTITY = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.BOX_NO = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.HEAT_NO = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));
                        mmsItem.UNIT_NO = GetStringFromNode(GetCellNodeOfTableByPosition(table, i, colIndex++));

                        listMmtItem.Add(mmsItem);
                    }

                }
            }

            return new Tuple<string, MMS_MMT, List<MMS_MMT_ITEM>>(listOrderNumber, mmt, listMmtItem);
        }

        /// <summary>
        /// 获取给定节点中的文本信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetStringFromNode(Node node)
        {
            return node == null ? "" : node.ToString(SaveFormat.Text).Replace("\r\n","");
        }

        /// <summary>
        /// 根据 单元格的位置 获取Word文档中 Table对象的单元
        /// </summary>
        /// <param name="wordTable">Word中的Table对象</param>
        /// <param name="rowIndex">单元格的行索引</param>
        /// <param name="colIndex">单元格的列索引</param>
        /// <returns></returns>
        public Cell GetCellNodeOfTableByPosition(Table wordTable, int rowIndex, int colIndex)
        {
            return (wordTable.ChildNodes[rowIndex] as Row).ChildNodes[colIndex] as Cell;
        }
    }
}
