using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Core.Events
{
    public interface IEventStoreService
    {

        void Save<T>(T theEvent) where T : Event;
    }
}
