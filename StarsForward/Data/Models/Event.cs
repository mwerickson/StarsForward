using System;
using System.Collections.Generic;
using Realms;
using StarsForward.Enums;

namespace StarsForward.Data.Models
{
    public class Event : RealmObject
    {
        public Event()
        {
            this.RecordStatus = RecordStatusType.Active;
        }

        [PrimaryKey]
        public string Id { get; set; }    // guid
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public IList<Donor> Donors { get; }

        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public int RecordStatusRaw { get; set; }

        public RecordStatusType RecordStatus
        {
            get => (RecordStatusType)RecordStatusRaw;
            set => RecordStatusRaw = (int)value;
        }
    }
}