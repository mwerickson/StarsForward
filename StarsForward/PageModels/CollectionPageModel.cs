using System;
using System.Collections.Generic;
using System.Net;
using Acr.UserDialogs;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Realms;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Extensions;
using StarsForward.Messages;
using StarsForward.ViewModels;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class CollectionPageModel : FreshBasePageModel
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;

        public CollectionPageModel(IMapper mapper, IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }

        public DonorViewModel Donor { get; set; }
        public EventViewModel Event { get; set; }

        public string Message { get; set; }

        public List<string> States { get; set; }


        #region COMMANDS

        public Command ExitCollectionCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PopPageModel(true); });
            }
        }

        public Command SubmitCommand
        {
            get
            {
                return new Command(async () =>
                {
                    // validate the donor record
                    if (!Donor.IsValid())
                    {
                        return;
                    }

                    var e = _mapper.Map<Event>(Event);

                    // save current donor   
                    Donor.DateCreated = DateTimeOffset.Now;
                    Donor.DateModified = DateTimeOffset.Now;
                    var entity = _mapper.Map<Donor>(Donor);
                    if (entity != null)
                    {
                        _donorRepository.AddToEvent(e, entity);
                    }

                    // start a new one
                    ClearForm();

                    // thank the donor!
                    await CoreMethods.PushPopupPageModel<ThankYouPopupPageModel>();

                });
            }
        }

        public Command ClearCommand => new Command(ClearForm);

        #endregion


        private void ClearForm()
        {
            Donor = new DonorViewModel();
            MessagingCenter.Send(new ResetFormMessage(), ResetFormMessage.Message);
        }


        // OVERRIDES
        public override void Init(object initData)
        {
            ClearForm();

            States = Data.States.Names();

            if (initData is EventViewModel model)
            {
                Event = model;
            }

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

    }
}