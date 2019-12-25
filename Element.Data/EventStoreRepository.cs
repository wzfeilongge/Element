using Element.Core.Events;
using Element.Data.EntityFrameworkCores;
using Element.Domain.Interface;
using Element.Infra.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Data
{
    public class EventStoreRepository : BaseRepository<StoredEvent>, IEventStoreRepository
    {

      

        public EventStoreRepository(ILogger<BaseRepository<StoredEvent>> logger):base(logger)
        {

        }


        public async Task<IList<StoredEvent>> All(Guid aggregateId)
        {
            return  await base.GetAll(e => e.AggregateId == aggregateId).ToAsyncEnumerable().ToList();
        }

        public async Task Store(StoredEvent theEvent)
        {
            await base.AddModel(theEvent);
            //return;          
        }
    }
}
