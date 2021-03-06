using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace AzureFunctions.TestHelpers
{
    internal static class DurableOrchestrationClientExtensions
    {
        public static async Task Wait(this IDurableOrchestrationClient client,
            Func<IList<DurableOrchestrationStatus>, bool> until, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var status = await client.GetStatusAsync(token);
                if (until(status))
                {
                    break;
                }

                await Task.Delay(TimeSpan.FromSeconds(5), token);
            }
        }
    }
}