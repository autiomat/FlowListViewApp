using DLToolkit.Forms.Controls;
using Xamarin.Forms;

namespace FlowListViewApp
{
    public partial class App : Application
    {
        private MainViewModel _mainVM = new MainViewModel();

        public App()
        {
            InitializeComponent();

            FlowListView.Init();

            MainPage = new FlowListViewApp.MainPage(_mainVM);
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
