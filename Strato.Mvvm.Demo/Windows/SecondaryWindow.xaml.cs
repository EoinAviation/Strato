// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondaryWindow.xaml.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.Windows
{
    using System;
    using System.Windows;

    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Demo.ViewModels;
    using Strato.Mvvm.Demo.Views;
    using Strato.Mvvm.Navigation;
    using Strato.Mvvm.Navigation.Events;

    /// <summary>
    ///     Interaction logic for SecondaryWindow.xaml.
    /// </summary>
    public partial class SecondaryWindow : Window
    {
        /// <summary>
        ///     Gets the <see cref="IEventAggregator"/>.
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        ///     Gets the <see cref="INavigationContext"/>.
        /// </summary>
        public INavigationContext NavigationContext { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SecondaryWindow"/> class.
        /// </summary>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/>.
        /// </param>
        /// <param name="eventAggregator">
        ///     The <see cref="IEventAggregator"/>.
        /// </param>
        public SecondaryWindow(
            INavigationContext navigationContext,
            IEventAggregator eventAggregator)
        {
            InitializeComponent();

            // Setup the navigation context
            NavigationContext = navigationContext ?? throw new ArgumentNullException(nameof(navigationContext));
            NavigationContext.Register<SecondaryView, SecondaryViewModel>();
            NavigationControl.UseNavigationContext(NavigationContext);

            // Setup the Event Aggregator
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            EventAggregator.Subscribe<CloseEvent>(HandleCloseEvent);
        }

        /// <summary>
        ///     Method called when the current <see cref="Window"/> has loaded.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The <see cref="RoutedEventArgs"/>.
        /// </param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigationContext.NavigateTo<SecondaryViewModel>();
        }

        /// <summary>
        ///     Handles the <see cref="CloseEvent"/>.
        /// </summary>
        /// <param name="closeEvent">
        ///     The <see cref="CloseEvent"/> to handle.
        /// </param>
        private void HandleCloseEvent(CloseEvent closeEvent)
        {
            if (closeEvent.ViewModelType == null ||
                closeEvent.ViewModelType == NavigationContext.CurrentViewModel.GetType())
            {
                Close();
            }
        }
    }
}
