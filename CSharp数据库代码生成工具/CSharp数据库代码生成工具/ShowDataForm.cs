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
    public partial class ShowDataForm : Form
    {
        public ShowDataForm()
        {
            InitializeComponent();
        }

        private void ShowDataForm_Load(object sender, EventArgs e)
        {
            var strTableName = ((Tables)this.Owner).StrTableName;
            this.Text = strTableName + "-" + "表数据";

            var ds = ((Tables)this.Owner).GetDataSet("select * from " + strTableName);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = false;
            //MessageBox.Show(((Tables)this.Owner).StrTableName);
        }
    }
}
