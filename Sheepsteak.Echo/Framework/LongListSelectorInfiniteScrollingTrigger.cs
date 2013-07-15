using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Interactivity;

namespace Sheepsteak.Echo.Framework
{
    public class LongListSelectorInfiniteScrollingTrigger : TriggerBase<LongListSelector>
    {
        public static readonly DependencyProperty IsLoadingProperty;

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        static LongListSelectorInfiniteScrollingTrigger()
        {
            IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(LongListSelectorInfiniteScrollingTrigger), new PropertyMetadata(false));
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.ItemRealized += AssociatedObject_ItemRealized;
        }

        private void AssociatedObject_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (this.AssociatedObject.ItemsSource == null)
            {
                return;
            }

            var item = e.Container.Content;
            var items = this.AssociatedObject.ItemsSource;
            var index = items.IndexOf(item);

            if (!this.IsLoading && items.Count - index <= 1)
            {
                this.InvokeActions(null);
            }
        }
    }
}
