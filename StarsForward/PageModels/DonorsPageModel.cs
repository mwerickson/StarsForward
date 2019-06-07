using System;
using System.Net;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace StarsForward.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class DonorsPageModel : FreshBasePageModel
    {
        public DonorsPageModel()
        {
        }


        #region COMMANDS

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