// WaruKit — Interfaces de interaccion
// Archivo dedicado pa' interfaces reutilizables (patron de ZeldaLike/InterFaces.cs).
// Modular y reutilizable: los componentes implementan la interfaz, no se acoplan entre si.
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}

public interface IHealable
{
    void Heal(float amount);
}

public interface Interactuable
{
    void Interact();
}

public interface ICollectable
{
    void Collect();
}

public interface IKnockbackable
{
    void Knockback(Vector2 direction, float force);
}
