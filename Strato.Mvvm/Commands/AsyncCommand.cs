// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncCommand.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Commands
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     A command whose sole purpose is to relay its functionality to other objects by invoking delegates
    ///     asynchronously.
    /// </summary>
    public class AsyncCommand : IExtendedCommand
    {
        /// <summary>
        ///     The <see cref="Func{TResult}"/> to execute.
        /// </summary>
        private readonly Func<Task> _execute;

        /// <summary>
        ///     The <see cref="Func{TResult}"/> to determine whether or not the <see cref="_execute"/> can be executed.
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        ///     A value indicating whether or not the current <see cref="AsyncCommand"/> is executing.
        /// </summary>
        private bool _isExecuting;

        /// <summary>
        ///     Gets a value indicating whether or not the current <see cref="AsyncCommand"/> is executing.
        /// </summary>
        public bool IsExecuting
        {
            get => _isExecuting;
            private set
            {
                _isExecuting = value;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether or not the current <see cref="AsyncCommand"/> can be executed whilst it
        ///     is already running.
        /// </summary>
        public bool CanExecuteConcurrently { get; }

        /// <summary>
        ///     The event raised when the return value of the <see cref="CanExecute()"/> method has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// </summary>
        /// <param name="execute">
        ///     The <see cref="Func{TResult}"/> to execute.
        /// </param>
        /// <param name="canExecute">
        ///     The <see cref="Func{TResult}"/> to determine whether or not the <paramref name="execute"/> can be
        ///     executed.
        /// </param>
        /// <param name="canExecuteConcurrently">
        ///     If set to <c>true</c>, then the <paramref name="execute"/> can still be executed whilst it is already
        ///     executing. Otherwise, the <paramref name="execute"/> can not be executed whilst it is already running.
        /// </param>
        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null, bool canExecuteConcurrently = false)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), "The function to execute cannot be null.");
            _canExecute = canExecute;
            IsExecuting = false;
            CanExecuteConcurrently = canExecuteConcurrently;
        }

        /// <summary>
        ///     Determines whether the current <see cref="AsyncCommand"/> can be executed in its current state.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the current <see cref="AsyncCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute()
        {
            // Can't run if we're already running and not allowed to run concurrently
            if (!CanExecuteConcurrently && IsExecuting) return false;

            return _canExecute?.Invoke() ?? true;
        }

        /// <summary>
        ///     Executes the current <see cref="AsyncCommand"/> as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    IsExecuting = true;
                    await _execute();
                }
                finally
                {
                    IsExecuting = false;
                }
            }
        }

        /// <summary>
        ///     Executes the current <see cref="AsyncCommand"/>.
        /// </summary>
        public void Execute() => Task.Run(ExecuteAsync);

        /// <summary>
        ///     Determines whether the current <see cref="AsyncCommand"/> can be executed in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Unused.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the current <see cref="AsyncCommand"/> can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(object parameter) => CanExecute();

        /// <summary>
        ///     Executes the current <see cref="AsyncCommand"/>.
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
