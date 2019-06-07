using System.Collections.Generic;
using StarsForward.Data.Helpers;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;

namespace StarsForward.Data.Repositories
{
    public class DonorDataStore : IDonorRepository
    {
        #region IDatastore Implementation

        public bool AddItem(Donor item)
        {
            var context = RealmHelper.GetInstance();
            // TODO: get highest ID value and increment
            throw new System.NotImplementedException();
        }

        public bool UpdateItem(Donor item)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteItem(long id)
        {
            throw new System.NotImplementedException();
        }

        public Donor GetItem(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Donor> GetItems(bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        public IEnumerable<Donor> GetItemsByEventAsync(long eventId, bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }
    }
}