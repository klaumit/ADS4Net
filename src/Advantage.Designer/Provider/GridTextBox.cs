using System.Windows.Forms;

namespace Advantage.Data.Provider
{
    internal class GridTextBox : TextBox
    {
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                case Keys.Return:
                case Keys.Tab | Keys.Shift:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }
    }
}