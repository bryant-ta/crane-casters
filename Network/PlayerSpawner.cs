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
		
		int playerId = PhotonNetwork.LocalPlayer.ActorNumber;
		
		Transform spawnPos = _spawnPositions[playerId - 1];
		object[] initData = {playerId};
		GameObject playerObj = PhotonNetwork.Instantiate(Constants.PhotonPrefabsPath + _playerObj.name, spawnPos.position, spawnPos.rotation, 0, initData);

		// note: player object likely does not exist immediately after PhotonNetwork.Instantiate, so can't rely on it here
	}
}
