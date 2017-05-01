using System.Diagnostics;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using JirkaDb.Attributes;
using JirkaDb.Extensions;
using JirkaDb.FileDialogs;
using JirkaDb.Import;

namespace JirkaDb.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private readonly BindableCollection<GridViewModel> m_models;
        private readonly Importer m_importer;


        public MainWindowViewModel()
        {
            m_importer = new Importer();
            m_models = new BindableCollection<GridViewModel>();
        }

        
        public BindableCollection<GridViewModel> Models => m_models;


        [ForUi]
        public void ExportToCsv()
        {
            //string file = SaveDialog.GetCsvFile();
            string folder = SaveDialog.GetDirectoryForSave();
            if (string.IsNullOrEmpty(folder))
                return;
            m_models.ForEach(model => model.ExportToCsv(folder));
            OpenAfterExport(folder);
        }

        [ForUi]
        public void ExportToExcel()
        {
            // TODO: Nefunguje, pokusit se exportovat jinak.
            string file = SaveDialog.GetXlsFile();
            if (string.IsNullOrEmpty(file))
                return;
            m_models.ForEach(model => model.ExportToXls(file));
            OpenAfterExport(file);
        }

        [ForUi]
        public void ImportSql()
        {
            string file = OpenDialog.GetSqlFile();
            if (string.IsNullOrEmpty(file))
                return;
            m_models.Clear();
            var tables = m_importer.Import(file);
            m_models.AddRange(tables.Where(table => table.Rows.Count > 0).Select(table => new GridViewModel(table)));
            NotifyOfPropertyChange(nameof(Models));
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