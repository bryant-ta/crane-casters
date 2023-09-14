using System;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    O1,
    O2,
    I2,
    I3,
    I4,
    L,
}

public static class BlockTypeLookUp {
    public static Dictionary<BlockType, BlockData> LookUp = new Dictionary<BlockType, BlockData>() {
        {
            BlockType.O1, new BlockData() {
                shape = new int[,] {
                    {1, 0, 0, 0},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0},
                }
            }
        }, {
            BlockType.L, new BlockData() {
                shape = new int[,] {
                    {1, 0, 0, 0},
                    {1, 0, 0, 0},
                    {1, 1, 0, 0},
                    {0, 0, 0, 0},
                }
            }
        }, {
            BlockType.I4, new BlockData() {
                shape = new int[,] {
                    {1, 0, 0, 0},
                    {1, 0, 0, 0},
                    {1, 0, 0, 0},
                    {1, 0, 0, 0},
                }
            }
        },
    };
}

[Serializable]
public struct BlockData {
    public int[,] shape;
    public Color color;
}