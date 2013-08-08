using Microsoft.Phone.Controls;
using System;
using System.Windows;

namespace Sheepsteak.EchoesJS.UI
{
    public partial class App : Application
    {
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Standard XAML initialization
            InitializeComponent();

            ThemeManager.OverrideOptions = ThemeManagerOverrideOptions.SystemTrayColors;
            ThemeManager.SetCustomTheme(this.Resources.MergedDictionaries[0], Theme.Light);
        }
    }
}