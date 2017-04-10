using Caliburn.Micro;
using JirkaDb.Attributes;
using JirkaDb.FileDialogs;

namespace JirkaDb.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private readonly GridViewModel m_gridModel;


        public MainWindowViewModel()
        {
            m_gridModel = new GridViewModel();
        }


        public GridViewModel GridModel => m_gridModel;


        [ForUi]
        public void ExportToCsv()
        {
            SaveDialog.GetCsvFile();
        }

        [ForUi]
        public void ExportToExcel()
        {
            SaveDialog.GetXlsFile();
        }

        [ForUi]
        public void ImportSql()
        {
            OpenDialog.GetSqlFile();
        }
    }
}