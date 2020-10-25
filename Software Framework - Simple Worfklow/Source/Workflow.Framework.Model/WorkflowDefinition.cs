using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Framework.Model
{
    public class WorkflowDefinition
    {
        [JsonIgnore]
        public string FileName { get; set; }
        public IList<StepDefinition> Steps { get; set; }
    }
}
