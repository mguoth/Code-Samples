using System.Threading.Tasks;
using Workflow.Abstractions;

namespace Workflow.Extensions
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

            //For the real functionality do something like:
            //1. Load a background job definition by given id (can be stored in any repository)
            //2. Use a service responsible for running of the particular background job (e.g. Run external Powershell process)

            return Task.CompletedTask;
        }
    }
}
