// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Strato.EventAggregator;
    using Strato.Mvvm.Commands;
    using Strato.Mvvm.Demo.Events;
    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The Main Windows <see cref="ViewModel"/>.
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        ///     Gets or sets the input text.
        /// </summary>
        public string InputText
        {
            get => Get<string>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="ObservableCollection{T}"/> of names.
        /// </summary>
        public ObservableCollection<string> Names
        {
            get => Get<ObservableCollection<string>>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the selected name.
        /// </summary>
        public string SelectedName
        {
            get => Get<string>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="ObservableCollection{T}"/> used for demonstrating
        ///     <see cref="AsyncCommand"/>s.
        /// </summary>
        public ObservableCollection<string> AsyncDemoList
        {
            get => Get<ObservableCollection<string>>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="Progress{T}"/> used for demonstrating <see cref="AsyncCommand"/>s.
        /// </summary>
        public Progress<int> AsyncDemoProgress
        {
            get => Get<Progress<int>>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="AsyncDemoProgress"/>.
        /// </summary>
        public int AsyncDemoProgressValue
        {
            get => Get<int>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> for adding names to the <see cref="Names"/>.
        /// </summary>
        public RelayCommand AddNameCommand => Get(new RelayCommand(AddName));

        /// <summary>
        ///     Gets the <see cref="AsyncCommand"/> that demonstrates how <see cref="AsyncCommand"/>s work.
        /// </summary>
        public AsyncCommand DoStuffAsyncCommand => Get(new AsyncCommand(DoStuffAsync));

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> that demonstrates how <see cref="AsyncCommand"/>s work when
        ///     executing synchronously.
        /// </summary>
        public RelayCommand DoStuffSyncCommand => Get(new RelayCommand(() => DoStuffAsyncCommand.Execute(), DoStuffAsyncCommand.CanExecute));

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> that demonstrates the <see cref="EventAggregator"/>.
        /// </summary>
        public RelayCommand CloseCommand => Get(new RelayCommand(Close));

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            Names = new ObservableCollection<string>();
            AsyncDemoList = new ObservableCollection<string>();
        }

        /// <summary>
        ///     Adds a name to the <see cref="Names"/>.
        /// </summary>
        public void AddName()
        {
            Names.Add(InputText);
            InputText = string.Empty;
        }

        /// <summary>
        ///     Demonstrates how <see cref="AsyncCommand"/>s work.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task DoStuffAsync()
        {
            AsyncDemoProgress = new Progress<int>(OnProgressChanged);

            for (int i = 1; i <= 10; i++)
            {
                AsyncDemoList.Add($"Number {i}.");
                ((IProgress<int>)AsyncDemoProgress).Report(i);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            AsyncDemoProgress = null;
            AsyncDemoProgressValue = 0;
            AsyncDemoList.Clear();
        }

        /// <summary>
        ///     Closes the current <see cref="ViewModel"/> and any associated Views.
        /// </summary>
        public void Close()
        {
            EventAggregator.Singleton.Publish(new CloseEvent());
        }

        /// <summary>
        ///     Method called when the progress of <see cref="DoStuffAsync"/> has changed.
        /// </summary>
        /// <param name="newValue">
        ///     The new value.
        /// </param>
        private void OnProgressChanged(int newValue)
        {
            AsyncDemoProgressValue = newValue;
        }
    }
}
