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

                    // save current donor   
                    var entity = _mapper.Map<Donor>(Donor);
                    if (entity != null)
                    {
                        _donorRepository.Add(entity);
                    }

                    // start a new one
                    ClearForm();

                    // thank the donor!
                    await CoreMethods.PushPopupPageModel<ThankYouPopupPageModel>();

                });
            }
        }

        #endregion


        private void ClearForm()
        {
            Donor = new DonorViewModel();
        }



        // OVERRIDES
        public override void Init(object initData)
        {
            ClearForm();

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