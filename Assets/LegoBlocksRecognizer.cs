using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoBlocksRecognizer : MonoBehaviour {

	public Texture2D legoBlocks;
	public List<Color> colors;

	// Use this for initialization
	void Start () {
		List<Item<Color>> list = (new GridRecognizerFactory (new Vector2Int (4, 3), colors)).GetObject ().Recognize (legoBlocks);
		foreach(Item<Color> item in list)
		{
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = new Vector3(item.Position.x, item.Position.y, 0);
			Material material = new Material (Shader.Find ("Standard"));
			material.color = item.Type;
			cube.GetComponent<MeshRenderer> ().material = material;
		}
	}

}
