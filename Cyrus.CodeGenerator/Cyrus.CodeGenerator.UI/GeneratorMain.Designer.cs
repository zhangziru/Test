namespace Cyrus.CodeGenerator.UI
{
    partial class GeneratorMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tbxContent = new System.Windows.Forms.TextBox();
            this.btnSaveLocal = new System.Windows.Forms.Button();
            this.btnBatchSaveLocal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxNameSpace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbxTable = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxTableName
            // 
            this.tbxTableName.Location = new System.Drawing.Point(93, 85);
            this.tbxTableName.Name = "tbxTableName";
            this.tbxTableName.Size = new System.Drawing.Size(144, 21);
            this.tbxTableName.TabIndex = 0;
            this.tbxTableName.Text = "TblPerson";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "表名";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(676, 77);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(104, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "单表生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tbxContent
            // 
            this.tbxContent.Location = new System.Drawing.Point(31, 138);
            this.tbxContent.Multiline = true;
            this.tbxContent.Name = "tbxContent";
            this.tbxContent.Size = new System.Drawing.Size(642, 377);
            this.tbxContent.TabIndex = 3;
            // 
            // btnSaveLocal
            // 
            this.btnSaveLocal.Location = new System.Drawing.Point(676, 138);
            this.btnSaveLocal.Name = "btnSaveLocal";
            this.btnSaveLocal.Size = new System.Drawing.Size(104, 23);
            this.btnSaveLocal.TabIndex = 4;
            this.btnSaveLocal.Text = "单表保存到本地";
            this.btnSaveLocal.UseVisualStyleBackColor = true;
            this.btnSaveLocal.Click += new System.EventHandler(this.btnSaveLocal_Click);
            // 
            // btnBatchSaveLocal
            // 
            this.btnBatchSaveLocal.Location = new System.Drawing.Point(676, 211);
            this.btnBatchSaveLocal.Name = "btnBatchSaveLocal";
            this.btnBatchSaveLocal.Size = new System.Drawing.Size(110, 23);
            this.btnBatchSaveLocal.TabIndex = 5;
            this.btnBatchSaveLocal.Text = "批量生成到本地";
            this.btnBatchSaveLocal.UseVisualStyleBackColor = true;
            this.btnBatchSaveLocal.Click += new System.EventHandler(this.btnBatchSaveLocal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "命名空间";
            // 
            // tbxNameSpace
            // 
            this.tbxNameSpace.Location = new System.Drawing.Point(325, 85);
            this.tbxNameSpace.Name = "tbxNameSpace";
            this.tbxNameSpace.Size = new System.Drawing.Size(144, 21);
            this.tbxNameSpace.TabIndex = 6;
            this.tbxNameSpace.Text = "Cyrus.CodeGenerator.Model";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "数据库服务器";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 21);
            this.textBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "数据库";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 50);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(144, 21);
            this.textBox2.TabIndex = 10;
            // 
            // cbxTable
            // 
            this.cbxTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTable.FormattingEnabled = true;
            this.cbxTable.Items.AddRange(new object[] {
            "全部",
            "基础表",
            "视图"});
            this.cbxTable.Location = new System.Drawing.Point(93, 112);
            this.cbxTable.Name = "cbxTable";
            this.cbxTable.Size = new System.Drawing.Size(121, 20);
            this.cbxTable.TabIndex = 12;
            this.cbxTable.SelectedIndexChanged += new System.EventHandler(this.cbxTable_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "表类型";
            // 
            // GeneratorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 517);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxTable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxNameSpace);
            this.Controls.Add(this.btnBatchSaveLocal);
            this.Controls.Add(this.btnSaveLocal);
            this.Controls.Add(this.tbxContent);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxTableName);
            this.Name = "GeneratorMain";
            this.Text = "GeneratorMain";
            this.Load += new System.EventHandler(this.GeneratorMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox tbxContent;
        private System.Windows.Forms.Button btnSaveLocal;
        private System.Windows.Forms.Button btnBatchSaveLocal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxNameSpace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cbxTable;
        private System.Windows.Forms.Label label5;
    }
}