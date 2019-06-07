using System;
using System.Net;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using StarsForward.Data.Interfaces;
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


        #region COMMANDS

        public Command ExitCommand
        {
            get
            {
                return new Command(() =>
                {
                    // switch to admin page
                });
            }
        }

        public Command SubmitCommand
        {
            get
            {
                return new Command(() =>
                {
                    // save current donor   

                    // start a new one
                    Donor = new DonorViewModel();
                });
            }
        }

        #endregion



        // OVERRIDES
        public override void Init(object initData)
        {
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