using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FreshMvvm;
using PropertyChanged;
using StarsForward.Extensions;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class ThankYouPopupPageModel : FreshBasePageModel
    {
        private readonly IMapper _mapper;

        public ThankYouPopupPageModel(IMapper mapper)
        {
            _mapper = mapper;
        }


        #region COMMANDS

        public Command CloseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PopPopupPageModel();
                });
            }
        }

        #endregion



        // OVERRIDES
        public override void Init(object initData)
        {
            base.Init(initData);
        }

        private async Task StartTimer()
        {
            // start timer
            await Task.Delay(10000);

            await CoreMethods.PopPopupPageModel();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            StartTimer().RunForget();
        }

    }
}