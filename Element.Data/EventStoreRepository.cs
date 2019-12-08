using Element.Core.Events;
using Element.Infra.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public IList<StoredEvent> All(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public void Store(StoredEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}
