// --------------------------------------------------------------------------------------------------------------------
// <copyright file="View.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Controls
{
    using System.Windows.Controls;

    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The <see cref="ContentControl"/> representing an <see cref="IView"/>.
    /// </summary>
    public class View : ContentControl, IView
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The <see cref="ViewModel"/> to bind to the <see cref="View"/> instance.
        /// </param>
        public View(ViewModel viewModel)
            : base() =>
            DataContext = viewModel;
    }
}
