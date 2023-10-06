
// Created by lordofduct - https://forum.unity.com/threads/recommended-event-system.856294/#post-5644981
//
// Usage example:
//
// public delegate void OnDiedEvent(GameObject go);
//
// Events<OnDiedEvent>.Trigger?.Invoke(gameObject);
//
// Events<OnDiedEvent>.Register(go => {
//     Debug.Log($"{go.name} died.");
// });

using System;

public sealed class Events<T> where T : System.Delegate
{
    private static Lazy<Events<T>> instance 
        = new Lazy<Events<T>>(() => new Events<T>());

    public static void Reset() { instance = new Lazy<Events<T>>(() => new Events<T>()); }
    public static Events<T> Instance { get { return instance.Value; } }
    public void Register(T callback) => Trigger = System.Delegate.Combine(Trigger, callback) as T;
    public void Unregister(T callback) => Trigger = System.Delegate.Remove(Trigger, callback) as T;
    public T Trigger { get; private set; }
}