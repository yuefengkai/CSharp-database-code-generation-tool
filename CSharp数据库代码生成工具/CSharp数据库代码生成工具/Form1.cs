using Maunite.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp数据库代码生成工具
{
    public partial class form11 : Form
    {
        public form11()
        {
            InitializeComponent();
        }

        private string _strConn = "";
        public string comDataBaseText
        {
            get
            {
                return comDataBase.Text;
            }
        }
        private void btnConnection_Click(object sender, EventArgs e)
        {

            if (txtUser.Text.Trim() != "" && txtPwd.Text.Trim() != "" && txtServerUrl.Text.Trim() != "")
            {
                try
                {
                    _strConn = "server=" + txtServerUrl.Text.Trim() + ";uid=" + txtUser.Text.Trim() + ";pwd=" + txtPwd.Text.Trim() + ";database=master";
                    
                    DbHelperSQL.connectionString = _strConn;
                    BindDatabase();
                    btnConnection.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnConnection.Enabled = true;
                }
                
            }
        }
        private void form11_Load(object sender, EventArgs e)
        {
            labDataBase.Visible = false;
            comDataBase.Visible = false;
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns></returns>
        public  void BindDatabase()
        {

            var ds = DbHelperSQL.Query("select * from [sysdatabases] order by [name]");
            if (ds!=null&&ds.Tables[0].Rows.Count>0)
            {

                comDataBase.DataSource = ds.Tables[0];    //将表绑定到控件
                comDataBase.DisplayMember = "name";     //定义要显示的内容为列名为x的内容
                comDataBase.ValueMember = "dbid";       //定义要映射的值为y的值

                labDataBase.Visible = true;
                comDataBase.Visible = true;
                   
            }
            

            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Tables table = new Tables();
            table.Owner = this;
            table.Show();
            Hide();
        }
    }
}
