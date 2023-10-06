using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviourPun {
    [SerializeField] Piece _heldPiece;

    Board _playerBoard;
    List<GameObject> _nearObjs = new();

    void Start() {
        _playerBoard = GetComponentInParent<Board>();
    }
    
    public void Interact() {
        if (_heldPiece == null) {
            PickUp();
        } else {
            Drop();
        }
    }

    void PickUp() {
        if (_nearObjs.Count == 0) return;

        // Find nearest piece
        float minDistance = int.MaxValue;
        foreach (GameObject obj in _nearObjs.ToList()) {
            float d = Vector2.Distance(transform.position, obj.transform.position);
            if (d < minDistance && obj.TryGetComponent(out Piece piece)) {
                minDistance = d;
                _heldPiece = piece;
            }
        }

        // Pickup piece
        if (_heldPiece) {
            // Take ownership of held piece
            // _heldPiece.photonView.RequestOwnership(); // server authoritative - requires Piece Ownership -> Request
            _heldPiece.photonView.TransferOwnership(photonView.Owner); // client authoritative - requires Piece Ownership -> Takeover
            
            // Make piece a child object of player
            GameManager.Instance.photonView.RPC(nameof(NetworkUtils.S_SetTransform), RpcTarget.All, _heldPiece.photonView.ViewID,
                Vector3.zero, Quaternion.identity, photonView.ViewID, false);
        }
    }

    void Drop() {
        if (_heldPiece == null) return;

        // localPosition works when Player is child of Board and centered at origin
        Vector2Int hoverPoint = new Vector2Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y));

        print("Hoverpoint" + hoverPoint);

        if (!_playerBoard.PlacePiece(_heldPiece, hoverPoint)) {
            print("cannot place block here");
            return;
        }

        // GameManager.Instance.NetworkUtils.S_SetTransform(heldPieceObj, heldPieceObj.transform.position, heldPieceObj.transform.rotation, null);
        _heldPiece = null;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!_nearObjs.Contains(col.gameObject)) {
            _nearObjs.Add(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (_nearObjs.Contains(col.gameObject)) {
            _nearObjs.Remove(col.gameObject);
        }
    }
}