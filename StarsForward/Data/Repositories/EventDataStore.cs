using System.Collections.Generic;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;

namespace StarsForward.Data.Repositories
{
    public class EventDataStore : IEventRepository
    {
        public bool AddItem(Event item)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateItem(Event item)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteItem(long id)
        {
            throw new System.NotImplementedException();
        }

        public Event GetItem(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Event> GetItems(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }
    }
}