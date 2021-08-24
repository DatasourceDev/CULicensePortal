using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CULCS.AD
{
    public class GetGraphItemsWorker : IHostedService
    {
        private readonly IGraphProvider _microsoftGraphProvider;

        public GetGraphItemsWorker(IGraphProvider microsoftGraphProvider)
        {
            _microsoftGraphProvider = microsoftGraphProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //var emailObjectId = await _microsoftGraphProvider.GetIdByEmail("6200001235@student.chula.ac.th");
            //Console.WriteLine(emailObjectId);

            //var groupObjectId = await _microsoftGraphProvider.GetIdByGroupName("(LL.M.) Advance Admin Law 2/2562");
            //Console.WriteLine(groupObjectId);

            //Console.ReadLine();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
