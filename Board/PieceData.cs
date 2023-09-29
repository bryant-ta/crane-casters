using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum PieceType {
    O1,
    O2,
    I2,
    I3,
    I4,
    L,
}

public static class PieceTypeLookUp {
    public static Dictionary<PieceType, PieceData> LookUp = new Dictionary<PieceType, PieceData>() {
        {
            PieceType.O1, new PieceData() {
                shape = new List<Vector2Int>() {
                    new(0,0)
                }
            }
        }, {
            PieceType.L, new PieceData() {
                shape = new List<Vector2Int>() {
                    new(0,0),
                    new(0,1),
                    new(0,-1),
                    new(1,-1),
                }
            }
        }, {
            PieceType.I4, new PieceData() {
                shape = new List<Vector2Int>() {
                    new(0,0),
                    new(0,1),
                    new(0,-1),
                    new(0,-2),
                }
            }
        },
    };
}

[Serializable]
public struct PieceData {
    public List<Vector2Int> shape;
    public Color color;
    public bool canRotate;
    
    #region Serialization

    public static byte[] Serialize(object input) {
        var data = (PieceData) input;
        
        using (MemoryStream stream = new MemoryStream())
        using (BinaryWriter writer = new BinaryWriter(stream)) {
            writer.Write(data.shape.Count);
            foreach (Vector2Int pos in data.shape) {
                writer.Write(pos.x);
                writer.Write(pos.y);
            }

            writer.Write(data.color.r);
            writer.Write(data.color.g);
            writer.Write(data.color.b);
            writer.Write(data.color.a);

            writer.Write(data.canRotate);

            return stream.ToArray();
        }
    }

    public static object Deserialize(byte[] data) {
        PieceData result = new PieceData();

        using (MemoryStream stream = new MemoryStream(data))
        using (BinaryReader reader = new BinaryReader(stream)) {
            int shapeCount = reader.ReadInt32();
            List<Vector2Int> shape = new List<Vector2Int>();
            for (int i = 0; i < shapeCount; i++) {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                shape.Add(new Vector2Int(x, y));
            }
            result.shape = shape;

            float r = reader.ReadSingle();
            float g = reader.ReadSingle();
            float b = reader.ReadSingle();
            float a = reader.ReadSingle();
            result.color = new Color(r, g, b, a);

            result.canRotate = reader.ReadBoolean();
        }

        return result;
    }

    #endregion
}