// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Commands
{
    using System;

    /// <summary>
    ///     A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    /// </summary>
    public class RelayCommand : IExtendedCommand
    {
        /// <summary>
        ///     The <see cref="Action"/> to execute.
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        ///     The <see cref="Func{TResult}"/> to determine whether or not the <see cref="_execute"/> can be executed.
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        ///     The event raised when the return value of the <see cref="CanExecute()"/> method has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">
        ///     The <see cref="Action"/> to execute.
        /// </param>
        /// <param name="canExecute">
        ///     The <see cref="Func{TResult}"/> to determine whether or not the <paramref name="execute"/> can be
        ///     executed.
        /// </param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), "The action to execute cannot be null.");
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Determines whether the current <see cref="RelayCommand"/> can be executed in its current state.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the current <see cref="RelayCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute() => _canExecute?.Invoke() ?? true;

        /// <summary>
        ///     Executes the current <see cref="RelayCommand"/>.
        /// </summary>
        public void Execute()
        {
            if (CanExecute()) _execute();
        }

        /// <summary>
        ///     Determines whether the current <see cref="RelayCommand"/> can be executed in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Unused.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the current <see cref="RelayCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(object parameter) => CanExecute();

        /// <summary>
        ///     Executes the current <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="parameter">
        ///     Unused.
        /// </param>
        public void Execute(object parameter) => Execute();

        /// <summary>
        ///     Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
