using System;
using Realms;
using StarsForward.Enums;

namespace StarsForward.Data.Models
{
    public class Donor : RealmObject
    {
        public Donor()
        {
            this.RecordStatus = RecordStatusType.Active;
        }


        [PrimaryKey]
        public string Id { get; set; }  // guid
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AdditionalInformation { get; set; }

        public bool BecomeAVolunteer { get; set; }
        public bool BecomeMonthlyDonor { get; set; }
        public bool HostFundraiser { get; set; }
        public bool SponsorFundraiser { get; set; }
        public bool ReceiveNewsletter { get; set; }
        public bool ConnectOnSocialMedia { get; set; }
        public bool BecomeCorporateSponsor { get; set; }
        public bool RaffleDonations { get; set; }
        public bool Other { get; set; }

        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public DateTimeOffset? DateExported { get; set; }
        public int RecordStatusRaw { get; set; }

        public RecordStatusType RecordStatus
        {
            get => (RecordStatusType)RecordStatusRaw;
            set => RecordStatusRaw = (int)value;
        }

    }
}