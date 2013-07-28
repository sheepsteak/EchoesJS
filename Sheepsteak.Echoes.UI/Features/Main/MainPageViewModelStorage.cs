using Caliburn.Micro;

namespace Sheepsteak.Echoes.UI.Features.Main
{
    public class MainPageViewModelStorage : StorageHandler<MainPageViewModel>
    {
        public override void Configure()
        {
            this.ActiveItemIndex()
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}
