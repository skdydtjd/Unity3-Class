using System;
using System.Collections.Generic;
using DefinesEnum;

public class EventManager
{
    // 이벤트 이름(Key)과 연결된 함수 리스트(Value)
    private Dictionary<EEventType, Action<object>> _events = new Dictionary<EEventType, Action<object>>();

    public void Init()
    {
        Clear();
    }

    // 1. 이벤트 구독 (Listen)
    public void Subscribe(EEventType eventName, Action<object> action)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName] += action;
        }
        else
        {
            _events.Add(eventName, action);
        }
    }

    // 2. 이벤트 구독 해제 (Unsubscribe) - 메모리 누수 방지용
    public void Unsubscribe(EEventType eventName, Action<object> action)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName] -= action;
        }
    }

    // 3. 이벤트 발생 (Publish / Trigger)
    public void TriggerEvent(EEventType eventName, object args = null)
    {
        if (_events.TryGetValue(eventName, out Action<object> action))
        {
            action.Invoke(args);
        }
    }

    public void Clear()
    {
        _events.Clear();
    }
}
