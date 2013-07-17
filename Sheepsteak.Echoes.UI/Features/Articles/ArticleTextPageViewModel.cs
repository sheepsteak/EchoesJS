using Caliburn.Micro;
using Sheepsteak.Echoes.Core;
using Sheepsteak.Echoes.UI.Framework;

namespace Sheepsteak.Echoes.UI.Features.Articles
{
    public class ArticleTextPageViewModel : Screen
    {
        private Article article;
        private readonly ICacheService cacheService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;

        public ArticleTextPageViewModel(
            ICacheService cacheService,
            IEventAggregator eventAggregator,
            INavigationService navigationService)
        {
            this.cacheService = cacheService;
            this.eventAggregator = eventAggregator;
            this.navigationService = navigationService;
        }

        public int ArticleId { get; set; }

        public Article Article
        {
            get { return this.article; }
            private set
            {
                this.article = value;

                this.NotifyOfPropertyChange(() => this.Article);
            }
        }

        public void ShowComments()
        {
            this.navigationService.UriFor<CommentsPageViewModel>()
                .WithParam(c => c.ArticleId, this.ArticleId)
                .WithParam(c => c.ArticleTitle, this.Article.Title)
                .Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.Article = this.cacheService.Articles[this.ArticleId];

            this.NotifyOfPropertyChange(() => this.Article);
        }
    }
}
