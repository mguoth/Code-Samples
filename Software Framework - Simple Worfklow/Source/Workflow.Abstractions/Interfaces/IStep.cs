using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Abstractions
{
    public interface IStep
    {
        Task ExecuteAsync(IExecutionContext context);
    }

    public interface IStep<TParam> : IStep
    {
    }
}