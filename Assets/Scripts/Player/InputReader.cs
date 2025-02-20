using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour {
    PlayerInput playerInput;

    InputAction moveAction;
    InputAction AttackAction;

    public Vector2 Move => moveAction.ReadValue<Vector2>();
    public bool Attack => AttackAction.IsPressed();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        AttackAction = playerInput.actions["Attack"];
    }
}
