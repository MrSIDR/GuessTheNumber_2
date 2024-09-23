using System;
using System.Collections.Generic;
using UnityEngine;

namespace GuessTheNumber.Common.Events
{
    public class EventManager : IEventManager
    {
        private readonly Dictionary<Type, Dictionary<object, Action>> m_Events = new();
        private readonly Dictionary<Type, Dictionary<object, object>> m_DataEvents = new();
        
        public void Trigger<TSignal>()
        {
            var type = typeof(TSignal);
            if (!m_Events.TryGetValue(type, out var listeners))
            {
                return;
            }

            if (listeners.Count == 0)
            {
                return;
            }

            foreach (var listener in listeners)
            {
                if (listener.Key == null)
                {
                    continue;
                }
                
                listener.Value?.Invoke();
            }
        }

        public void Trigger<TSignal, TData>(TData data)
        {
            var type = typeof(TSignal);
            if (!m_DataEvents.TryGetValue(type, out var listeners))
            {
                return;
            }

            if (listeners.Count == 0)
            {
                return;
            }

            foreach (var listener in listeners)
            {
                if (listener.Key == null)
                {
                    continue;
                }

                var wrapper = listener.Value as ActionWrapper<TData>;
                wrapper?.Invoke(data);
            }
        }

        public void Subscribe<TSignal>(object listener, Action action)
        {
            if (listener == null || action == null)
            {
                return;
            }

            var type = typeof(TSignal);
            if (!m_Events.ContainsKey(type))
            {
                m_Events[type] = new Dictionary<object, Action>();
            }

            if (m_Events[type].ContainsKey(listener))
            {
                Debug.LogError($"object {listener} with signal {type} already exist!");
                return;
            }
            
            m_Events[type].Add(listener, action);
        }

        public void Subscribe<TSignal, TData>(object listener, Action<TData> action)
        {
            if (listener == null || action == null)
            {
                return;
            }

            var type = typeof(TSignal);
            if (!m_DataEvents.ContainsKey(type))
            {
                m_DataEvents[type] = new Dictionary<object, object>();
            }

            if (m_DataEvents[type].ContainsKey(listener))
            {
                Debug.LogError($"object {listener} with signal {type} already exist!");
                return;
            }

            var wrapper = new ActionWrapper<TData>(listener, action);
            m_DataEvents[type].Add(listener, wrapper);
        }

        public void Unsubscribe<TSignal>(object listener)
        {
            var type = typeof(TSignal);
            if (m_DataEvents.ContainsKey(type))
            {
                m_DataEvents[type].Remove(listener);
            }
            
            if (m_Events.ContainsKey(type))
            {
                m_Events[type].Remove(listener);
            }
        }
    }
}