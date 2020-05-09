// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondDemoViewModel.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.ViewModels
{
    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Commands;
    using Strato.Mvvm.Navigation;
    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The second demonstration <see cref="ViewModel"/>.
    /// </summary>
    public class SecondDemoViewModel : ViewModel
    {
        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> demonstrating the <see cref="INavigationContext"/>.
        /// </summary>
        public RelayCommand GoBackCommand => Get(new RelayCommand(GoBack));

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondDemoViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">
        ///     The <see cref="IEventAggregator"/>.
        /// </param>
        /// <param name="navigationContext">
        ///     the <see cref="INavigationContext"/>.
        /// </param>
        public SecondDemoViewModel(IEventAggregator eventAggregator, INavigationContext navigationContext)
            : base(eventAggregator, navigationContext)
        {
        }

        /// <summary>
        ///     Navigates back to the <see cref="FirstDemoViewModel"/>.
        /// </summary>
        public void GoBack()
        {
            NavigationContext.NavigateTo<FirstDemoViewModel>();
        }
    }
}
