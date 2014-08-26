namespace CSharp数据库代码生成工具
{
    partial class Tables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tables));
            this.listViewTables = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listViewColumns = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewTemplate = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTemplate = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richResult = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labSelectTableName = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewTables
            // 
            this.listViewTables.GridLines = true;
            this.listViewTables.Location = new System.Drawing.Point(6, 6);
            this.listViewTables.Name = "listViewTables";
            this.listViewTables.Size = new System.Drawing.Size(433, 504);
            this.listViewTables.TabIndex = 0;
            this.listViewTables.UseCompatibleStateImageBehavior = false;
            this.listViewTables.View = System.Windows.Forms.View.Details;
            this.listViewTables.SelectedIndexChanged += new System.EventHandler(this.listViewTables_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 542);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewColumns);
            this.tabPage1.Controls.Add(this.listViewTables);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(919, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "选择表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listViewColumns
            // 
            this.listViewColumns.FullRowSelect = true;
            this.listViewColumns.GridLines = true;
            this.listViewColumns.Location = new System.Drawing.Point(480, 6);
            this.listViewColumns.Name = "listViewColumns";
            this.listViewColumns.Size = new System.Drawing.Size(433, 504);
            this.listViewColumns.TabIndex = 0;
            this.listViewColumns.UseCompatibleStateImageBehavior = false;
            this.listViewColumns.View = System.Windows.Forms.View.Details;
            this.listViewColumns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewColumns_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labSelectTableName);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.listViewTemplate);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.richTemplate);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.richResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(919, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "自定义";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewTemplate
            // 
            this.listViewTemplate.GridLines = true;
            this.listViewTemplate.Location = new System.Drawing.Point(6, 370);
            this.listViewTemplate.Name = "listViewTemplate";
            this.listViewTemplate.Size = new System.Drawing.Size(182, 140);
            this.listViewTemplate.TabIndex = 5;
            this.listViewTemplate.UseCompatibleStateImageBehavior = false;
            this.listViewTemplate.View = System.Windows.Forms.View.Details;
            this.listViewTemplate.SelectedIndexChanged += new System.EventHandler(this.listViewTemplate_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(838, 341);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "字段说明";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(757, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "字段名";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTemplate
            // 
            this.richTemplate.Location = new System.Drawing.Point(194, 370);
            this.richTemplate.Name = "richTemplate";
            this.richTemplate.Size = new System.Drawing.Size(719, 140);
            this.richTemplate.TabIndex = 2;
            this.richTemplate.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(906, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richResult
            // 
            this.richResult.Location = new System.Drawing.Point(6, 6);
            this.richResult.Name = "richResult";
            this.richResult.Size = new System.Drawing.Size(907, 301);
            this.richResult.TabIndex = 0;
            this.richResult.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "当前选择表：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 6;
            // 
            // labSelectTableName
            // 
            this.labSelectTableName.AutoSize = true;
            this.labSelectTableName.Location = new System.Drawing.Point(78, 346);
            this.labSelectTableName.Name = "labSelectTableName";
            this.labSelectTableName.Size = new System.Drawing.Size(0, 12);
            this.labSelectTableName.TabIndex = 6;
            // 
            // Tables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 566);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(967, 605);
            this.MinimumSize = new System.Drawing.Size(967, 605);
            this.Name = "Tables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码生成器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tables_FormClosing);
            this.Load += new System.EventHandler(this.Tables_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewTables;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewColumns;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richResult;
        private System.Windows.Forms.RichTextBox richTemplate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listViewTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labSelectTableName;
        private System.Windows.Forms.Label label2;
    }
}