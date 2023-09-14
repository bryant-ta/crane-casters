using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public int[,] board;
    
    public int Width => width;
    public int Height => height;
    [SerializeField] int width, height;

    void Awake() {
        board = new int[width, height];
    }

    public bool PlaceBlock(BlockData block, Vector2Int hoverPoint) {
        List<Vector2Int> newOccupiedPoints = new();
        
        for (int x = 0; x < block.shape.GetLength(0); x++) {
            for (int y = 0; y < block.shape.GetLength(1); y++) {
                Vector2Int checkPoint = new Vector2Int(hoverPoint.x + x, hoverPoint.y + y);

                // Block shape is out of bounds (allow empty parts of shape to be "out of bounds")
                if (!IsInBounds(checkPoint.x, checkPoint.y) && block.shape[x, y] == 1) {
                    return false;
                }
                
                // Value of board check point + block point should equal >1 overlapping occupied space
                if (board[checkPoint.x, checkPoint.y] + block.shape[x, y] > 1) {
                    return false;
                }
                
                newOccupiedPoints.Add(checkPoint);
            }
        }
        
        // Passed placement checks, update board with new block
        foreach (Vector2Int point in newOccupiedPoints) {
            board[point.x, point.y] = 1;
        }

        return true;
    }

    public bool IsInBounds(int x, int y) {
        return x <= width - 1 && x >= 0 && y <= height - 1 && y >= 0;
    }
}