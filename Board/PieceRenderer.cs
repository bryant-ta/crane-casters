using System.Collections.Generic;
using UnityEngine;

public class PieceRenderer : MonoBehaviour {
    [SerializeField] Piece piece;

    Dictionary<Block, SpriteRenderer> _blockSprites = new();

    // Should usually call after initializing Piece
    public void Init() {
        // Create SpriteRenderers for Piece's Blocks
        foreach (Block block in piece.Blocks) {
            GameObject blockObj = Instantiate(PieceFactory.BlockBase, piece.transform, true);

            if (blockObj.TryGetComponent(out SpriteRenderer sr)) {
                _blockSprites[block] = blockObj.GetComponent<SpriteRenderer>();
            } else {
                Debug.LogError("Expected SpriteRender on Block base object");
            }
        }
        
        Render();
    }

    public void Render() {
        // Update render for Piece's Blocks
        foreach (Block block in piece.Blocks) {
            SpriteRenderer sr = _blockSprites[block];
            sr.transform.localPosition = new Vector3(block._position.x, block._position.y, 0);
            sr.color = block._color;
        }
    }
}