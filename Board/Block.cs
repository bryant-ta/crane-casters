using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Block {
    public Vector2Int position;
    public Color color;
    public bool isActive;

    public Block(Vector2Int position, Color color, bool isActive = true) {
        this.position = position;
        this.color = color;
        this.isActive = isActive;
    }

    public void MoveTo(Vector2Int pos) {
        // TODO: add event triggering render in BlockRenderer
        position = new Vector2Int(pos.x, pos.y);
    }

    public void MoveTo(int x, int y) {
        // TODO: add event triggering render in BlockRenderer
        position = new Vector2Int(x, y);
    }

    #region Serialization
    
    public static byte[] Serialize(object input)
    {
    	var block = (Block)input;
       
    	// Create a MemoryStream to store the serialized data
    	using (MemoryStream stream = new MemoryStream())
    	using (BinaryWriter writer = new BinaryWriter(stream))
    	{
    		// Serialize each field of the Block class
    		writer.Write(block.position.x);
    		writer.Write(block.position.y);
    		writer.Write(block.color.r);
    		writer.Write(block.color.g);
    		writer.Write(block.color.b);
    		writer.Write(block.color.a);
    		writer.Write(block.isActive);
           
    		// Convert the MemoryStream to a byte array and return it
    		return stream.ToArray();
    	}
    }
    
    public static object Deserialize(byte[] data)
    {
    	// Create a Block object with default values
    	var result = new Block(Vector2Int.zero, Color.white);
       
    	// Create a MemoryStream from the input data
    	using (MemoryStream stream = new MemoryStream(data))
    	using (BinaryReader reader = new BinaryReader(stream))
    	{
    		// Deserialize each field of the Block class
    		result.position.x = reader.ReadInt32();
    		result.position.y = reader.ReadInt32();
    		result.color.r = reader.ReadSingle();
    		result.color.g = reader.ReadSingle();
    		result.color.b = reader.ReadSingle();
    		result.color.a = reader.ReadSingle();
    		result.isActive = reader.ReadBoolean();
    	}
       
    	return result;
    }
    
    #endregion
}