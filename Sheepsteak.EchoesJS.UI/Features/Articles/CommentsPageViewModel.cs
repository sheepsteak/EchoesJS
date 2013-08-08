using Caliburn.Micro;
using Sheepsteak.EchoesJS.Core;
using Sheepsteak.EchoesJS.UI.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Sheepsteak.EchoesJS.UI.Resources;

namespace Sheepsteak.EchoesJS.UI.Features.Articles
{
    public class CommentsPageViewModel : Screen
    {
        private bool isBusy;
        private readonly EchoJsClient echoJsClient;
        private readonly IEventAggregator eventAggregator;

        public CommentsPageViewModel(
            EchoJsClient echoJsClient,
            IEventAggregator eventAggregator)
        {
            this.echoJsClient = echoJsClient;
            this.eventAggregator = eventAggregator;

            this.Comments = new BindableCollection<Comment>();
        }

        public int ArticleId { get; set; }

        public string ArticleTitle { get; set; }

        public BindableCollection<Comment> Comments { get; private set; }

        public bool IsBusy
        {
            get { return this.isBusy; }
            private set
            {
                this.isBusy = value;

                this.NotifyOfPropertyChange(() => this.IsBusy);
                this.NotifyOfPropertyChange(() => this.ShowNoCommentsMessage);
            }
        }

        public bool ShowComments
        {
            get { return this.Comments.Any(); }
        }

        public bool ShowNoCommentsMessage
        {
            get { return !this.IsBusy && !this.Comments.Any(); }
        }

        public void OpenInBrowser()
        {
            //this.eventAggregator.RequestTask<WebBrowserTask>(task =>
            //{
            //    task.Uri = new Uri(this.Article.Url);
            //});
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            await this.GetComments();
        }

        private async Task GetComments()
        {
            this.IsBusy = true;

            IEnumerable<Comment> comments = null;
            bool showFailMessage = false;
            try
            {
                comments = await this.echoJsClient.GetCommentsForArticle(this.ArticleId);
                comments = this.FlattenComments(comments);
            }
            catch (HttpRequestException)
            {
                showFailMessage = true;
            }
            catch (UnsupportedMediaTypeException)
            {
                showFailMessage = true;
            }

            this.IsBusy = false;

            if (showFailMessage)
            {
                await Task.Delay(50);

                var showMessageBoxResult = new ShowMessageBoxResult(
                    "There was an error trying to get the comments.",
                    AppResources.NetworkErrorMessageBoxCaption);

                await showMessageBoxResult.ExecuteAsync();
            }
            else
            {
                this.Comments.AddRange(comments);
            }

            this.NotifyOfPropertyChange(() => this.ShowNoCommentsMessage);
            this.NotifyOfPropertyChange(() => this.ShowComments);
        }

        private IEnumerable<Comment> FlattenComments(IEnumerable<Comment> comments)
        {
            var flattenedComments = new List<Comment>();

            foreach (var comment in comments)
            {
                flattenedComments.Add(comment);

                if (comment.Replies != null && comment.Replies.Any())
                {
                    flattenedComments.AddRange(this.FlattenComments(comment.Replies));
                }
            }

            return flattenedComments;
        }
    }
}
