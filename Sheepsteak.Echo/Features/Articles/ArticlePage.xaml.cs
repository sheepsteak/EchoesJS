using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Sheepsteak.Echo.Features.Articles
{
    public partial class ArticlePage : PhoneApplicationPage
    {
        private readonly EchoJsClient echoJsClient;

        public ArticlePage()
        {
            InitializeComponent();

            this.echoJsClient = new EchoJsClient();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string title;
            string url;
            string description;
            
            this.NavigationContext.QueryString.TryGetValue("title", out title);
            this.articleTitle.Text = title;

            this.NavigationContext.QueryString.TryGetValue("url", out url);
            this.webBrowser.Source = new Uri(url);

            this.NavigationContext.QueryString.TryGetValue("description", out description);
            this.descriptionText.Text = description;
        }
    }
}