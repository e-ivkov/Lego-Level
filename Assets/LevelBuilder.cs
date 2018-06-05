using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelBuilder : MonoBehaviour {

	public List<Structure> structures;

	public Texture2D legoBlocks;

	// Use this for initialization
	void Start () {
		GenerateStructures ();
		/*foreach(Item<Color> item in list)
		{
			GameObject cube = Instantiate(blocks[item.Type]);
			cube.transform.position = new Vector3(item.Position.x, 0, item.Position.y);
		}*/
	}

	void Recognize(IRecognizer, Dictionary

	void GenerateStructures(){
		HashSet<Color> colors = new HashSet<Color> ();
		foreach (Structure structure in structures)
			foreach (Color c in structure.Blocks.Select(block => block.Type))
				colors.Add (c);
		HashSet<Item<Color>> recognizedBlocks = new HashSet<>((new GridRecognizerFactory 
			(new Vector2Int (4, 3), colors.ToList())).GetObject ().Recognize (legoBlocks));
		List<Structure> sortedStructures = structures.OrderBy (structure => structure.Priority);
		foreach (Structure structure in sortedStructures) {
			bool recognized
		}
	}
}
