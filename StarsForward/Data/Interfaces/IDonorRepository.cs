using System.Collections.Generic;
using Realms;
using StarsForward.Data.Models;

namespace StarsForward.Data.Interfaces
{
    public interface IDonorRepository : IRepository<Donor>
    {
        void AddToEvent(Event e, Donor entity);
        void Exported(List<Donor> entities);
        void Update(Donor entity);
        void ResetExportDate(Donor entity);
    }
}