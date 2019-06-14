using System;
using System.Net;
using Acr.UserDialogs;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Extensions;
using StarsForward.ViewModels;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class DonorEditPopupPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;
        private readonly IDonorRepository _donorRepository;

        public DonorEditPopupPageModel(IMapper mapper, IDonorRepository donorRepository)
        {
            _mapper = mapper;
            _donorRepository = donorRepository;
        }

        public string Title => "Edit Donor";
        public DonorViewModel Donor { get; set; }


        #region COMMANDS

        public Command SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        // map viewmodel to realm object
                        // update the realm object.
                        var donor = _mapper.Map<Donor>(Donor);
                        _donorRepository.Update(donor);
                        UserDialogs.Instance.Toast("Donor record updated");
                        await CoreMethods.PopPopupPageModel();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                });
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PopPopupPageModel(); });
            }
        }

        #endregion


        // OVERRIDES
        public override void Init(object initData)
        {
            if (initData is DonorViewModel model)
            {
                Donor = model;
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

    }
}