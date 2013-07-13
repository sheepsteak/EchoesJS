using Caliburn.Micro;
using Sheepsteak.Echo.Core;
using Sheepsteak.Echo.Features.Articles;
using Sheepsteak.Echo.Framework;
using Sheepsteak.Echo.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Main
{
    public class LatestViewModel : Screen, IRefreshableScreen
    {
        private readonly ICacheService cacheService;
        private readonly EchoJsClient echoJsClient;
        private bool isRefreshing;
        private readonly INavigationService navigationService;
        private bool showFailureMessage;

        public LatestViewModel(
            INavigationService navigationService,
            ICacheService cacheService,
            EchoJsClient echoJsClient)
        {
            this.navigationService = navigationService;
            this.cacheService = cacheService;
            this.echoJsClient = echoJsClient;

            this.Articles = new BindableCollection<Article>();
            this.DisplayName = AppResources.LatestPivotTitle;
        }

        public BindableCollection<Article> Articles { get; private set; }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                this.isRefreshing = value;
                this.NotifyOfPropertyChange(() => this.IsRefreshing);
            }
        }

        public bool ShowFailureMessage
        {
            get { return this.showFailureMessage; }
            private set
            {
                this.showFailureMessage = value;
                this.NotifyOfPropertyChange(() => this.ShowFailureMessage);
            }
        }
        
        protected async override void OnInitialize()
        {
            base.OnActivate();

            await this.RefreshArticles();
        }

        public void ArticleSelected(Article article)
        {
            var uriBuilder = this.navigationService.UriFor<ArticlePageViewModel>();
            uriBuilder.WithParam(v => v.Article, article);
            this.navigationService.Navigate(uriBuilder.BuildUri());
        }

        public async Task RefreshArticles()
        {
            if (this.IsRefreshing)
            {
                return;
            }

            this.ShowFailureMessage = false;
            this.IsRefreshing = true;

            this.Articles.Clear();

            IEnumerable<Article> articles = null;

            bool showFailMessage = false;
            try
            {
                articles = await this.echoJsClient.GetLatestNews();
            }
            catch (HttpRequestException)
            {
                showFailMessage = true;
            }

            this.IsRefreshing = false;

            if (showFailMessage)
            {
                await Task.Delay(50);

                var showMessageBoxResult = new ShowMessageBoxResult(
                    "There was an error trying to get the latest articles.",
                    "Echo");

                await showMessageBoxResult.ExecuteAsync();
                this.ShowFailureMessage = true;
            }
            else
            {
                this.Articles.AddRange(articles.ToList());
            }
        }
    }
}
