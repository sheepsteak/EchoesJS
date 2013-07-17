using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sheepsteak.Echoes.Core;
using Sheepsteak.Echoes.UI.Features.Articles;
using Sheepsteak.Echoes.UI.Features.Main;
using Sheepsteak.Echoes.UI.Features.Settings;
using Sheepsteak.Echoes.UI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace Sheepsteak.Echoes.UI
{
    public class AppBootstrapper : PhoneBootstrapperBase
    {
        private PhoneContainer container;

        public AppBootstrapper()
        {
            Start();
        }

        protected override void Configure()
        {
            this.container = new PhoneContainer();
            if (!Execute.InDesignMode)
            {
                this.container.RegisterPhoneServices(this.RootFrame);
            }

            this.container.PerRequest<ArticleTextPageViewModel>();
            this.container.PerRequest<ArticleWebPageViewModel>();
            this.container.PerRequest<CommentsPageViewModel>();
            this.container.PerRequest<MainPageViewModel>();
            this.container.PerRequest<TopViewModel>();
            this.container.PerRequest<LatestViewModel>();
            this.container.PerRequest<SettingsPageViewModel>();
            this.container.Singleton<ICacheService, CacheService>();
            this.container.Singleton<EchoJsClient>();

            AddCustomConventions();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = this.container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }

        private static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Sheepsteak.Echoes.UI.Controls.LongListSelector>(Sheepsteak.Echoes.UI.Controls.LongListSelector.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (!ConventionManager.SetBindingWithoutBindingOrValueOverwrite(
                        viewModelType, path, property, element, convention, Sheepsteak.Echoes.UI.Controls.LongListSelector.ItemsSourceProperty))
                    {
                        return false;
                    }

                    ApplyLongListSelectorItemTemplate((Sheepsteak.Echoes.UI.Controls.LongListSelector)element, property);
                    ConventionManager
                        .ConfigureSelectedItem(element, Sheepsteak.Echoes.UI.Controls.LongListSelector.SelectedItemProperty, viewModelType, path);

                    return true;
                    //if (ConventionManager
                    //    .GetElementConvention(typeof(ItemsControl))
                    //    .ApplyBinding(viewModelType, path, property, element, convention))
                    //{
                    //    ConventionManager
                    //        .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);

                    //    return true;
                    //}
                };

            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<ProgressIndicator>(ProgressIndicator.IsVisibleProperty, "ValueProperty", null);
            ConventionManager.AddElementConvention<WebBrowser>(WebBrowser.SourceProperty, "Source", "LoadCompleted");

            ConventionManager.AddElementConvention<BindableAppBarButton>(Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(Control.IsEnabledProperty, "DataContext", "Click");

            MessageBinder.SpecialValues.Add("$selecteditem", c =>
            {
                if (c == null || c.EventArgs == null)
                    return null;

                var selectedItemEventArgs = (SelectionChangedEventArgs)c.EventArgs;

                return selectedItemEventArgs.AddedItems.Cast<object>().FirstOrDefault();
            });
        }

        /// <summary>
        /// Attempts to apply the default item template to the items control.
        /// </summary>
        /// <param name="itemsControl">The items control.</param>
        /// <param name="property">The collection property.</param>
        private static void ApplyLongListSelectorItemTemplate(Sheepsteak.Echoes.UI.Controls.LongListSelector longlistSelector, PropertyInfo property)
        {
            if (longlistSelector.ItemTemplate != null)
            {
                return;
            }

            longlistSelector.ItemTemplate = ConventionManager.DefaultItemTemplate;
        }
    }
}