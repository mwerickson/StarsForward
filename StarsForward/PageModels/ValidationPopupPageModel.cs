using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using Realms;
using StarsForward.Extensions;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class ValidationPopupPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;

        public ValidationPopupPageModel(IMapper mapper)
        {
            _mapper = mapper;
        }


        #region COMMANDS

        public Command OkCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PopPopupPageModel(); });
            }
        }

        #endregion

        public ObservableCollection<string> Messages { get; set; }


        // OVERRIDES
        public override void Init(object initData)
        {
            if (initData is List<string> model)
            {
                Messages = new ObservableCollection<string>(model);
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

    }
}