using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JirkaDb.Import
{
    public class ImportTable
    {
        private const char KeySymbol = '`';


        public IEnumerable<DataTable> Import(IEnumerable<string> lines)
        {
            //string all = string.Join(" ", lines);
            //foreach (string createTable in all.Split(';'))
            //{
            //    new DataTable()
            //}
            DataTable actual = null;
            foreach (string line in lines)
            {
                if (IsCreateTable(line))
                {
                    if (actual != null)
                        throw new ArgumentException();
                    actual = new DataTable(GetTableName(line));
                    continue;
                }
                if (actual == null)
                    continue;

                if (TryAddColumn(actual, line))
                    continue;

                if (line.Contains(";"))
                {
                    yield return actual;
                    actual = null;
                }
            }
        }

        public IEnumerable<string> GetValidRows(IEnumerable<string> rows)
        {
            bool create = false;
            foreach (string row in rows)
            {
                if (create || IsCreateTable(row))
                {
                    create = true;
                    yield return row;
                }
                if (row.Contains(";"))
                    create = false;
            }
        }


        private bool IsCreateTable(string row)
        {
            return row.StartsWith("create table", StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetTableName(string row)
        {
            return Trim(GetRowItems(row)[2]);
        }

        private bool TryAddColumn(DataTable table, string row)
        {
            string[] rowItems = GetRowItems(row);
            string columnName = rowItems[0];
            if (!IsColumnName(columnName) || !CanImport(columnName))
                return false;

            table.Columns.Add(Trim(columnName));//, ReadType(rowItems[1]));
            return true;
        }

        private bool CanImport(string columnName)
        {
            return Trim(columnName) != "id";
        }

        private string[] GetRowItems(string row)
        {
            return row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string Trim(string name)
        {
            return name.Trim(KeySymbol);
        }

        private bool IsColumnName(string name)
        {
            return name.StartsWith(KeySymbol.ToString()) && name.EndsWith(KeySymbol.ToString());
        }
    }
}