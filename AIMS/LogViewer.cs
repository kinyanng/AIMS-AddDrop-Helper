using System.Windows.Forms;

namespace AIMS
{
    public partial class LogViewer : Form
    {
        public LogViewer(LogManager logManager)
        {
            InitializeComponent();
            rtb_log.Text = logManager.ToString();
        }
    }
}
