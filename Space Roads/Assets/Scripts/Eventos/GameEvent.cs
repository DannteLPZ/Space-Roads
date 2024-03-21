using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName ="Scriptable Objects/Event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> Listeners = new();

    public void Register(GameEventListener listener)
    {
        if(Listeners.Contains(listener) == false)
            Listeners.Add(listener);
    }

    public void Deregister(GameEventListener listener)
    {
        if (Listeners.Contains(listener) == true)
            Listeners.Remove(listener);
    }

    [ContextMenu("Invoke")]
    public void Invoke()
    {
        foreach(GameEventListener listener in Listeners)
            listener.Rise();
    }
}
