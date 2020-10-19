using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Workflow.Abstractions;
using Model = Workflow.Library.Model;

namespace Workflow.Library.SendMail
{
    [Step("SendMail")]
    public class SendMailStep : StepBase<Model.SendMailParam>
    {
        public SendMailStep(Model.SendMailParam param) : base(param) 
        {
        }

        protected override Task ExecuteAsync(IExecutionContext context)
        {
            if (this.Param.MailDefinitionId == 3)
            {
                Console.WriteLine($@"Sending mail ""{this.Param.MailDefinitionId}"" ... ERROR");
                context.Fail("invalid recipent address");
            }
            else
            {
                Console.WriteLine($@"Sending mail ""{this.Param.MailDefinitionId}"" ... OK");
            }

            return Task.CompletedTask;
        }
    }
}
