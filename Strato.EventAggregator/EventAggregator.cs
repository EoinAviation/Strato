// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventAggregator.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.EventAggregator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Strato.EventAggregator.Abstractions;

    /// <summary>
    ///     The default <see cref="IEventAggregator"/> implementation for working with <see cref="IEvent"/>s within the
    ///     current instance of an Application.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        ///     The <see cref="List{T}"/> of <see cref="EventHandler"/>s.
        /// </summary>
        private readonly List<EventHandler> _eventHandlers;

        /// <summary>
        ///     The <see cref="SemaphoreSlim"/> used for preventing concurrency issues with the
        ///     <see cref="_eventHandlers"/>.
        /// </summary>
        private readonly SemaphoreSlim _eventHandlersSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        ///     The <see cref="List{T}"/> of <see cref="AsyncEventHandler"/>s.
        /// </summary>
        private readonly List<AsyncEventHandler> _asyncEventHandlers;

        /// <summary>
        ///     The <see cref="SemaphoreSlim"/> used for preventing concurrency issues with the
        ///     <see cref="_asyncEventHandlers"/>.
        /// </summary>
        private readonly SemaphoreSlim _asyncEventHandlersSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventAggregator"/> class.
        /// </summary>
        public EventAggregator()
        {
            _eventHandlers = new List<EventHandler>();
            _asyncEventHandlers = new List<AsyncEventHandler>();
        }

        /// <summary>
        ///     Subscribes the given <see cref="Action{T}"/> to the all published <see cref="IEvent"/>s of type
        ///     <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="handlerAction">
        ///     The <see cref="Action{T}"/> handling the <typeparamref name="TEvent"/>.
        /// </param>
        public void Subscribe<TEvent>(Action<TEvent> handlerAction)
            where TEvent : IEvent
        {
            _eventHandlersSemaphore.Wait();
            try
            {
                // Add a new handler if one does not already exist
                if (_eventHandlers.All(h => !h.Handler.Equals(handlerAction)))
                {
                    _eventHandlers.Add(new EventHandler(typeof(TEvent), handlerAction));
                }
                else
                {
                    // Todo: Throw an exception, ignore, or allow duplicates?
                }
            }
            finally
            {
                _eventHandlersSemaphore.Release();
            }
        }

        /// <summary>
        ///     Subscribes the given <see cref="Func{T, Task}"/> to the all published <see cref="IEvent"/>s of type
        ///     <typeparamref name="TEvent"/> which were published asynchronously.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="handlerAction">
        ///     The <see cref="Func{T, Task}"/> handling the <typeparamref name="TEvent"/> as an asynchronous
        ///     operation.
        /// </param>
        public void Subscribe<TEvent>(Func<TEvent, Task> handlerAction)
            where TEvent : IEvent
        {
            _asyncEventHandlersSemaphore.Wait();
            try
            {
                // Add a new handler if one does not already exist
                if (_asyncEventHandlers.All(h => !h.Handler.Equals(handlerAction)))
                {
                    _asyncEventHandlers.Add(new AsyncEventHandler(typeof(TEvent), handlerAction));
                }
                else
                {
                    // Todo: Throw an exception, ignore, or allow duplicates?
                }
            }
            finally
            {
                _asyncEventHandlersSemaphore.Release();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether or not the given <paramref name="handlerAction"/>  is subscribed to any
        ///     <see cref="IEvent"/>s.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="handlerAction">
        ///     The <see cref="Func{T, Task}"/> which may be handling the <typeparamref name="TEvent"/> as an
        ///     asynchronous operation.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="handlerAction"/> is subscribed to any <see cref="IEvent"/>.
        /// </returns>
        public bool IsSubscribed<TEvent>(Action<TEvent> handlerAction)
            where TEvent : IEvent
        {
            return _eventHandlers.Any(h => h.Handler.Equals(handlerAction));
        }

        /// <summary>
        ///     Gets a value indicating whether or not the given <paramref name="handlerAction"/>  is subscribed to any
        ///     <see cref="IEvent"/>s.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="handlerAction">
        ///     The <see cref="Func{T, Task}"/> which may be handling the <typeparamref name="TEvent"/> as an
        ///     asynchronous operation.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="handlerAction"/> is subscribed to any <see cref="IEvent"/>.
        /// </returns>
        public bool IsSubscribed<TEvent>(Func<TEvent, Task> handlerAction)
            where TEvent : IEvent
        {
            return _asyncEventHandlers.Any(h => h.Handler.Equals(handlerAction));
        }

        /// <summary>
        ///     Publishes the <typeparamref name="TEvent"/> to for all subscribers to handle.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="event">
        ///     The <typeparamref name="TEvent"/> to publish.
        /// </param>
        public void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            _eventHandlersSemaphore.Wait();
            try
            {
                // Get all handlers for the current event
                EventHandler[] handlers = _eventHandlers.Where(h => h.EventType == typeof(TEvent)).ToArray();

                // Handle
                foreach (EventHandler handler in handlers)
                {
                    handler.Handle(@event);
                }
            }
            finally
            {
                _eventHandlersSemaphore.Release();
            }
        }

        /// <summary>
        ///     Publishes the <typeparamref name="TEvent"/> to for all subscribers to handle as an asynchronous
        ///     operation.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="event">
        ///     The <typeparamref name="TEvent"/> to publish.
        /// </param>
        /// <returns>
        ///     The <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            await _asyncEventHandlersSemaphore.WaitAsync();
            try
            {
                // Get all handlers for the current event
                AsyncEventHandler[] handlers = _asyncEventHandlers.Where(h => h.EventType == typeof(TEvent)).ToArray();

                // Wait for all tasks to complete
                if (handlers.Any())
                {
                    Task[] tasks = handlers.Select(h => h.HandleAsync(@event)).ToArray();
                    await Task.WhenAll(tasks);
                }
            }
            finally
            {
                _asyncEventHandlersSemaphore.Release();
            }
        }

        /// <summary>
        ///     Unsubscribes the given <see cref="Action{T}"/> from all published <see cref="IEvent"/>s of type
        ///     <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="handlerAction">
        ///     The <see cref="Action{T}"/> handling the <typeparamref name="TEvent"/>.
        /// </param>
        public void Unsubscribe<TEvent>(Action<TEvent> handlerAction)
            where TEvent : IEvent
        {
            _eventHandlersSemaphore.Wait();
            try
            {
                // Remove the handler if it exists
                if (_eventHandlers.Any(h => h.Handler.Equals(handlerAction)))
                {
                    _eventHandlers.RemoveAll(h => h.Handler.Equals(handlerAction));
                }
                else
                {
                    // Todo: Throw an exception or ignore?
                }
            }
            finally
            {
                _eventHandlersSemaphore.Release();
            }
        }

        /// <summary>
        ///     Unsubscribes the given <see cref="Func{T, Task}"/> from all published <see cref="IEvent"/>s of type
        ///     <typeparamref name="TEvent"/> which were published asynchronously.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/>.
        /// </typeparam>
        /// <param name="handlerAction">
        ///     The <see cref="Func{T, Task}"/> handling the <typeparamref name="TEvent"/> as an asynchronous
        ///     operation.
        /// </param>
        public void Unsubscribe<TEvent>(Func<TEvent, Task> handlerAction)
            where TEvent : IEvent
        {
            _asyncEventHandlersSemaphore.Wait();
            try
            {
                // Remove the handler if it exists
                if (_asyncEventHandlers.Any(h => h.Handler.Equals(handlerAction)))
                {
                    _asyncEventHandlers.RemoveAll(h => h.Handler.Equals(handlerAction));
                }
                else
                {
                    // Todo: Throw an exception or ignore?
                }
            }
            finally
            {
                _asyncEventHandlersSemaphore.Release();
            }
        }
    }
}
