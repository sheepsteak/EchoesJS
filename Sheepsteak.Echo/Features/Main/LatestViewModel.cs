using Caliburn.Micro;
using Sheepsteak.Echo.Core;
using Sheepsteak.Echo.Features.Articles;
using Sheepsteak.Echo.Framework;
using Sheepsteak.Echo.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Main
{
    public class LatestViewModel : Screen, IRefreshableScreen
    {
        private readonly EchoJsClient echoJsClient;
        private bool isBusy;
        private readonly INavigationService navigationService;

        public LatestViewModel(
            INavigationService navigationService,
            EchoJsClient echoJsClient)
        {
            this.navigationService = navigationService;
            this.echoJsClient = echoJsClient;

            this.Articles = new BindableCollection<Article>();
            this.DisplayName = AppResources.LatestPivotTitle;
        }

        public BindableCollection<Article> Articles { get; private set; }

        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                this.NotifyOfPropertyChange(() => this.IsBusy);
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
            if (this.IsBusy)
            {
                return;
            }

            this.IsBusy = true;

            this.Articles.Clear();

            var articles = await this.echoJsClient.GetLatestNews();

            this.Articles.AddRange(articles.ToList());

            this.IsBusy = false;
        }

    }
}
