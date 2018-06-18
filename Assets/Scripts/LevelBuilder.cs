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
    public WebCamPhotoCamera webCamPhotoCamera;

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

    public IEnumerator BuildLevel()
    {

        var colorBlockNames = new Dictionary<Color, string>();
        var namedPrefabs = new Dictionary<string, GameObject>();

        foreach (ColoredBlock coloredBlock in ColorPaletteNames)
            colorBlockNames.Add(coloredBlock.Color, coloredBlock.Name);
        foreach (NamedPrefab namedPrefab in NamedPrefabs)
            namedPrefabs.Add(namedPrefab.Name, namedPrefab.gameObject);
        /*byte[] bytes = LegoBlocks.EncodeToPNG();
        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/PicToRecognize.png", bytes);*/
        var factory = new StructureRecognizerFactory(Structures, new GridRecognizerFactory(GridNumber, colorBlockNames), (float)0.7);
        var rec = new ThreadedRecognizer();
        rec.Recognize(factory.GetObject(), LegoBlocks.GetPixels(), LegoBlocks.width, LegoBlocks.height);

        yield return new WaitUntil(() => rec.Completed);
        var recognizedItems = rec.Result;

        foreach (var item in recognizedItems)
        {
            var sceneObject = Instantiate(namedPrefabs[item.Name]);
            float scaleFactor = WebCamPhotoCamera.scale.y / (float)GridNumber.x;
            var scale = webCamPhotoCamera.GetScale();
            sceneObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            var unit_x = (Corners[2].transform.position.x - Corners[0].transform.position.x) / GridNumber.x;
            var unit_y = (Corners[0].transform.position.z - Corners[2].transform.position.z) / GridNumber.y;
            Vector3 translate = new Vector3(Corners[0].transform.position.x, 0, Corners[2].transform.position.z);
            sceneObject.transform.position = new Vector3(item.Position.x * unit_x + translate.x, translate.y, item.Position.y * unit_y + translate.z);
            switch(item.Name){
                case "wall":
                    sceneObject.transform.Translate(scaleFactor, (float)0.5*scaleFactor, scaleFactor);
                    break;
                case "tower":
                    sceneObject.transform.Translate(2*scaleFactor, scaleFactor, 2*scaleFactor);
                    break;
                case "Gate":
                    sceneObject.transform.Translate(3*scaleFactor, scaleFactor, 3*scaleFactor);
                    break;
                case "EnemyPortal":
                    sceneObject.transform.Translate(3*scaleFactor, scaleFactor, 3*scaleFactor);
                    break;
            }
        }


    }

    public Vector2[] GetCornerVectors()
    {
        var vectors = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            int x = (int)(Corners[i].transform.position.x / WebCamPhotoCamera.scale.x * LegoBlocks.width);
            int y = (int)(Corners[i].transform.position.z / WebCamPhotoCamera.scale.y * LegoBlocks.height);
            vectors[i] = new Vector2(x, y);
        }
        return vectors;
    }

    void Start()
    {
        webCamPhotoCamera = GameObject.Find("Plane").GetComponent<WebCamPhotoCamera>();
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
