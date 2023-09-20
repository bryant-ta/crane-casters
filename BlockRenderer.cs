using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class BlockRenderer : NetworkBehaviour {
	public Block block;
	
	SpriteRenderer sr;

	void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}

	public void Init(Block block) {
		this.block = block;
		
		Render();
	}

	public void Render() {
		transform.localPosition = new Vector3(block.position.x, block.position.y, 0);
		sr.color = block.color;
	}
}
