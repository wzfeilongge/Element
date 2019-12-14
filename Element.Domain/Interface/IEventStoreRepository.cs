using Element.Core.Events;
using Element.Infra.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Domain.Interface
{
   public interface IEventStoreRepository:IBaseRepository<StoredEvent>
    {
        Task Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);


    }
}
