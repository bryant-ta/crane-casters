using FishNet.Object;
using UnityEngine;

public static class Utils {
	[ServerRpc(RequireOwnership = false)]
	public static void S_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
		O_SetTransform(obj, position, rotation, parent, keepWorldPosition);
	}

	[ObserversRpc]
	public static void O_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
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
