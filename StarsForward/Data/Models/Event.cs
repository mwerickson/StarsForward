using System;
using System.Collections.Generic;
using Realms;

namespace StarsForward.Data.Models
{
    public class Event : RealmObject
    {
        [PrimaryKey]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public IList<Donor> Donors { get; }

        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}