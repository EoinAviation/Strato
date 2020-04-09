// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockViewModel.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm.Mocks
{
    using System;
    using System.Windows.Input;

    using Strato.Mvvm;
    using Strato.Mvvm.Commands;

    /// <summary>
    ///     The mocked <see cref="ViewModel"/>.
    /// </summary>
    public class MockViewModel : ViewModel
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/>.
        /// </summary>
        public Guid Guid
        {
            get => Get<Guid>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets an <see cref="int"/>.
        /// </summary>
        public int Integer
        {
            get => Get<int>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets a <see cref="TestClass"/>.
        /// </summary>
        public TestClass Class
        {
            get => Get<TestClass>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the invalid property.
        /// </summary>
        public string InvalidProperty
        {
            get => Get<string>(null);
            set => Set(value, string.Empty);
        }

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> for incrementing the <see cref="Integer"/> property.
        /// </summary>
        public RelayCommand IncrementIntegerCommand => Get(new RelayCommand(IncrementInteger));

        /// <summary>
        ///     Increments the <see cref="Integer"/> property.
        /// </summary>
        public void IncrementInteger() => Integer++;
    }
}
