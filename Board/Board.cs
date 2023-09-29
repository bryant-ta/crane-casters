using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board : MonoBehaviour {
    public Block[] Blocks { get; private set; }

    public int Width => _width;
    public int Height => _height;
    [SerializeField] int _width, _height;

    [SerializeField] BoardRenderer br;

    public void Init(int width, int height) {
        _width = width;
        _height = height;
        
        Blocks = new Block[width * height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Blocks[x + y * width] = new Block(new Vector2Int(x, y), Color.black, false);
            }
        }
    }

    public bool PlacePiece(Piece piece, Vector2Int hoverPos) {
        Debug.Log("PlaceBlock - hoverPos: " + hoverPos);

        // width and height /2 when board center is gameobject origin
        Vector2Int pieceOrigin = new Vector2Int(hoverPos.x + _width/2, hoverPos.y + _height/2);
        List<Block> newBlocks = new();

        foreach (Block block in piece.Blocks) {
            Vector2Int boardPos = new Vector2Int(pieceOrigin.x + block._position.x, pieceOrigin.y + block._position.y);

            if (!IsValidPlacement(boardPos.x, boardPos.y)) return false;
            
            newBlocks.Add(block);
            Debug.Log("Point: " + boardPos);
        }

        // Passed placement checks, update board with new block
        foreach (Block block in newBlocks) {
            Vector2Int boardPos = new Vector2Int(pieceOrigin.x + block._position.x, pieceOrigin.y + block._position.y);

            Block boardBlock = GetBlockAt(boardPos.x, boardPos.y);
            boardBlock._isActive = true;
            boardBlock._color = block._color;
        }

        return true;
    }

    public bool IsValidPlacement(int x, int y) { return IsInBounds(x, y) && !GetBlockAt(x, y)._isActive; }

    public bool IsInBounds(int x, int y) { return !(x < 0 || x >= _width || y < 0 || y >= _height); }

    public Block GetBlockAt(int x, int y) {
        if (!IsInBounds(x, y)) {
            Debug.LogError("Board: Block position out of bounds!");
            return null;
        }
        
        return Blocks[x + y * _width];
    }
}