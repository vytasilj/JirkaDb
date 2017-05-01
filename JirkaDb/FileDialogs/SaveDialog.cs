using JetBrains.Annotations;
using Microsoft.Win32;
using Syncfusion.Windows.Tools.Controls;

namespace JirkaDb.FileDialogs
{
    public static class SaveDialog
    {
        [CanBeNull]
        public static string GetXlsFile()
        {
            return GetFile("Excel files|*.xlsx, *.xls");
        }

        [CanBeNull]
        public static string GetCsvFile()
        {
            return GetFile("Csv|*.csv");
        }

        [CanBeNull]
        public static string GetDirectoryForSave()
        {
            return GetFolder();
        }


        [CanBeNull]
        private static string GetFile(string filter)
        {
            return new SaveFileDialog {Filter = filter}.GetFile();
        }

        [CanBeNull]
        private static string GetFolder()
        {
            var browser = new FolderBrowser();
            if (browser.ShowDialog() == true)
                return browser.SelectedDirectory;

            return null;
        }
    }
}