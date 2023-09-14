using System.Collections.Generic;
using System.Linq;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : NetworkBehaviour {
    [SerializeField] Block heldBlock;
    [SerializeField] List<GameObject> nearObjs = new();

    [SerializeField] Board playerBoard;

    void Interact() {
        if (heldBlock == null) {
            PickUp();
        } else {
            Drop();
        }
    }

    void PickUp() {
        if (nearObjs.Count == 0) return;

        float minDistance = int.MaxValue;
        foreach (GameObject obj in nearObjs.ToList()) {
            float d = Vector2.Distance(transform.position, obj.transform.position);
            if (d < minDistance && obj.TryGetComponent(out Block block)) {
                minDistance = d;
                heldBlock = block;
            }
        }
        
        Utils.S_SetTransform(heldBlock.gameObject, Vector3.zero, Quaternion.identity, transform, false);
    }

    void Drop() {
        if (heldBlock == null) return;

        Vector2Int hoverPoint = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        if (!playerBoard.IsInBounds(hoverPoint.x, hoverPoint.y)) {
            return;
        }

        if (!playerBoard.PlaceBlock(heldBlock.BlockData, hoverPoint)) {
            print("cannot place block here");
            return;
        }
        
        Utils.S_SetTransform(heldBlock.gameObject, heldBlock.transform.position, heldBlock.transform.rotation, null);
        heldBlock = null;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!nearObjs.Contains(col.gameObject)) {
            nearObjs.Add(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (nearObjs.Contains(col.gameObject)) {
            nearObjs.Remove(col.gameObject);
        }
    }

    /***************************    Input Callbacks    ***************************/

    #region UnityEventInput

    public void OnInteract(InputAction.CallbackContext ctx) {
        if (!base.IsOwner) return;

        if (ctx.performed) {
            print("performed");
            Interact();
        }
    }

    #endregion

    // can try..
    // didnt work, other clients still received Interact callback even when disabled
    // public override void OnStartClient() {
    //     base.OnStartClient();
    //
    //     if (!base.IsOwner) {
    //         print("disabling?");
    //         enabled = false;
    //     }
    // }
}