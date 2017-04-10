using JetBrains.Annotations;
using Microsoft.Win32;

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
        private static string GetFile(string filter)
        {
            return new SaveFileDialog {Filter = filter}.GetFile();
        }
    }
}