using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks {
	[SerializeField] GameObject _playerObj;
	[SerializeField] Transform[] _spawnPositions;

	public override void OnJoinedRoom() {
		int curNumPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
		if (curNumPlayers > _spawnPositions.Length) {
			Debug.LogError("Failed to spawn player: not enough spawn positions.");
			return;
		}
		
		Transform spawnPos = _spawnPositions[curNumPlayers - 1];
		PhotonNetwork.Instantiate(Constants.PhotonPrefabsPath + _playerObj.name, spawnPos.position, spawnPos.rotation);
	}
}
