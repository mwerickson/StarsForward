using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using Acr.UserDialogs;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Realms;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Enums;
using StarsForward.Extensions;
using StarsForward.Messages;
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

            MessagingCenter.Subscribe<RefreshEventsMessage>(this, RefreshEventsMessage.Message, message =>
            {
                RefreshDataCommand.Execute(null);
            });
        }

        public EventViewModel Event { get; set; }
        public List<DonorViewModel> Donors { get; set; }
        public string ExportText => Event == null ? "Export 0 donors" : $"Export {Donors?.Count(x => x.DateExported == null) ?? 0} donors";

        public string ClearText =>
            Event == null ? "Clear 0 donors" : $"Clear {Donors?.Count(x => x.DateExported != null)} donors";

        public bool ExportEnabled => Donors?.Count > 0;
        public bool ClearEnabled => Donors?.Count(x => x.DateExported != null) > 0;

        private DonorViewModel _selectedDonor;

        public DonorViewModel SelectedDonor
        {
            get { return _selectedDonor; }
            set
            {
                _selectedDonor = value;
                if (value == null) return;
                DonorSelectedCommand.Execute(SelectedDonor);
                SelectedDonor = null;
            }
        }


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
                        Donors = Event.Donors.Where(x => x.RecordStatus != RecordStatusType.Deleted).OrderBy(x => x.FullName).ToList();
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

                    var donors = Event.Donors.Where(x => x.DateExported == null).ToList();
                    foreach (var donor in donors)
                    {
                        donor.EventName =
                            $"{Event.Name} - {Event.StartDate:d} thru {Event.EndDate:d}";
                        donor.DateExported = DateTimeOffset.Now;
                    }

                    // get file name
                    var fullPath = Path.Combine(FileSystem.CacheDirectory,
                        $"{Event.Name.Replace(" ", "")}_{DateTimeOffset.Now:MMddyyyy_hhmmss}.csv");

                    var output = CsvConverter.Serialize<DonorViewModel>(donors, fullPath);

                    var message = new EmailMessage()
                    {
                        Subject = $"Donors from {Event.Name}",
                        Body = $"Here is the export of {donors.Count()} donors from the {Event.Name} event.  Please see attached CSV file."
                    };

                    message.Attachments.Add(new EmailAttachment(fullPath));

                    //var output = csv.Serialize(donors, Event.Name);
                    await Email.ComposeAsync(message);

                    // set the exported date field
                    var donorList = _mapper.Map<List<Donor>>(donors);
                    _donorRepository.Exported(donorList);
                    RefreshDataCommand.Execute(null);

                    // delete the csv file
                    File.Delete(fullPath);
                });
            }
        }

        public Command DonorSelectedCommand
        {
            get
            {
                return new Command<DonorViewModel>(async (model) =>
                {
                    await CoreMethods.PushPopupPageModel<DonorEditPopupPageModel>(model);
                    RefreshDataCommand.Execute(null);
                });
            }
        }

        public Command ResetExportCommand
        {
            get
            {
                return new Command<DonorViewModel>((model) =>
                {
                    var donor = _mapper.Map<Donor>(model);
                    _donorRepository.ResetExportDate(donor);
                    UserDialogs.Instance.Toast($"Export date for {model.FullName} has been reset");
                    RefreshDataCommand.Execute(null);
                });
            }
        }

        public Command DeleteDonor
        {
            get
            {
                return new Command<DonorViewModel>(async (model) =>
                {
                    var confirmed = await UserDialogs.Instance.ConfirmAsync(
                        $"Are you sure you want to delete donor {model.FullName}?", "Delete Donor?", "Yes", "No");
                    if (confirmed)
                    {
                        _donorRepository.DeleteById(model.Id);
                        UserDialogs.Instance.Toast("Donor was successfully deleted.");
                        RefreshDataCommand.Execute(null);
                    }
                });
            }
        }

        public Command EditEventCommand
        {
            get
            {
                return new Command(async () =>
                    {
                        await CoreMethods.PushPopupPageModel<NewEventPopupPageModel>(Event);
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