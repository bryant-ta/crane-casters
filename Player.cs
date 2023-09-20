using System.Collections.Generic;
using System.Linq;
using FishNet.Object;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : NetworkBehaviour {
    [SerializeField] [CanBeNull] Piece heldPiece;
    [SerializeField] GameObject heldPieceObj;

    [SerializeField] List<GameObject> nearObjs = new();

    Board playerBoard;
    [SerializeField] BoardRenderer playerBoardRenderer;

    // [SyncVar] public Client client;

    void Awake() { heldPiece = null; }

    void Start() {
        playerBoard = new Board(6, 6); // TODO: unhardcode
        playerBoardRenderer.Init(playerBoard);
    }

    void Interact() {
        if (heldPiece == null) {
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
            if (d < minDistance && obj.TryGetComponent(out PieceRenderer pr)) {
                minDistance = d;
                heldPiece = pr.piece;
                heldPieceObj = obj;
            }
        }

        GameManager.Instance.NetworkUtils.S_SetTransform(heldPieceObj, Vector3.zero, Quaternion.identity, transform, false);
    }

    void Drop() {
        if (heldPiece == null) return;

        // localPosition works when Player is child of Board and centered at origin
        Vector2Int hoverPoint = new Vector2Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y));

        print("Hoverpoint" + hoverPoint);

        if (!playerBoard.PlacePiece(heldPiece, hoverPoint)) {
            print("cannot place block here");
            return;
        }

        GameManager.Instance.NetworkUtils.S_SetTransform(heldPieceObj, heldPieceObj.transform.position, heldPieceObj.transform.rotation,
            null);
        heldPiece = null;
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

    // uses Action Type "Button"
    public void OnInteract(InputAction.CallbackContext ctx) {
        if (!base.IsOwner) return;

        if (ctx.performed) {
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