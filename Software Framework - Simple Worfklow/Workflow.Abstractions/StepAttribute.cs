using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Abstractions
{
    public class StepAttribute : Attribute
    {
        public string Type { get; }

        public StepAttribute(string type)
        {
            this.Type = type;
        }
    }
}
