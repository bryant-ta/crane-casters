using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Piece {
	public Vector2Int[] shape;
	public Color color;

	public List<Block> blocks = new();

	bool canRotate;
	
	public Piece(PieceData pieceData) {
		shape = pieceData.shape;
		color = pieceData.color;
		canRotate = pieceData.canRotate;
		
		foreach (Vector2Int blockOffset in pieceData.shape) {
			Block block = new Block(blockOffset, pieceData.color, true);
			blocks.Add(block);
		}
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