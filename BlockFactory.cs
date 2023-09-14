using UnityEngine;

public class BlockFactory : MonoBehaviour {
	public static BlockFactory Instance => _instance;
	static BlockFactory _instance;

	public GameObject blockBase;
	public static GameObject _blockBase;
	
	void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
		} else {
			_instance = this;
		}

		_blockBase = blockBase;
		
		// DEBUG
		BlockData bd = BlockTypeLookUp.LookUp[BlockType.I4];
		bd.color = Color.cyan;
		CreateBlock(new Vector2(0, 2), bd);
	}

	public static Block CreateBlock(Vector2 pos, BlockData blockData) {
		Block block = Instantiate(_blockBase, pos, Quaternion.identity).GetComponent<Block>();
		block.SetBlockData(blockData);

		return block;
	}
}
