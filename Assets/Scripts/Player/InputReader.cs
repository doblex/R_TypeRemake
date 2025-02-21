using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour {
    PlayerInput playerInput;

    InputAction moveAction;
    InputAction fireAction;

    public Vector2 Move => moveAction.ReadValue<Vector2>();
    public bool Fire => fireAction.IsPressed();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        fireAction = playerInput.actions["Attack"];
    }
}
