using System;
using System.Collections.Generic;
using StarsForward.Data.Models;

namespace StarsForward.ViewModels
{
    public class EventViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public List<Donor> Donors { get; set; }

        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
    }
}