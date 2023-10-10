using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPun {
	public static GameManager Instance { get; private set; }

	public static Dictionary<int, Player> PlayerList => new Dictionary<int, Player>(_playerList);
	static Dictionary<int, Player> _playerList = new(); // players registered when client joins

	[SerializeField] bool _canStart;

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}
	}

	public void UpdatePlayerList() {
		// can optimize later by keeping all Player objects in scene and enabling + assigning ownership when client joins
		foreach (GameObject playerObj in GameObject.FindGameObjectsWithTag("Player")) {
			Player player = playerObj.GetComponent<Player>();
			if (!_playerList.ContainsKey(player.PlayerId)) {
				_playerList[player.PlayerId] = player; ;
			}
		}
		
		//debug
		print("PlayerList: ");
		foreach (var player in _playerList) {
			print(player.Value.PlayerId);
		}
	}
	
	// TODO: implement call to unregister when player disconnects
	public void UnregisterPlayer(Player player) {
		// possibly dont need to register if "!_playerList.Contains(player)" works in RegisterPlayer
		// must keep each player index constant
	}
}
