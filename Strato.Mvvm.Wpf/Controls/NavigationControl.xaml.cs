// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationControl.xaml.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Controls
{
    using System;
    using System.Windows.Controls;

    using Strato.Mvvm.Navigation;

    /// <summary>
    ///     Interaction logic for NavigationControl.xaml.
    ///     The <see cref="UserControl"/> capable of navigating between different <see cref="IView"/>s.
    /// </summary>
    public partial class NavigationControl : UserControl, INavigationControl
    {
        /// <summary>
        ///     Gets the <see cref="INavigationContext"/> to use for managing navigation.
        /// </summary>
        public INavigationContext NavigationContext { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationControl"/> class.
        /// </summary>
        public NavigationControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationControl"/> class.
        /// </summary>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/> to use for managing navigation.
        /// </param>
        public NavigationControl(INavigationContext navigationContext)
        {
            if (navigationContext == null) throw new ArgumentNullException(nameof(navigationContext));
            UseNavigationContext(navigationContext);
        }

        /// <summary>
        ///     Sets the <see cref="NavigationContext"/> to the given <paramref name="navigationContext"/>.
        /// </summary>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/> to use.
        /// </param>
        public void UseNavigationContext(INavigationContext navigationContext)
        {
            NavigationContext = navigationContext;
            NavigationContext.OnNavigationRequestedAction = OnNavigationRequested;
        }

        /// <summary>
        ///     The method raised when the <see cref="NavigationContext"/> has requested for navigation to be
        ///     conducted.
        /// </summary>
        /// <param name="view">
        ///     The <see cref="IView"/> to navigate to.
        /// </param>
        public void OnNavigationRequested(IView view)
        {
            NavigationFrame.Navigate(view);
        }
    }
}
