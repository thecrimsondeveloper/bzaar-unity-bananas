using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crimson;
using Bzaar;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    AppState currentAppState;
    public AppState CurrentAppState => currentAppState;
    public UnityEvent onAppStateChange;

    ControlState currentControlState;
    public ControlState CurrentControlState => currentControlState;
    public UnityEvent onControlStateChange;


    public static StateController instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }


    public void SetAppState(AppState newState)
    {
        AppState oldState = currentAppState;
        currentAppState = newState;
        if (oldState != currentAppState)
        {
            onAppStateChange.Invoke();
        }
    }
    public void SetControlState(ControlState newState)
    {
        ControlState oldState = currentControlState;
        currentControlState = newState;
        if (oldState != currentControlState)
        {
            onControlStateChange.Invoke();
        }
    }
    public enum AppState
    {
        Closet,
        Editor,
        Loading
    }

    public enum ControlState
    {
        None,
        Rotating,
        Scaling,
        Moving
    }
}
