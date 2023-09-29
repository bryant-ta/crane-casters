using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Piece : MonoBehaviourPun, IPunInstantiateMagicCallback {
    public List<Vector2Int> _shape;
    public Color _color;
    public bool _canRotate;

    public List<Block> Blocks => _blocks;
    List<Block> _blocks = new();

    [SerializeField] PieceRenderer pr;

    public void Init(PieceData pieceData) {
        _shape = pieceData.shape;
        _color = pieceData.color;
        _canRotate = pieceData.canRotate;

        // Populate Block list
        foreach (Vector2Int blockOffset in pieceData.shape) {
            Block block = new Block(blockOffset, pieceData.color, true);
            _blocks.Add(block);
        }
        
        // Init PieceRenderer with populated Block list
        pr.Init();
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info) {
        Init((PieceData)info.photonView.InstantiationData[0]);
    }

    // Rotates the current piece clockwise.
    // public bool RotatePiece()
    // {
    // 	if (!piece.canRotate)
    // 	{
    // 		return false;
    // 	}
    //
    // 	Dictionary<Block, Position> piecePosition = piece.GetPositions();
    // 	var offset = piece.blocks[0].Position;
    //
    // 	foreach (var block in piece.blocks)
    // 	{
    // 		var row = block.Position.Row - offset.Row;
    // 		var column = block.Position.Column - offset.Column;
    // 		block.MoveTo(-column + offset.Row, row + offset.Column);
    // 	}
    //
    // 	if (HasCollisions() && !ResolveCollisionsAfterRotation())
    // 	{
    // 		RestoreSavedPiecePosition(piecePosition);
    // 		return false;
    // 	}
    // 	return true;
    // }
}