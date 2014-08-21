using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace Common
{
    public abstract class ListViewHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ListViewHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 显示数据集
        /// </summary>
        /// <param name="listView">要显示的ListView</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="includeNo">是否包含序号</param>
        public static void DisplayDataSet(ListView listView, DataSet dataSet, bool includeNo)
        {
            if (dataSet == null)
            {
                return;
            }
            listView.Items.Clear();
            DataTable dataTable = dataSet.Tables[0];
            int rowno = 0;
            ListViewItem item;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                item = new ListViewItem();
                rowno++;
                if (includeNo)
                {
                    item.Text = rowno.ToString();
                }
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string str = dataRow[i].ToString();
                    item.SubItems.Add(dataRow[i].ToString());
                }
                listView.Items.Add(item);
            }
        }
        /// <summary>
        /// 显示数据集
        /// </summary>
        /// <param name="listView">要显示的ListView</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="includeNo">是否包含序号</param>
        public static void DisplayDataSetWithTag(ListView listView, DataSet dataSet, bool includeNo)
        {
            if (dataSet == null)
            {
                return;
            }
            listView.Items.Clear();
            DataTable dataTable = dataSet.Tables[0];
            int rowno = 0;
            ListViewItem item;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                item = new ListViewItem();
                rowno++;
                if (includeNo)
                {
                    item.Text = rowno.ToString();
                }
                item.Tag = dataRow[0].ToString();
                for (int i = 1; i < dataTable.Columns.Count; i++)
                {
                    string str = dataRow[i].ToString();
                    item.SubItems.Add(dataRow[i].ToString());
                }
                listView.Items.Add(item);
            }
        }
        /// <summary>
        /// 显示数据集
        /// </summary>
        /// <param name="listView">要显示的ListView</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="columnTitles">列标题</param>
        /// <param name="columnFields">列显示字段</param>
        /// <param name="columnSizes">列宽</param>
        /// <param name="valueField">添加到Tag属性的字段名</param>
        public static void DisplayDataSetWithTag(ListView listView, DataSet dataSet, string[] columnTitles, string[] columnFields, int[] columnSizes, string valueField)
        {
            listView.Items.Clear();
            listView.Columns.Clear();
            listView.Columns.Add("SEQ", 40, HorizontalAlignment.Left);
            for (int i = 0; i < columnTitles.Length; i++)
                listView.Columns.Add(columnTitles[i], columnSizes[i], HorizontalAlignment.Left);
            if (dataSet == null)
                return;
            DataView dv = dataSet.Tables[0].DefaultView;
            int rowNo = 0;
            for (int i = 0; i < dv.Count; i++)
            {
                rowNo = rowNo + 1;
                listView.Items.Add(rowNo.ToString());
                for (int k = 0; k < columnTitles.Length; k++)
                    listView.Items[i].SubItems.Add(dv[i].Row[columnFields[k]].ToString());
                listView.Items[i].Tag = dv[i].Row[valueField].ToString();
            }
        }
        /// <summary>
        /// 重新设置序号
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="startIndex"></param>
        public static void ResetSEQ(ListView listView, int startIndex)
        {
            for (int i = startIndex; i < listView.Items.Count; i++)
            {
                listView.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
        }
    }
}