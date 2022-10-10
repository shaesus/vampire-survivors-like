using UnityEngine;
using UnityEngine.Events;

public static class GlobalEventManager
{
    public static UnityEvent OnGameStageChanged = new UnityEvent();
    public static UnityEvent OnScoreChanged = new UnityEvent();

    public static void SendOnGameStageChanged()
    {
        OnGameStageChanged.Invoke();
    }

    public static void SendOnScoreChanged()
    {
        OnScoreChanged.Invoke();
    }
}
