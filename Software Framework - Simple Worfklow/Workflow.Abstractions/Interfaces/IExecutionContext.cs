using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Abstractions
{
    public interface IExecutionContext
    {
        void UpdateStatus(string status);
        void Fail(string error);
    }
}
