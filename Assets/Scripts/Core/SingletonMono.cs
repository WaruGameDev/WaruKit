// WaruKit — SingletonMono
// Base singleton para managers en escena (GameManager, StoryManager, etc).
// Uso: public class GameManager : SingletonMono<GameManager> { }
// Acceso global con null check: if (GameManager.Instance != null) ...
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Singleton duplicado de {typeof(T).Name} detectado, destruyendo el nuevo.");
            Destroy(gameObject);
            return;
        }
        Instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
