using System;
using Model = Workflow.Framework.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Linq;

namespace Workflow.CLI
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Count() < 1)
            {
                Console.WriteLine(@"Missing argument.
Syntax: ""Workflow.ConsoleApp <workflow-definition-filename.json>""");
                return;
            }

            string workflowFileName = args[0];
            string workflowJson = File.ReadAllText(workflowFileName);
            Model.WorkflowDefinition workflowDefinition = JsonConvert.DeserializeObject<Model.WorkflowDefinition>(workflowJson);
            workflowDefinition.FileName = workflowFileName;

            //create and validate workflow
            Workflow.Framework.Workflow workflow = new Workflow.Framework.Workflow(workflowDefinition, Assembly.Load("Workflow.Extensions"));

            //execute workflow
            await workflow.ExecuteAsync();
        }
    }
}
