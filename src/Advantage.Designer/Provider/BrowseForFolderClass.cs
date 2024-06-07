using System.Windows.Forms.Design;

namespace Advantage.Data.Provider
{
    public class BrowseForFolderClass : FolderNameEditor
    {
        private FolderBrowser myFolderBrowser;

        public string BrowseForFolder(string title)
        {
            myFolderBrowser = new FolderBrowser();
            myFolderBrowser.Description = title;
            var num = (int)myFolderBrowser.ShowDialog();
            var directoryPath = myFolderBrowser.DirectoryPath;
            if (directoryPath.Length > 0 && directoryPath.Substring(directoryPath.Length - 1, 1) != "\\")
                directoryPath += "\\";
            return directoryPath;
        }

        ~BrowseForFolderClass() => myFolderBrowser.Dispose();
    }
}