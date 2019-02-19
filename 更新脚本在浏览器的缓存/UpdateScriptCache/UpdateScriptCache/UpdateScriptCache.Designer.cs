namespace UpdateScriptCache
{
    partial class UpdateScriptCache
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateScriptCache));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxProjectPath = new System.Windows.Forms.TextBox();
            this.tbxScriptSuffix = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxReferenceSuffix = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSearchAllScript = new System.Windows.Forms.Button();
            this.btnUpdateScriptVersion = new System.Windows.Forms.Button();
            this.btnSearchChangeScript = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbxScriptOutput = new System.Windows.Forms.ListBox();
            this.ctmRigthControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.lbxReferenceFilesOutput = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSercheSpecifiedScript = new System.Windows.Forms.Button();
            this.btnUpdateSpecifiedScriptVersion = new System.Windows.Forms.Button();
            this.btnSerchSpecifiedChangeReference = new System.Windows.Forms.Button();
            this.tbxReferenceFilesPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxScriptPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctmRigthControl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "脚本后缀";
            // 
            // tbxProjectPath
            // 
            this.tbxProjectPath.Location = new System.Drawing.Point(97, 14);
            this.tbxProjectPath.Name = "tbxProjectPath";
            this.tbxProjectPath.Size = new System.Drawing.Size(213, 21);
            this.tbxProjectPath.TabIndex = 2;
            this.tbxProjectPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxProjectPath_MouseDoubleClick);
            // 
            // tbxScriptSuffix
            // 
            this.tbxScriptSuffix.Location = new System.Drawing.Point(97, 51);
            this.tbxScriptSuffix.Name = "tbxScriptSuffix";
            this.tbxScriptSuffix.Size = new System.Drawing.Size(213, 21);
            this.tbxScriptSuffix.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbxReferenceSuffix);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxScriptSuffix);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxProjectPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 146);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(95, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "说明：多种后缀以“;”分号隔开";
            // 
            // tbxReferenceSuffix
            // 
            this.tbxReferenceSuffix.Location = new System.Drawing.Point(97, 97);
            this.tbxReferenceSuffix.Name = "tbxReferenceSuffix";
            this.tbxReferenceSuffix.Size = new System.Drawing.Size(213, 21);
            this.tbxReferenceSuffix.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "引用文件后缀";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(95, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "说明：多种后缀以“;”分号隔开";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearchAllScript);
            this.groupBox2.Controls.Add(this.btnUpdateScriptVersion);
            this.groupBox2.Controls.Add(this.btnSearchChangeScript);
            this.groupBox2.Location = new System.Drawing.Point(402, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 140);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // btnSearchAllScript
            // 
            this.btnSearchAllScript.Location = new System.Drawing.Point(92, 20);
            this.btnSearchAllScript.Name = "btnSearchAllScript";
            this.btnSearchAllScript.Size = new System.Drawing.Size(157, 23);
            this.btnSearchAllScript.TabIndex = 2;
            this.btnSearchAllScript.Text = "检索所有的脚本";
            this.btnSearchAllScript.UseVisualStyleBackColor = true;
            this.btnSearchAllScript.Click += new System.EventHandler(this.btnSearchAllScript_Click);
            // 
            // btnUpdateScriptVersion
            // 
            this.btnUpdateScriptVersion.Enabled = false;
            this.btnUpdateScriptVersion.Location = new System.Drawing.Point(92, 88);
            this.btnUpdateScriptVersion.Name = "btnUpdateScriptVersion";
            this.btnUpdateScriptVersion.Size = new System.Drawing.Size(157, 23);
            this.btnUpdateScriptVersion.TabIndex = 1;
            this.btnUpdateScriptVersion.Text = "更新引用文件的脚本版本";
            this.btnUpdateScriptVersion.UseVisualStyleBackColor = true;
            this.btnUpdateScriptVersion.Click += new System.EventHandler(this.btnUpdateScriptVersion_Click);
            // 
            // btnSearchChangeScript
            // 
            this.btnSearchChangeScript.Location = new System.Drawing.Point(92, 54);
            this.btnSearchChangeScript.Name = "btnSearchChangeScript";
            this.btnSearchChangeScript.Size = new System.Drawing.Size(157, 23);
            this.btnSearchChangeScript.TabIndex = 0;
            this.btnSearchChangeScript.Text = "检索变更的脚本";
            this.btnSearchChangeScript.UseVisualStyleBackColor = true;
            this.btnSearchChangeScript.Click += new System.EventHandler(this.btnSearchChangeScript_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbxScriptOutput);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbxReferenceFilesOutput);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Size = new System.Drawing.Size(777, 297);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 6;
            // 
            // lbxScriptOutput
            // 
            this.lbxScriptOutput.ContextMenuStrip = this.ctmRigthControl;
            this.lbxScriptOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxScriptOutput.FormattingEnabled = true;
            this.lbxScriptOutput.ItemHeight = 12;
            this.lbxScriptOutput.Location = new System.Drawing.Point(29, 0);
            this.lbxScriptOutput.Name = "lbxScriptOutput";
            this.lbxScriptOutput.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxScriptOutput.Size = new System.Drawing.Size(230, 297);
            this.lbxScriptOutput.TabIndex = 2;
            // 
            // ctmRigthControl
            // 
            this.ctmRigthControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem});
            this.ctmRigthControl.Name = "ctmRigthControl";
            this.ctmRigthControl.Size = new System.Drawing.Size(101, 26);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "脚本";
            // 
            // lbxReferenceFilesOutput
            // 
            this.lbxReferenceFilesOutput.ContextMenuStrip = this.ctmRigthControl;
            this.lbxReferenceFilesOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxReferenceFilesOutput.FormattingEnabled = true;
            this.lbxReferenceFilesOutput.ItemHeight = 12;
            this.lbxReferenceFilesOutput.Location = new System.Drawing.Point(77, 0);
            this.lbxReferenceFilesOutput.Name = "lbxReferenceFilesOutput";
            this.lbxReferenceFilesOutput.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxReferenceFilesOutput.Size = new System.Drawing.Size(437, 297);
            this.lbxReferenceFilesOutput.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "引用脚本文件";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Location = new System.Drawing.Point(17, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(783, 317);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "检索输出";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSercheSpecifiedScript);
            this.groupBox4.Controls.Add(this.btnUpdateSpecifiedScriptVersion);
            this.groupBox4.Controls.Add(this.btnSerchSpecifiedChangeReference);
            this.groupBox4.Controls.Add(this.tbxReferenceFilesPath);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbxScriptPath);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(12, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(761, 102);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "局部操作";
            // 
            // btnSercheSpecifiedScript
            // 
            this.btnSercheSpecifiedScript.Location = new System.Drawing.Point(348, 29);
            this.btnSercheSpecifiedScript.Name = "btnSercheSpecifiedScript";
            this.btnSercheSpecifiedScript.Size = new System.Drawing.Size(157, 23);
            this.btnSercheSpecifiedScript.TabIndex = 9;
            this.btnSercheSpecifiedScript.Text = "检索所有的脚本";
            this.btnSercheSpecifiedScript.UseVisualStyleBackColor = true;
            this.btnSercheSpecifiedScript.Click += new System.EventHandler(this.btnSercheSpecifiedScript_Click);
            // 
            // btnUpdateSpecifiedScriptVersion
            // 
            this.btnUpdateSpecifiedScriptVersion.Enabled = false;
            this.btnUpdateSpecifiedScriptVersion.Location = new System.Drawing.Point(540, 65);
            this.btnUpdateSpecifiedScriptVersion.Name = "btnUpdateSpecifiedScriptVersion";
            this.btnUpdateSpecifiedScriptVersion.Size = new System.Drawing.Size(157, 23);
            this.btnUpdateSpecifiedScriptVersion.TabIndex = 8;
            this.btnUpdateSpecifiedScriptVersion.Text = "更新引用文件的脚本版本";
            this.btnUpdateSpecifiedScriptVersion.UseVisualStyleBackColor = true;
            this.btnUpdateSpecifiedScriptVersion.Click += new System.EventHandler(this.btnUpdateSpecifiedScriptVersion_Click);
            // 
            // btnSerchSpecifiedChangeReference
            // 
            this.btnSerchSpecifiedChangeReference.Location = new System.Drawing.Point(348, 65);
            this.btnSerchSpecifiedChangeReference.Name = "btnSerchSpecifiedChangeReference";
            this.btnSerchSpecifiedChangeReference.Size = new System.Drawing.Size(157, 23);
            this.btnSerchSpecifiedChangeReference.TabIndex = 7;
            this.btnSerchSpecifiedChangeReference.Text = "检索变更的脚本";
            this.btnSerchSpecifiedChangeReference.UseVisualStyleBackColor = true;
            this.btnSerchSpecifiedChangeReference.Click += new System.EventHandler(this.btnSerchSpecifiedChangeReference_Click);
            // 
            // tbxReferenceFilesPath
            // 
            this.tbxReferenceFilesPath.Location = new System.Drawing.Point(97, 67);
            this.tbxReferenceFilesPath.Name = "tbxReferenceFilesPath";
            this.tbxReferenceFilesPath.Size = new System.Drawing.Size(213, 21);
            this.tbxReferenceFilesPath.TabIndex = 6;
            this.tbxReferenceFilesPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxReferenceFilesPath_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "引用文件路径";
            // 
            // tbxScriptPath
            // 
            this.tbxScriptPath.Location = new System.Drawing.Point(97, 26);
            this.tbxScriptPath.Name = "tbxScriptPath";
            this.tbxScriptPath.Size = new System.Drawing.Size(213, 21);
            this.tbxScriptPath.TabIndex = 4;
            this.tbxScriptPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxScriptPath_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "脚本路径";
            // 
            // UpdateScriptCache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 593);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateScriptCache";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新脚本缓存";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctmRigthControl.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxProjectPath;
        private System.Windows.Forms.TextBox tbxScriptSuffix;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdateScriptVersion;
        private System.Windows.Forms.Button btnSearchChangeScript;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxReferenceSuffix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSearchAllScript;
        private System.Windows.Forms.ListBox lbxScriptOutput;
        private System.Windows.Forms.ListBox lbxReferenceFilesOutput;
        private System.Windows.Forms.ContextMenuStrip ctmRigthControl;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSercheSpecifiedScript;
        private System.Windows.Forms.Button btnUpdateSpecifiedScriptVersion;
        private System.Windows.Forms.Button btnSerchSpecifiedChangeReference;
        private System.Windows.Forms.TextBox tbxReferenceFilesPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxScriptPath;
        private System.Windows.Forms.Label label6;
    }
}

