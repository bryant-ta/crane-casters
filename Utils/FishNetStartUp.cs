using FishNet;
using ParrelSync;
using UnityEngine;

public class FishNetStartUp : MonoBehaviour {
    public NetworkHudCanvases networkHudCanvases;

    int count = 100;
    bool serverStarted = false;
    void Update() {
        if (!serverStarted && !ClonesManager.IsClone()) { // need this instead of InstanceFinder.ServerManager.Started to only click once
            networkHudCanvases.OnClick_Server();
            serverStarted = true;
        } else if (ClonesManager.IsClone()) {
            count--;

            if (count < 0) {
                networkHudCanvases.OnClick_Client();
                enabled = false;
                return;
            }
        } else if (InstanceFinder.ServerManager.Started) {
            networkHudCanvases.OnClick_Client();
            enabled = false;
        }
    }
}