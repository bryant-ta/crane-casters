using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : NetworkBehaviour {
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float maxAcceleration = 10f;

    Vector2 moveInput;

    Rigidbody2D rb;

    void Start() { rb = GetComponent<Rigidbody2D>(); }

    void FixedUpdate() {
        Vector2 dir = new Vector2(moveInput.x, moveInput.y);
        dir = Vector2.ClampMagnitude(dir, 1f);

        Vector2 targetV = dir * maxSpeed;

        AccelerateTo(targetV);
    }

    void AccelerateTo(Vector2 targetVelocity) {
        float limit = maxAcceleration;

        Vector2 deltaV = targetVelocity - rb.velocity;

        Vector2 accel = deltaV / Time.fixedDeltaTime;
        accel = Vector2.ClampMagnitude(accel, limit);

        rb.AddForce(accel, ForceMode2D.Force);
    }

    /***************************    Input Callbacks    ***************************/

    #region UnityEventInput

    public void OnMove(InputAction.CallbackContext context) {
        if (!base.IsOwner) return;
        moveInput = context.ReadValue<Vector2>();
    }

    #endregion
}