using System;
using System.Data;
using System.IO;
using System.Text;
using Caliburn.Micro;
using JetBrains.Annotations;
using JirkaDb.Views;
using Syncfusion.Windows.Controls.Grid;
using Syncfusion.Windows.Controls.Grid.Converter;
using Syncfusion.XlsIO;

namespace JirkaDb.ViewModels
{
    public class GridViewModel : Screen
    {
        private GridControl m_grid;
        private readonly DataTable m_table;


        public GridViewModel([NotNull] DataTable table)
        {
            m_table = table ?? throw new ArgumentNullException(nameof(table));
        }


        public DataTable Table => m_table;


        public void ExportToXls(string folder)
        {
            // TODO: Export více listů
            GetGrid().Model.ExportToExcel($"{folder}\\{m_table.TableName}.xlsx", ExcelVersion.Excel2016);
        }

        public void ExportToCsv(string folder)
        {
            CopyTextToBuffer(GetGrid().Model, out string buffer);
            File.WriteAllText($"{folder}\\{m_table.TableName}.csv", buffer);
        }


        protected override void OnViewLoaded(object view)
        {
            m_grid = ((GridView) view).GridControl;
            SetupGrid(m_grid);
        }


        private GridControl GetGrid()
        {
            if (m_grid != null)
                return m_grid;

            var result = new GridControl();
            SetupGrid(result);
            return result;
        }

        private void SetupGrid(GridControl gridControl)
        {
            gridControl.QueryCellInfo += GridOnQueryCellInfo;

            gridControl.Model.RowCount = m_table.Rows.Count;
            gridControl.Model.ColumnCount = m_table.Columns.Count;

            gridControl.Model.InvalidateVisual();
        }

        private void GridOnQueryCellInfo(object sender, GridQueryCellInfoEventArgs e)
        {
            e.Style.IsEditable = false;
            
            if (e.Cell.RowIndex == 0)
            {
                if (e.Cell.ColumnIndex == 0)
                {
                    //e.Style.CellValue = m_table.TableName;
                    return;
                }

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

        /// <summary>
        /// Převzato od Syncfusionů, abych mohl změnit str1 a přidat názvy sloupců.
        /// </summary>
        /// <param name="gridModel"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private  bool CopyTextToBuffer(GridModel gridModel, out string buffer)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str1 = ";";
            for (int top = 0; top <= gridModel.RowCount; ++top)
            {
                bool flag = true;
                for (int left = 0; left <= gridModel.ColumnCount; ++left)
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