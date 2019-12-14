using Element.Core.Events;
using Element.Domain.Interface;
using Element.Infra.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Applicaion.EventSource
{
    public class EventStoreService : IEventStoreService
    {

        private readonly IEventStoreRepository _EventStoreRepository;


        public EventStoreService(IEventStoreRepository eventStoreRepository)
        {
            _EventStoreRepository = eventStoreRepository;
        }


        public void Save<T>(T theEvent) where T : Event
        {
            var name = typeof(T).Name;
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                name);

            _EventStoreRepository.Store(storedEvent);
        }
    }
}
