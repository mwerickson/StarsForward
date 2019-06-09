using System;
using System.Net;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.ViewModels;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class EventPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventPageModel(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public EventViewModel Event { get; set; }
        public string ExportText => Event == null ? "Export 0 donors" : $"Export {Event.Donors.Count} donors";
        public bool ExportEnabled => Event?.Donors?.Count > 0;


        #region COMMANDS

        public Command UpdateCommand
        {
            get
            {
                return new Command(() =>
                {
                    
                });
            }
        }

        public Command CollectCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PushPageModel<CollectionPageModel>(null, true); });
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
                    }

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