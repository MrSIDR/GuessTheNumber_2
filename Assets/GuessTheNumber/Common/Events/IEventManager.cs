using System;

namespace GuessTheNumber.Common.Events
{
    public interface IEventManager
    {
        void Trigger<TSignal>();
        void Trigger<TSignal, TData>(TData data);
        
        void Subscribe<TSignal>(object listener, Action action);
        void Subscribe<TSignal, TData>(object listener, Action<TData> action);
        
        void Unsubscribe<TSignal>(object listener);
    }
}