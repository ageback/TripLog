using Ninject;
using Ninject.Modules;
using TripLog.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }

        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            // 注册核心服务
            Kernel = new StandardKernel(new TripLogCoreModule(), new TripLogNavModule());
            // 注册特定平台服务
            Kernel.Load(platformModules);
            SetMainPage();
        }

        void SetMainPage()
        {
            var mainPage = new NavigationPage(new MainPage())
            {
                BindingContext = Kernel.Get<MainViewModel>()
            };
            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;
            navService.XamarinFormsNav = mainPage.Navigation;
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}