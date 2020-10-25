using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Abstractions
{
    public class ExecutionContext : IExecutionContext
    {
        private readonly Action<string> onStatusUpdate;

        public ExecutionContext(Action<string> onStatusUpdate)
        {
            this.onStatusUpdate = onStatusUpdate;
        }

        public bool HasSuccess { get; private set; } = true;
        public string Error { get; private set; }

        public void UpdateStatus(string status)
        {
            this.onStatusUpdate?.Invoke(status);
        }

        public void Fail(string error)
        {
            this.HasSuccess = false;
            this.Error = error;
        }
    }
}
