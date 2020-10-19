using System.Threading.Tasks;
using Workflow.Abstractions;

namespace Workflow.Extensions.SendMail
{
    [Step("SendMail")]
    public class SendMailStep : StepBase<Model.SendMailParam>
    {
        public SendMailStep(Model.SendMailParam param) : base(param) 
        {
        }

        protected override Task ExecuteAsync(IExecutionContext context)
        {
            if (this.Param.MailDefinitionId == 999)
            {
                context.UpdateStatus($@"Simulate sending mail with MailDefinitionId ""{this.Param.MailDefinitionId}""... Failed");
                context.Fail($@"The MailDefinitionId ""{this.Param.MailDefinitionId}"" was not found");
            }
            else
            {
                context.UpdateStatus($@"Simulate sending mail with MailDefinitionId ""{this.Param.MailDefinitionId}""... Done");
            }

            return Task.CompletedTask;
        }
    }
}
