using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Abstractions
{
    public abstract class StepBase<TParam> : IStep<TParam>
    {
        public StepBase(TParam param)
        {
            this.Param = param;
        }

        protected TParam Param { get; }

        Task IStep.ExecuteAsync(IExecutionContext context) => this.ExecuteAsync(context);

        protected abstract Task ExecuteAsync(IExecutionContext context);
    }
}
