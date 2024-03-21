using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEvent _unityEvent;

    private void OnEnable() => _gameEvent.Register(this);
    private void OnDisable() => _gameEvent.Deregister(this);

    public void Rise() => _unityEvent?.Invoke();
}
