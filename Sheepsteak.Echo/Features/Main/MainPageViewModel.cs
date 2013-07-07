using Caliburn.Micro;
using Sheepsteak.Echo.Features.Articles;
using Sheepsteak.Echo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Main
{
    public class MainPageViewModel : Screen
    {
        private readonly EchoJsClient echoJsClient;
        private bool isBusy;
        private readonly INavigationService navigationService;
        private Article selectedArticle;

        public MainPageViewModel(INavigationService navigationService, EchoJsClient echoJsClient)
        {
            this.navigationService = navigationService;
            this.echoJsClient = echoJsClient;

            this.Articles = new BindableCollection<Article>();
        }

        public BindableCollection<Article> Articles { get; private set; }

        public Article SelectedArticle
        {
            get { return this.selectedArticle; }
            set
            {
                this.selectedArticle = value;

                var uriBuilder = this.navigationService.UriFor<ArticlePageViewModel>();
                uriBuilder.WithParam(v => v.Article, this.selectedArticle);
                this.navigationService.Navigate(uriBuilder.BuildUri());
            }
        }

        protected override async void OnActivate()
        {
            base.OnActivate();

            await this.Refresh();
        }

        private async Task Refresh()
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            this.Articles.Clear();

            var articles = await this.echoJsClient.GetTopNews();

            this.Articles.AddRange(articles.ToList());

            this.isBusy = false;
        }

        //private void TopList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var article = e.AddedItems[0] as Article;

        //    var uriString = "/Features/Articles/ArticlePage.xaml?" +
        //        "id=" + article.Id +
        //        "&url=" + Uri.EscapeDataString(article.Url) +
        //        "&title=" + Uri.EscapeDataString(article.Title) +
        //        "&description=" + Uri.EscapeDataString(article.Description);
        //    this.NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
        //}
    }
}
