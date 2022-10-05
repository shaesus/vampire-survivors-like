using UnityEngine;
using UnityEngine.Events;

public static class GlobalEventManager
{
    public static UnityEvent OnGameStageChanged = new UnityEvent();

    public static void SendOnGameStageChanged()
    {
        OnGameStageChanged.Invoke();
    }
}
