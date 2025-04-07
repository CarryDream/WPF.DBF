using System;
using System.Threading;
using System.Windows.Forms;

namespace WF.DBF
{
    public partial class ProgressForm : Form
    {
        private CancellationTokenSource _cancellationTokenSource;

        public ProgressForm()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        public void SetProgress(int value, int maximum)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetProgress(value, maximum)));
                return;
            }

            progressBar.Maximum = maximum;
            progressBar.Value = Math.Min(value, maximum);
            lblStatus.Text = $"处理中... {value}/{maximum}";
            Application.DoEvents();
        }

        public void SetStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetStatus(status)));
                return;
            }

            lblStatus.Text = status;
            Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
            btnCancel.Enabled = false;
            lblStatus.Text = "正在取消操作...";
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        // Dispose方法已在Designer.cs中定义
    }
}
