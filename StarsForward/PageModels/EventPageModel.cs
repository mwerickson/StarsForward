using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Acr.UserDialogs;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Realms;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Serializers;
using StarsForward.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class EventPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IDonorRepository _donorRepository;

        public EventPageModel(IMapper mapper, IEventRepository eventRepository, IDonorRepository donorRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _donorRepository = donorRepository;
        }

        public EventViewModel Event { get; set; }
        public List<DonorViewModel> Donors { get; set; }
        public string ExportText => Event == null ? "Export 0 donors" : $"Export {Donors.Count(x => x.DateExported == null)} donors";

        public string ClearText =>
            Event == null ? "Clear 0 donors" : $"Clear {Donors?.Count(x => x.DateExported != null)} donors";

        public bool ExportEnabled => Donors?.Count > 0;
        public bool ClearEnabled => Donors?.Count(x => x.DateExported != null) > 0;


        #region COMMANDS

        public Command ClearCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var confirmed = await UserDialogs.Instance.ConfirmAsync(
                        "This will remove all exported donors.  Do you wish to continue?", "Confirm", "Yes", "No");
                    if (confirmed)
                    {
                        var count = _eventRepository.DeleteExported(Event.Id);
                        UserDialogs.Instance.Toast($"Removed {count} exported donors from {Event.Name}");
                        RefreshDataCommand.Execute(null);
                    }
                });
            }
        }

        public Command CollectCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PushPageModel<CollectionPageModel>(Event, true); });
            }
        }

        public Command RefreshDataCommand
        {
            get
            {
                return new Command(() =>
                {
                    var e = _eventRepository.GetById(Event.Id);
                    if (e != null)
                    {
                        Event = _mapper.Map<EventViewModel>(e);
                        Donors = Event.Donors.OrderBy(x => x.FullName).ToList();
                    }

                });
            }
        }

        public Command ExportCommand
        {
            get
            {
                return new Command(async () =>
                {
                    // TODO:  Need to have the event name included with the donor rows
                    var csv = new CsvSerializer();
                    var donors = Event.Donors.Where(x => x.DateExported == null);
                    var output = csv.Serialize(Event.Donors, Event.Name);
                    await Email.ComposeAsync($"Donors from {Event.Name}", output, "merickson91@gmail.com");
                    
                    // set the exported date field
                    var donorList = _mapper.Map<List<Donor>>(donors);
                    _donorRepository.Exported(donorList);
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

            RefreshDataCommand.Execute(null);
        }

    }
}