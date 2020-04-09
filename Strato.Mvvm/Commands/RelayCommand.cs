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
        ///     The <see cref="Action{TParam}"/> to execute.
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        ///     The <see cref="Func{TParam, TResult}"/> to determine whether or not the <see cref="_execute"/> can be
        ///     executed.
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        ///     The event raised when the return value of the <see cref="CanExecute"/> method has changed.
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
            if (execute == null) throw new ArgumentNullException(nameof(execute), "The action to execute cannot be null.");

            // Wrap the expressions
            _execute = o => execute.Invoke();
            _canExecute = o => canExecute == null || canExecute.Invoke();
        }

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
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), "The action to execute cannot be null.");
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Determines whether the current <see cref="RelayCommand"/> can be executed in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this parameter can be
        ///     omitted.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the current <see cref="RelayCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        ///     Executes the current <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this parameter can be
        ///     omitted.
        /// </param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter)) _execute(parameter);
        }

        /// <summary>
        ///     Determines whether the current <see cref="RelayCommand"/> can be executed in its current state.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the current <see cref="RelayCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute() => CanExecute(null);

        /// <summary>
        ///     Executes the current <see cref="RelayCommand"/>.
        /// </summary>
        public void Execute() => Execute(null);
    }
}
