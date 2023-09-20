using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class Client : NetworkBehaviour {
	[SyncVar] public string username;

	[SyncVar] public bool isReady;

	[SyncVar] public Player player;

	public override void OnStartServer() {
		base.OnStartServer();
		
		GameManager.Instance.clients.Add(this);
	}
	public override void OnStopServer() {
		base.OnStopServer();
		
		GameManager.Instance.clients.Remove(this);
	}

	void Update() {
		if (!IsOwner) return;
		
		// TODO: add readying mechanic for starting game
	}

	[ServerRpc]
	void ServerSetIsReady(bool val) {
		isReady = val;
	}
}
