# WaruKit 🔧🐱

Código Unity reutilizable con los patrones de Waru (WaruGameDev). Modular, con comentarios que explican el *porqué*, y listo pa' copiar a cualquier proyecto.

## Qué incluye

| Archivo | Qué hace |
|---|---|
| `Core/SingletonMono.cs` | Base singleton pa' managers (`GameManager.Instance`) |
| `Core/DataManager.cs` | Clase estática FUERA de escena pa' data entre escenas, con diccionarios y PlayerPrefs al final |
| `Core/Interfaces.cs` | `IDamageable`, `IHealable`, `Interactuable`, `ICollectable` — archivo dedicado |
| `Core/EventManager.cs` | Bus de eventos con delegados pa' desacoplar sistemas |
| `Core/InputHandler.cs` | Input centralizado con flag `canUseInput` (bloqueable en cutscenes) |
| `Core/Health.cs` | Vida reutilizable con eventos `OnDamaged`/`OnHealed`/`OnDied` |
| `Core/FormatUtils.cs` | Formato de números K/M/B y F1 |

## Reglas de los patrones de Waru

1. **Singleton**: `Instance` (proyectos personales) o `instance` (clases) — mantener la convención del proyecto actual.
2. **DataManager**: solo data, nada de lógica de MonoBehaviour. Diccionarios pa' data compleja.
3. **PlayerPrefs SIEMPRE al final**: memoria primero, persistir con `Save()` explícito al cerrar/guardar. Nunca en `Update()`.
4. **Eventos/delegados**: `Action` + `?.Invoke()` pa' desacoplar. Sistemas se suscriben, no se acoplan.
5. **ScriptableObject** pa' data de diseño con `[CreateAssetMenu]` (ver `UnitData` de AutoBattlerUGM).
6. **DOTween** pa' animaciones juicy: `Sequence` + `AppendCallback`/`AppendInterval`.
7. **`RemoveAll(u => u == null)`** antes de indexar listas de GameObjects.
8. **Comentarios con chilenismos** y que explican el porqué, no el qué.

## Cómo usar

- Copia `Assets/Scripts/Core/` a tu proyecto.
- Hereda `SingletonMono<T>` pa' tus managers.
- Usa `DataManager` pa' pasar data entre escenas.
- Suscríbete a eventos con `EventManager.Subscribe(GameEvents.ScoreChanged, OnScore)`.
- `Health` implementa `IDamageable` altiro.

## Verificación

- Probado en Unity 6000.3.x headless (ver skill `unity-headless-dev`).
- Cada script compila standalone (sin dependencias externas excepto DOTween en proyectos que lo usen).
