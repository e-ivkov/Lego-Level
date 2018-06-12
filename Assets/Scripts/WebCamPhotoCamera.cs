using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamPhotoCamera : MonoBehaviour
{

    WebCamTexture webCamTexture;
    public string DeviceName;
    public GameObject topLeft;
    public GameObject bottomRight;
    public GameObject gridPlane;
    LevelBuilder levelBuilder;
    Texture2D photo;
    public Vector2[] Corners { get; set; }

    void Start()
    {
        levelBuilder = GameObject.Find("Main Camera").GetComponent<LevelBuilder>();
        webCamTexture = new WebCamTexture();
        if (!string.IsNullOrEmpty(DeviceName))
            webCamTexture.deviceName = DeviceName;
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
        photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        gridPlane.GetComponent<Renderer>().material.mainTexture = photo;
        foreach (var device in WebCamTexture.devices)
        {
            Debug.Log(device.name);
        }
    }

    private void Update()
    {
        int x = (int)(topLeft.transform.position.x / 8 * webCamTexture.width);
        int y = (int)(bottomRight.transform.position.z / 6 * webCamTexture.height);
        int width = (int)((bottomRight.transform.position.x - topLeft.transform.position.x) / 8 * webCamTexture.width);
        int height = (int)((topLeft.transform.position.z - bottomRight.transform.position.z) / 6 * webCamTexture.height);
        if (Input.GetMouseButtonDown(0))
        {
            var m1Texture = new Texture2D(webCamTexture.width, webCamTexture.height);
            var pixels = webCamTexture.GetPixels();
            System.Array.Reverse(pixels);
            m1Texture.SetPixels(pixels);
            m1Texture.Apply();
            Color[] c = m1Texture.GetPixels(x, y, width - 1, height - 1);
            Texture2D m2Texture = new Texture2D(width - 1, height - 1);
            m2Texture.SetPixels(c);
            m2Texture.Apply();
            levelBuilder.LegoBlocks = m2Texture;
            levelBuilder.BuildLevel();
        }
        if (Input.GetMouseButtonDown(1)){
            photo = new Texture2D(webCamTexture.width, webCamTexture.height);
            //var pixels = webCamTexture.GetPixels();
            //System.Array.Reverse(pixels);
            for (int i = 0; i < webCamTexture.width; i++)
                for (int j = 0; j < webCamTexture.height; j++)
                    photo.SetPixel(i, j, Color.clear);
            for (int i = 0; i < width; i++)
            {
                if (i % (width/levelBuilder.GridNumber.x) == 0)
                {
                    for (int j = 0; j < height; j++)
                    {
                        photo.SetPixel(x + i, y + j, Color.green);
                    }
                }
            }
            for (int i = 0; i < height; i++)
            {
                if (i % (height/levelBuilder.GridNumber.y) == 0)
                {
                    for (int j = 0; j < width; j++)
                    {
                        photo.SetPixel(x + j, y + i, Color.green);

                    }
                }
            }
            photo.SetPixel(0, 0, Color.red);
            Color[] colors = photo.GetPixels();
            System.Array.Reverse(colors);
            photo.SetPixels(colors);
            photo.Apply();
            gridPlane.GetComponent<Renderer>().material.mainTexture = photo;
        }

    }

    void TakePhoto()
    {

        // NOTE - you almost certainly have to do this here:

        //yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        //File.WriteAllBytes(your_path + "photo.png", bytes);
    }
}
