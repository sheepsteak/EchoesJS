using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Sheepsteak.Echo.Framework
{
    public class LongListSelectorInfiniteScrollingTrigger : TriggerBase<LongListSelector>
    {
        public static readonly DependencyProperty IsLoadingProperty;
        public static readonly DependencyProperty OffsetProperty;

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public int Offset
        {
            get { return (int)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        static LongListSelectorInfiniteScrollingTrigger()
        {
            IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(LongListSelectorInfiniteScrollingTrigger), new PropertyMetadata(false));
            OffsetProperty = DependencyProperty.Register("Offset", typeof(int), typeof(LongListSelectorInfiniteScrollingTrigger), new PropertyMetadata(5));
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.ItemRealized += AssociatedObject_ItemRealized;
        }

        void AssociatedObject_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (this.AssociatedObject.ItemsSource == null)
            {
                return;
            }

            var currentCount = this.AssociatedObject.ItemsSource.Count;

            if (currentCount >= this.Offset && e.ItemKind == LongListSelectorItemKind.Item)
            {
                if (e.Container.Content.Equals(this.AssociatedObject.ItemsSource[currentCount - this.Offset]))
                {
                    this.InvokeActions(null);
                }
            }
        }

        //protected override string GetEventName()
        //{
        //    return "ItemRealized";
        //}

        //protected override void OnEvent(EventArgs eventArgs)
        //{
        //   // base.OnEvent(eventArgs);

        //}
    }
}
