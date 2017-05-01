using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JirkaDb.Import
{
    public class ImportValues
    {
        public void Import(IEnumerable<string> lines, DataTable[] tables)
        {
            var dataTables = tables.ToDictionary(table => table.TableName);

            foreach (string line in lines)
            {
                ImportLine(line, dataTables[GetTableName(line)]);
            }
        }

        public IEnumerable<string> GetValidRows(IEnumerable<string> rows)
        {
            return rows.Where(row => row.StartsWith("insert into", StringComparison.InvariantCultureIgnoreCase));
        }


        private bool ImportLine(string line, DataTable table)
        {
            string joinValue = line.Split(new[] {"VALUES"}, StringSplitOptions.RemoveEmptyEntries)[1];
            joinValue = joinValue.Split('(', ')')[1];
            DataRow row = table.NewRow();
            string[] values = joinValue.Split(',');
            for (var index = 0; index < values.Length; index++)
            {
                row[index] = values[index].Trim('`', '\'');
            }
            table.Rows.Add(row);
            return true;
        }

        private string GetTableName(string line)
        {
            return line.Split('(')[0].Split(' ').Last();
        }
    }
}