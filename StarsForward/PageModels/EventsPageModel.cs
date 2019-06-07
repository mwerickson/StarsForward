using System;
using System.Collections.ObjectModel;
using System.Net;
using FreshMvvm;
using PropertyChanged;
using StarsForward.Data.Models;
using StarsForward.Pages;
using StarsForward.ViewModels;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class EventsPageModel : FreshBasePageModel
    {
        public EventsPageModel()
        {
        }

        public ObservableCollection<EventViewModel> Events { get; set; }

        private EventViewModel _selectedEvent;

        public EventViewModel SelectedItem
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                if (value == null) return;
                ItemSelectedCommand.Execute(SelectedItem);
                SelectedItem = null;
            }
        }


        #region COMMANDS

        public Command ItemSelectedCommand
        {
            get
            {
                return new Command<EventViewModel>(async (item) =>
                {
                    // start collecting donor information
                    await CoreMethods.PushPageModel<CollectionPageModel>(item);
                });
            }
        }

        public Command NewEventCommand
        {
            get
            {
                return new Command(() =>
                {
                    
                });
            }
        }

        #endregion



        // OVERRIDES
        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

    }
}