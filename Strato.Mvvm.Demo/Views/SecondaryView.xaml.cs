// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondaryView.xaml.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.Views
{
    using Strato.Mvvm.Demo.ViewModels;
    using Strato.Mvvm.Wpf.Controls;

    /// <summary>
    ///     Interaction logic for SecondaryView.xaml.
    /// </summary>
    public partial class SecondaryView : View
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SecondaryView"/> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The <see cref="SecondaryViewModel"/>.
        /// </param>
        public SecondaryView(SecondaryViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
