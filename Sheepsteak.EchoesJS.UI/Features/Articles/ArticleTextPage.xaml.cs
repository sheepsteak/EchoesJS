using Microsoft.Phone.Controls;
using System.Windows.Media;

namespace Sheepsteak.EchoesJS.UI.Features.Articles
{
    public partial class ArticleTextPage : PhoneApplicationPage
    {
        public ArticleTextPage()
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
