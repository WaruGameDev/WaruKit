// WaruKit — FormatUtils
// Utilidades de formateo reutilizables (patron de Upgrade.cs del Gatito Clicker).
// Numeros grandes con sufijos K/M/B.
using UnityEngine;

public static class FormatUtils
{
    /// <summary>Formatea un numero largo con sufijos: 999 -> "999", 1500 -> "1.5K", 2M, 3.2B.</summary>
    public static string FormatLong(long c)
    {
        if (c >= 1_000_000_000) return (c / 1_000_000_000f).ToString("0.#") + "B";
        if (c >= 1_000_000) return (c / 1_000_000f).ToString("0.#") + "M";
        if (c >= 1_000) return (c / 1_000f).ToString("0.#") + "K";
        return c.ToString();
    }

    /// <summary>Formatea un float con 1 decimal (velocidades, distancias).</summary>
    public static string F1(float value) => value.ToString("F1");
}
