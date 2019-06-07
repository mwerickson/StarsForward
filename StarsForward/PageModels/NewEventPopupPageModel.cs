using System;
using System.Net;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using FreshMvvm.Popups;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
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
                    var model = _mapper.Map<Event>(Event);
                    _eventRepository.AddItem(model);
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