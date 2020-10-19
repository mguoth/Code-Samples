using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Abstractions
{
    public class ExecutionContext : IExecutionContext
    {
        public bool HasSuccess { get; private set; } = true;
        public string Error { get; private set; }

        public void Fail(string error)
        {
            this.HasSuccess = false;
            this.Error = error;
        }
    }
}
