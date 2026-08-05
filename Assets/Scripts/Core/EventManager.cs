// WaruKit — EventManager
// Bus de eventos simple con delegados (patron de Waru: desacoplar sistemas).
// Emisor: EventManager.Raise(GameEvents.ScoreChanged, newScore);
// Receptor: EventManager.Subscribe(GameEvents.ScoreChanged, OnScoreChanged);
//           void OnScoreChanged(object data) { ... }
// Usa ?.Invoke (null-conditional) pa' no reventar sin suscriptores.
using System;
using System.Collections.Generic;

public static class EventManager
{
    private static readonly Dictionary<string, Action<object>> events = new Dictionary<string, Action<object>>();

    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (!events.ContainsKey(eventName)) events[eventName] = null;
        events[eventName] += listener;
    }

    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (events.ContainsKey(eventName)) events[eventName] -= listener;
    }

    public static void Raise(string eventName, object data = null)
    {
        if (events.ContainsKey(eventName))
            events[eventName]?.Invoke(data);
    }

    public static void ClearAll()
    {
        events.Clear();
    }
}

// Nombres de eventos centralizados pa' no escribir strings magicos
public static class GameEvents
{
    public const string ScoreChanged = "SCORE_CHANGED";
    public const string LivesChanged = "LIVES_CHANGED";
    public const string PlayerDied = "PLAYER_DIED";
    public const string GameOver = "GAME_OVER";
    public const string EnemyDefeated = "ENEMY_DEFEATED";
}
