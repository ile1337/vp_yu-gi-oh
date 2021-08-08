using System;
using System.Windows.Forms;

namespace yu_gi_oh.Components
{
    class FileUtilities
    {
        public static void CallFileExplorer(FileDialog dialog, Action<FileDialog> action)
        {
            dialog.AddExtension = true;
            dialog.DefaultExt = Configuration.YGO_DEFAULT_EXTENSION;
            dialog.Filter = Configuration.YGO_FILTER_EXTENSION;

            if (dialog.ShowDialog() == DialogResult.OK) action.Invoke(dialog);
        }
    }
}
