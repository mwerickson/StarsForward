using StarsForward.Data.Models;

namespace StarsForward.Data.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        int DeleteExported(string eventId);
        void Update(Event entity);
    }
}