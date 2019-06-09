using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Realms;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Extensions;
using StarsForward.Messages;
using StarsForward.Pages;
using StarsForward.ViewModels;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class EventsPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventsPageModel(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;

            MessagingCenter.Subscribe<RefreshEventsMessage>(this, RefreshEventsMessage.Message, message =>
            {
                LoadDataCommand.Execute(null);
            });
        }

        public ObservableCollection<EventViewModel> Events { get; set; }

        private EventViewModel _selectedEvent;

        public EventViewModel SelectedItem
        {
            get { return _selectedEvent; }
            set
            {
                if (_selectedEvent != value)
                {
                    _selectedEvent = value;
                }
                ItemSelectedCommand.Execute(SelectedItem);
            }
        }


        #region COMMANDS

        public Command ItemSelectedCommand
        {
            get
            {
                return new Command<EventViewModel>(async (item) =>
                {
                    await CoreMethods.PushPageModel<EventPageModel>(item);
                });
            }
        }

        public Command NewEventCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PushPopupPageModel<NewEventPopupPageModel>(); });
            }
        }

        public Command LoadDataCommand
        {
            get
            {
                return new Command(() =>
                {
                    var items = _eventRepository.List();
                    if (items != null)
                    {
                        var events = _mapper.Map<List<EventViewModel>>(items);
                        Events = new ObservableCollection<EventViewModel>(events);
                    }
                });
            }
        }

        #endregion



        // OVERRIDES
        public override void Init(object initData)
        {
            Events = new ObservableCollection<EventViewModel>();
        }

        public override void ReverseInit(object returnedData)
        {
            if (returnedData is EventViewModel model)
            {
                // refresh the list
                LoadDataCommand.Execute(null);
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            LoadDataCommand.Execute(null);
        }

    }
}