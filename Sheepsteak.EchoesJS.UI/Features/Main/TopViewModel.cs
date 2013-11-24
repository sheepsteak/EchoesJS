using Caliburn.Micro;
using Sheepsteak.EchoesJS.Core;
using Sheepsteak.EchoesJS.UI.Features.Articles;
using Sheepsteak.EchoesJS.UI.Framework;
using Sheepsteak.EchoesJS.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sheepsteak.EchoesJS.UI.Features.Main
{
    public class TopViewModel : Screen, IRefreshableScreen
    {
        private readonly ICacheService cacheService;
        private readonly EchoJsClient echoJsClient;
        private bool isRefreshing;
        private readonly INavigationService navigationService;
        private bool showFailureMessage;

        public TopViewModel(
            INavigationService navigationService,
            ICacheService cacheService,
            EchoJsClient echoJsClient)
        {
            this.navigationService = navigationService;
            this.cacheService = cacheService;
            this.echoJsClient = echoJsClient;

            this.Articles = new BindableCollection<Article>();
            this.DisplayName = AppResources.TopPivotTitle;
        }

        public BindableCollection<Article> Articles { get; private set; }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                this.isRefreshing = value;
                this.NotifyOfPropertyChange(() => this.IsRefreshing);
                this.NotifyOfPropertyChange(() => this.ShowLoadingMessage);
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

        public bool ShowLoadingMessage
        {
            get { return this.IsRefreshing && this.Articles.Count == 0; }
        }

        public void ShowArticle(Article article)
        {
            this.cacheService.Articles[article.Id] = article;

            if (article.IsText)
            {
                this.navigationService.UriFor<ArticleTextPageViewModel>()
                    .WithParam(v => v.ArticleId, article.Id)
                    .Navigate();
            }
            else
            {
                this.navigationService.UriFor<ArticleWebPageViewModel>()
                    .WithParam(v => v.ArticleId, article.Id)
                    .Navigate();
            }
        }

        public void ShowComments(Article article)
        {
            this.navigationService.UriFor<CommentsPageViewModel>()
                .WithParam(c => c.ArticleId, article.Id)
                .WithParam(c => c.ArticleTitle, article.Title)
                .Navigate();
        }

        public async Task LoadMore()
        {
            if (this.IsRefreshing)
            {
                return;
            }

            this.IsRefreshing = true;

            IEnumerable<Article> articles = null;
            bool showFailMessage = false;
            try
            {
                articles = await this.echoJsClient.GetTopNews(this.Articles.Count);
            }
            catch (HttpRequestException)
            {
                showFailMessage = true;
            }
            catch (UnsupportedMediaTypeException)
            {
                showFailMessage = true;
            }

            this.IsRefreshing = false;

            if (showFailMessage)
            {
                await Task.Delay(50);

                var showMessageBoxResult = new ShowMessageBoxResult(
                    "There was an error trying to get the top articles.",
                    AppResources.NetworkErrorMessageBoxCaption);

                await showMessageBoxResult.ExecuteAsync();
            }
            else
            {
                foreach (var article in articles)
                {
                    this.Articles.Add(article);
                }
            }
        }

        public async Task RefreshArticles()
        {
            if (this.IsRefreshing)
            {
                return;
            }

            this.ShowFailureMessage = false;
            this.Articles.Clear();
            this.IsRefreshing = true;

            IEnumerable<Article> articles = null;

            bool showFailMessage = false;
            try
            {
                articles = await this.echoJsClient.GetTopNews();
            }
            catch (HttpRequestException)
            {
                showFailMessage = true;
            }
            catch (UnsupportedMediaTypeException)
            {
                showFailMessage = true;
            }

            this.IsRefreshing = false;

            if (showFailMessage)
            {
                await Task.Delay(50);

                var showMessageBoxResult = new ShowMessageBoxResult(
                    "There was an error trying to get the top articles.",
                    AppResources.NetworkErrorMessageBoxCaption);

                await showMessageBoxResult.ExecuteAsync();
                this.ShowFailureMessage = true;
            }
            else
            {
                this.Articles.AddRange(articles.ToList());
            }
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            if (this.Articles.Count == 0)
            {
                await this.RefreshArticles();
            }
        }
    }
}
