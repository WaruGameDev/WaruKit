// WaruKit — InputHandler
// Input centralizado con flag canUseInput (patron de ZeldaLike).
// TODO el input pasa por aca: teclado + tactil + mouse.
// Otros sistemas bloquean/desbloquean: InputHandler.instance.canUseInput = false; (cutscenes, UI, etc)
// EJEMPLO de uso: suscribir las acciones en Start() de otros scripts, o asignar
// characterController/attack/interactable en el inspector. Este archivo es generico:
// en tu proyecto, reemplaza las lineas comentadas por tus controllers reales.
using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    [Header("Estado")]
    public bool canUseInput = true;

    [Header("Referencias opcionales (asignar en Inspector)")]
    public MonoBehaviour moveTarget;      // ej: tu TopDownCharacterController
    public MonoBehaviour actionTarget;    // ej: tu Attack
    public MonoBehaviour interactTarget;  // ej: tu Interact

    // Delegados pa' desacoplar: otros scripts hacen InputHandler.instance.OnMove += MiMetodo;
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnInteract;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!canUseInput) return;

        // --- Teclado / gamepad (ejes) ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
            OnMove?.Invoke(new Vector2(h, v));

        // --- Acciones ---
        if (Input.GetButtonDown("Jump"))
            OnJump?.Invoke();
        if (Input.GetKeyDown(KeyCode.E))
            OnInteract?.Invoke();
    }
}
