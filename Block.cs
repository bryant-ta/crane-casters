using System;
using UnityEngine;

[Serializable]
public class Block {
	public Vector2Int position;
	public Color color;
	public bool isActive;

	public Block() {
		
	}

	public Block(Vector2Int position, Color color, bool isActive) {
		this.position = position;
		this.color = color;
		this.isActive = isActive;
	}

	public void MoveTo(Vector2Int pos) {
		// TODO: add event triggering render in BlockRenderer
		position = new Vector2Int(pos.x, pos.y);
	}

	public void MoveTo(int x, int y) {
		// TODO: add event triggering render in BlockRenderer
		position = new Vector2Int(x, y);
	}
}