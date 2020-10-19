using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Framework.Model
{
    public class WorkflowDefinition
    {
        public IList<StepDefinition> Steps { get; set; }
    }
}
