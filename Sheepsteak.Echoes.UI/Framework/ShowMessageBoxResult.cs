using Caliburn.Micro;
using System;
using System.Windows;

namespace Sheepsteak.Echoes.UI.Framework
{
    public class ShowMessageBoxResult : Result
    {
        private string caption;
        private string messageBoxText;
        private bool showCancel;

        public ShowMessageBoxResult(string messageBoxText, string caption, bool showCancel = false)
        {
            if (string.IsNullOrEmpty(messageBoxText))
            {
                throw new ArgumentNullException("messageBoxText");
            }

            if (string.IsNullOrEmpty(caption))
            {
                throw new ArgumentNullException("caption");
            }

            this.messageBoxText = messageBoxText;
            this.caption = caption;
            this.showCancel = showCancel;
        }
        public override void Execute(ActionExecutionContext context)
        {
            var result = MessageBox.Show(this.messageBoxText, this.caption,
                this.showCancel ? MessageBoxButton.OKCancel : MessageBoxButton.OK);

            this.OnCompleted(new ResultCompletionEventArgs());
        }
    }
}
