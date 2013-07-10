using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sheepsteak.Echo.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Sheepsteak.Echo.Features.Main
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool isRefreshing = false;
        private AppBarButton refreshButton;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                this.isRefreshing = value;

                if (this.isRefreshing)
                {
                    this.progressIndicator.IsVisible = true;
                    this.refreshButton.IsEnabled = false;
                }
                else
                {
                    this.progressIndicator.IsVisible = false;
                    this.refreshButton.IsEnabled = true;
                }
            }
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.BackgroundColor = Color.FromArgb(255, 175, 29, 29);
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // Create a new button and set the text value to the localized string from AppResources.
            this.refreshButton = new AppBarButton();
            this.refreshButton.IconUri = new Uri("/Assets/Images/refresh.png", UriKind.Relative);
            this.refreshButton.Message = "RefreshArticles";
            this.refreshButton.Text = AppResources.RefreshAppBarButtonText;
            this.ApplicationBar.Buttons.Add(refreshButton);
        }
    }
}