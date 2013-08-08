using Caliburn.Micro;

namespace Sheepsteak.EchoesJS.UI.Features.Articles
{
    public class ArticleTextPageViewModelStorage : StorageHandler<ArticleTextPageViewModel>
    {
        public override void Configure()
        {
            this.Property(a => a.Article)
                .InPhoneState();
        }
    }
}
