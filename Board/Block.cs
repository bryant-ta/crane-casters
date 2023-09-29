using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Block {
	public Vector2Int _position;
	public Color _color;
	public bool _isActive;

	public Block(Vector2Int position, Color color, bool isActive = true) {
		_position = position;
		_color = color;
		_isActive = isActive;
	}
	
	public void MoveTo(Vector2Int pos) {
		// TODO: add event triggering render in BlockRenderer
		_position = new Vector2Int(pos.x, pos.y);
	}

	public void MoveTo(int x, int y) {
		// TODO: add event triggering render in BlockRenderer
		_position = new Vector2Int(x, y);
	}

	// #region Serialization
	//
	// public static byte[] Serialize(object input)
	// {
	// 	var block = (Block)input;
 //    
	// 	// Create a MemoryStream to store the serialized data
	// 	using (MemoryStream stream = new MemoryStream())
	// 	using (BinaryWriter writer = new BinaryWriter(stream))
	// 	{
	// 		// Serialize each field of the Block class
	// 		writer.Write(block._position.x);
	// 		writer.Write(block._position.y);
	// 		writer.Write(block._color.r);
	// 		writer.Write(block._color.g);
	// 		writer.Write(block._color.b);
	// 		writer.Write(block._color.a);
	// 		writer.Write(block._isActive);
 //        
	// 		// Convert the MemoryStream to a byte array and return it
	// 		return stream.ToArray();
	// 	}
	// }
	//
	// public static object Deserialize(byte[] data)
	// {
	// 	// Create a Block object with default values
	// 	var result = new Block(Vector2Int.zero, Color.white);
 //    
	// 	// Create a MemoryStream from the input data
	// 	using (MemoryStream stream = new MemoryStream(data))
	// 	using (BinaryReader reader = new BinaryReader(stream))
	// 	{
	// 		// Deserialize each field of the Block class
	// 		result._position.x = reader.ReadInt32();
	// 		result._position.y = reader.ReadInt32();
	// 		result._color.r = reader.ReadSingle();
	// 		result._color.g = reader.ReadSingle();
	// 		result._color.b = reader.ReadSingle();
	// 		result._color.a = reader.ReadSingle();
	// 		result._isActive = reader.ReadBoolean();
	// 	}
 //    
	// 	return result;
	// }
	//
	// #endregion
}