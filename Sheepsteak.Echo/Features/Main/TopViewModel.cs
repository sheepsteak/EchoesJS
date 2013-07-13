using Caliburn.Micro;
using Sheepsteak.Echo.Core;
using Sheepsteak.Echo.Features.Articles;
using Sheepsteak.Echo.Framework;
using Sheepsteak.Echo.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Main
{
    public class TopViewModel : Screen, IRefreshableScreen
    {
        private readonly ICacheService cacheService;
        private readonly EchoJsClient echoJsClient;
        private bool isRefreshing;
        private readonly INavigationService navigationService;

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
            }
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            await this.RefreshArticles();
        }

        public void ArticleSelected(Article article)
        {
            this.cacheService.Articles[article.Id] = article;
            var uriBuilder = this.navigationService.UriFor<ArticlePageViewModel>();
            uriBuilder.WithParam(v => v.ArticleId, article.Id);
            this.navigationService.Navigate(uriBuilder.BuildUri());
        }

        public async Task RefreshArticles()
        {
            if (this.IsRefreshing)
            {
                return;
            }

            this.IsRefreshing = true;

            this.Articles.Clear();

            var articles = await this.echoJsClient.GetTopNews();

            this.Articles.AddRange(articles.ToList());

            this.IsRefreshing = false;
        }

    }
}
