using Microsoft.Phone.Controls;
using System.Windows.Media;

namespace Sheepsteak.Echoes.UI.Features.Main
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.applicationBar.BackgroundColor = ((SolidColorBrush)App.Current.Resources["PhoneChromeBrush"]).Color;
                this.applicationBar.ForegroundColor = ((SolidColorBrush)App.Current.Resources["PhoneChromeForegroundBrush"]).Color;
            };
        }
    }
}