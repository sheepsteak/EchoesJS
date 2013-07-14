using System.Windows;
using System.Windows.Controls;

namespace Sheepsteak.Echo.Controls
{
    public class LongListSelector : Microsoft.Phone.Controls.LongListSelector
    {
        public LongListSelector()
        {
            this.SelectionChanged += LongListSelector_SelectionChanged;
        }

        public new object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(LongListSelector),
                new PropertyMetadata(null, OnSelectedItemChanged)
            );

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = base.SelectedItem;
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            var selector = (LongListSelector)d;
            selector.ChangeBaseSelectedItem(e.NewValue);
        }

        private void ChangeBaseSelectedItem(object selectedItem)
        {
            base.SelectedItem = selectedItem;
        }
    }
}
