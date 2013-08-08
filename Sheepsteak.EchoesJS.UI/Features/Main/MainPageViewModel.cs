using Caliburn.Micro;
using Sheepsteak.EchoesJS.UI.Features.Settings;
using Sheepsteak.EchoesJS.UI.Framework;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sheepsteak.EchoesJS.UI.Features.Main
{
    public class MainPageViewModel : Conductor<IRefreshableScreen>.Collection.OneActive
    {
        private readonly LatestViewModel latestViewModel;
        private readonly INavigationService navigationService;
        private readonly TopViewModel topViewModel;

        public MainPageViewModel(
            INavigationService navigationService,
            TopViewModel topViewModel,
            LatestViewModel latestViewModel)
        {
            this.navigationService = navigationService;
            this.topViewModel = topViewModel;
            this.latestViewModel = latestViewModel;
        }

        public bool IsBusy
        {
            get { return this.ActiveItem != null ? this.ActiveItem.IsRefreshing : false; }
        }

        public async Task RefreshArticles()
        {
            var selectedItem = this.ActiveItem;

            if (selectedItem != null)
            {
                await selectedItem.RefreshArticles();
            }
        }

        public void Settings()
        {
            this.navigationService.UriFor<SettingsPageViewModel>().Navigate();
        }

        protected override void ChangeActiveItem(IRefreshableScreen newItem, bool closePrevious)
        {
            this.UnlistenToActiveItem();

            base.ChangeActiveItem(newItem, closePrevious);

            this.ListenToActiveItem();

            this.NotifyOfPropertyChange(() => this.IsBusy);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.Items.Add(this.topViewModel);
            this.Items.Add(this.latestViewModel);
        }

        private void ListenToActiveItem()
        {
            if (this.ActiveItem != null)
            {
                this.ActiveItem.PropertyChanged += this.ActiveItem_PropertyChanged;
            }
        }

        private void UnlistenToActiveItem()
        {
            if (this.ActiveItem != null)
            {
                this.ActiveItem.PropertyChanged -= this.ActiveItem_PropertyChanged;
            }
        }

        private void ActiveItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRefreshing")
            {
                this.NotifyOfPropertyChange(() => this.IsBusy);
            }
        }
    }
}
