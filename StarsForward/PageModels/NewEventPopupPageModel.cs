using System;
using System.Net;
using Acr.UserDialogs;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Extensions;
using StarsForward.Messages;
using StarsForward.ViewModels;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewEventPopupPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        
        public NewEventPopupPageModel(IEventRepository eventRepository, IMapper mapper)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public string Title { get; set; } = "New Event";
        public EventViewModel Event { get; set; }


        #region COMMANDS

        public Command CancelCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PopPopupPageModel(); });
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Event.StartDate = Event.StartDatePicked;
                    Event.EndDate = Event.EndDatePicked;
                    var model = _mapper.Map<Event>(Event);
                    _eventRepository.Add(model);

                    UserDialogs.Instance.Toast("Event has been saved successfully!");

                    MessagingCenter.Send(new RefreshEventsMessage(), RefreshEventsMessage.Message);
                    await CoreMethods.PopPopupPageModel();
                });
            }
        }

        #endregion



        // OVERRIDES
        public override void Init(object initData)
        {
            Event = new EventViewModel();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

    }
}