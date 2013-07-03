using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Sheepsteak.Echo.Resources;

namespace Sheepsteak.Echo.Features.Articles
{
    public partial class ArticlePage : PhoneApplicationPage
    {
        private const string readabilityUrl = "http://readability.com/read?url={0}";

        private string articleUrl;
        private readonly EchoJsClient echoJsClient;
        private bool inTextView;
        private ApplicationBarIconButton textButton;

        public ArticlePage()
        {
            InitializeComponent();

            this.BuildLocalizedApplicationBar();

            this.echoJsClient = new EchoJsClient();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string title;
            string description;

            this.NavigationContext.QueryString.TryGetValue("title", out title);
            this.articleTitle.Text = title;

            this.NavigationContext.QueryString.TryGetValue("url", out this.articleUrl);
            this.webBrowser.Source = new Uri(this.articleUrl);

            this.NavigationContext.QueryString.TryGetValue("description", out description);
            this.descriptionText.Text = description;
        }

        private void BuildLocalizedApplicationBar()
        {
            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.BackgroundColor = Color.FromArgb(255, 175, 29, 29);
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // Create a new button and set the text value to the localized string from AppResources.
            this.textButton = new ApplicationBarIconButton(new Uri("/Assets/Images/text.png", UriKind.Relative));
            this.textButton.Click += this.Text_Click;
            this.textButton.Text = AppResources.TextAppBarButtonText;
            this.ApplicationBar.Buttons.Add(textButton);
        }

        private void Text_Click(object sender, EventArgs e)
        {
            if (this.inTextView)
            {
                this.webBrowser.Source = new Uri(this.articleUrl);
                this.textButton.IconUri = new Uri("/Assets/Images/text.png", UriKind.Relative);
                this.textButton.Text = AppResources.TextAppBarButtonText;
            }
            else
            {
                var textViewUrl = new Uri(string.Format(readabilityUrl, this.articleUrl));
                this.webBrowser.Source = textViewUrl;
                this.textButton.IconUri = new Uri("/Assets/Images/web.png", UriKind.Relative);
                this.textButton.Text = AppResources.WebAppBarButtonText;
            }

            this.inTextView = !this.inTextView;
        }
    }
}