using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sheepsteak.Echo.Resources;
using System;
using System.Windows.Media;
using Caliburn.Micro;

namespace Sheepsteak.Echo.Features.Articles
{
    public partial class ArticlePage : PhoneApplicationPage
    {
        private AppBarButton textButton;

        public ArticlePage()
        {
            InitializeComponent();

            this.BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.BackgroundColor = Color.FromArgb(255, 175, 29, 29);
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // Create a new button and set the text value to the localized string from AppResources.
            this.textButton = new AppBarButton();
            this.textButton.IconUri = new Uri("/Assets/Images/text.png", UriKind.Relative);
            this.textButton.Message = "SwitchView";
            this.textButton.Text = AppResources.TextAppBarButtonText;
            this.ApplicationBar.Buttons.Add(textButton);
        }
    }
}