using Caliburn.Micro;
using Sheepsteak.Echo.Framework;
using System.Threading.Tasks;

namespace Sheepsteak.Echo.Features.Main
{
    public class MainPageViewModel : Conductor<IRefreshableScreen>.Collection.OneActive
    {
        private bool isBusy;
        private readonly LatestViewModel latestViewModel;
        private readonly TopViewModel topViewModel;

        public MainPageViewModel(
            TopViewModel topViewModel,
            LatestViewModel latestViewModel)
        {
            this.topViewModel = topViewModel;
            this.latestViewModel = latestViewModel;
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            private set
            {
                this.isBusy = value;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            }
        }

        public async Task RefreshArticles()
        {
            var selectedItem = this.ActiveItem;

            if (selectedItem != null)
            {
                await selectedItem.RefreshArticles();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.Items.Add(this.topViewModel);
            this.Items.Add(this.latestViewModel);
        }
    }
}
