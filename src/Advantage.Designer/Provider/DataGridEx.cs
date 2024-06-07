using System.Windows.Forms;

namespace Advantage.Data.Provider
{
    internal class DataGridEx : DataGrid
    {
        public int InsideWidth
        {
            get => !VertScrollBar.Visible ? Width : Width - VertScrollBar.Width;
        }
    }
}