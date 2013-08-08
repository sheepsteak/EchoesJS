using Caliburn.Micro;

namespace Sheepsteak.EchoesJS.UI.Features.Main
{
    public class LatestViewModelStorage : StorageHandler<LatestViewModel>
    {
        public override void Configure()
        {
            this.Property(t => t.Articles)
                .InPhoneState();             
        }
    }
}
