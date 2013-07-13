using Caliburn.Micro;
using System;

namespace Sheepsteak.Echo.Framework
{
    public abstract class Result : IResult
    {
        public event EventHandler<ResultCompletionEventArgs> Completed;

        public abstract void Execute(ActionExecutionContext context);

        protected virtual void OnCompleted(ResultCompletionEventArgs e)
        {
            var handler = this.Completed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
