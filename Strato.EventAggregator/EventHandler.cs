// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventHandler.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.EventAggregator
{
    using System;

    using Strato.EventAggregator.Abstractions;

    /// <summary>
    ///     The class containing a synchronous handler for a specific <see cref="IEvent"/>.
    /// </summary>
    internal sealed class EventHandler
    {
        /// <summary>
        ///     Gets the <see cref="Type"/> of the event being handled.
        /// </summary>
        internal Type EventType { get; }

        /// <summary>
        ///     Gets the <see cref="object"/> handling the event.
        ///     This is assumed to be an <see cref="Action{T}"/>.
        /// </summary>
        internal object Handler { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventHandler"/> class.
        /// </summary>
        /// <param name="eventType">
        ///     The <see cref="Type"/> of the event being handled.
        /// </param>
        /// <param name="handler">
        ///     The <see cref="Action{T}"/> handling the event.
        /// </param>
        internal EventHandler(Type eventType, object handler)
        {
            // Ensure the eventType is an event
            if (!typeof(IEvent).IsAssignableFrom(eventType))
            {
                throw new ArgumentException($"The {nameof(eventType)} does not implement {nameof(IEvent)}.", nameof(eventType));
            }

            // Ensure the Handler is of the correct type
            if (handler.GetType() != GetHandlerType(eventType))
            {
                throw new ArgumentException($"The {nameof(handler)} is not appropriate for handling the event type \"{eventType.Name}\".", nameof(handler));
            }

            EventType = eventType;
            Handler = handler;
        }

        /// <summary>
        ///     Handles the <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">
        ///     The type of <see cref="IEvent"/> to handle.
        /// </typeparam>
        /// <param name="event">
        ///     The <typeparamref name="TEvent"/> to handle.
        /// </param>
        internal void Handle<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            // Ensure the event type is correct
            Type eventType = typeof(TEvent);
            if (eventType != EventType)
            {
                throw new NotSupportedException($"The handler for \"{EventType.Name}\" does not support handling \"{eventType.Name}\".");
            }

            ((Action<TEvent>)Handler).Invoke(@event);
        }

        /// <summary>
        ///     Gets the expected <see cref="Type"/> of the <see cref="Handler"/> given a <see cref="Type"/> for the
        ///     parameter.
        /// </summary>
        /// <param name="eventType">
        ///     The <see cref="Type"/> of event to use as the parameter.
        /// </param>
        /// <returns>
        ///     The <see cref="Type"/> of handler.
        /// </returns>
        private Type GetHandlerType(Type eventType)
        {
            Type actionType = typeof(Action<>);
            return actionType.MakeGenericType(eventType);
        }
    }
}
