using System;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPun {
	public static GameManager Instance { get; private set; }

	[SerializeField] bool _canStart;

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
		} else {
			Instance = this;
		}
	}

	public void Start() {
		
	}
}
