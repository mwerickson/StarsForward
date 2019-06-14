using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarsForward.Messages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarsForward.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionPage : ContentPage
    {
        public CollectionPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<ResetFormMessage>(this, ResetFormMessage.Message,
                message => { FirstName.Focus(); });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FirstName.Focus();
        }
    }
}