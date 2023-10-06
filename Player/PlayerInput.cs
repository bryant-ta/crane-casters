using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

// Online multiplayer PlayerInput. Prob replace with local multiplayer version here for local
public class PlayerInput : MonoBehaviourPun {
    Player _player;
    PlayerMovement _playerMovement;

    void Awake() {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (!photonView.IsMine) return;

        _playerMovement.moveInput = context.ReadValue<Vector2>();
    }

    // uses Action Type "Button"
    public void OnInteract(InputAction.CallbackContext ctx) {
        if (!photonView.IsMine) return;

        if (ctx.performed) {
            _player.Interact();
        }
    }
}