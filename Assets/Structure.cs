using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Structure
{
	public int Priority { get; private set; }

	private List<Item<Color>> _blocks;

	public List<Item<Color>> Blocks { 
		get { 
			return Extensions.Clone (_blocks).ToList(); 
		}
		private set {
			_blocks = Extensions.Clone (value).ToList();
		}
	}
	public string Name { get; private set; }
	public GameObject Model { get; private set; }

	public Structure (int priority, List<Item<Color>> blocks, string name, GameObject model)
	{
		Priority = priority;
		Blocks = blocks;
		Name = name;
		Model = model;
	}
}


