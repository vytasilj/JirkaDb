using System;
using System.Data;
using System.IO;
using System.Text;
using Caliburn.Micro;
using JirkaDb.Views;
using Syncfusion.Windows.Controls.Grid;
using Syncfusion.Windows.Controls.Grid.Converter;
using Syncfusion.XlsIO;

namespace JirkaDb.ViewModels
{
    public class GridViewModel : Screen
    {
        private GridControl m_grid;
        private DataTable m_table;


        public void ExportToXls(string file)
        {
            // TODO: Export více listů
            m_grid.Model.ExportToExcel(file, ExcelVersion.Excel2016);
        }

        public void ExportToCsv(string file)
        {
            CopyTextToBuffer(m_grid.Model, out string buffer);
            File.WriteAllText(file, buffer);
        }

        public void ShowTable(DataTable table)
        {
            m_table = table;

            m_grid.Model.RowCount = table.Rows.Count;
            m_grid.Model.ColumnCount = table.Columns.Count;

            m_grid.Model.InvalidateVisual();
        }


        protected override void OnViewLoaded(object view)
        {
            m_grid = ((GridView) view).GridControl;
            m_grid.QueryCellInfo += GridOnQueryCellInfo;
        }

        private void GridOnQueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            e.Style.IsEditable = false;

            if (e.Cell.RowIndex == 0)
            {
                e.Style.CellValue = m_table.Columns[e.Cell.ColumnIndex - 1].ColumnName;
                return;
            }
            if (e.Cell.ColumnIndex == 0)
            {
                e.Style.CellValue = e.Cell.RowIndex;
                return;
            }

            e.Style.CellValue = m_table.Rows[e.Cell.RowIndex - 1][e.Cell.ColumnIndex - 1];
        }

        private  bool CopyTextToBuffer(GridModel gridModel, out string buffer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str1 = ";";
            for (int top = 0; top <= m_table.Rows.Count; ++top)
            {
                bool flag = true;
                for (int left = 0; left <= m_table.Columns.Count; ++left)
                {
                    if (!flag)
                        stringBuilder.Append(str1);
                    GridStyleInfo gridStyleInfo = gridModel[top, left];
                    string str2 = new StringBuilder(gridStyleInfo.GetFormattedText(gridStyleInfo.CellValue, 1))
                        .Replace(Environment.NewLine, " ").Replace("\r", " ").Replace("\n", " ").ToString().Trim();
                    stringBuilder.Append("\"" + str2 + "\"");
                    flag = false;
                }
                stringBuilder.Append(Environment.NewLine);
            }
            buffer = stringBuilder.ToString();
            return true;
        }
    }
}