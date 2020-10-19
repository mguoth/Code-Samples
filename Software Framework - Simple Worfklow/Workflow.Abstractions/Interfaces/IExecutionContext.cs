using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Abstractions
{
    public interface IExecutionContext
    {
        void Fail(string error);
    }
}
