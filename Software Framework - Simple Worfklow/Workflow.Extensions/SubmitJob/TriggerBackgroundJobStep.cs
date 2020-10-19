using System.Threading.Tasks;
using Workflow.Abstractions;

namespace Workflow.Extensions.SendMail
{
    [Step("TriggerBackgroundJob")]
    public class TriggerBackgroundJobStep : StepBase<Model.TriggerBackgroundJobParam>
    {
        public TriggerBackgroundJobStep(Model.TriggerBackgroundJobParam param) : base(param)
        {
        }

        protected override Task ExecuteAsync(IExecutionContext context)
        {
            context.UpdateStatus($@"Simulate trigerring background job with JobDefinitionId ""{this.Param.JobDefinitionId}""... Done");

            return Task.CompletedTask;
        }
    }
}
