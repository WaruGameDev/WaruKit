// WaruKit — DOTweenHelper
// Utilidades DOTween reutilizables (DOTween es el copiloto de Waru).
// Patrones sacados de AutoBattlerUGM (BattleManager/Unit.cs) y ZeldaLike (Chest.cs):
//   - DOJump pa' ataques/reposicionamiento
//   - DOPunchScale pa' hit feedback
//   - DOLocalRotate con Ease.OutBack pa' abrir cosas
//   - Sequence con AppendCallback / AppendInterval / Join pa' coreografias
// Requiere: paquete DOTween (Demigiant) en el proyecto.
using DG.Tweening;
using UnityEngine;

public static class DOTweenHelper
{
    /// <summary>Pop de escala jugoso (1 -> 1+amount -> 1). Uso: feedback de click/recoger.</summary>
    public static Tweener Pop(Transform target, float amount = 0.25f, float duration = 0.15f)
    {
        return target.DOPunchScale(Vector3.one * amount, duration, 2, 0.5f);
    }

    /// <summary>Sacudida de dano (hit feedback). Uso: cuando una unidad recibe golpe.</summary>
    public static Tweener ShakeHit(Transform target, float strength = 0.2f, float duration = 0.25f)
    {
        return target.DOPunchScale(new Vector3(strength, -strength, 0f), duration, 2).SetRelative(true);
    }

    /// <summary>Numero de dano flotante: aparece, salta y se destruye solo.</summary>
    public static void DamageNumber(Transform origin, string text, float jumpHeight = 0.5f, float duration = 0.25f)
    {
        // El prefab de texto es responsabilidad del caller (patron de Unit.cs)
        // Este helper asume que 'origin' YA es el TextMeshPro instanciado en posicion.
        var t = origin;
        t.DOJump(t.position + Vector3.up * jumpHeight, jumpHeight, 1, duration)
            .OnComplete(() => Object.Destroy(t.gameObject));
        var _ = text; // el caller setea t.GetComponent<TextMeshPro>().text antes de llamar
    }

    /// <summary>Ataque "clash": salta a un punto, golpea, y vuelve a su posicion original.</summary>
    public static void ClashAttack(Transform unit, Vector3 targetPos, System.Action onHit, float jumpDuration = 0.25f)
    {
        Vector3 origin = unit.position;
        unit.DOJump(targetPos, 1f, 1, jumpDuration).OnComplete(() =>
        {
            onHit?.Invoke();
            unit.DOJump(origin, 1f, 1, jumpDuration * 0.8f);
        });
    }

    /// <summary>Coreografia de secuencia: encadena callbacks e intervalos (patron BattleManager.Clash).</summary>
    public static Sequence SequenceOf(params System.Action[] callbacks)
    {
        Sequence seq = DOTween.Sequence();
        foreach (var cb in callbacks)
        {
            seq.AppendCallback(cb);
            seq.AppendInterval(0.5f);
        }
        return seq;
    }

    /// <summary>Abrir/cerrar una tapa con Ease.OutBack (patron Chest.cs).</summary>
    public static Tweener OpenLid(Transform lid, float angle = -120f, float duration = 0.5f)
    {
        return lid.DOLocalRotate(new Vector3(angle, 0f, 0f), duration).SetEase(Ease.OutBack);
    }

    /// <summary>Reposicionar unidades a sus puestos con salto (patron ReorderUnit).</summary>
    public static void JumpToPositions(Transform unit, Vector3 pos, float jumpHeight = 1f, float duration = 0.25f)
    {
        unit.DOJump(pos, jumpHeight, 1, duration);
    }
}
