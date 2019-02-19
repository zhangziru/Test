using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateScriptCache
{
    public partial class UpdateScriptCache : Form
    {
        //引用脚本的 版本控制字段
        private string verStr = "ver";
        //构造函数
        public UpdateScriptCache()
        {
            InitializeComponent();
            tbxScriptSuffix.Text = "js;css";//默认检索js和css文件的修改
            tbxReferenceSuffix.Text = "html;aspx;cshtml";//默认检索heml和aspx文件的引用
            tbxProjectPath.Text = @"D:\WebRelease\AutoVerForcedRefresh";//测试的默认值
        }
        //检索所有的脚本
        private void btnSearchAllScript_Click(object sender, EventArgs e)
        {
            try
            {
                //项目路径
                string projectPath = tbxProjectPath.Text.Trim();
                //脚本后缀(用 | 替换“;” 分号)
                string scriptSuffix = string.Format("({0})$", tbxScriptSuffix.Text.Trim().Replace(";", "|"));

                if (string.IsNullOrEmpty(projectPath))
                {
                    MessageBox.Show("请输入项目路径");
                    return;
                }

                if (!Directory.Exists(projectPath))
                {
                    MessageBox.Show("您给定的项目路径不存在！");
                    return;
                }

                lbxScriptOutput.Items.Clear();
                //所有的脚本路径
                string[] scriptPaths = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in scriptPaths)
                {
                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    if (Regex.IsMatch(ext, scriptSuffix))
                    {
                        //文件的筛选     
                        lbxScriptOutput.Items.Add(item.Replace(projectPath, ""));
                    }
                }
                btnUpdateScriptVersion.Enabled = true;
                MessageBox.Show("检索完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 检索变更的脚本文件
        private void btnSearchChangeScript_Click(object sender, EventArgs e)
        {
            try
            {
                //项目路径
                string projectPath = tbxProjectPath.Text.Trim();
                //脚本后缀(用 | 替换“;” 分号)
                string scriptSuffix = string.Format("({0})$", tbxScriptSuffix.Text.Trim().Replace(";", "|"));
                //引用文件后缀(用 | 替换“;” 分号)
                string referenceSuffix = string.Format("^({0})$", tbxReferenceSuffix.Text.Trim().Replace(";", "|"));
                if (string.IsNullOrEmpty(projectPath))
                {
                    MessageBox.Show("请输入项目路径");
                    return;
                }

                if (!Directory.Exists(projectPath))
                {
                    MessageBox.Show("您给定的项目路径不存在！");
                    return;
                }

                lbxScriptOutput.Items.Clear();
                lbxScriptOutput.Items.Clear();
                int count = 0;
                //所有的脚本路径
                string[] scriptPaths = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in scriptPaths)
                {

                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    //当前脚本 是否编辑
                    bool isEdit = false;
                    if (Regex.IsMatch(ext, scriptSuffix))
                    {

                        //文件的筛选
                        List<FileInfo> list = new List<FileInfo>();
                        GetAllReferenceScriptFiles(projectPath, projectPath, referenceSuffix, item, list, out isEdit);
                        if (isEdit)
                        {
                            lbxScriptOutput.Items.Add(item.Replace(projectPath, ""));
                            count++;
                        }
                    }
                }
                btnUpdateScriptVersion.Enabled = true;
                MessageBox.Show(string.Format("当前有{0}个脚本文件被更新", count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //更新引用文件中 脚本的版本
        private void btnUpdateScriptVersion_Click(object sender, EventArgs e)
        {
            try
            {
                //项目路径
                string projectPath = tbxProjectPath.Text.Trim();
                //脚本后缀(用 | 替换“;” 分号)
                string scriptSuffix = string.Format("({0})$", tbxScriptSuffix.Text.Trim().Replace(";", "|"));
                //引用文件后缀(用 | 替换“;” 分号)
                string referenceSuffix = string.Format("^({0})$", tbxReferenceSuffix.Text.Trim().Replace(";", "|"));
                if (string.IsNullOrEmpty(projectPath))
                {
                    MessageBox.Show("请输入项目路径");
                    return;
                }

                if (!Directory.Exists(projectPath))
                {
                    MessageBox.Show("您给定的项目路径不存在！");
                    return;
                }

                //lbxScriptOutput.Items.Clear();
                lbxReferenceFilesOutput.Items.Clear();
                int count = 0;
                //所有的脚本路径
                string[] scriptPaths = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in scriptPaths)
                {
                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    //当前脚本 是否编辑
                    bool isEdit = false;
                    if (Regex.IsMatch(ext, scriptSuffix))
                    {
                        //lbxScriptOutput.Items.Add(item.Replace(projectPath, ""));
                        //文件的筛选
                        List<FileInfo> list = new List<FileInfo>();
                        UpdateAllReferenceScriptFiles(projectPath, projectPath, referenceSuffix, item, list, out isEdit);
                        if (isEdit)
                        {
                            lbxReferenceFilesOutput.Items.Add(item.Replace(projectPath, ""));
                            foreach (var reference in list)
                            {
                                lbxReferenceFilesOutput.Items.Add("\t" + reference.FullName.Replace(projectPath, ""));
                            }
                            count++;
                        }
                    }
                }
                btnUpdateScriptVersion.Enabled = false;
                MessageBox.Show(string.Format("当前有{0}个脚本文件被更新，{1}处脚本引用的版本号被替换。", count, lbxReferenceFilesOutput.Items.Count - count));
            }
            catch (Exception)
            {
                MessageBox.Show("当前的脚本引用替换出错！");
            }
        }

        #region 获取给定文件夹下的文件
        //获取指定类型的文件
        private void GetSpecifiedTypeFiles(string floderPath, string extFilter, List<FileInfo> listFiles)
        {
            //所有文件路径
            string[] filePaths = Directory.GetFiles(floderPath);
            foreach (var item in filePaths)
            {
                //获取后缀（不要点）
                string ext = Path.GetExtension(item).Substring(1);
                if (Regex.IsMatch(ext, extFilter))
                {
                    //文件的筛选
                    FileInfo fileInfo = new FileInfo(item);
                    listFiles.Add(fileInfo);
                }
            }

            //所有文件夹路径
            string[] directoryPaths = Directory.GetDirectories(floderPath);
            foreach (var item in directoryPaths)
            {
                GetSpecifiedTypeFiles(item, extFilter, listFiles);//递归调用，直到最后一层没有目录，结束调用
            }
        }
        #endregion

        #region MD5
        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        #endregion

        /// <summary>
        /// 更新 给定文件夹下，指定文件类型 对脚本的引用文件 中脚本的版本号
        /// </summary>
        /// <param name="projectRootPath">项目根路径</param>
        /// <param name="referenceRootPath">引用文件根路径</param>
        /// <param name="extFilter">后缀过滤器（引用脚本文件）</param>
        /// <param name="scriptPath">脚本的绝对路径</param>
        /// <param name="listReferenceScriptFiles">引用变更脚本的文件列表</param>
        /// <param name="isEdit">判断脚本文件是否被编辑过，true：是，false：否(默认)</param> 
        private void UpdateAllReferenceScriptFiles(string projectRootPath, string referenceRootPath, string extFilter, string scriptPath, List<FileInfo> listReferenceScriptFiles, out bool isEdit)
        {
            try
            {
                //判断脚本是否更新
                //如果是更新的，筛选出引用文件
                isEdit = false;
                string scriptRelativePath = scriptPath.Substring(projectRootPath.Length + 1).Replace("\\", "/");//脚本相对路径
                string scriptContent = File.ReadAllText(scriptPath, Encoding.Default);
                string scriptMD5 = MD5(scriptContent);//脚本文件的MD5码
                string[] filePaths = Directory.GetFiles(referenceRootPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in filePaths)
                {
                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    if (Regex.IsMatch(ext, extFilter))
                    {
                        #region 文件的过滤（指定的类型做相应的处理）
                        string fileContent = File.ReadAllText(item, Encoding.Default);
                        string newFileContent = string.Empty;//新的文件内容
                        //匹配脚本的正则表达式，并且提取出引用的链接
                        string patternRoot = "(@Href\\(\"~/\"\\)|~/|/)";
                        string patternJS = "<script[^>]*?src=\"" + patternRoot + "([^>]*?)\"[^>]*?>";//js脚本引用 正则匹配模式
                        string patternCss = "<link[^>]*?href=\"" + patternRoot + "([^>]*?)\"[^>]*?>";//css脚本引用 正则匹配模式

                        #region JS脚本的匹配

                        MatchCollection mcs = Regex.Matches(fileContent, patternJS, RegexOptions.IgnoreCase);
                        foreach (Match m in mcs)
                        {
                            bool isControl = false;//该脚本在引用时，是否受版本控制（如果，不受控制，就没有 旧的MD5值）
                            bool matchStatus = false;//引用文件中的脚本 是否与当前的脚本 匹配
                            string scriptLink = m.Groups[2].Value;
                            //判断 js的路径 与 引用文件中的 JS 是否匹配
                            if (scriptLink.IndexOf("?") > -1)
                            {//最关键的是配对成功
                                matchStatus = scriptRelativePath.EndsWith(scriptLink.Substring(0, scriptLink.IndexOf("?")).Replace("~/", ""));
                            }
                            if (matchStatus)
                            {
                                //当前的脚本 与 文件中的引用 匹配成功

                                #region 匹配成功处理
                                string[] linkParams = scriptLink.Substring(scriptLink.IndexOf("?") + 1).Split('&');//脚本链接后面的参数对 集合
                                string oldMD5 = string.Empty;//脚本链接后面最后参数对(包含版本参数v，表示受版本控制，没有版本参数不予处理)
                                foreach (var param in linkParams)
                                {
                                    //是否包含 版本控制参数
                                    if (param.Split('=')[0] == verStr)
                                    {
                                        isControl = true;//该脚本受版本控制，获取 老的MD5码
                                        oldMD5 = param.Split('=')[1];
                                    }
                                }

                                if (isControl)
                                {
                                    if (oldMD5 == scriptMD5)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        //当前脚本被更改
                                        isEdit = true;
                                        //更新文件中，引用脚本的版本
                                        scriptLink = scriptLink.Insert(scriptLink.LastIndexOf(verStr + "="), ")(");
                                        scriptLink = scriptLink.Insert(scriptLink.LastIndexOf(verStr + "=") + oldMD5.Length + verStr.Length + 1, ")(");//向提取的链接中添加一个分组（构建新的匹配模式）
                                        scriptLink = scriptLink.Replace("?", "\\?");
                                        string newPattern = "(<script[^>]*?src=\"" + patternRoot + scriptLink + "\"[^>]*?>)";
                                        //新的文件内容（得到替换后的文件内容）
                                        newFileContent = Regex.Replace(fileContent, newPattern, string.Format("$1{0}={1}$4", verStr, scriptMD5));
                                        //更新源文件
                                        File.WriteAllText(item, newFileContent, Encoding.Default);
                                        //获取该文件的信息
                                        FileInfo fileInfo = new FileInfo(item);
                                        listReferenceScriptFiles.Add(fileInfo);
                                    }
                                    //提取到的JS脚本链接
                                    //提取JS脚本 的MD5码
                                    //判断是否与源文件的MD5码=>是否更新
                                    //最后更新 
                                }
                                else
                                {
                                    //该引用不受版本控制，不做处理
                                }
                                #endregion
                            }
                            else
                            {
                                continue;
                            }

                        }
                        #endregion

                        #region CSS脚本的匹配
                        mcs = Regex.Matches(fileContent, patternCss, RegexOptions.IgnoreCase);
                        foreach (Match m in mcs)
                        {
                            bool isControl = false;//该脚本在引用时，是否受版本控制
                            bool matchStatus = false;//引用文件中的脚本 是否与当前的脚本 匹配
                                                     //提取到的Css脚本链接
                            string scriptLink = m.Groups[2].Value;
                            //判断 js的路径 与 引用文件中的 CSS 是否匹配
                            if (scriptLink.IndexOf("?") > -1)
                            {
                                matchStatus = scriptRelativePath.EndsWith(scriptLink.Substring(0, scriptLink.IndexOf("?")).Replace("~/", ""));
                            }
                            if (matchStatus)
                            {
                                //当前的脚本 与 文件中的引用 匹配成功

                                #region 匹配成功处理                        
                                string[] linkParams = scriptLink.Substring(scriptLink.IndexOf("?") + 1).Split('&');//脚本链接后面的参数对 集合
                                string oldMD5 = string.Empty;//脚本链接后面最后参数对(包含版本参数v，表示受版本控制，没有版本参数不予处理)
                                foreach (var param in linkParams)
                                {
                                    //是否包含 版本控制参数
                                    if (param.Split('=')[0] == verStr)
                                    {
                                        isControl = true;//该脚本受版本控制，获取 老的MD5码
                                        oldMD5 = param.Split('=')[1];
                                    }
                                }
                                if (isControl)
                                {
                                    if (oldMD5 == scriptMD5)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        //当前脚本被更改
                                        isEdit = true;
                                        //更新文件中，引用脚本的版本
                                        scriptLink = scriptLink.Insert(scriptLink.LastIndexOf(verStr + "="), ")(");
                                        scriptLink = scriptLink.Insert(scriptLink.LastIndexOf(verStr + "=") + oldMD5.Length + verStr.Length + 1, ")(");//向提取的链接中添加一个分组（构建新的匹配模式）
                                        scriptLink = scriptLink.Replace("?", "\\?");
                                        string newPattern = "(<link[^>]*?href=\"" + patternRoot + scriptLink + "\"[^>]*?>)";
                                        //新的文件内容（得到替换后的文件内容）
                                        newFileContent = Regex.Replace(fileContent, newPattern, string.Format("$1{0}={1}$4", verStr, scriptMD5));
                                        //更新源文件
                                        File.WriteAllText(item, newFileContent, Encoding.Default);
                                        //获取该文件的信息
                                        FileInfo fileInfo = new FileInfo(item);
                                        listReferenceScriptFiles.Add(fileInfo);
                                    }
                                }
                                else
                                {
                                    //该引用不受版本控制，不做处理
                                }
                                #endregion
                            }
                            else
                            {
                                continue;
                            }
                        }
                        #endregion


                        #endregion
                    }
                    else
                    {
                        //不是指定的后缀 不做处理
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取给定文件夹下，筛选指定文件类型 对脚本的引用文件
        /// </summary>
        /// <param name="projectRootPath">项目根路径</param>
        /// <param name="referenceRootPath">引用文件根路径</param>
        /// <param name="extFilter">后缀过滤器（引用脚本文件）</param>
        /// <param name="scriptPath">脚本的绝对路径</param>
        /// <param name="listReferenceScriptFiles">引用变更脚本的文件列表</param>
        /// <param name="isEdit">判断脚本文件是否被编辑过，true：是，false：否(默认)</param> 
        private void GetAllReferenceScriptFiles(string projectRootPath, string referenceRootPath, string extFilter, string scriptPath, List<FileInfo> listReferenceScriptFiles, out bool isEdit)
        {
            //判断脚本是否更新
            //如果是更新的，筛选出引用文件
            isEdit = false;
            try
            {             
                string scriptRelativePath = scriptPath.Substring(projectRootPath.Length + 1).Replace("\\", "/");//脚本相对路径
                string scriptContent = File.ReadAllText(scriptPath, Encoding.Default);
                string scriptMD5 = MD5(scriptContent);//脚本文件的MD5码
                string[] filePaths = Directory.GetFiles(referenceRootPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in filePaths)
                {
                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    if (Regex.IsMatch(ext, extFilter))
                    {
                        #region 文件的过滤（指定的类型做相应的处理）
                        string fileContent = File.ReadAllText(item, Encoding.Default);
                        string newFileContent = string.Empty;//新的文件内容
                                                             //匹配脚本的正则表达式，并且提取出引用的链接
                        string patternRoot = "(@Href\\(\"~/\"\\)|~/|/)";
                        string patternJS = "<script[^>]*?src=\"" + patternRoot + "([^>]*?)\"[^>]*?>";//js脚本引用 正则匹配模式
                        string patternCss = "<link[^>]*?href=\"" + patternRoot + "([^>]*?)\"[^>]*?>";//css脚本引用 正则匹配模式

                        #region JS脚本的匹配

                        MatchCollection mcs = Regex.Matches(fileContent, patternJS, RegexOptions.IgnoreCase);
                        foreach (Match m in mcs)
                        {
                            bool isControl = false;//该脚本在引用时，是否受版本控制（如果，不受控制，就没有 旧的MD5值）
                            bool matchStatus = false;//引用文件中的脚本 是否与当前的脚本 匹配
                            string oldMD5 = string.Empty;//脚本链接后面最后参数对(包含版本参数v，表示受版本控制，没有版本参数不予处理)
                            string scriptLink = m.Groups[2].Value;
                            //判断 js的路径 与 引用文件中的 JS 是否匹配
                            if (scriptLink.IndexOf("?") > -1)
                            {//最关键的是配对成功
                                matchStatus = scriptRelativePath.EndsWith(scriptLink.Substring(0, scriptLink.IndexOf("?")).Replace("~/", ""));
                                string[] linkParams = scriptLink.Substring(scriptLink.IndexOf("?") + 1).Split('&');//脚本链接后面的参数对 集合                            
                                foreach (var param in linkParams)
                                {
                                    //是否包含 版本控制参数
                                    if (param.Split('=')[0] == verStr)
                                    {
                                        isControl = true;//该脚本受版本控制，获取 老的MD5码
                                        oldMD5 = param.Split('=')[1];
                                    }
                                }
                            }
                            else
                            {
                                matchStatus = scriptRelativePath.EndsWith(scriptLink);
                                isControl = false;//(没有?表示：没有版本控制参数，所以不可控制)
                            }

                            if (matchStatus)
                            {
                                //当前的脚本 与 文件中的引用 匹配成功

                                #region 匹配成功处理                            

                                if (isControl)
                                {
                                    if (oldMD5 == scriptMD5)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        //当前脚本被更改
                                        isEdit = true;
                                        //获取该文件的信息
                                        FileInfo fileInfo = new FileInfo(item);
                                        listReferenceScriptFiles.Add(fileInfo);
                                    }
                                    //提取到的JS脚本链接
                                    //提取JS脚本 的MD5码
                                    //判断是否与源文件的MD5码=>是否更新
                                    //最后更新 
                                }
                                else
                                {
                                    //该引用不受版本控制，不做处理
                                }
                                #endregion
                            }
                            else
                            {
                                continue;
                            }

                        }
                        #endregion

                        #region CSS脚本的匹配
                        mcs = Regex.Matches(fileContent, patternCss, RegexOptions.IgnoreCase);
                        foreach (Match m in mcs)
                        {
                            bool isControl = false;//该脚本在引用时，是否受版本控制
                            bool matchStatus = false;//引用文件中的脚本 是否与当前的脚本 匹配
                                                     //提取到的Css脚本链接
                            string scriptLink = m.Groups[2].Value;
                            string oldMD5 = string.Empty;//脚本链接后面最后参数对(包含版本参数v，表示受版本控制，没有版本参数不予处理)
                                                         //判断 js的路径 与 引用文件中的 JS 是否匹配
                            if (scriptLink.IndexOf("?") > -1)
                            {
                                matchStatus = scriptRelativePath.EndsWith(scriptLink.Substring(0, scriptLink.IndexOf("?")));
                                string[] linkParams = scriptLink.Substring(scriptLink.IndexOf("?") + 1).Split('&');//脚本链接后面的参数对 集合

                                foreach (var param in linkParams)
                                {
                                    //是否包含 版本控制参数
                                    if (param.Split('=')[0] == verStr)
                                    {
                                        isControl = true;//该脚本受版本控制，获取 老的MD5码
                                        oldMD5 = param.Split('=')[1];
                                    }
                                }
                            }
                            else
                            {
                                matchStatus = scriptRelativePath.EndsWith(scriptLink);
                                isControl = false;//(没有?表示：没有版本控制参数，所以不可控制)
                            }

                            if (matchStatus)
                            {
                                //当前的脚本 与 文件中的引用 匹配成功

                                #region 匹配成功处理                        

                                if (isControl)
                                {
                                    if (oldMD5 == scriptMD5)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        //当前脚本被更改
                                        isEdit = true;
                                        //获取该文件的信息
                                        FileInfo fileInfo = new FileInfo(item);
                                        listReferenceScriptFiles.Add(fileInfo);
                                    }
                                }
                                else
                                {
                                    //该引用不受版本控制，不做处理
                                }
                                #endregion
                            }
                            else
                            {
                                continue;
                            }
                        }
                        #endregion


                        #endregion
                    }
                    else
                    {
                        //不是指定的后缀 不做处理
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectText = string.Empty;
            foreach (var item in ((ListBox)ctmRigthControl.SourceControl).SelectedItems)
            {
                selectText += item.ToString() + Environment.NewLine;
            }            
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }
        //项目路径文本框双击
        private void tbxProjectPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbxProjectPath.Text = dialog.SelectedPath;
            }
        }
        //脚本指定路径文本框双击
        private void tbxScriptPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbxScriptPath.Text = dialog.SelectedPath;
            }
        }
        //引用指定路径文本框双击
        private void tbxReferenceFilesPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbxReferenceFilesPath.Text = dialog.SelectedPath;
            }
        }
        //检索指定路径的脚本文件
        private void btnSercheSpecifiedScript_Click(object sender, EventArgs e)
        {
            try
            {
                //项目路径
                string scriptPath = tbxScriptPath.Text.Trim();
                //脚本后缀(用 | 替换“;” 分号)
                string scriptSuffix = string.Format("({0})$", tbxScriptSuffix.Text.Trim().Replace(";", "|"));
                if (string.IsNullOrEmpty(scriptPath))
                {
                    MessageBox.Show("请输入脚本路径");
                    return;
                }

                if (!Directory.Exists(scriptPath))
                {
                    MessageBox.Show("您给定的脚本路径不存在！");
                    return;
                }

                lbxScriptOutput.Items.Clear();
                //所有的脚本路径
                string[] scriptPaths = Directory.GetFiles(scriptPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in scriptPaths)
                {
                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    if (Regex.IsMatch(ext, scriptSuffix))
                    {
                        //文件的筛选 
                        lbxScriptOutput.Items.Add(item.Replace(scriptPath, ""));
                    }
                }
                btnUpdateSpecifiedScriptVersion.Enabled = true;
                MessageBox.Show("检索完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //检索指定路径脚本 在指定的文件中的引用 是否改变
        private void btnSerchSpecifiedChangeReference_Click(object sender, EventArgs e)
        {
            try
            {
                //项目根路径
                string projectPath = tbxProjectPath.Text.Trim();
                //脚本路径
                string scriptPath = tbxScriptPath.Text.Trim();
                //引用文件路径
                string referenceFilePath = tbxReferenceFilesPath.Text.Trim();
                //脚本后缀(用 | 替换“;” 分号)
                string scriptSuffix = string.Format("({0})$", tbxScriptSuffix.Text.Trim().Replace(";", "|"));
                //引用文件后缀(用 | 替换“;” 分号)
                string referenceSuffix = string.Format("^({0})$", tbxReferenceSuffix.Text.Trim().Replace(";", "|"));

                #region Check
                if (string.IsNullOrEmpty(projectPath))
                {
                    MessageBox.Show("请输入项目路径");
                    return;
                }

                if (!Directory.Exists(projectPath))
                {
                    MessageBox.Show("您给定的项目路径不存在！");
                    return;
                }
                if (string.IsNullOrEmpty(scriptPath))
                {
                    MessageBox.Show("请输入脚本路径");
                    return;
                }

                if (!Directory.Exists(scriptPath))
                {
                    MessageBox.Show("您给定的脚本路径不存在！");
                    return;
                }

                if (string.IsNullOrEmpty(referenceFilePath))
                {
                    MessageBox.Show("请输入引用文件路径");
                    return;
                }

                if (!Directory.Exists(referenceFilePath))
                {
                    MessageBox.Show("您给定的引用文件路径不存在！");
                    return;
                } 
                #endregion

                lbxScriptOutput.Items.Clear();
                lbxScriptOutput.Items.Clear();
                int count = 0;
                //所有的脚本路径
                string[] scriptPaths = Directory.GetFiles(scriptPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in scriptPaths)
                {

                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    //当前脚本 是否编辑
                    bool isEdit = false;
                    if (Regex.IsMatch(ext, scriptSuffix))
                    {

                        //文件的筛选
                        List<FileInfo> list = new List<FileInfo>();
                        GetAllReferenceScriptFiles(projectPath, referenceFilePath, referenceSuffix, item, list, out isEdit);
                        if (isEdit)
                        {
                            lbxScriptOutput.Items.Add(item.Replace(projectPath, ""));
                            count++;
                        }
                    }
                }
                btnUpdateSpecifiedScriptVersion.Enabled = true;
                MessageBox.Show(string.Format("当前有{0}个脚本文件被更新", count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //更新指定路径下引用文件中 脚本的引用
        private void btnUpdateSpecifiedScriptVersion_Click(object sender, EventArgs e)
        {
            try
            {
                //项目根路径
                string projectPath = tbxProjectPath.Text.Trim();
                //脚本路径
                string scriptPath = tbxScriptPath.Text.Trim();
                //引用文件路径
                string referenceFilePath = tbxReferenceFilesPath.Text.Trim();
                //脚本后缀(用 | 替换“;” 分号)
                string scriptSuffix = string.Format("({0})$", tbxScriptSuffix.Text.Trim().Replace(";", "|"));
                //引用文件后缀(用 | 替换“;” 分号)
                string referenceSuffix = string.Format("^({0})$", tbxReferenceSuffix.Text.Trim().Replace(";", "|"));

                #region Check
                if (string.IsNullOrEmpty(projectPath))
                {
                    MessageBox.Show("请输入项目路径");
                    return;
                }

                if (!Directory.Exists(projectPath))
                {
                    MessageBox.Show("您给定的项目路径不存在！");
                    return;
                }
                if (string.IsNullOrEmpty(scriptPath))
                {
                    MessageBox.Show("请输入脚本路径");
                    return;
                }

                if (!Directory.Exists(scriptPath))
                {
                    MessageBox.Show("您给定的脚本路径不存在！");
                    return;
                }

                if (string.IsNullOrEmpty(referenceFilePath))
                {
                    MessageBox.Show("请输入引用文件路径");
                    return;
                }

                if (!Directory.Exists(referenceFilePath))
                {
                    MessageBox.Show("您给定的引用文件路径不存在！");
                    return;
                }
                #endregion


                //lbxScriptOutput.Items.Clear();
                lbxReferenceFilesOutput.Items.Clear();
                int count = 0;
                //所有的脚本路径
                string[] scriptPaths = Directory.GetFiles(scriptPath, "*.*", SearchOption.AllDirectories);
                foreach (var item in scriptPaths)
                {
                    //获取后缀（不要点）
                    string ext = Path.GetExtension(item).Substring(1);
                    //当前脚本 是否编辑
                    bool isEdit = false;
                    if (Regex.IsMatch(ext, scriptSuffix))
                    {
                        //lbxScriptOutput.Items.Add(item.Replace(projectPath, ""));
                        //文件的筛选
                        List<FileInfo> list = new List<FileInfo>();
                        UpdateAllReferenceScriptFiles(projectPath, referenceFilePath, referenceSuffix, item, list, out isEdit);
                        if (isEdit)
                        {
                            lbxReferenceFilesOutput.Items.Add(item.Replace(scriptPath, ""));
                            foreach (var reference in list)
                            {
                                lbxReferenceFilesOutput.Items.Add("\t" + reference.FullName.Replace(projectPath, ""));
                            }
                            count++;
                        }
                    }
                }
                btnUpdateScriptVersion.Enabled = false;
                MessageBox.Show(string.Format("当前有{0}个脚本文件被更新，{1}处脚本引用的版本号被替换。", count, lbxReferenceFilesOutput.Items.Count - count));
            }
            catch (Exception)
            {
                MessageBox.Show("当前的脚本引用替换出错！");
            }
        }
    }
}
