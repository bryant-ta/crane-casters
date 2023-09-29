using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviourPun {
    [SerializeField] [CanBeNull] Piece heldPiece;
    [SerializeField] GameObject heldPieceObj;

    [SerializeField] List<GameObject> nearObjs = new();

    Board playerBoard;
    [SerializeField] BoardRenderer playerBoardRenderer;

    // [SyncVar] public Client client;

    void Awake() { heldPiece = null; }

    void Start() {
        // playerBoard = new Board(6, 6); // TODO: unhardcode
        // playerBoardRenderer.Init(playerBoard);
    }

    public void Interact() {
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
            if (d < minDistance && obj.TryGetComponent(out Piece piece)) {
                minDistance = d;
                heldPiece = piece;
                heldPieceObj = obj;
            }
        }

        if (heldPiece) {
            GameManager.Instance.photonView.RPC(nameof(NetworkUtils.S_SetTransform), RpcTarget.All, heldPiece.photonView.ViewID,
                Vector3.zero, Quaternion.identity, photonView.ViewID, false);
        }
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

        // GameManager.Instance.NetworkUtils.S_SetTransform(heldPieceObj, heldPieceObj.transform.position, heldPieceObj.transform.rotation, null);
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
}