﻿namespace CSharp数据库代码生成工具
{
    partial class form11
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form11));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.comDataBase = new System.Windows.Forms.ComboBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.labDataBase = new System.Windows.Forms.Label();
            this.labPwd = new System.Windows.Forms.Label();
            this.txtServerUrl = new System.Windows.Forms.TextBox();
            this.labUser = new System.Windows.Forms.Label();
            this.labServerUrl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.comDataBase);
            this.groupBox1.Controls.Add(this.btnConnection);
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.labDataBase);
            this.groupBox1.Controls.Add(this.labPwd);
            this.groupBox1.Controls.Add(this.txtServerUrl);
            this.groupBox1.Controls.Add(this.labUser);
            this.groupBox1.Controls.Add(this.labServerUrl);
            this.groupBox1.Location = new System.Drawing.Point(24, 24);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(998, 552);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Location = new System.Drawing.Point(584, 394);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(150, 46);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // comDataBase
            // 
            this.comDataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDataBase.FormattingEnabled = true;
            this.comDataBase.Location = new System.Drawing.Point(260, 314);
            this.comDataBase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comDataBase.Name = "comDataBase";
            this.comDataBase.Size = new System.Drawing.Size(600, 32);
            this.comDataBase.TabIndex = 3;
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(260, 394);
            this.btnConnection.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(150, 46);
            this.btnConnection.TabIndex = 2;
            this.btnConnection.Text = "连接";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(260, 236);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(600, 35);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.Text = "sa";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(260, 156);
            this.txtUser.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(600, 35);
            this.txtUser.TabIndex = 1;
            this.txtUser.Text = "sa";
            // 
            // labDataBase
            // 
            this.labDataBase.AutoSize = true;
            this.labDataBase.Location = new System.Drawing.Point(142, 320);
            this.labDataBase.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labDataBase.Name = "labDataBase";
            this.labDataBase.Size = new System.Drawing.Size(106, 24);
            this.labDataBase.TabIndex = 0;
            this.labDataBase.Text = "数据库：";
            // 
            // labPwd
            // 
            this.labPwd.AutoSize = true;
            this.labPwd.Location = new System.Drawing.Point(164, 242);
            this.labPwd.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labPwd.Name = "labPwd";
            this.labPwd.Size = new System.Drawing.Size(82, 24);
            this.labPwd.TabIndex = 0;
            this.labPwd.Text = "密码：";
            // 
            // txtServerUrl
            // 
            this.txtServerUrl.Location = new System.Drawing.Point(262, 70);
            this.txtServerUrl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtServerUrl.Name = "txtServerUrl";
            this.txtServerUrl.Size = new System.Drawing.Size(600, 35);
            this.txtServerUrl.TabIndex = 1;
            this.txtServerUrl.Text = "192.168.10.162\\MSSQL_2005";
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(142, 162);
            this.labUser.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(106, 24);
            this.labUser.TabIndex = 0;
            this.labUser.Text = "登录名：";
            // 
            // labServerUrl
            // 
            this.labServerUrl.AutoSize = true;
            this.labServerUrl.Location = new System.Drawing.Point(96, 76);
            this.labServerUrl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labServerUrl.Name = "labServerUrl";
            this.labServerUrl.Size = new System.Drawing.Size(154, 24);
            this.labServerUrl.TabIndex = 0;
            this.labServerUrl.Text = "服务器地址：";
            // 
            // form11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 536);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1052, 607);
            this.Name = "form11";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharp数据库代码生成工具";
            this.Load += new System.EventHandler(this.form11_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labServerUrl;
        private System.Windows.Forms.TextBox txtServerUrl;
        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label labPwd;
        private System.Windows.Forms.Label labDataBase;
        public System.Windows.Forms.ComboBox comDataBase;
        private System.Windows.Forms.Button btnConfirm;
    }
}

