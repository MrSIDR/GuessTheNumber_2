using System;

namespace GuessTheNumber.Common.Events
{
    public class ActionWrapper<T>
    {
        private readonly object m_Listener;
        private readonly Action<T> m_Action;

        public ActionWrapper(object listener, Action<T> action)
        {
            m_Listener = listener;
            m_Action = action;
        }

        public void Invoke(T data)
        {
            if (m_Listener == null)
            {
                return;   
            }

            m_Action?.Invoke(data);
        }
    }
}