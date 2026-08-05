// WaruKit — InputHandler
// Centraliza el input con flag canUseInput (patron de ZeldaLike).
// Todo el input pasa por aca: teclado + tactil + mouse.
// Otros sistemas bloquean/desbloquean: InputHandler.instance.canUseInput = false; (cutscenes, UI, etc)
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    [Header("Referencias")]
    public TopDownCharacterController characterController;
    public Attack attack;
    public Interact interactuable;

    [Header("Estado")]
    public bool canUseInput = true;

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
        characterController.Move(new Vector2(h, v));

        // --- Acciones ---
        if (Input.GetButtonDown("Jump"))
            attack.PerformAttack();
        if (Input.GetKeyDown(KeyCode.E))
            interactuable.PerformInteract();
    }
}
