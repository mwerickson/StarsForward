using System.Collections.Generic;

namespace StarsForward.Data.Interfaces
{
    public interface IDataStore<T>
    {
        bool AddItem(T item);
        bool UpdateItem(T item);
        bool DeleteItem(long id);
        T GetItem(long id);
        IEnumerable<T> GetItems(bool forceRefresh = false);
    }
}