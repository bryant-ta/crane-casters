using System;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPun {
	public static GameManager Instance { get; private set; }

	[SerializeField] bool _canStart;

	public NetworkUtils NetworkUtils => _networkUtils;
	[SerializeField] NetworkUtils _networkUtils;

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}
	}
}
