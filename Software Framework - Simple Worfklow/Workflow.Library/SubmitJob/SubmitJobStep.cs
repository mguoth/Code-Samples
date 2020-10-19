using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Workflow.Abstractions;
using Model = Workflow.Library.Model;

namespace Workflow.Library.SendMail
{
    [Step("SubmitJob")]
    public class SubmitJobStep : StepBase<Model.SubmitJobParam>
    {
        public SubmitJobStep(Model.SubmitJobParam param) : base(param)
        {
        }

        protected override Task ExecuteAsync(IExecutionContext context)
        {
            Console.WriteLine($@"Submitting job ""{this.Param.JobDefinitionId}"" ... OK");

            return Task.CompletedTask;
        }
    }
}
