using System;
using System.Collections.Generic;
using System.Reflection;
using Workflow.Abstractions;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Workflow.Framework
{
    public class Workflow
    {           
        public Workflow(Model.WorkflowDefinition workflowDefinition, params Assembly[] frameworkExtensionsAssemblies)
        {
            this.WorkflowDefinition = workflowDefinition;

            //Validate and build worfklow steps
            foreach (Model.StepDefinition step in workflowDefinition.Steps)
            {
                //Find concrete step class decorated by "StepAttribute" with the step type commming from workflow definition
                Type stepType = frameworkExtensionsAssemblies
                    .ScanTypesForCustomAttributes((StepAttribute attribute) => attribute.Type == step.Type)
                    .SingleOrDefault();

                if (stepType == null)
                    throw new InvalidOperationException($@"The worfklow step type ""{step.Type}"" was not found in registered framework extensions");

                Type stepInterfaceType = stepType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IStep<>)).FirstOrDefault();

                //Create a concrete step instance
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
                        throw new NotSupportedException($@"Step ""{step.Type}"" has invalid ""Param"" property type ""{step.Param.GetType()}""");

                    IStep stepInstance = (IStep) stepType.GetConstructor(new Type[] { paramType }).Invoke(new object[] { paramValue });

                    this.Steps.Add(new Step(step, stepInstance));
                }
                else
                    throw new NotSupportedException($@"The step type ""{stepType}"" doesn't implement ""{typeof(IStep<>)}"" interface");
            }
        }

        public Model.WorkflowDefinition WorkflowDefinition { get; }

        protected IList<Step> Steps { get; } = new List<Step>();

        public async Task ExecuteAsync()
        {
            Console.WriteLine($@"The execution of workflow definition ""{this.WorkflowDefinition.FileName}"" has started...");

            //Execute worfklow steps
            int i = 1;
            foreach (Step step in this.Steps)
            {
                string stepLabel = $"Step {i} of {this.Steps.Count()} <{step.Definition.Type}>";
                Console.WriteLine($@"{stepLabel}: Started...");

                ExecutionContext context = new ExecutionContext((status) => Console.WriteLine($@"{stepLabel}: {status}"));

                await step.Instance.ExecuteAsync(context);

                if (context.HasSuccess)
                {
                    Console.WriteLine($@"{stepLabel}: Completed");
                }
                else
                {
                    Console.WriteLine($@"{stepLabel}: ERROR - " + context.Error);
                    Console.WriteLine("The execution of workflow has stopped!");
                    return;
                }
                i++;
            }

            Console.WriteLine("The execution of workflow has successfully completed.");
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