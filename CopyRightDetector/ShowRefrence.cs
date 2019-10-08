using System.Windows.Forms;

namespace CopyRightDetector
{
    public partial class ShowRefrence : Form
    {
        public string TextContent { get; }

        public ShowRefrence(string content)
        {
            TextContent = TextContent;

            InitializeComponent();

            txtContent.Text = content;
        }


    }
}
