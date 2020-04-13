// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Extensions
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     The <see cref="Task"/> extension methods.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Fires and forgets an async <see cref="Task"/>.
        /// </summary>
        /// <param name="task">
        ///     The <see cref="Task"/>.
        /// </param>
        /// <param name="onException">
        ///     The <see cref="Action{T}"/> to handle <see cref="Exception"/>s.
        /// </param>
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, Action<Exception> onException = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
            }
        }
    }
}
