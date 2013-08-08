using Caliburn.Micro;

namespace Sheepsteak.EchoesJS.UI.Features.Main
{
    public class TopViewModelStorage : StorageHandler<TopViewModel>
    {
        public override void Configure()
        {
            this.Property(t => t.Articles)
                .InPhoneState();             
        }
    }
}
