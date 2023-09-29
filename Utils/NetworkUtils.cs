using System.IO;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NetworkUtils : MonoBehaviour {
    /// <summary>
    /// RPC for basic transform changes.
    /// </summary>
    /// <param name="targetID">PhotonView ID of object to change.</param>
    /// <param name="parentID">PhotonView ID of object to parent to. -1 = do not set parent</param>
    [PunRPC]
    public void S_SetTransform(int targetID, Vector3 position, Quaternion rotation, int parentID, bool keepWorldPosition) {
        Transform targetTransform = PhotonView.Find(targetID).gameObject.transform;

        if (parentID != -1) {
            Transform parentTransform = PhotonView.Find(parentID).gameObject.transform;
            targetTransform.SetParent(parentTransform);
        }

        if (!keepWorldPosition) {
            targetTransform.localPosition = position;
            targetTransform.localRotation = rotation;
        } else {
            targetTransform.position = position;
            targetTransform.rotation = rotation;
        }
    }
    
    [PunRPC]
    public void S_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
        obj.transform.SetParent(parent);

        if (!keepWorldPosition) {
            obj.transform.localPosition = position;
            obj.transform.localRotation = rotation;
        } else {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
    }
    
    // public void S_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
    //     O_SetTransform(obj, position, rotation, parent, keepWorldPosition);
    // }
    //
    // public void O_SetTransform(GameObject obj, Vector3 position, Quaternion rotation, Transform parent, bool keepWorldPosition = true) {
    //     obj.transform.SetParent(parent);
    //
    //     if (!keepWorldPosition) {
    //         obj.transform.localPosition = position;
    //         obj.transform.localRotation = rotation;
    //     } else {
    //         obj.transform.position = position;
    //         obj.transform.rotation = rotation;
    //     }
    // }
    
    // public static byte[] Vector2IntToBytes(Vector2Int vector) {
    //     using MemoryStream stream = new MemoryStream();
    //     using (BinaryWriter writer = new BinaryWriter(stream)) {
    //         writer.Write(vector.x);
    //         writer.Write(vector.y);
    //         return stream.ToArray();
    //     }
    // }
    //
    // public static Vector2Int BytesToVector2Int(byte[] bytes) {
    //     using MemoryStream stream = new MemoryStream(bytes);
    //     using (BinaryReader reader = new BinaryReader(stream)) {
    //         int x = reader.ReadInt32();
    //         int y = reader.ReadInt32();
    //         return new Vector2Int(x, y);
    //     }
    // }
}