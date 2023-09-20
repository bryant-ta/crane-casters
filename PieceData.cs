using System;
using System.Collections.Generic;
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
                shape = new Vector2Int[] {
                    new Vector2Int(0,0),
                }
            }
        }, {
            PieceType.L, new PieceData() {
                shape = new Vector2Int[] {
                    new Vector2Int(0,0),
                    new Vector2Int(0,1),
                    new Vector2Int(0,-1),
                    new Vector2Int(1,-1),
                }
            }
        }, {
            PieceType.I4, new PieceData() {
                shape = new Vector2Int[] {
                    new Vector2Int(0,0),
                    new Vector2Int(0,1),
                    new Vector2Int(0,-1),
                    new Vector2Int(0,-2),
                }
            }
        },
    };
}

[Serializable]
public struct PieceData {
    public Vector2Int[] shape;
    public Color color;
    public bool canRotate;
}