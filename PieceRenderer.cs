using UnityEngine;

public class PieceRenderer : MonoBehaviour {
    public Piece piece;

    public void Init(Piece piece) {
        this.piece = piece;

        // TODO: create blocks with color of pieceData
        // Render blocks of the piece
        foreach (Block block in piece.blocks) {
            BlockRenderer br = PieceFactory.Instance.CreateBlockObj(block, Vector2.zero);
            br.transform.SetParent(transform);
            br.Render();
        }
    }

    public void Render() { }
}