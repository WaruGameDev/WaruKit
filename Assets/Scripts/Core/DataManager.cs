// WaruKit — DataManager
// Guarda data entre escenas SIN GameObject (clase estática).
// Regla de Waru: los managers estáticos solo guardan data; los MonoBehaviour la leen en Initialize().
// PlayerPrefs se toca SOLO al final del flujo (Save()/Load() explícitos), nunca en Update.
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    // --- Data simple ---
    public static int score;
    public static int lives = 3;

    // --- Data compleja en diccionarios ---
    public static Dictionary<string, int> questProgress = new Dictionary<string, int>();
    public static Dictionary<string, bool> flags = new Dictionary<string, bool>();

    // --- Listas entre escenas (tipos del proyecto; UnitData es ejemplo de AutoBattler) ---
    // public static List<UnitData> selectedUnits = new List<UnitData>();
    public static List<object> crossSceneData = new List<object>();

    /// <summary>Resetea TODO el estado (pa' new game o reinicio).</summary>
    public static void ResetAll()
    {
        score = 0;
        lives = 3;
        questProgress.Clear();
        flags.Clear();
        selectedUnits.Clear();
    }

    // --- Persistencia (SIEMPRE al final del flujo) ---
    public static void Save()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lives", lives);

        // Diccionarios se serializan como JSON (aceptable segun patron de Waru)
        string questJson = JsonUtility.ToJson(new QuestProgressSave(questProgress));
        PlayerPrefs.SetString("questProgress", questJson);
        PlayerPrefs.Save(); // al final, todo junto
    }

    public static void Load()
    {
        score = PlayerPrefs.GetInt("score", 0);
        lives = PlayerPrefs.GetInt("lives", 3);

        if (PlayerPrefs.HasKey("questProgress"))
        {
            var save = JsonUtility.FromJson<QuestProgressSave>(PlayerPrefs.GetString("questProgress"));
            questProgress = save.ToDict();
        }
    }

    // Helper pa' serializar diccionario con JsonUtility (no soporta Dictionary directo)
    [System.Serializable]
    private class QuestProgressSave
    {
        public List<string> keys = new List<string>();
        public List<int> values = new List<int>();

        public QuestProgressSave() { }
        public QuestProgressSave(Dictionary<string, int> dict)
        {
            foreach (var kv in dict) { keys.Add(kv.Key); values.Add(kv.Value); }
        }
        public Dictionary<string, int> ToDict()
        {
            var d = new Dictionary<string, int>();
            for (int i = 0; i < keys.Count && i < values.Count; i++) d[keys[i]] = values[i];
            return d;
        }
    }
}
