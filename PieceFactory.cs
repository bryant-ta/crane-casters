using FishNet;
using FishNet.Object;
using UnityEngine;

public class PieceFactory : NetworkBehaviour {
	public static PieceFactory Instance { get; private set; }

	public GameObject pieceBase;
	public static GameObject _pieceBase;
	
	public GameObject blockBase;
	public static GameObject _blockBase;
	
	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}

		_pieceBase = pieceBase;
		_blockBase = blockBase;
	}

	bool flag = true;
	[Server]
	void Update() {
		
		// DEBUG
		if (Instance.ServerManager.Started && flag) {
			PieceData bd = PieceTypeLookUp.LookUp[PieceType.L];
			bd.color = Color.cyan;

			CreatePieceObj(new Piece(bd), new Vector2(0, 2));

			flag = false;
		}
	}
	
	[Server]
	public PieceRenderer CreatePieceObj(Piece piece, Vector2 position) {
		GameObject pieceObj = Instantiate(_pieceBase, position, Quaternion.identity);
		PieceRenderer pr = pieceObj.GetComponent<PieceRenderer>();
		pr.Init(piece);
		
		InstanceFinder.ServerManager.Spawn(pieceObj);

		return pr;
	}

	[Server]
	public BlockRenderer CreateBlockObj(Block block, Vector2 position) {
		GameObject blockObj = Instantiate(_blockBase, position, Quaternion.identity);
		BlockRenderer br = blockObj.GetComponent<BlockRenderer>();
		br.Init(block);
		
		InstanceFinder.ServerManager.Spawn(blockObj);

		return br;
	}
}
