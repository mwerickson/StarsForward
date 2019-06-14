using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Realms;
using StarsForward.Enums;
using StarsForward.Validators;

namespace StarsForward.ViewModels
{
    public class DonorViewModel
    {
        private readonly IValidator _validator;

        public DonorViewModel()
        {
            _validator = new DonorValidator();    
        }


        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Phone Number")]
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

        public string EventName { get; set; }

        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public DateTimeOffset? DateExported { get; set; }

        public int RecordStatusRaw { get; set; }
        public RecordStatusType RecordStatus
        {
            get => (RecordStatusType)RecordStatusRaw;
            set => RecordStatusRaw = (int)value;
        }

        public List<string> ValidationMessages { get; set; }

        public string FullName => $"{LastName}, {FirstName}";
        public string FullAddress => $"{Address1}, {City} {State}, {Zip}";
        public string ContactInfo => $"Phone: {Phone}  Email: {Email}";

        public string DateCreatedText => DateCreated.ToString("g");
        public string DateModifiedText => DateModified?.ToString("g") ?? "n/a";
        public string DateExportedText => DateExported?.ToString("g") ?? "n/a";

        public bool IsValid()
        {
            var validationResults = _validator.Validate(this);

            if (validationResults.IsValid) return true;

            App.Current.MainPage.DisplayAlert("Missing or Incorrect Information",
                validationResults.Errors[0].ErrorMessage, "Ok");

            return false;

            //ValidationMessages = new List<string>();

            //if (string.IsNullOrEmpty(FirstName))
            //{
            //    ValidationMessages.Add("First Name is required. ");
            //}

            //if (string.IsNullOrEmpty(LastName))
            //{
            //    ValidationMessages.Add("Last Name is required. ");
            //}

            //if (string.IsNullOrEmpty(Address1))
            //{
            //    ValidationMessages.Add("Address is required. ");
            //}

            //if (string.IsNullOrEmpty(City))
            //{
            //    ValidationMessages.Add("City is required. ");
            //}

            //if (string.IsNullOrEmpty(State))
            //{
            //    ValidationMessages.Add("State is required. ");
            //}

            //if (string.IsNullOrEmpty(Zip))
            //{
            //    ValidationMessages.Add("Zip code is required. ");
            //}

            //if (string.IsNullOrEmpty(Phone))
            //{
            //    ValidationMessages.Add("Phone number is required. ");
            //}

            //if (string.IsNullOrEmpty(Email))
            //{
            //    ValidationMessages.Add("Email address is required. ");
            //}

            //return ValidationMessages.Count == 0;
        }
    }
}