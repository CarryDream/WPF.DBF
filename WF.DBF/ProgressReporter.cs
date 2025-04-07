using System;

namespace WF.DBF
{
    /// <summary>
    /// 自定义进度报告类
    /// </summary>
    public class ProgressReporter : IProgress<(int current, int total, string message)>
    {
        private readonly ProgressForm _progressForm;

        public ProgressReporter(ProgressForm progressForm)
        {
            _progressForm = progressForm;
        }

        public void Report((int current, int total, string message) value)
        {
            _progressForm.SetProgress(value.current, value.total);
            _progressForm.SetStatus(value.message);
        }
    }
}
