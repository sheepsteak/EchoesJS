using Caliburn.Micro;

namespace Sheepsteak.EchoesJS.UI.Features.Articles
{
    public class ArticleWebPageViewModelStorage : StorageHandler<ArticleWebPageViewModel>
    {
        public override void Configure()
        {
            this.Property(a => a.Article)
                .InPhoneState();
        }
    }
}
