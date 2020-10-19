using System;
using Model = Workflow.Framework.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Workflow.Abstractions;

namespace TestConsole
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string workflowdefinitionJson = @"
            {
                Steps: [
                {
                    Type: ""SendMail"",
                    Param: {
                        MailDefinitionId: 1
                    }
                },
                {
                    Type: ""SendMail"",
                    Param: {
                        MailDefinitionId: 3
                    }
                },
                {
                    Type: ""SubmitJob"",
                    Param: {
                        JobDefinitionId: 1
                    }
                }
                ]
            }";

            Model.WorkflowDefinition workflowDefinition = JsonConvert.DeserializeObject<Model.WorkflowDefinition>(workflowdefinitionJson);
            
            //create workflow
            Workflow.Framework.Workflow workflow = new Workflow.Framework.Workflow(workflowDefinition, Assembly.Load("Workflow.Library"));

            //execute workflow
            await workflow.ExecuteAsync();
        }
    }
}
