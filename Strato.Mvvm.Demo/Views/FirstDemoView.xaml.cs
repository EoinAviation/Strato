// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirstDemoView.xaml.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.Views
{
    using System;
    using System.Windows;

    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Demo.ViewModels;
    using Strato.Mvvm.Demo.Windows;
    using Strato.Mvvm.Wpf.Controls;
    using Strato.Mvvm.Wpf.Events;

    /// <summary>
    ///     Interaction logic for FirstDemoView.xaml.
    /// </summary>
    public partial class FirstDemoView : View
    {
        /// <summary>
        ///     Gets the <see cref="IEventAggregator"/>.
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FirstDemoView"/> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The <see cref="FirstDemoViewModel"/>.
        /// </param>
        /// <param name="eventAggregator">
        ///     The <see cref="IEventAggregator"/>.
        /// </param>
        public FirstDemoView(FirstDemoViewModel viewModel, IEventAggregator eventAggregator)
            : base(viewModel)
        {
            InitializeComponent();
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        /// <summary>
        ///     Opens a new <see cref="SecondaryWindow"/>.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The <see cref="RoutedEventArgs"/>.
        /// </param>
        public void OpenWindow(object sender, RoutedEventArgs e)
        {
            EventAggregator.Publish(OpenWindowEvent.Create<SecondaryWindow>());
        }

        /// <summary>
        ///     Opens a new <see cref="SecondaryWindow"/> as a dialog.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The <see cref="RoutedEventArgs"/>.
        /// </param>
        public void OpenWindowAsDialog(object sender, RoutedEventArgs e)
        {
            EventAggregator.Publish(OpenWindowEvent.Create<SecondaryWindow>(true));
        }
    }
}
