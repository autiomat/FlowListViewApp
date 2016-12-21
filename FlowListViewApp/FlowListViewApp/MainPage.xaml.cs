using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlowListViewApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainVM)
        {
            InitializeComponent();
            BindingContext = mainVM;
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Implementation details
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        private string _timeNow;

        private void LoadItems()
        {
            for (int i = 0; i < 200; i++)
            {
                var item = new Item()
                {
                    Id = i,
                    Name = String.Format("Item{0}", i),
                    Timer = ""
                };
                Items.Add(item);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void GetTimeNow()
        {
            // https://forums.xamarin.com/discussion/30936/device-starttimer-async
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Factory.StartNew(() =>
                {
                    // Do the actual request and wait for it to finish.
                    OnTimerTick();

                    // Switch back to the UI thread to update the UI
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // Now repeat by scheduling a new request
                        GetTimeNow();
                    });
                });
                // Don't repeat the timer (we will start a new timer when the request is finished)
                return false;
            });
        }

        private bool OnTimerTick()
        {
            TimeNow = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

            foreach (var item in Items)
            {
                item.Timer = DateTime.Now.ToString("mm:ss", CultureInfo.InvariantCulture);
            }

            return true;
        }
        #endregion

        #region API
        public MainViewModel()
        {
            LoadItems();

            GetTimeNow();
        }

        public ObservableCollection<Item> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }

        public string TimeNow
        {
            get
            {
                return _timeNow;
            }
            set
            {
                _timeNow = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    public class Item : INotifyPropertyChanged
    {
        private string _timer;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Timer
        {
            get
            {
                return _timer;
            }
            set
            {
                _timer = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
