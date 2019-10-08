using System.Windows.Forms;

namespace CopyRightDetector.Controls
{
    public class NumberTextBox : TextBox
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void WndProc(ref Message m)
        {
            // Trap WM_PASTE:
            if (m.Msg == 0x302 && Clipboard.ContainsText())
            {
                int value;
                if (int.TryParse(Clipboard.GetText(), out value))
                {
                    SelectedText = value.ToString();
                }
                return;
            }

            base.WndProc(ref m);
        }
    }
}
