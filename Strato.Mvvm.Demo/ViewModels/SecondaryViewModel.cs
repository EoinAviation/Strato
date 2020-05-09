// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondaryViewModel.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.ViewModels
{
    using Strato.EventAggregator;
    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Commands;
    using Strato.Mvvm.Demo.Events;
    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The secondary <see cref="ViewModel"/>.
    /// </summary>
    public class SecondaryViewModel : ViewModel
    {
        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> that demonstrates the <see cref="EventAggregator"/>.
        /// </summary>
        public RelayCommand CloseCommand => Get(new RelayCommand(Close));

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> that demonstrates the <see cref="EventAggregator"/>.
        /// </summary>
        public RelayCommand CloseAllCommand => Get(new RelayCommand(CloseAll));

        /// <summary>
        ///     Initializes a new instance of the <see cref="SecondaryViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">
        ///     The <see cref="IEventAggregator"/>.
        /// </param>
        public SecondaryViewModel(IEventAggregator eventAggregator)
            : base(eventAggregator)
        {
        }

        /// <summary>
        ///     Closes the current <see cref="ViewModel"/> and any associated Views.
        /// </summary>
        public void Close()
        {
            EventAggregator.Publish(new CloseEvent(this));
        }

        /// <summary>
        ///     Closes all <see cref="ViewModel"/>s and Views.
        /// </summary>
        public void CloseAll()
        {
            EventAggregator.Publish(new CloseEvent());
        }
    }
}
