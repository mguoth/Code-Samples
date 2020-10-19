using System;
using Model = Workflow.Framework.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using Workflow.Abstractions;
using System.IO;
using System.Linq;

namespace Workflow.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Count() < 1)
            {
                Console.WriteLine(@"Run ""Workflow.ConsoleApp <workflow-definition-filename.json>""");
                return;
            }

            string workflowdefinitionJson = File.ReadAllText(args[0]);

            Model.WorkflowDefinition workflowDefinition = JsonConvert.DeserializeObject<Model.WorkflowDefinition>(workflowdefinitionJson);
            
            //create workflow
            Workflow.Framework.Workflow workflow = new Workflow.Framework.Workflow(workflowDefinition, Assembly.Load("Workflow.Library"));

            //execute workflow
            await workflow.ExecuteAsync();
        }
    }
}
