using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace JirkaDb.Import
{
    public class Importer
    {
        private ImportTable m_importTable;
        private ImportValues m_importValues;


        public Importer()
        {
            m_importTable = new ImportTable();
            m_importValues = new ImportValues();
        }


        public IEnumerable<DataTable> Import(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            var tables = m_importTable.Import(m_importTable.GetValidRows(lines)).ToArray();
            m_importValues.Import(m_importValues.GetValidRows(lines), tables);

            return tables;
        }
    }
}