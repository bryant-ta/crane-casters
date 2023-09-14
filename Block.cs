using UnityEngine;

public class Block : MonoBehaviour {
	public BlockData BlockData => blockData;
	[SerializeField] BlockData blockData;

	public void SetBlockData(BlockData blockData) {
		this.blockData = blockData;
	}

	// TODO: adapt for Block
	public int[,] RotateRight(int[,] shape, int n) {
		int[,] ret = new int[n, n];

		for (int i = 0; i < n; ++i) {
			for (int j = 0; j < n; ++j) {
				ret[i, j] = shape[n - j - 1, i];
			}
		}

		return ret;
	}
}