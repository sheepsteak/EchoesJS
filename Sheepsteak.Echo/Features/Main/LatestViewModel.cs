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
        private Article selectedArticle;

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

            await this.RefreshArticles();
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
