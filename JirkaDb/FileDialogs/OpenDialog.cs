using JetBrains.Annotations;
using Microsoft.Win32;

namespace JirkaDb.FileDialogs
{
    public static class OpenDialog
    {
        [CanBeNull]
        public static string GetSqlFile()
        {
            return GetFile("SQL|*.sql");
        }


        [CanBeNull]
        private static string GetFile(string filter)
        {
            return new OpenFileDialog { Filter = filter }.GetFile();
        }
    }
}