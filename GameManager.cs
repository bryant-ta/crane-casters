using System;
using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEditor;
using UnityEngine;

public class GameManager : NetworkBehaviour {
	public static GameManager Instance { get; private set; }

	[SyncObject] public readonly SyncList<Client> clients = new();

	[SyncVar] public bool canStart;

	public NetworkUtils NetworkUtils;
	
	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}

		NetworkUtils = GetComponent<NetworkUtils>();
	}

	void Update() {
		if (!IsServer) return;

		canStart = clients.All(client => client.isReady);
	}
}
