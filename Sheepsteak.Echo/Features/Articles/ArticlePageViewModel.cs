using Caliburn.Micro;
using Sheepsteak.Echo.Core;
using Sheepsteak.Echo.Framework;
using Sheepsteak.Echo.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Articles
{
    public class ArticlePageViewModel : Screen
    {
        private const string readabilityUrl = "http://readability.com/read?url={0}";
        private bool inReadabilityView;
        private bool isBusy;

        private readonly ICacheService cacheService;

        public ArticlePageViewModel(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public int ArticleId { get; set; }

        public Article Article { get; private set; }

        public bool IsBusy
        {
            get { return this.isBusy; }
            private set
            {
                this.isBusy = value;

                this.NotifyOfPropertyChange(() => this.IsBusy);
            }
        }

        public string SwitchViewIconUri
        {
            get { return this.inReadabilityView ? "/Assets/Images/web.png" : "/Assets/Images/text.png"; }
        }

        public string SwitchViewText
        {
            get { return this.inReadabilityView ? AppResources.WebAppBarButtonText : AppResources.TextAppBarButtonText; }
        }

        public string Url
        {
            get
            {
                if (this.Article == null)
                {
                    return null;
                }

                return this.inReadabilityView ? string.Format(readabilityUrl, this.Article.Url) : this.Article.Url;
            }
        }

        public void Navigated()
        {
            this.IsBusy = false;
        }

        public void Navigating()
        {
            this.IsBusy = true;
        }

        public void SwitchView()
        {
            this.inReadabilityView = !this.inReadabilityView;

            this.NotifyOfPropertyChange(() => this.Url);
            this.NotifyOfPropertyChange(() => this.SwitchViewIconUri);
            this.NotifyOfPropertyChange(() => this.SwitchViewText);
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            this.Article = this.cacheService.Articles[this.ArticleId];

            this.NotifyOfPropertyChange(() => this.Article);
            this.NotifyOfPropertyChange(() => this.Url);

            this.IsBusy = true;
        }
    }
}
