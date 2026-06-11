using System.Collections.Generic;
using UnityEngine.Events;

// 所有泛型委托类的父类，用于下方通过里氏替换实现传参
public class EventInfoBase
{

}
// 装载泛型委托
public class EventInfo<T>:EventInfoBase
{
    public UnityAction<T> action;
}
// 装载无泛型委托
public class EventInfo:EventInfoBase
{
    public UnityAction action;
}


public class EventCenter:SingletonBase<EventCenter>
{
    private EventCenter() { }

    //private Dictionary<string,UnityAction> eventDic = new Dictionary<string,UnityAction>();
    //使用UnityAction<object>解决函数参数传递问题
    //private Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();
    
    // 使用泛型传参，避免object时存在的装箱拆箱问题。
    // EventCenter不适合定义为泛型类会破坏唯一性，因此这里使用里氏替换原则包裹泛型类
    private Dictionary<E_EventType, EventInfoBase> eventDic = new Dictionary<E_EventType, EventInfoBase>();

    // 添加事件监听  使用泛型类型传参，防止需要使用触发对象
    public void AddListener<T>(E_EventType eventName, UnityAction<T> action)
    {
        if(!eventDic.ContainsKey(eventName))
            // 这里添加的时候使用null是为了统一使用+=来管理委托，便于移除
            eventDic.Add(eventName, new EventInfo<T>());
            (eventDic[eventName] as EventInfo<T>).action += action;
    }
    public void AddListener(E_EventType eventName, UnityAction action)
    {
        if(!eventDic.ContainsKey(eventName))
            // 这里添加的时候使用null是为了统一使用+=来管理委托，便于移除
            eventDic.Add(eventName, new EventInfo());
            (eventDic[eventName] as EventInfo).action += action;
    }
    // 移除事件监听
    public void RemoveListener<T>(E_EventType eventName,UnityAction<T> action)
    {
        if(eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T>).action -= action;
    }
    public void RemoveListener(E_EventType eventName,UnityAction action)
    {
        if(eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo).action -= action;
    }
    // 事件触发
    public void EventTrigger<T>(E_EventType eventName,T obj)
    {
        if(eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T>).action?.Invoke(obj);
    }
    public void EventTrigger(E_EventType eventName)
    {
        if(eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo).action?.Invoke();
    }

    // 清理监听
    public void Clear()
    {
        eventDic.Clear();
    }

    public void Clear(E_EventType eventName)
    {
        if(eventDic.ContainsKey(eventName))
            eventDic.Remove(eventName);
    }
}
