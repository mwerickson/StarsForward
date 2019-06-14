using System;
using AutoMapper;
using FreshMvvm;
using StarsForward.Data.Interfaces;
using StarsForward.Data.Models;
using StarsForward.Data.Repositories;
using StarsForward.PageModels;
using StarsForward.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarsForward
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            ConfigureIOC();
            CreateObjectMaps();

            MainPage = LoadMasterDetail();
        }

        public FreshMasterDetailNavigationContainer LoadMasterDetail()
        {
            try
            {
                var masterDetailNav = new FreshMasterDetailNavigationContainer();
                masterDetailNav.Init("Menu", "hamburger.png");
                masterDetailNav.AddPage<EventsPageModel>("Events", null);
                masterDetailNav.AddPage<DonorsPageModel>("Donors", null);
                masterDetailNav.AddPage<CollectionPageModel>("Collection", null);
                return masterDetailNav;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void ConfigureIOC()
        {
            FreshMvvm.FreshIOC.Container.Register<IDonorRepository, DonorDataStore>();
            FreshMvvm.FreshIOC.Container.Register<IEventRepository, EventDataStore>();
        }

        protected void CreateObjectMaps()
        {
            var config = RegisterMappings();
            IMapper mapper = config.CreateMapper();
            FreshIOC.Container.Register<IMapper>(mapper);
        }

        private MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Donor, DonorViewModel>();
                cfg.CreateMap<Event, EventViewModel>();

                cfg.CreateMap<EventViewModel, Event>();
                cfg.CreateMap<DonorViewModel, Donor>();
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
