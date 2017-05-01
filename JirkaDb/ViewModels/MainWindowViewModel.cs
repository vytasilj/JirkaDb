using System.Diagnostics;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using JirkaDb.Attributes;
using JirkaDb.FileDialogs;
using JirkaDb.Import;

namespace JirkaDb.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private readonly GridViewModel m_gridModel;
        private readonly Importer m_importer;


        public MainWindowViewModel()
        {
            m_gridModel = new GridViewModel();
            m_importer = new Importer();
        }


        public GridViewModel GridModel => m_gridModel;


        [ForUi]
        public void ExportToCsv()
        {
            string file = SaveDialog.GetCsvFile();
            if (string.IsNullOrEmpty(file))
                return;
            m_gridModel.ExportToCsv(file);
            OpenAfterExport(file);
        }

        [ForUi]
        public void ExportToExcel()
        {
            string file = SaveDialog.GetXlsFile();
            if (string.IsNullOrEmpty(file))
                return;
            m_gridModel.ExportToXls(file);
            OpenAfterExport(file);
        }

        [ForUi]
        public void ImportSql()
        {
            string file = OpenDialog.GetSqlFile();
            if (string.IsNullOrEmpty(file))
                return;
            var tables = m_importer.Import(file);
            m_gridModel.ShowTable(tables.First());
        }


        private void OpenAfterExport(string file)
        {
            if (
                MessageBox.Show("Otevřít vytvořený soubor", "soubor", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) ==
                MessageBoxResult.Yes)
                Process.Start(file);
        }
    }
}