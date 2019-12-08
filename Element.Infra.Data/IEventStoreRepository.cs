using Element.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Infra.Data
{
   public interface IEventStoreRepository
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);



    }
}
