using FishNet.Object;
using UnityEngine;

public class NetworkUtils : NetworkBehaviour {
	[ServerRpc(RequireOwnership = false)]
	public void S_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
		O_SetTransform(obj, position, rotation, parent, keepWorldPosition);
	}

	[ObserversRpc]
	public void O_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
		obj.transform.SetParent(parent);
        
		if (!keepWorldPosition) {
			obj.transform.localPosition = position;
			obj.transform.localRotation = rotation;
		} else {
			obj.transform.position = position;
			obj.transform.rotation = rotation;
		}
	}
	
}
