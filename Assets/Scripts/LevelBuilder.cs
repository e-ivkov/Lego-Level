using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;

public class LevelBuilder : MonoBehaviour
{

    public List<Structure> Structures;
    public List<ImportStructure> ImportStructures;

    public Texture2D LegoBlocks;

    [Serializable]
    public struct ColoredBlock
    {
        public Color Color;
        public string Name;
    }

    [Serializable]
    public struct NamedPrefab
    {
        public string Name;
        public GameObject gameObject;
    }

    public List<ColoredBlock> ColorBlockNames; // import structures
    public List<ColoredBlock> ColorPaletteNames; // recognized palette

    public List<NamedPrefab> NamedPrefabs;

    public Vector2Int GridNumber;

    public GameObject[] Corners;

    // Use this for initialization

    public void BuildLevel()
    {
        var colorBlockNames = new Dictionary<Color, string>();
        var namedPrefabs = new Dictionary<string, GameObject>();

        foreach (ColoredBlock coloredBlock in ColorPaletteNames)
            colorBlockNames.Add(coloredBlock.Color, coloredBlock.Name);
        foreach (NamedPrefab namedPrefab in NamedPrefabs)
            namedPrefabs.Add(namedPrefab.Name, namedPrefab.gameObject);
        byte[] bytes = LegoBlocks.EncodeToPNG();
        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/PicToRecognize.png", bytes);
        var factory = new StructureRecognizerFactory(Structures, new GridRecognizerFactory(GridNumber, colorBlockNames), 1);
        foreach (var item in factory.GetObject().Recognize(LegoBlocks))
        {
            var sceneObject = Instantiate(namedPrefabs[item.Name]);
            float scaleFactor = 6 / (float)GridNumber.x;
            sceneObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            Vector3 translate = new Vector3(Corners[0].transform.position.x, (float)0.01, Corners[2].transform.position.z);
            translate += (sceneObject.GetComponent<Collider>().bounds.extents + sceneObject.GetComponent<Collider>().bounds.center) * scaleFactor;
            if (item.Name == "tower")
            {
                translate += sceneObject.GetComponent<Collider>().bounds.size;
                translate.y -= sceneObject.GetComponent<Collider>().bounds.size.y;
            }
            sceneObject.transform.position = new Vector3(item.Position.x * scaleFactor + translate.x, translate.y, item.Position.y * scaleFactor + translate.z);
        }

    }

    public Vector2[] GetCornerVectors()
    {
        var vectors = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            int x = (int)(Corners[i].transform.position.x / 8 * LegoBlocks.width);
            int y = (int)(Corners[i].transform.position.z / 6 * LegoBlocks.height);
            vectors[i] = new Vector2(x, y);
        }
        return vectors;
    }

    void Start()
    {
        DoImportStructures();
    }

    void DoImportStructures()
    {
        var colorNames = new Dictionary<Color, String>();
        foreach (ColoredBlock coloredBlock in ColorBlockNames)
        {
            colorNames.Add(coloredBlock.Color, coloredBlock.Name);
            Debug.Log(coloredBlock.Color);
        }
        foreach (var imStruct in ImportStructures)
        {
            Structures.Add(new Structure(imStruct.priority, imStruct.texture, colorNames, imStruct.name));
        }
    }
}
