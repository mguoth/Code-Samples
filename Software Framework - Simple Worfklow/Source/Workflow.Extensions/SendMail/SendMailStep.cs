using System.Threading.Tasks;
using Workflow.Abstractions;

namespace Workflow.Extensions
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

            //For the real functionality do something like:
            //1. Load a mail definition by given id (can be stored in any repository)
            //2. Use any service for sending mail (e.g. System.Net.Mail.SmtpClient);

            return Task.CompletedTask;
        }
    }
}
