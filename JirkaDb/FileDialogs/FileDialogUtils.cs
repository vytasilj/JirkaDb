using JetBrains.Annotations;
using Microsoft.Win32;

namespace JirkaDb.FileDialogs
{
    public static class FileDialogUtils
    {
        [CanBeNull]
        public static string GetFile(this FileDialog dialog)
        {
            if (dialog.ShowDialog() == true)
                return dialog.FileName;

            return null;
        }
    }
}