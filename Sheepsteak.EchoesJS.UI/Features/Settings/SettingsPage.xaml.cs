using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sheepsteak.EchoesJS.UI.Features.Settings
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            //Loaded += (s, e) =>
            //{
            //    this.applicationBar.BackgroundColor = ((SolidColorBrush)App.Current.Resources["PhoneChromeBrush"]).Color;
            //    this.applicationBar.ForegroundColor = ((SolidColorBrush)App.Current.Resources["PhoneChromeForegroundBrush"]).Color;
            //};
        }
    }
}
