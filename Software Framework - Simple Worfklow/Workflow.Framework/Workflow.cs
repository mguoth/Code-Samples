using System;
using System.Collections.Generic;
using System.Reflection;
using Workflow.Abstractions;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Workflow.Framework.Extensions;

namespace Workflow.Framework
{
    public class Workflow
    {
        public Workflow(Model.WorkflowDefinition workflowDefinition, Assembly assemblyToScan)
        {
            foreach (Model.StepDefinition step in workflowDefinition.Steps)
            {
                //Scan for classes decorated  by "StepAttribute" with particular step type
                AttributeInstance<StepAttribute> attributeInstance = assemblyToScan
                    .ScanTypesForCustomAttributes((StepAttribute attribute) => attribute.Type == step.Type)
                    .Single();

                Type stepType = attributeInstance.Type;
                Type stepInterfaceType = stepType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IStep<>)).FirstOrDefault();

                if (stepInterfaceType != null)
                {
                    Type paramType = stepInterfaceType.GetGenericArguments().Single();

                    object paramValue;
                    if (step.Param.GetType() == paramType)
                    {
                        paramValue = step.Param;
                    }
                    else if (step.Param is JToken jtoken)
                    {
                        paramValue = jtoken.ToObject(paramType);
                    }
                    else
                    {
                        throw new NotSupportedException($@"Step ""{step.Type}"" has invalid ""Param"" property type ""{step.Param.GetType()}""");
                    }

                    IStep stepInstance = (IStep) stepType.GetConstructor(new Type[] { paramType }).Invoke(new object[] { paramValue });

                    this.Steps.Add(new Step(step, stepInstance));
                }
                else
                {
                    throw new NotSupportedException($@"The step type ""{stepType}"" doesn't implement ""{typeof(IStep<>)}"" interface");
                }
            }
        }

        protected IList<Step> Steps { get; } = new List<Step>();

        public async Task ExecuteAsync()
        {
            ExecutionContext context = new ExecutionContext();

            int i = 1;
            foreach (Step step in this.Steps)
            {
                Console.WriteLine($@"Step {i}/{this.Steps.Count()} (""{step.Definition.Type}""): STARTED ...");

                await step.Instance.ExecuteAsync(context);

                if (context.HasSuccess)
                {
                    Console.WriteLine($@"Step {i}/{this.Steps.Count()} (""{step.Definition.Type}""): COMPLETED");
                }
                else
                {
                    Console.WriteLine($@"Step {i}/{this.Steps.Count()} (""{step.Definition.Type}""): FAILED - " + context.Error);
                    Console.WriteLine("Execution of workflow was cancelled");
                    return;
                }
                i++;
            }

            Console.WriteLine("Execution of workflow has successfully completed");
        }

        protected class Step
        {
            public Step(Model.StepDefinition definition, IStep instance)
            {
                this.Definition = definition;
                this.Instance = instance;
            }

            public Model.StepDefinition Definition { get; }

            public IStep Instance { get; }
        }
    }
}